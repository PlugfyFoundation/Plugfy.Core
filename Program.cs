using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using CommandLine;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using Plugfy.Core.Commons.Runtime;

namespace Plugfy.Core
{
    public class Program
    {
        private static IConfiguration Configuration { get; set; }

        static int Main(string[] args)
        {
            // Configure appsettings.json and environment variables
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            // Verify arguments
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: plugfy <extension> <command> [options]");
                return 1;
            }

            var extensionName = args[0];
            var extensionArgs = args.Skip(1).ToArray();

            return Parser.Default.ParseArguments<ExtensionOptions>(extensionArgs)
                .MapResult(
                    options => ExecuteExtension(extensionName, options),
                    errs => 1
                );
        }

        private static int ExecuteExtension(string extensionName, ExtensionOptions options)
        {
            try
            {
                // Load extensions from configuration
                var extensionsPath = Configuration["ExtensionsPath"];
                var extensions = LoadExtensions(extensionsPath, extensionName);

                if (extensions == null || !extensions.Any())
                {
                    Console.WriteLine($"No compatible versions found for extension '{extensionName}'.");
                    return 1;
                }

                var extension = extensions.First();

                // Verify command
                var command = options.Command;
                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine("No command specified. Use --command <command>.");
                    return 1;
                }

                // Find execution option
                var executionOption = extension.ExecutionOptions.FirstOrDefault(o => o.Name.Equals(command, StringComparison.OrdinalIgnoreCase));
                if (executionOption == null)
                {
                    Console.WriteLine($"Command '{command}' not found in extension '{extensionName}'.");
                    return 1;
                }

                // Process execution parameters
                dynamic executionParameters = options.Parameters;

                // Optional event handler
                EventHandler eventHandler = (s, e) =>
                {
                    Console.WriteLine($"Event: {JsonConvert.SerializeObject(e)}");
                };

                // Execute command
                extension.Execute(executionOption, executionParameters, eventHandler);

                Console.WriteLine($"Command '{command}' executed successfully.");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
                return 1;
            }
        }

        private static List<IExtension> LoadExtensions(string extensionsPath, string extensionName)
        {
            var extensions = new List<IExtension>();

            if (!Directory.Exists(extensionsPath))
            {
                Console.WriteLine($"Extensions directory '{extensionsPath}' not found.");
                return extensions;
            }

            var extensionDirectory = Path.Combine(extensionsPath, extensionName);
            if (!Directory.Exists(extensionDirectory))
            {
                Console.WriteLine($"Extension directory '{extensionDirectory}' not found.");
                return extensions;
            }

            var compiledPath = Path.Combine(extensionDirectory, "Compiled");
            if (!Directory.Exists(compiledPath))
            {
                Console.WriteLine($"Compiled directory '{compiledPath}' not found for extension '{extensionName}'.");
                return extensions;
            }

            var versionDirectories = Directory.GetDirectories(compiledPath)
                .Select(dir => new { Path = dir, Version = ParseVersion(Path.GetFileName(dir)) })
                .Where(v => v.Version != null)
                .OrderByDescending(v => v.Version)
                .ToList();

            if (!versionDirectories.Any())
            {
                Console.WriteLine($"No valid versions found for extension '{extensionName}'.");
                return extensions;
            }

            var latestVersion = versionDirectories.First();

            foreach (var dll in Directory.GetFiles(latestVersion.Path, "*.dll"))
            {
                try
                {
                    Assembly assembly = null;

                    List<Type>? extensionTypes = null;
                    try
                    {
                        assembly = Assembly.LoadFrom(dll);
                        extensionTypes = assembly.GetTypes()?.Where(static t => t.GetInterfaces() != null && t.GetInterfaces().Length > 0 && t.GetInterfaces().Contains(typeof(IExtension))).ToList();

                    }
                    catch (Exception)
                    {

                    }

                    if (extensionTypes == null || !extensionTypes.Any())
                        continue;

                    foreach (var type in extensionTypes)
                    {
                        if (type.GetInterfaces().First() != typeof(IExtension))
                            continue;

                        try
                        {
                            var extensionInstance = Activator.CreateInstance(type, Configuration) as IExtension;
                            if (extensionInstance != null)
                            {
                                extensions.Add(extensionInstance);
                            }
                        }
                        catch (Exception exx)
                        {
                            Console.WriteLine($"Failed to load extension from '{dll}': {exx}");
                        }
                      
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load extension from '{dll}': {ex}");
                }
            }

            return extensions;
        }


        private static Version ParseVersion(string versionString)
        {
            return Version.TryParse(versionString, out var version) ? version : null;
        }
    }

    class ExtensionOptions
    {
        [Option('c', "command", Required = true, HelpText = "Command to execute in the extension.")]
        public string Command { get; set; }

        [Option('p', "parameters", Required = false, HelpText = "Parameters for the command in JSON format.")]
        public string Parameters { get; set; }
    }

    public class ExecutionOption
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
