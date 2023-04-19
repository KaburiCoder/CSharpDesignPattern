using System.Collections.Generic;

namespace WpfTutorials.DesignPattern.Models
{
  public interface IPersonRepository
  {
    IEnumerable<Person>? GetAll();
    bool SaveOne(Person person);
    bool DeleteOne(int id);
    bool Exist(int id);
  }
}