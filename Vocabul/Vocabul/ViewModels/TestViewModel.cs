using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.Service;
using Vocabul.Enums;
using System.Threading.Tasks;

namespace Vocabul.ViewModels
{
    public class TestViewModel : ViewModelBase
    {
        public SelectedMode SelectedMode;
        private List<Vocabulary> _vocables;
        private DatabaseService _database;
        public TestPageModel TestModel { get; set; }
        public string Message;
        
        public List<Vocabulary> Vocables
        {
            get { return _vocables; }
            set
            {
                _vocables = value;
                OnPropertyChanged("ListViewLession");

            }
        }

        private SelectionChanged _selectionChanged;

        private ObservableCollection<Lession> _lessionsSelected;
        public ObservableCollection<Lession> LessionsSelected
        {
            get
            {
                return _lessionsSelected;
            }
            set
            {
                _lessionsSelected = value;
                OnPropertyChanged("LessionsSelected");
            }

        }

        public string Title { get; set; }
        private List<int> _selectedIDs;
        public List<int> SelectedIDs {
            get
            {
                return _selectedIDs;
            }
            set
            {
                _selectedIDs = value;
                OnPropertyChanged("SelectedIDs");
            }
        }

        public TestViewModel()
        {
            TestModel = new TestPageModel();
            _database = new DatabaseService();
            _selectionChanged = new SelectionChanged();
            Vocables = GetVocables(SelectViewModel.SelectedModulIDs);
            TestModel.Modul_Names = new ObservableCollection<string>();
            Title = "Click play to start a test!";
            SelectedIDs = SelectViewModel.SelectedModulIDs;
            SelectedMode = new SelectedMode();
            SelectionChanged.SelecedIDsChanged += RefreshSelection;
        }

        public void RefreshData()
        {
            Vocables = GetVocables(SelectViewModel.SelectedModulIDs);
        }

        private void RefreshSelection(object sender, EventArgs e)
        {
            TestModel.Modul_Names.Clear();

            foreach (var name in SelectViewModel.SelectedModulNames)
            {
                TestModel.Modul_Names.Add(name);
            }        

            SelectedIDs = SelectViewModel.SelectedModulIDs;
        }
        private List<Vocabulary> GetVocables(List<int> ModulIDs)
        {
            List<Vocabulary> vocables = new List<Vocabulary>();
            if (ModulIDs != null)
            {
                foreach (var ID in ModulIDs)
                {
                    foreach (var vocable in _database.GetVocableAsync(ID).Result)
                    {
                        vocables.Add(vocable);
                    }

                }
            }


            return vocables;
        } 
        

    }
}
