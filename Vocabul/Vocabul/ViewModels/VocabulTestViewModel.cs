using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Vocabul.Businesses;
using Vocabul.Enums;
using Vocabul.Models;
using Xamarin.Forms;

namespace Vocabul.ViewModels
{
    class VocabulTestViewModel : ViewModelBase
    {
        ILessionBusinessFactory _lessionBusinessFactory;
        public List<Vocabulary> OriginalList { get; set; }
        public ObservableCollection<Vocabulary> TestVocables { get; set; }

        private SelectedMode _selectionMode;
        public VocabulTestViewModel(List<Vocabulary> vocables, SelectedMode selectionMode)
        {
            _lessionBusinessFactory = LessionBusinessClass.CreateLessionBusinessFactory();
            _selectionMode = selectionMode;
            if (vocables != null)
            {
                OriginalList = new List<Vocabulary>(vocables);
                                
                switch (_selectionMode)
                {
                    case SelectedMode.Left:
                        TestVocables = new ObservableCollection<Vocabulary>(_lessionBusinessFactory.LeftOnlyMode(OriginalList, selectionMode));
                        break;
                    case SelectedMode.Random:
                        TestVocables = new ObservableCollection<Vocabulary>(_lessionBusinessFactory.RandomMode(OriginalList, selectionMode));
                        break;
                    case SelectedMode.Right:
                        TestVocables = new ObservableCollection<Vocabulary>(_lessionBusinessFactory.RightOnlyMode(OriginalList, selectionMode));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
