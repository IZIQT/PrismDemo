using System;
using System.Windows.Input;

namespace PrismDemo.Interface
{
    public interface ITab
    {
        string Name { get; set; }
        ICommand CloseCommand { get; set; }
        event EventHandler CloseRequested;
    }

    //public abstract class Tab : ITab
    //{
    //    public Tab()
    //    {
    //        CloseCommand = new DelegateCommand<object>(p => CloseRequested?.Invoke(this, EventArgs.Empty));
    //    }
    //    public string Name { get; set; }
    //    public ICommand CloseCommand { get; set; }
    //    public event EventHandler CloseRequested;
    //}
}
