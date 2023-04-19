using System;
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

namespace WpfTutorials.DesignPattern.MVVM.Views
{
  /// <summary>
  /// MainView.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainView : Window
  {
    private void Txt_TextChanged(object sender, TextChangedEventArgs e)
    {
      BindingExpression be = ((TextBox)sender).GetBindingExpression(TextBox.TextProperty);
      be.UpdateSource();
    }

    public MainView()
    {
      InitializeComponent();
      txtId.TextChanged += Txt_TextChanged;
      txtName.TextChanged += Txt_TextChanged;
      txtSex.TextChanged += Txt_TextChanged;
      txtAge.TextChanged += Txt_TextChanged;
    }
  }
}
