using lab4.Services.Abstract;
namespace lab4.Models
{
    public class Cat : IAnimal
    {
       
        public string Name { get; init; }

        public string Sound { get; init; }

        public Cat()
        {
            Name = "Кіт";
            Sound = "Мяу";
            
        }
    }
}
