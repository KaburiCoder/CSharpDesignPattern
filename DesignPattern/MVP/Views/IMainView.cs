using System;
using System.Collections;
using System.Windows;
using WpfTutorials.DesignPattern.MVC.Controllers;

namespace WpfTutorials.DesignPattern.MVP.Views
{
  public interface IMainView
  {
    event RoutedEventHandler OnLoaded;
    event RoutedEventHandler OnSave;
    event RoutedEventHandler OnDelete;
    event RoutedEventHandler OnCancel;
    event Action<object> OnListItemSelected;
    int Id { get; set; }
    string Name { get; set; }
    string Sex { get; set; }
    int Age { get; set; }
    IEnumerable ItemSource { get; set; }    
  }
}
