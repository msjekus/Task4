using lab4.Services.Abstract;
namespace lab4.Models
{
    public class Dog : IAnimal
    {
        public string Name { get; init; }

        public string Sound { get; init; }

        public Dog()
        {
            Name = "Dog";
            Sound = "Gav";
        }
    }
}
