using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.Service;
using Vocabul.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabul.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Select : ContentPage
    {
        SelectViewModel selectViewModel;
        public Select()
        {
            BindingContext = selectViewModel = new SelectViewModel();
            RefreshDataHelper.DataChanged += RefreshData;
            InitializeComponent();
        }

        private void RefreshData(object sender, EventArgs e)
        {
            LessionSelection.SelectedItems.Clear();
        }

        private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectViewModel.SelectedLessions = LessionSelection.SelectedItems.Cast<Lession>().ToList();


        }
    }
}