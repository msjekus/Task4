
using lab4.Services.Abstract;

namespace lab4.Services
{
    public class FileAnimalSender : IAnimalSender
    {
        private readonly IAnimal animal;
        private readonly IHostEnvironment environment;
        public FileAnimalSender(IAnimal animal, IHostEnvironment environment)
        {
            this.animal = animal;
            this.environment = environment;
        }

        public async Task GetAnimal(HttpContext context)
        {
            
            string filename = $"{animal.Name}.txt";
            string rootPath = environment.ContentRootPath;
            string fullname = Path.Combine(rootPath, filename);
            using FileStream fs = File.Create(fullname);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.WriteLine($"Name: {animal.Name}");
                writer.WriteLine($"Sound: {animal.Sound}");
            }
            //context.Response.Headers.ContentDisposition = $"attachment; filename={filename}";
            await context.Response.SendFileAsync(fullname);
        }
    } 
}
