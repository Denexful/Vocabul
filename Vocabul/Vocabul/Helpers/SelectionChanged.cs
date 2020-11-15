using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabul.Helpers
{
    public class SelectionChanged
    {
        public static event EventHandler SelecedIDsChanged;
        public void CallChange(EventArgs e)
        {
            EventHandler handler = SelecedIDsChanged;
            if (handler != null)
            {
                handler.Invoke(this, e);
            }
        }
    }
}
