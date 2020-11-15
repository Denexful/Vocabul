using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Input;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.Service;
using Xamarin.Forms;

namespace Vocabul.ViewModels
{
    public class SelectViewModel : ViewModelBase
    {
        private static SelectionChanged _selectionChanged;
        private static List<int> _selectedLessionsIDs;
        public static List<int> SelectedModulIDs
        {
            get
            {
                if (_selectedLessionsIDs == null)
                {
                    _selectedLessionsIDs = new List<int>();
                }
                return _selectedLessionsIDs;
            }
            set
            {
                if (_selectedLessionsIDs == null)
                {
                    _selectedLessionsIDs = new List<int>();
                }


                _selectedLessionsIDs = value;
                

            }
        }
        private static List<string> _selectedModulNames;
        public static List<string> SelectedModulNames
        {
            get
            {
                return _selectedModulNames;
            }
            set
            {
                if (_selectedModulNames == null)
                {
                    _selectedModulNames = new List<string>();
                }


                _selectedModulNames = value;


            }
        }


        private string _selectedLessionNames;
        public string SelectedLessionNames
        {
            get
            {
                return _selectedLessionNames;
            }
            set
            {
                _selectedLessionNames = value;
                OnPropertyChanged("SelectedLessionNames");

            }
        }
        private List<Lession> _selectedLessions;
        public List<Lession> SelectedLessions
        {
            get
            {
                return _selectedLessions;
            }
            set
            {
                _selectedLessions?.Clear();
                _selectedLessions = value;
                SelectedLessionNames = String.Empty;
                SelectedModulIDs?.Clear();
                SelectedModulNames?.Clear();
                switch (SelectedLessions.Count)
                {
                    case 0:
                        SelectedLessionNames = "None";
                        _selectionChanged?.CallChange(null);
                        break;
                    default:
                        for (int i = 0; i < _selectedLessions.Count; i++)
                        {
                            if (i == _selectedLessions.Count-1)
                            {
                                SelectedLessionNames += _selectedLessions[i].Modul_Name;
                            }
                            else
                            {
                                SelectedLessionNames += _selectedLessions[i].Modul_Name + ", ";
                            }
                            SelectedModulIDs.Add(_selectedLessions[i].ModulID);
                            SelectedModulNames.Add(_selectedLessions[i].Modul_Name);
                            _selectionChanged?.CallChange(null);
                        }
                        break;
                }
            }
        }
        private List<Lession> _listViewLession;
        public List<Lession> ListViewLession
        {
            get { return _listViewLession; }
            set
            {
                _listViewLession = value;
                OnPropertyChanged("ListViewLession");

            }
        }
        public SelectViewModel()
        {
            ListViewLession = new List<Lession>(StoreList.Lessions.Select(x => x.ShallowCopy()));
            SelectedLessions = new List<Lession>();
            SelectedLessionNames = "None";
            SelectedModulIDs = new List<int>();
            SelectedModulNames = new List<string>();
            _selectionChanged = new SelectionChanged();
            RefreshDataHelper.DataChanged += RefreshData;
        }
        public void RefreshData(object sender, EventArgs e)
        {
            SelectedLessionNames = "None";
            SelectedModulIDs?.Clear();
            SelectedLessions?.Clear();
            ListViewLession = new List<Lession>(StoreList.Lessions.Select(x => x.ShallowCopy()));
        }

        private void SelectionChanged()
        {

        }
    }
}
