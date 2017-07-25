# **Calculations**

___

Kforms allows for cross field calculations to be intuitively defined as properties of the output fields. 

For example, in the calculation form example, given a radius value in a text input field, the volume of a sphere is calculated in real time as the input value is adjusted. The output field is linked to the input field by a unique field ID provided when the input field is instantiated.

To set up a calculation you need:

- One or more input fields, each with a unique Id Property
- One or more output fields, each with a Function Property and a Variables property with the specific variables that correspond to the previously defined Function Property.
