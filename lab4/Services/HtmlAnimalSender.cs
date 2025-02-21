using lab4.Services.Abstract;
using System.Text;

namespace lab4.Services
{
    public class HtmlAnimalSender : IAnimalSender
    {
        private readonly IAnimal animal;
        public HtmlAnimalSender(IAnimal animal) 
        {
            this.animal = animal;
        }

        public async Task GetAnimal(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<h1 style='text-align:center'>Animal info: </h1>");
            sb.Append($"<h2>Name: {animal.Name}</h1>");
            sb.Append($"<h2>Sound: {animal.Sound}</h2>");
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync(sb.ToString());
        }
    }
}
