using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Enums;
using Vocabul.Models;
using Vocabul.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabul.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VocabulTest : ContentPage
    {
        private VocabulTestViewModel _vocabulTestViewModel;
        public VocabulTest(List<Vocabulary> Vocables, SelectedMode selectionMode)
        {
           
            BindingContext = _vocabulTestViewModel = new VocabulTestViewModel(Vocables, selectionMode);
            InitializeComponent();
        }

        private void TestDone_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EvaluationPage(_vocabulTestViewModel.OriginalList, _vocabulTestViewModel.TestVocables.ToList()));
        }
    }
}