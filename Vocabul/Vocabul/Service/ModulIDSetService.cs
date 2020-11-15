using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Vocabul.Models;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Vocabul.Service
{
    public class ModulIDSetService
    {
        private int returnValue;

        public ModulIDSetService()
        {

        }
        public int ReturnAvailableID(ObservableCollection<Lession> LessionCollection)
        {
            returnValue = 0;
            var SortedList = LessionCollection.OrderBy(x => x.ModulID).ToList();
            returnValue = SortedList[SortedList.Count - 1].ModulID + 1;
            return returnValue;
        }
    }
}
