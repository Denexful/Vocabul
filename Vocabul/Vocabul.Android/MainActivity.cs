using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.IO;
using SQLite;
using ButtonCircle.FormsPlugin.Droid;

namespace Vocabul.Droid
{
    [Activity(Label = "Vocabul", Icon = "@drawable/Favico", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            ButtonCircleRenderer.Init();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            string fileName = "VocalDB.db";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
            var t2st = Directory.GetFiles(folderPath);


            string folderPath2 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var tf2st = Directory.GetFiles(folderPath2);

            string folderPath32 = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var tf2sft = Directory.GetFiles(folderPath32);

            string dbPath = FileAccessHelper.GetLocalFilePath("VocalDB.db");
            Xamarin.Forms.Forms.SetFlags(new string[] { "Expander_Experimental", "Shapes_Experimental"});
            string completePath = Path.Combine(folderPath, fileName);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            if (File.Exists(completePath))
            {
        

                var anothertest = Assets.GetLocales();

                var test = Assets.Class;

            }
            LoadApplication(new App(dbPath));
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}