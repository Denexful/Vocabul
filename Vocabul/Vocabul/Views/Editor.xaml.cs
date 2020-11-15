using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Vocabul.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Editor : ContentPage
    {
        private bool isOnInit;
        private EditorViewModel _editorView;
        public Editor()
        {
            BindingContext = _editorView = new EditorViewModel();
            RefreshDataHelper.DataChanged += Refresh;
            isOnInit = true;
            InitializeComponent();
            isOnInit = false;
        }

        private void NewButton_Clicked(object sender, EventArgs e)
        {
            switch (_editorView.ListViewLession.Count)
            {
                case 0:
                    _editorView.EnableConstructionRun();
                    _editorView.SelectionLession = new Lession() { Modul_Name = "" };
                    _editorView.DisableConstructionRun();
                    LessionPicker.SelectedIndex = 0;
                    break;
                default:
                    LessionPicker.SelectedIndex = -1;
                    break;
            }

        }
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.OldTextValue != null)
            {
                switch (isOnInit)
                {
                    case true:
                        break;
                    default:
                        if (EditorViewModel.IsRefreshingAfterTest)
                        {

                        }
                        else
                        {
                            SaveButton.BorderColor = Color.Red;
                            LessionPicker.IsEnabled = false;
                            NewButton.IsEnabled = false;
                            EditorViewModel.NotSaved = true;

                        }
                        break;
                }
                _editorView.ValueChanged(sender, e);
            }


        }
        private void TrashButton_Clicked(object sender, EventArgs e)
        {
            switch (_editorView.ListViewLession.Count)
            {
                case 1:
                    _editorView.ManualRefresh();
                    _editorView.DeleteLession(LessionPicker.SelectedIndex);
                    _editorView.ExecuteRefreshCommand();

                    break;
                default:
                    _editorView.DeleteLession(LessionPicker.SelectedIndex);
                    break;
            }

        }

        private void RenameLession(object sender, TextChangedEventArgs e)
        {
            _editorView.RenameLession(LessionPicker.SelectedIndex, e.NewTextValue);
            switch (isOnInit)
            {
                case true:
                    break;
                default:
                    if (EditorViewModel.IsRefreshingAfterTest)
                    {

                    }
                    else
                    {
                        SaveButton.BorderColor = Color.Red;
                        EditorViewModel.NotSaved = true;
                        LessionPicker.IsEnabled = false;
                        NewButton.IsEnabled = false;

                    }
                    break;
            }
            _editorView.ValueChanged(sender, e);
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            switch (RenameLessionEntry.Text)
            {
                case "":
                    await DisplayAlert("Alert", "Add your preferred group name where \"Rename this!\" was", "OK");
                    break;
                case "Rename this!":
                    await DisplayAlert("Alert", "Change \"Rename this!\" to your preferred group name!", "OK");
                    break;
                default:
                    if (_editorView.CurrentVocables[0].English == "" || _editorView.CurrentVocables[0].German == "")
                    {
                        await DisplayAlert("Alert", "The first vocable must have a value in the left and the right column", "OK");

                    }
                    else
                    {
                        await _editorView.SaveLessionsToDatabase();
                        Refresh(sender, e);
                        switch (isOnInit)
                        {
                            case true:
                                break;
                            default:
                                SaveButton.BorderColor = Color.Transparent;
                                LessionPicker.IsEnabled = true;
                                NewButton.IsEnabled = true;
                                EditorViewModel.NotSaved = false;

                                break;
                        }
                    }

                    break;
            }
        }
        private void Refresh(object sender, EventArgs e)
        {
            int tempIndex = LessionPicker.SelectedIndex;
            _editorView.ManualRefresh();
            LessionPicker.ItemsSource = new List<string>();
            LessionPicker.SelectedItem = null;
            _editorView.RefreshListViewLession(sender, e);
            LessionPicker.SelectedItem = _editorView.SelectionLession;
            LessionPicker.ItemsSource = _editorView.ListViewLession;
            _editorView.ExecuteRefreshCommand();
            LessionPicker.SelectedIndex = tempIndex;
        }

        private void LessionPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (isOnInit)
            {
                case true:
                    break;
                default:
                    if (EditorViewModel.IsRefreshingAfterTest)
                    {

                    }
                    else
                    {
                        SaveButton.IsEnabled = true;
                        NewButton.IsEnabled = false;
                        EditorViewModel.NotSaved = false;

                    }
                    break;
            }
        }
    }
}