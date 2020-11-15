using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.Service;
using Xamarin.Forms;

namespace Vocabul.ViewModels
{
    public class EditorViewModel : ViewModelBase
    {
        private ObservableCollection<Lession> _listViewLession;
        private object _selectionLession;
        private RefreshDataHelper _refreshDataHelper;
        private string _selectedLessionName;
        private ObservableCollection<Vocabulary> _currentVocables;
        public ICommand SaveCommand;
        private LessionService _lessionService;
        public static bool NotSaved { get; set; }
        public static bool IsRefreshingAfterTest { get; set; }
        public bool Reload { get; set; }
        public ICommand RefreshCommand { get; }
        private bool _constructionRun;
        bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        private int _SelectedItemIndex;
        public int SelectedItemIndex
        {
            get { return _SelectedItemIndex; }
            set
            {
                _SelectedItemIndex = value;
                if (value == -1){ }
                else{ }
            }
        }
        public ObservableCollection<Vocabulary> CurrentVocables
        {
            get { return _currentVocables; }
            set
            {
                _currentVocables = value;
                OnPropertyChanged("CurrentVocables");
            }
        }
        public string SelectedLessionName
        {
            get { return _selectedLessionName; }
            set
            {
                _selectedLessionName = value;

                OnPropertyChanged("SelectedLessionName");
            }
        }
        public ObservableCollection<Lession> ListViewLession
        {
            get { return _listViewLession; }
            set
            {
                _listViewLession = value;
                OnPropertyChanged("ListViewLession");
            }
        }
        public object SelectionLession
        {
            get { return _selectionLession; }
            set
            {
                _selectionLession = (Lession)value;
                Lession TemplessionObject = (Lession)_selectionLession;
                if (_selectionLession == null && _constructionRun != true && IsRefreshing != true)
                {
                    TemplessionObject = new Lession(ListViewLession);
                    SelectedLessionName = "Rename this!";
                    TemplessionObject.Modul_Name = SelectedLessionName;
                    TemplessionObject.Vocables = new ObservableCollection<Vocabulary>();
                    TemplessionObject.Vocables.Add(new Vocabulary { German = "", English = "", Counter = 0, ModulID = TemplessionObject.ModulID });
                    TemplessionObject.Vocables.Add(new Vocabulary { German = "", English = "", Counter = 0, ModulID = TemplessionObject.ModulID });
                    CurrentVocables = TemplessionObject.Vocables;
                    _selectionLession = TemplessionObject;
                    ListViewLession.Add(TemplessionObject);
                    CheckLastValue();
                }
                else if ((_selectionLession == null || TemplessionObject.Modul_Name == "" || TemplessionObject.Modul_Name == null) && _constructionRun == true && IsRefreshing != true)
                {
                    SelectedLessionName = "Rename this!";
                    TemplessionObject.Modul_Name = SelectedLessionName;
                    TemplessionObject.Vocables = new ObservableCollection<Vocabulary>();
                    TemplessionObject.Vocables.Add(new Vocabulary { German = "", English = "", Counter = 0, ModulID = TemplessionObject.ModulID });
                    TemplessionObject.Vocables.Add(new Vocabulary { German = "", English = "", Counter = 0, ModulID = TemplessionObject.ModulID });
                    CurrentVocables = TemplessionObject.Vocables;
                    _selectionLession = TemplessionObject;
                    ListViewLession.Add(TemplessionObject);
                    CheckLastValue();
                }
                else if (IsRefreshing != true)
                {
                    SelectedLessionName = TemplessionObject.Modul_Name;
                    CurrentVocables = PopulateVocables(TemplessionObject.Vocables);
                    TemplessionObject.Vocables = CurrentVocables;
                    CheckLastValue();
                }
                else
                {
                    CheckLastValue();
                }
                OnPropertyChanged("SelectionLession");
            }
        }
        public EditorViewModel()
        {
            ListViewLession = PopulateListViewLession();
            _constructionRun = true;
            SelectionLession = new Lession(ListViewLession);
            _constructionRun = false;
            RefreshCommand = new Command(ExecuteRefreshCommand);
            _lessionService = new LessionService();
            SaveCommand = new Command(SaveLessionsToDatabaseCommand);
            _refreshDataHelper = new RefreshDataHelper();

        }
        public void ValueChanged(object sender, EventArgs args)
        {
            var inputfield = (Entry)sender;
            if (inputfield.Text != null)
            {
                var temp = PopulateListViewLession();
                CheckLastValue();
            }
        }
        public void CheckLastValue()
        {

            int index = CurrentVocables.Count();
            if (index != 0)
            {
                index -= 1;
                try
                {
                    Vocabulary BeforeLastVocable = null;
                    var LastVocable = CurrentVocables[(index)];
                    while (LastVocable.German == "" && LastVocable.English == "" && LastVocable.VocableID == 0)
                    {
                        CurrentVocables.RemoveAt(index);
                        CurrentVocables = CurrentVocables;
                        try
                        {
                            index = CurrentVocables.Count() - 1;
                            LastVocable = CurrentVocables[(index)];
                        }
                        catch (Exception)
                        {
                            break;
                        }
                        try
                        {
                            BeforeLastVocable = CurrentVocables[index - 1];
                        }
                        catch (Exception)
                        {
                            break;
                        }

                        if (BeforeLastVocable == null)
                            break;
                    }
                    BeforeLastVocable = CurrentVocables[(index)];
                    CurrentVocables.Add(new Vocabulary() { German = "", English = "", Counter = 0, ModulID = BeforeLastVocable.ModulID });
                }
                catch (Exception)
                {

                }
            }
        }
        public void DeleteLession(int index)
        {
            switch (index)
            {
                case -1:
                    break;
                case 0:
                    CurrentVocables.Clear();
                    SelectedLessionName = "";
                    DeleteItem(ListViewLession[index]);
                    ListViewLession.RemoveAt(index);
                    break;
                default:
                    DeleteItem(ListViewLession[index]);
                    ListViewLession.RemoveAt(index);
                    break;
            }
        }
        public void RenameLession(int index, string Value)
        {
            try
            {
                ListViewLession[index].Modul_Name = Value;
            }
            catch (Exception)
            {
            }
   
        }
        public void ExecuteRefreshCommand()
        {
            // Stop refreshing
            IsRefreshing = false;
        }
        public void ManualRefresh()
        {
            isRefreshing = true;
        }
        public void EnableConstructionRun()
        {
            _constructionRun = true;
        }
        public void DisableConstructionRun()
        {
            _constructionRun = false;
        }
        public async Task SaveLessionsToDatabase()
        {
            await _lessionService.InsertIntoDatabase(ListViewLession);
            _refreshDataHelper.CallChange(null, "EditorViewModel");
        }
        private void SaveLessionsToDatabaseCommand()
        {
            Task.Run(SaveLessionsToDatabase);


        }
        private async void DeleteItem(Lession lessionToDelete)
        { 

            await _lessionService.DeleteEntryInDatabase(lessionToDelete);
            _refreshDataHelper.CallChange(null, "EditorViewModel");
        }
        public ObservableCollection<Lession> PopulateListViewLession()
        {
            ObservableCollection<Lession> NewLessions = new ObservableCollection<Lession>();

            foreach (var lession in StoreList.Lessions)
            {
                NewLessions.Add(lession);
            }
            return NewLessions;
        }
        public void RefreshListViewLession(object sender, EventArgs e)
        {
            ListViewLession = PopulateListViewLession();
        }
        private ObservableCollection<Vocabulary> PopulateVocables(ObservableCollection<Vocabulary> Vocables)
        {
            ObservableCollection<Vocabulary> NewVocables = new ObservableCollection<Vocabulary>();

            foreach (var vocable in Vocables)
            {
                NewVocables.Add(vocable);
            }
            return NewVocables;
        }

    }
}
