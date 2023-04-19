using WpfTutorials.DesignPattern.Models;
using WpfTutorials.DesignPattern.MVVM.ViewModels;

namespace WpfTutorials.DesignPattern.MVVM.Commands
{
  public class SaveCommand : CommandBase
  {
    private MainViewModel _mainViewModel;
    private IPersonRepository _personRepository;

    private Person GetPerson()
    {
      Person? person = new Person()
      {
        Name = _mainViewModel.Name,
        Sex = _mainViewModel.Sex,
      };

      int.TryParse(_mainViewModel.Id, out int id);
      int.TryParse(_mainViewModel.Age, out int age);
      person.Id = id;
      person.Age = age;
      return person;
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

    private void Save()
    {
      Person? person = GetPerson();
      if (_personRepository.SaveOne(person))
      {
        _mainViewModel.Clear();
        _mainViewModel.DisplayListView();
      }
    }

    public SaveCommand(MainViewModel mainViewModel, IPersonRepository personRepository)
    {
      this._mainViewModel = mainViewModel;
      this._personRepository = personRepository;
    }

    public override bool CanExecute(object? parameter)
    {
      return IsValidSave(GetPerson());
    }

    public override void Execute(object? parameter)
    {
      Save();
    }
  }
}
