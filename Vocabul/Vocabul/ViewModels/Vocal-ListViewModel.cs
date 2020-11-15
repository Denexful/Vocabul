using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Vocabul.Classes;
using Vocabul.Helpers;
using Vocabul.Models;
using Vocabul.Service;
using Xamarin.Forms;

namespace Vocabul.ViewModels
{
    public class Vocal_ListViewModel : ViewModelBase
    {
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
        public Vocal_ListViewModel()
        {       
            ListViewLession = new List<Lession>(StoreList.Lessions.Select(x => x.ShallowCopy()));
            RefreshDataHelper.DataChanged += RefreshData;
        }
        public void RefreshData(object sender, EventArgs e)
        {
            ListViewLession = new List<Lession>(StoreList.Lessions.Select(x => x.ShallowCopy()));
        }
    }
}
