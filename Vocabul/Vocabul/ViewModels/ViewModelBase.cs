using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Vocabul.Classes;

namespace Vocabul.ViewModels
{
    public abstract class ViewModelBase : ObservableProperty
    {
        public Dictionary<string, ICommand> Commands { get; protected set; }
        public ViewModelBase()
        {
            Commands = new Dictionary<string, ICommand>();
        }
    }
}
