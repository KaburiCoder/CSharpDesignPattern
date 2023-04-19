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
using WpfTutorials.DesignPattern.MVC.Controllers;

namespace WpfTutorials.DesignPattern.MVC.Views
{
  /// <summary>
  /// MainView.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainView : Window, IMainView
  {
    private MainController _controller = default!;  

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
      if (_controller.Save())
      {
        _controller.Cancel();
        _controller.Display();
      }
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
      if (_controller.Delete())
      {
        _controller.Cancel();
        _controller.Display();
      }
    }

    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
      _controller.Cancel();
    }

    private void ListViewItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
      FrameworkElement? element = (FrameworkElement)e.OriginalSource;
      _controller.LoadPerson(element.DataContext);       
    }

    private void MainView_Loaded(object sender, RoutedEventArgs e)
    {
      _controller.Display();
    }

    public MainView()
    {
      InitializeComponent();
      Loaded += MainView_Loaded;
    }
       
    public int Id { get
      {
        int.TryParse(txtId.Text, out int value);
        return value; 
      } set
      {
        txtId.Text = value == 0 ? "" : value.ToString();
      }
    }

    string IMainView.Name { get => txtName.Text.Trim(); set => txtName.Text =value; }

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

    public void SetController(MainController controller)
    {
      _controller = controller;
    }   
  }
}
