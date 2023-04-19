using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfTutorials.DesignPattern.MVP.Views
{
  /// <summary>
  /// MainView.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainView : Window, IMainView
  {
    public event RoutedEventHandler? OnSave;
    public event RoutedEventHandler? OnDelete;
    public event RoutedEventHandler? OnCancel;
    public event Action<object>? OnListItemSelected;
    public event RoutedEventHandler? OnLoaded;

    private bool IsValidSave()
    {
      if (Id <= 0)
      {
        return false;
      }

      if (string.IsNullOrEmpty(Name))
      {
        return false;
      }

      if (string.IsNullOrEmpty(Sex))
      {
        return false;
      }

      if (Age <= 0)
      {
        return false;
      }

      return true;
    }

    private bool IsValidDelete()
    {
      if (Id <= 0)
      {
        return false;
      }

      return true;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      if (IsValidSave())
      {
        OnSave?.Invoke(sender, e);
      }
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
      if (IsValidDelete())
      {
        OnDelete?.Invoke(sender, e);
      }
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
      OnCancel?.Invoke(sender, e);
    }

    private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {      
      FrameworkElement element = (FrameworkElement)sender;
      OnListItemSelected?.Invoke(element.DataContext);
    }

    public MainView()
    {
      InitializeComponent();

      Loaded += (s, e) => OnLoaded?.Invoke(s, e);
    }

    public int Id
    {
      get
      {
        int.TryParse(txtId.Text, out int value);
        return value;
      }
      set
      {
        txtId.Text = value == 0 ? "" : value.ToString();
      }
    }

    public new string Name { get => txtName.Text.Trim(); set => txtName.Text = value; }

    public string Sex { get => txtSex.Text.Trim(); set => txtSex.Text = value; }

    public int Age
    {
      get
      {
        int.TryParse(txtAge.Text, out int value);
        return value;
      }
      set
      {
        txtAge.Text = value == 0 ? "" : value.ToString();
      }
    }

    public IEnumerable ItemSource { get => lstView.ItemsSource; set => lstView.ItemsSource = value; }
  }
}
