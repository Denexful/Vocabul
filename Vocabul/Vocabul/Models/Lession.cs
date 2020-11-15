
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Vocabul.Service;
using Xamarin.Forms;

namespace Vocabul.Models
{
    public class Lession: Modul, INotifyPropertyChanged
    {
        private bool _childVisibility;
        private ModulIDSetService _id;
        private string _modul_Name;
        public new string Modul_Name
        {
            get { return _modul_Name; }
            set
            {
                _modul_Name = value;
                OnPropertyChanged("Modul_Name");
            }
        }
        public ICommand CmdVisibility { get; private set; }
        public ObservableCollection<Vocabulary> Vocables { get; set; }
        public bool ChildVisibility
        {
            get { return _childVisibility; }
            set
            {
                _childVisibility = value;
                OnPropertyChanged("ChildVisibility");
            }
        } 
        public Lession(ObservableCollection<Lession> lessions = null) 
        {
            if (lessions != null)
            {
                _id = new ModulIDSetService();
               ModulID = _id.ReturnAvailableID(lessions);
            }
            Vocables = new ObservableCollection<Vocabulary>();
            ChildVisibility = true;
            CmdVisibility = new Command(ChangeVisibility);
         
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public void ChangeVisibility()
        {
            ChildVisibility = !ChildVisibility;
        }
        public Lession ShallowCopy()
        {
            return (Lession)this.MemberwiseClone();
        }
    }
}
