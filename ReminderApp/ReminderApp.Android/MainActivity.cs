using Android.App;
using Android.Widget;
using Android.OS;
using Kordata.KForms;
using Android.Content;

namespace ReminderApp.Android
{
	[Activity(Label = "ReminderApp", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.launchFormButton);

			button.Click += (s, e) => LaunchForm();
		}

		public void LaunchForm()
		{
			// Pass your form to GetLaunchIntent and we handle the rest
			var intent = FormActivity.GetLaunchIntent(this, GetReminderForm());

			StartActivityForResult(intent, 42);
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (requestCode == 42)
			{
				switch (resultCode)
				{
					case Result.Ok:
						// Form was saved. Let's get the data out.
						var form = FormActivity.GetSavedForm(data);
						var jsonData = form.SaveContext.Object;

						// Save your form data here.
						// For this tutorial we're just presenting the raw JSON data
						TextView textView = FindViewById<TextView>(Resource.Id.formData);
						textView.Text = "Here is the JSON data from the form:\n" + jsonData;

						break;
					case Result.Canceled:
						// Form was cancelled. Handle that case here.
						break;
				}
			}
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
