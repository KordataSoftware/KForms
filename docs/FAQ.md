# Frequently Asked Questions

### Can I integrate with my database or web service? What about dynamically loading the options for a DropDown when they can't be known ahead of time?
Yes and yes! Kforms makes no assumptions about where data is loaded from or saved to. To dynamically load options from an external source, create a subclass of FormDataProvider and pair it with an ObjectSelectField. You can even filter the list of options based on the value of another field, such as filtering a list of book titles based on the author the user selected. See [this](xref:Kordata.KForms.Fields.ObjectSelectField) this page for more information about the powerful flexibility of the ObjectSelectField.

### How do I get KForms form data out?
After saving a form, KForm generates a FormSaveContext that contains the saved data as a JSON object with the properties being the PropertyNames that were setup during the building of the form and the values being the values entered by the user while filling the form. Learn more about @Kordata.KForms.FormSaveContext

FormSaveContext contains also a list of LineItems that were included in the form. Learn more about @Kordata.KForms.LineItem

### I'm a student, can I use KForms for free?
Absolutely! Students can use KForms library for free until they deploy to an app store. At which point we require a valid paid license.
To know more about our pricing please refer to [KForms.io](https://kforms.io)

### Where can I submit feedback?
You can submit your issues and feedback to our upcoming github repository.
