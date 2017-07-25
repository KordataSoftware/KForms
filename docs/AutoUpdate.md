---
uid: AutoUpdate
---
# AutoUpdate

At times it can be useful to automatically update the value of a field based on the value of other fields the user has already filled out. For example, if you want to remind the user at the top of each page about a value they selected on the first page of a long form, you could use AutoUpdate to have a field (or multiple fields) "mirror" the value of a field from the first page.

```csharp
var myForm = new KForm
{
    Pages = {
        new KPage {
            Title = "Basic Information",
            Fields = {
                new NumberField {
                    Id = "employeeNumberField",
                    Title = "Employee #",
                    PropertyName = "employeeNumber"
                },
                ...
            }
        },
        new KPage {
            Title = "Another Page",
            Fields = {
                new NumberField {
                    Title = "Employee #",
                    IsEditable = false,
                    AutoUpdate = AutoUpdate.Mirror("employeeNumberField")
                },
                ...
            }
        }
    }
};
```

AutoUpdate can be paired with conditional expressions to dynamically change the content of a field based on which conditions are met. For example, the following form has a "Compliance" field that automatically adjust its value based on how many tests have failed according to the user.


```csharp
var myForm = new KForm
{
    Fields = {
        new TextDisplay {
            Title = "0 Fails = Compliant\n1 Fail = Problem Area\n2 Fails = Non-Compliant\n3+ Fails = Serious Violation" },
        new YesNoField { Title = "Does Test A Pass?", Id = "testA" },
        new YesNoField { Title = "Does Test B Pass?", Id = "testB" },
        new YesNoField { Title = "Does Test C Pass?", Id = "testC" },
        new YesNoField { Title = "Does Test D Pass?", Id = "testD" },
        new TextInputField {
            Title = "Compliance",
            IsEditable = false,
            AutoUpdate =
                AutoUpdate
                .To("Serious Violation").When(valueOf => IsSeriousViolation(valueOf("testA"), valueOf("testB"), valueOf("testC")))
                .To("Non-Compliant").When(valueOf => IsNonCompliant(valueOf("testA"), valueOf("testB"), valueOf("testC")))
                .To("Problem Area").When(valueOf => IsProblemArea(valueOf("testA"), valueOf("testB"), valueOf("testC")))
                .Otherwise("Compliant")
            }
    }
};
```

