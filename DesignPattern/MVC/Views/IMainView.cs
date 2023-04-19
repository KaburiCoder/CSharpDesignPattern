using System.Collections;
using WpfTutorials.DesignPattern.MVC.Controllers;

namespace WpfTutorials.DesignPattern.MVC.Views
{
  public interface IMainView
  {
    int Id { get; set; }
    string Name { get; set; }
    string Sex { get; set; }
    int Age { get; set; }
    IEnumerable ItemSource { get; set; }
    void SetController(MainController controller);
  }
}
