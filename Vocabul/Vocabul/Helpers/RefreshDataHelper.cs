using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabul.Helpers
{
    public class RefreshDataHelper
    { 
        public static event EventHandler DataChanged;
        public void CallChange(EventArgs e, string FROM)
        {
            EventHandler handler = DataChanged;
            if (handler != null)
            {
                handler.Invoke(this,e);
            }
        }
    }
}
