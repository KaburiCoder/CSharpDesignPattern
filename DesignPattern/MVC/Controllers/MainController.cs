using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTutorials.DesignPattern.Models;
using WpfTutorials.DesignPattern.MVC.Views;

namespace WpfTutorials.DesignPattern.MVC.Controllers
{
  public class MainController
  {
    private readonly IMainView _view;
    private readonly IPersonRepository _personRepository;

    private Person GetPersonFromView()
    {
      return new Person
      {
        Id = _view.Id,
        Name = _view.Name,
        Sex = _view.Sex,
        Age = _view.Age,
      };
    }

    private void LoadPerson(Person? person = null)
    {
      _view.Id = person?.Id ?? 0;
      _view.Name = person?.Name ?? "";
      _view.Sex = person?.Sex ?? "";
      _view.Age = person?.Age ?? 0;
    }

    private bool IsValidSave(Person person)
    {
      if (person.Id <= 0)
      {
        return false;
      }

      if (string.IsNullOrEmpty(person.Name))
      {
        return false;
      }

      if (string.IsNullOrEmpty(person.Sex))
      {
        return false;
      }

      if (person.Age <= 0)
      {
        return false;
      }

      return true;
    }

    public MainController(IMainView view, IPersonRepository personRepository)
    {
      _view = view;
      _personRepository = personRepository;
      _view.SetController(this);
    }

    internal bool Save()
    {
      Person person = GetPersonFromView();
      if (!IsValidSave(person)) return false;

      return _personRepository.SaveOne(person);
    }  

    internal void LoadPerson(object dataContext)
    {
      LoadPerson(dataContext as Person);
    }

    internal void Cancel()
    {
      LoadPerson();     
    }

    internal void Display()
    {
      _view.ItemSource = _personRepository.GetAll()!;
    }

    internal bool Delete()
    {
      return _personRepository.DeleteOne(_view.Id);
    }
  }
}
