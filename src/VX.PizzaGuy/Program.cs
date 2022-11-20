using VX.PizzaGuy.SpeechEngine;

namespace VX.PizzaGuy
{
    public class Program
    {
        async static Task Main(string[] args)
        {
            var engine = new ChatBot();
            await engine.Start();
        }
    }
}
