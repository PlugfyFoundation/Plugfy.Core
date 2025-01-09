# **Plugfy Core**

### **A Modular Framework for Scalable and Flexible Applications**

Plugfy Core is an open-source modular framework designed to create scalable, flexible, and dynamic applications. Developed by the Plugfy Foundation and community, Plugfy Core enables seamless integration of modules, components, and functions, allowing developers to build extensible systems using reusable, event-driven architectures.

---

## **Table of Contents**

1. [Introduction](#introduction)
2. [Key Features](#key-features)
3. [Core Modules](#core-modules)
4. [Installation](#installation)
5. [Usage](#usage)
6. [Contributing](#contributing)
7. [Community](#community)
8. [License](#license)

---

## **Introduction**

Plugfy Core is a lightweight, extensible framework built on .NET Core, providing the foundation for modular application development. Its primary focus is to simplify the management of modules, components, and their interactions while offering out-of-the-box support for pipelines, condition handling, iteration, and more.

The project adheres to **GNU General Public License v3.0**, promoting transparency, collaboration, and community-driven innovation.

---

## **Key Features**

- **Modular Architecture**:
  Build applications using independent, reusable modules and components.

- **Event-Driven**:
  Seamless handling of system and custom events for lifecycle management.

- **Pipeline Support**:
  Define and execute complex workflows with sequential and parallel steps.

- **Conditional Logic**:
  Implement dynamic logic with `IF` and `SwitchCase` modules.

- **Iteration Management**:
  Easily process collections with the `ForEach` module.

- **Extensibility**:
  Add custom logic, modules, and components with ease.

- **Open Source**:
  Fully licensed under **GNU GPL v3.0**, ensuring long-term community collaboration.

---

## **Core Modules**

### **1. Plugfy.Foundation.Core.Module**
- **Purpose**: The core module for managing modular applications.
- **Features**:
  - Define `Modules`, `Components`, and their `Functions`.
  - Enable event-driven communication between modules.

### **2. Plugfy.Foundation.Core.Module.Pipeline**
- **Purpose**: Manage and execute workflows.
- **Features**:
  - Support sequential and parallel execution of steps.
  - Connect and manage dependent `ModuleFunctions`.

### **3. Plugfy.Foundation.Core.Module.IF**
- **Purpose**: Handle conditional branching.
- **Features**:
  - Evaluate logical expressions.
  - Execute `True` or `False` functions dynamically.

### **4. Plugfy.Foundation.Core.Module.SwitchCase**
- **Purpose**: Simplify decision-making logic.
- **Features**:
  - Map input values to specific functions.
  - Execute corresponding logic for matched cases.

### **5. Plugfy.Foundation.Core.Module.ForEach**
- **Purpose**: Handle iteration over collections.
- **Features**:
  - Execute functions for each item in a collection.
  - Support parallel execution for scalability.

---

## **Installation**

### **Prerequisites**
1. .NET Core 3.1 or higher installed on your system.
2. Git for cloning the repository.

### **Clone the Repository**
```bash
git clone https://github.com/PlugfyFoundation/PlugfyCore.git
cd PlugfyCore
```

### **Build the Project**
```bash
dotnet build
```

### **Run Unit Tests**
```bash
dotnet test
```

---

## **Usage**

### **Basic Example: Create and Execute a Module**

#### Define a Module
```csharp
using Plugfy.Foundation.Core.Module;

var coreModule = new Module("CoreModule");
coreModule.Parameters.Add("Version", "1.0");
```

#### Add a Component and Function
```csharp
var loggingComponent = new Component("LoggingComponent");
var logFunction = new Function("LogMessage");
logFunction.Parameters.Add("Message", "This is a test log.");

loggingComponent.AddFunction(logFunction);
coreModule.AddComponent(loggingComponent);
```

#### Execute a Function
```csharp
coreModule.Components.First().Functions.First().Execute();
```

---

### **Advanced Example: Execute a Pipeline**

#### Create a Pipeline
```csharp
using Plugfy.Foundation.Core.Module.Pipeline;

var pipeline = new Pipeline("DataProcessingPipeline");

var step1Function = new ModuleFunction("LoadData");
var step2Function = new ModuleFunction("ProcessData");

var step1 = new PipelineStep("Step1", false, step1Function, step2Function);
pipeline.AddStep(step1);

pipeline.Execute();
```

---

## **Contributing**

We welcome contributions from the community to make Plugfy Core better! Follow these steps to get started:

1. **Fork the Repository**: Click the fork button in the GitHub repository.
2. **Clone Your Fork**:
   ```bash
   git clone https://github.com/your-username/PlugfyCore.git
   ```
3. **Create a Feature Branch**:
   ```bash
   git checkout -b feature/your-feature
   ```
4. **Commit Your Changes**:
   ```bash
   git commit -m "Add your message here"
   ```
5. **Push Your Changes**:
   ```bash
   git push origin feature/your-feature
   ```
6. **Create a Pull Request**: Open a pull request on the main repository.

### **Code of Conduct**
We adhere to the [Contributor Covenant Code of Conduct](https://www.contributor-covenant.org/version/2/0/code_of_conduct/). Please read and follow it while contributing.

---

## **Community**

Join the Plugfy Foundation community to collaborate, learn, and innovate:

- **GitHub Discussions**: Share ideas and ask questions in the repository discussions.
- **Discord**: Join our [Discord server](#) for real-time collaboration (link coming soon).
- **Twitter**: Follow us on [Twitter](https://twitter.com/PlugfyFoundation) for updates.

---

## **License**

Plugfy Core is licensed under the **GNU General Public License v3.0**. You are free to use, modify, and distribute the software, provided you adhere to the license terms.

**Read more**: [GNU General Public License v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html)

---

## **Acknowledgments**

Special thanks to:
- The Plugfy Foundation for their vision and leadership.
- The open-source community for their contributions and support.

Letâs build the future of modular application development together!