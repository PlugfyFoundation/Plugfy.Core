

# Plugfy Core

## **What is Plugfy Core?**
**Plugfy Core** is an open-source framework developed by the **Plugfy Foundation** to simplify, standardize, and accelerate the development of complex systems. 
Its modular and scalable architecture enables the creation of flexible, dynamic, and highly reusable applications by efficiently and transparently integrating modules and business workflows.

---

## **The Origin of Plugfy Core**
The **Plugfy** project was born in **2017**, within an industrial context, to address the challenges of integration and communication between different systems, business rules, and heterogeneous environments. 
Initially focused on negotiation flows and customizable modules, it quickly became a powerful solution for managing complexity in corporate environments.

In **2023**, the framework was completely modernized, introducing a new architecture focused on **work pipelines**, **distributed execution**, and **extensibility**. This evolution expanded its capabilities to operate at the system level and in virtualized environments, making it ideal for modern IT scenarios.

---

## **Architecture of Plugfy Core**

### **High-Level Design**

```
Plugfy Core
├── Kernel
│   ├── Event Manager          (Manages system and custom events)
│   ├── Module Loader          (Dynamically loads and manages modules)
│   ├── Execution Engine       (Orchestrates workflows and pipelines)
│   └── Configuration Manager  (Manages system and module configurations)
├── Modules
│   ├── Core Modules
│   │   ├── IF                (Conditional logic handler)
│   │   ├── SwitchCase        (Decision-making logic handler)
│   │   ├── ForEach           (Iterative execution handler)
│   │   └── Pipeline          (Workflow execution handler)
│   └── Custom Modules         (User-defined modules)
├── Integrations
│   ├── Operating System       (System-level operations)
│   ├── Cloud Services         (Interactions with cloud APIs)
│   ├── External APIs          (Third-party API integration)
│   └── Databases              (Database connectivity and management)
└── External Interfaces        (Exposes REST APIs for module interaction)
```

---

## **Use Cases**

### **1. System Integration**
- **Scenario**: Integrate an ERP with a CRM and a logistics system to automate order processing and tracking.
- **Solution**:
  - Use the `Pipeline` module to orchestrate workflows.
  - Combine `SwitchCase` for decision-making and `ForEach` to iterate over batch orders.

### **2. Automating Workflows**
- **Scenario**: Automate data analysis from multiple sources and generate real-time reports.
- **Solution**:
  - Use the `ForEach` module to iterate over data sources.
  - Deploy a custom module to analyze data and a logging module to track the process.

---

## **Examples**

### **Example 1: Creating a Simple Module**
```csharp
using Plugfy.Foundation.Core.Module;

var simpleModule = new Module("SimpleModule");
var simpleFunction = new Function("PrintMessage");

simpleFunction.Execute = (parameters) =>
{
    var message = parameters.ContainsKey("Message") ? parameters["Message"] : "Hello, Plugfy!";
    Console.WriteLine(message);
};

simpleModule.AddFunction(simpleFunction);

// Execute the function
simpleFunction.Execute(new Dictionary<string, object> { { "Message", "Welcome to Plugfy Core!" } });
```

### **Example 2: Configuring and Running a Pipeline**
```csharp
using Plugfy.Foundation.Core.Module.Pipeline;

var dataPipeline = new Pipeline("DataPipeline");

var step1Function = new Function("LoadData");
step1Function.Execute = (parameters) => Console.WriteLine("Loading Data...");

var step2Function = new Function("ProcessData");
step2Function.Execute = (parameters) => Console.WriteLine("Processing Data...");

var step1 = new PipelineStep("Step1", false, step1Function, step2Function);

dataPipeline.AddStep(step1);
dataPipeline.Execute();
```

---

## **Next Steps**
**Plugfy Core** is a living platform, constantly evolving. Our goals are to:
- **Expand capabilities**.
- **Integrate new technologies**.
- **Build a collaborative community** that drives technological innovation.

**Join us** on this journey to transform the development of complex systems.  
**Plugfy Core**: The foundation for the modular future.

---

## **License**
Plugfy Core is licensed under the **GNU General Public License v3.0**. For more information, see [GNU GPL v3.0](https://www.gnu.org/licenses/gpl-3.0.en.html).
