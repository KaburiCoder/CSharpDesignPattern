using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfTutorials.DesignPattern.Models;
using WpfTutorials.DesignPattern.MVP.Views;

namespace WpfTutorials.DesignPattern.MVP.Persenters
{
  public class MainPresenter
  {
    private readonly IMainView _view;
    private readonly IPersonRepository _personRepository;

    private void AddEvents()
    {
      _view.OnSave += OnSave;
      _view.OnDelete += OnDelete;
      _view.OnCancel += OnCancel;
      _view.OnListItemSelected += OnListItemSelected;
      _view.OnLoaded += OnLoaded;
    }

    private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
    {
      RefreshView();
    }

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

    private void SetPersonInfo(Person? person = null)
    {
      _view.Id = person?.Id ?? 0;
      _view.Name = person?.Name ?? "";
      _view.Sex = person?.Sex ?? "";
      _view.Age = person?.Age ?? 0;
    }

    private void OnListItemSelected(object obj)
    {
      SetPersonInfo(obj as Person);
    }

    private void OnCancel(object sender, System.Windows.RoutedEventArgs e)
    {
      SetPersonInfo();
    }

    private void OnDelete(object sender, System.Windows.RoutedEventArgs e)
    {
      if (_personRepository.DeleteOne(_view.Id))
      {
        RefreshView();
      }
    }

    private void OnSave(object sender, System.Windows.RoutedEventArgs e)
    {
      var person = GetPersonFromView();
      if (_personRepository.SaveOne(person))
      {
        RefreshView();
      }
    }

    private void RefreshView()
    {
      SetPersonInfo();
      _view.ItemSource = _personRepository.GetAll()!;
    }

    public MainPresenter(IMainView view, IPersonRepository personRepository)
    {
      this._view = view;
      this._personRepository = personRepository;
      AddEvents();
    }
  }
}
