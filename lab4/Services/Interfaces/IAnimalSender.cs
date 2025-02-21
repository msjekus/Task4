namespace lab4.Services.Abstract
{
    public interface IAnimalSender
    {
        Task GetAnimal(HttpContext context);
    }
}
