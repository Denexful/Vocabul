using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Vocabul.Views;
using Vocabul.Service;
using Vocabul.Classes;

namespace Vocabul
{
    public partial class App : Application
    {
        public static string FilePath;
        public App()
        {
            InitializeComponent();
            StoreList storeList = new StoreList();
            MainPage = new TabbedMain();
            Device.SetFlags(new string[] { "Expander_Experimental" , "Shapes_Experimental" });     
        }

        public App(string filePath)
        {
            FilePath = filePath;
            InitializeComponent();
            StoreList storeList = new StoreList();
            MainPage = new TabbedMain(); 
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
