using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfTutorials.Properties;

namespace WpfTutorials.DesignPattern.Models
{
  public class PersonRepository : IPersonRepository
  {
    private void SaveSettings(IEnumerable<Person> people)
    {
      string jsonData = JsonSerializer.Serialize(people);
      Settings settings = new Settings();
      settings.PeopleJson = jsonData;
      settings.Save();
    }

    private List<Person> GetPeople()
    {
      IEnumerable<Person>? people = GetAll();

      List<Person> list;
      if (people == null)
      {
        list = new List<Person>();
      }
      else
      {
        list = people.ToList();
      }
      return list;
    }

    public IEnumerable<Person>? GetAll()
    {
      string jsonData = new Settings().PeopleJson;
      return string.IsNullOrWhiteSpace(jsonData) 
        ? null 
        : JsonSerializer.Deserialize<IEnumerable<Person>>(jsonData);
    }

    public bool SaveOne(Person person)
    {
      try
      {
        List<Person> people = GetPeople();
        Person? findPerson = people.Find(p => p.Id == person.Id);
        if (findPerson != null)
        {
          findPerson.Update(person);
        }
        else
        {
          people.Add(person);
        }
        SaveSettings(people);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }     

      return true;
    }

    public bool DeleteOne(int id)
    {
      try
      {
        List<Person> people = GetPeople();
        Person? findPerson = people.Find(p => p.Id == id);

        if (findPerson == null) return false;

        people.Remove(findPerson);
        SaveSettings(people);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message);
        return false;
      }
    
      return true;
    }

    public bool Exist(int id) => GetPeople().Exists(p => p.Id == id);       
  }
}
