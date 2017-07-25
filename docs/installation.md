---
uid: InstallationPage
---

[screenshotAndroid1]: ../images/android/ScreenShot1.png "New Project"
[screenshotAndroid2]: ../images/android/ScreenShot2.png "Select Android App"
[screenshotAndroid3]: ../images/android/ScreenShot3.png "Target Platform"
[screenshotAndroid4]: ../images/android/ScreenShot4.png "Project Settings"
[screenshotAndroid5]: ../images/android/ScreenShot5.png "Add Packages"
[screenshotAndroid6]: ../images/android/ScreenShot6.png "Add Kordata.KForms Package"
[screenshotAndroid7]: ../images/android/ScreenShot7.png "App Theme"
[screenshotAndroid8]: ../images/android/ScreenShot8.png "Reminder App Main"
[screenshotAndroid9]: ../images/android/ScreenShot9.png "Reminder App Form"
[screenshotAndroid10]: ../images/android/ScreenShot10.png "Reminder App Result"

[screenshotiOS1]: ../images/iOS/ScreenShot1.png "Select iOS App"
[screenshotiOS2]: ../images/iOS/ScreenShot2.png "Target Platform"
[screenshotiOS3]: ../images/iOS/ScreenShot3.png "Project Settings"
[screenshotiOS4]: ../images/iOS/ScreenShot4.png "Add Packages"
[screenshotiOS5]: ../images/iOS/ScreenShot5.png "Add Kordata.KForms Package"
[screenshotiOS6]: ../images/iOS/ScreenShot6.png "Startup Project"
[screenshotiOS7]: ../images/iOS/ScreenShot7.png "Button and TextView"
[screenshotiOS8]: ../images/iOS/ScreenShot8.png "Reminder App Main"
[screenshotiOS9]: ../images/iOS/ScreenShot9.png "Reminder App Form"
[screenshotiOS10]: ../images/iOS/ScreenShot10.png "Reminder App Result"

# Installation
##### This tutorial will show you how to get up and running with KForms and build a simple reminder form in minutes with Xamarin.Android and Xamarin.iOS.

### 1. Install Visual Studio 2017

You can find Visual Studio 2017 Community Edition for free at this link: [https://www.visualstudio.com/downloads](https://www.visualstudio.com/downloads/)
Make sure you to check Xamarin.Android (for Android) and Xamarin.iOS (for iOS) during Visual Studio 2017 installation process.

### 2. Create a Xamarin.Android Solution

Click **New Project...**

![alt text][screenshotAndroid1]

Select Android App

![alt text][screenshotAndroid2]

Configure your app name and Target platforms:

![alt text][screenshotAndroid3]

Configure your project and hit Create

![alt text][screenshotAndroid4]

### 3. Add the KForms nuget package to your project

You can add KForms nuget package via the Package Manager Console by running the following command: **Install-Package Kordata.KForms**

Alternatively, in your project folder you can right-click on **Packages** and select **Add Packages…**

![alt text][screenshotAndroid5]

Find the **Kordata.KForms** nuget package and select **Add Package**.

![alt text][screenshotAndroid6]

### 4. Setup the Form

We are now ready to make our first KForm. Our MainActivity is going to have a button to launch the form and show the data saved after the form is completed.

Here is the **MainActivity.cs** file

```csharp
using Android.App;
using Android.Widget;
using Android.OS;
using Kordata.KForms;
using Android.Content;
using Kordata.KForms.Fields;

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
```
Now add the form definition to **MainActivity.cs**

```csharp
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

			return form;
		}
	}
}
```

Let’s setup the app main layout in ReminderApp.Android/Resources/layout/**Main.axml**

```xml
<?xml version="1.0" encoding="utf-8"?>
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    android:orientation="vertical"
    android:layout_width="match_parent"
    android:layout_height="match_parent">
    <Button
        android:id="@+id/launchFormButton"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:text="Launch Reminder Form" />
    <TextView
        android:id="@+id/formData"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        android:text="We will show the saved form data here after completing the form."
        android:layout_margin="16dp" />
</LinearLayout>
```

### 5. Setup the app theme

KForms requires an Theme.AppCompat to be set. For this tutorial we’ll use **@style/Theme.AppCompat.Light.DarkActionBar** To do so, edit the AndroidManifest.xml file by clicking on it and set the Application Theme or you can edit the raw file like so

![alt text][screenshotAndroid7]

```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android" android:versionCode="1" android:versionName="1.0" package="com.tutorial.reminderapp">
    <uses-sdk android:minSdkVersion="16" />
    <application android:allowBackup="true" android:icon="@mipmap/icon" android:label="@string/app_name" android:theme="@style/Theme.AppCompat.Light.DarkActionBar"></application>
</manifest>
```

### 6. Launch the app

Press the **Launch Reminder Form** button

![alt text][screenshotAndroid8]

Fill out the form and save it

![alt text][screenshotAndroid9]

Observe the raw JSON data from the saved form

![alt text][screenshotAndroid10]

### 7. Create a Xamarin.iOS project

This part assumes that you already have Visual Studio 2017 installed. Please refer to step 1 if you haven't.
Add a new project to your solution (Single View App)

![alt text][screenshotiOS1]

Configure your iOS App. KForms supports iOS 9.0 and up.

![alt text][screenshotiOS2]

Configure your project name and hit Create.

![alt text][screenshotiOS3]

### 8. Add KForms nuget package to your iOS project

You can add KForms nuget package via the Package Manager Console by running the following command: **Install-Package Kordata.KForms**

Alternatively, in your project folder you can right-click on **Packages** and select **Add Packages…**

![alt text][screenshotiOS4]

Find the **Kordata.KForms** nuget package and select **Add Package**.

![alt text][screenshotiOS5]

### 9. Change Startup Project to the iOS project

If you followed this tutorial from step 1 you now have 2 projects. You need to select the Startup project like so:

![alt text][screenshotiOS6]

### 10. Add a **Button** and a **TextView** to the **Main.storyboard**

![alt text][screenshotiOS7]

### 11. Setup ViewController.cs

```csharp
using System;
using Kordata.KForms;
using UIKit;
using System.Threading.Tasks;
using Kordata.KForms.Fields;

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
			formVc.FormCancelledAsync += FormViewController_HandleCancelFormAsync;
			formVc.FormSubmittedAsync += FormViewController_HandleSaveFormAsync;

			// Present the form.
			PresentViewController(formVc, true, null);

        }

		async Task FormViewController_HandleCancelFormAsync(KForm form)
		{
			// Put your form cancel logic here
		}

		async Task FormViewController_HandleSaveFormAsync(KForm form)
		{
            // Form was saved. Let's get the data out.
			var jsonData = form.SaveContext.Object;

			// Save your form data here.
			// For this tutorial we're just presenting the raw JSON data
			formDataTextView.Text = "Here is the JSON data from the form:\n" + jsonData;
		}
```

Now add the form definition to **ViewController.cs**

```csharp
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

			return form;
		}
    }
}
```

### 12. Launch the app

Press the **Launch Reminder Form** button

![alt text][screenshotiOS8]

Fill out the form and save it

![alt text][screenshotiOS9]

Observe the raw JSON data from the saved form

![alt text][screenshotiOS10]
