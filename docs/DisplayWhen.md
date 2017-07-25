[DisplayWhenExample]: ../images/iOS/DisplayWhenExample.gif "DisplayWhen Example"

# DisplayWhen
### Conditionally Visible Fields/Sections/Pages

Have you ever filled out a long paper form and had to skip 4 pages because you answered "No" to a question on the first page? Or even worse, not noticed the instruction to skip those pages and filled them out unnecessarily? This type of poor user experience can be avoided by using DisplayWhen functionality to conditionally show or hide pages, sections, and fields.

For example, consider the following home inspection form. If the home being inspected doesn't have a pool, then there is no need to show any of the questions related to the condition of the pool. Instead, the form can be configured to only show the "Pool" section when the question "Does the home have a pool?" has been answered with "Yes."

![alt text][DisplayWhenExample]

### How It Works
Add the `DisplayWhen` property to any page, section, or field. 

`DisplayWhen` must be an expression that takes a GetFieldValue function as a parameter and returns a boolean of whether the field should be visible or not.

```csharp
public Expression<Func<GetFieldValue, bool>> DisplayWhen;
```

`GetFieldValue` is a function that you pass a string and it returns the current value of the field with that id.

```csharp
public delegate JToken GetFieldValue(string fieldId);
```

The GetFieldValue function can be called inside of the `DisplayWhen` expression to compare the current value of any field with any value you want. If the expression returns `true` then the field will be shown. For example the following code will only show the second TextInputField when `"myTargetValue"` is typed into the first TextInputField field.

```csharp
new TextInputField
{
    Id = "myField"
},
new TextInputField
{
    DisplayWhen = valueOf => (string)valueOf("myField") == "myTargetValue"
}
```

### Full Example
Here's the full example of the home inspection form with a conditionally visible section:

```csharp
var form = new KForm
{
    Title = "Home Inspection",
    Sections = {
        new KSection {
            Title = "General",
            Fields = {
                new YesNoField
                {
                    Title = "Does the home have a pool?",
                    Id = "pool"
                }
            }
        },
        new KSection {
            Title = "Pool",
            DisplayWhen = valueOf => (bool?)valueOf("pool") == true,
            Fields = {
                new YesNoField
                {
                    Title = "Are all diving boards and slides securely anchored?",
                },
                new SliderField
                {
                    Title = "Pressure gauge reading (psi)",
                    Id = "pressureGauge",
                    Minimum = 0,
                    Maximum = 20,
                    StepSize = 1
                },
                new YesNoField
                {
                    Title = "Did you clean the pressure filter?",
                }
            }
        }
    }				
};
```

