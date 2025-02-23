
using DocumentFormat.OpenXml.Presentation;
using lab4.Models;
using lab4.Services.Abstract;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;


namespace lab4.Services
{
    public class FileAnimalSender : IAnimalSender
    {
        private readonly IEnumerable<IAnimal> animals;
        private readonly IHostEnvironment environment;
        private readonly string format;

        public FileAnimalSender(IEnumerable<IAnimal> animals, IHostEnvironment environment, string format = "json")
        {
            this.animals = animals;
            this.environment = environment;
            this.format = format.ToLower();
        }

        public async Task GetAnimal(HttpContext context)
        {
            string filename = $"animals.{format}";
            string rootPath = environment.ContentRootPath;
            string fullname = Path.Combine(rootPath, filename);

            switch (format)
            {
                case "json":
                    string json = JsonSerializer.Serialize(animals);
                    await File.WriteAllTextAsync(fullname, json);
                    break;
                case "xml":
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IAnimal>), new XmlRootAttribute("Animals"));
                    using (StreamWriter writer = new StreamWriter(fullname))
                    {
                        xmlSerializer.Serialize(writer, animals.ToList());
                    }
                    break;
                case "txt":
                    using (StreamWriter writer = new StreamWriter(fullname))
                    {
                        foreach (var animal in animals)
                        {
                            writer.WriteLine($"Name: {animal.Name}, Sound: {animal.Sound}");
                        }
                    }
                    break;
            }
            await context.Response.SendFileAsync(fullname);
        }

        public List<IAnimal> LoadAnimals(string format)
        {
            string filename = $"animals.{format}";
            string rootPath = environment.ContentRootPath;
            string fullname = Path.Combine(rootPath, filename);
            if (!File.Exists(fullname)) return new List<IAnimal>();

            switch (format)
            {
                case "json":
                    string json = File.ReadAllText(fullname);
                    return JsonSerializer.Deserialize<List<IAnimal>>(json) ?? new List<IAnimal>();
                case "xml":
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<IAnimal>), new XmlRootAttribute("Animals"));
                    using (StreamReader reader = new StreamReader(fullname))
                    {
                        return (List<IAnimal>)(xmlSerializer.Deserialize(reader) ?? new List<IAnimal>());
                    }
                case "txt":
                    List<IAnimal> animalsList = new List<IAnimal>();
                    foreach (var line in File.ReadAllLines(fullname))
                    {
                        var parts = line.Split(", ");
                        if (parts.Length == 2)
                        {
                            animalsList.Add(new Cat { Name = parts[0].Split(": ")[1], Sound = parts[1].Split(": ")[1] });
                        }
                    }
                    return animalsList;
            }
            return new List<IAnimal>();
        }
    }
}



