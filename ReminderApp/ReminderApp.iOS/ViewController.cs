using System;
using Kordata.KForms;
using UIKit;

namespace ReminderApp.iOS
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void OnLaunchFormPressed(UIButton sender)
        {
			// Pass your form to the FormViewController.Create method.
            var formVc = FormViewController.Create(GetReminderForm());

			// Attach event handlers here.
			formVc.FormCancelled += FormViewController_HandleCancelForm;
			formVc.FormSubmitted += FormViewController_HandleSaveForm;

			// Present the form.
			PresentViewController(formVc, true, null);

        }

		void FormViewController_HandleCancelForm(object sender, EventArgs e)
		{
			// Put your form cancel logic here
		}

		async void FormViewController_HandleSaveForm(object sender, FormSubmittedEventArgs e)
		{
            // Form was saved. Let's get the data out.
			var jsonData = e.Form.SaveContext.Object;

			// Save your form data here.
			// For this tutorial we're just presenting the raw JSON data
			formDataTextView.Text = "Here is the JSON data from the form:\n" + jsonData;
		}

		public KForm GetReminderForm()
		{
			var form = new KForm
			{
				Title = "Reminder Form",
				Pages = {
					new KPage
					{
                        // Since we only have one page the page title won't show
                        Title = "Page 1",
						Sections = {
							new KSection
							{
								Title = "Section 1",
								Fields = {
									new TextInputField
									{
										Title = "Give your reminder a name:",
										PropertyName = "reminderName",
										IsRequired = true
									},
									new TextInputField
									{
										Title = "Reminder Description:",
										PropertyName= "reminderDescription"
									},
									new DatePickerField
									{
										Title = "When do you want to be reminded?",
										PropertyName="reminderDate",
										DataFormat = "yyyy-MM-dd",
										DisplayFormat = "MM/dd/yyyy",
										IsRequired = true
									}
								}
							}
						}
					}
				}
			};

			form.Prepare();

			return form;
		}
    }
}
