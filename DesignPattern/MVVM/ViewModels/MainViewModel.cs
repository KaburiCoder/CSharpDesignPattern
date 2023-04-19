using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfTutorials.DesignPattern.Models;
using WpfTutorials.DesignPattern.MVVM.Commands;

namespace WpfTutorials.DesignPattern.MVVM.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private readonly IPersonRepository _personRepository;
    private string _id = "";
    private string _name = "";
    private string _sex = "";
    private string _age = "";
    private ObservableCollection<Person>? _people;

    private void Delete(object _)
    {
      int.TryParse(Id, out int id);
      if (_personRepository.DeleteOne(id))
      {
        Clear();
        DisplayListView();
      }
    }

    private bool CanDelete(object _)
    {
      int.TryParse(Id, out int id);
      return id > 0;
    }

    private void MouseLeftButtonUp(Person person)
    {
      Id = person.Id.ToString();
      Name = person.Name;
      Sex = person.Sex;
      Age = person.Age.ToString();
    }

    public MainViewModel(IPersonRepository personRepository)
    {
      _personRepository = personRepository;
      SaveCommand = new SaveCommand(this, personRepository);
      DeleteCommand = new RelayCommand<object>(Delete, CanDelete);
      CancelCommand = new RelayCommand<object>(_ => Clear());
      MouseLeftButtonUpCommand = new RelayCommand<Person>(MouseLeftButtonUp);
      DisplayListView();
    }

    public void Clear()
    {
      Id = "";
      Name = "";
      Sex = "";
      Age = "";
    }

    public void DisplayListView()
    {
      People = new ObservableCollection<Person>(_personRepository.GetAll()!);
    }

    public ICommand SaveCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand CancelCommand { get; set; }
    public ICommand MouseLeftButtonUpCommand { get; set; }


    public string Id
    {
      get => _id; set
      {
        _id = value;
        OnPropertyChanged();
      }
    }

    public string Name
    {
      get => _name; set
      {
        _name = value;
        OnPropertyChanged();
      }
    }

    public string Sex
    {
      get => _sex; set
      {
        _sex = value;
        OnPropertyChanged();
      }
    }

    public string Age
    {
      get => _age; set
      {
        _age = value;
        OnPropertyChanged();
      }
    }

    public ObservableCollection<Person>? People
    {
      get => _people; set
      {
        _people = value;
        OnPropertyChanged();
      }
    }
  }
}
