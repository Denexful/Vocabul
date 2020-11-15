using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vocabul.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabul.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        TestViewModel _testViewModel;
        public TestPage()
        {
            BindingContext = _testViewModel = new TestViewModel();
            InitializeComponent();
            RandomMode_Clicked(null, null);
            
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (EditorViewModel.NotSaved)
            {
                await DisplayAlert("Alert", "Got to \"Editor\" and save your changes!", "OK");
            }
            else if(_testViewModel.SelectedIDs?.Count <1)
            {
                await DisplayAlert("Alert", "Go to \"Select\" and select a group!", "OK");
            }
            else
            {
                _testViewModel.RefreshData();
                await Navigation.PushAsync(new VocabulTest(_testViewModel.Vocables, _testViewModel.SelectedMode));
            }

        }

        private  void LeftMode_Clicked(object sender, EventArgs e)
        {
            _testViewModel.SelectedMode = Enums.SelectedMode.Left;
             LeftMode.ScaleTo(1.5,100);
             LeftMode.FadeTo(1, 100);
             
            RandomMode.ScaleTo(1, 100);
            RandomMode.FadeTo(0.5, 100);
            
            RightMode.ScaleTo(1, 100);
            RightMode.FadeTo(0.5, 100);
        }

        private  void RandomMode_Clicked(object sender, EventArgs e)
        {
            _testViewModel.SelectedMode = Enums.SelectedMode.Random;
            RandomMode.ScaleTo(1.5, 100);
            RandomMode.FadeTo(1, 100);
             
            LeftMode.ScaleTo(1, 100);
            LeftMode.FadeTo(0.5, 100);
            
            RightMode.ScaleTo(1, 100);
            RightMode.FadeTo(0.5, 100);
        }

        private  void RightMode_Clicked(object sender, EventArgs e)
        {
            _testViewModel.SelectedMode = Enums.SelectedMode.Right;
            RightMode.ScaleTo(1.5, 100);
            RightMode.FadeTo(1, 100);

            LeftMode.ScaleTo(1, 100);
            LeftMode.FadeTo(0.5, 100);

            RandomMode.ScaleTo(1, 100);
            RandomMode.FadeTo(0.5, 100);
        }
    }
}