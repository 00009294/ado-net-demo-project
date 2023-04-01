using AdoNetDemoProject.Interfaces.Repositories;
using AdoNetDemoProject.Models;
using AdoNetDemoProject.Repositories;

class Program
{
    public static async Task Main(string[] args)
    {
        IPersonRepository personRepository = new PersonRepository();
        Person person = new Person()
        {
            Name = "Farhod",
            Address = "Qarshi",
            PhoneNumber = "987521463"
        };
        
        var result = await personRepository.DeleteAsync(4);
        
            Console.WriteLine(result);
        
    }
}