using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Models;
using Vocabul.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabul.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluationPage : ContentPage
    {
        private EvaluationPageViewModel _evaluationPageViewModel;
        public EvaluationPage(List<Vocabulary> originalList, List<Vocabulary> userList)
        {
            BindingContext = _evaluationPageViewModel = new EvaluationPageViewModel(originalList,userList);
            InitializeComponent();
        }

        private void Finish_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
            EditorViewModel.IsRefreshingAfterTest = false;
        }
    }
}