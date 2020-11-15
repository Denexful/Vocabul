using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabul.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Vocabul.Service;
using Vocabul.Helpers;

namespace Vocabul.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Vocal_List : ContentPage
    {
        Vocal_ListViewModel viewModel;
        public Vocal_List()
        {
            BindingContext= viewModel =  new Vocal_ListViewModel();
            InitializeComponent();
        }
    }
}