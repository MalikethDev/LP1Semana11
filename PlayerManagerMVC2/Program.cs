using PlayerManagerMVC2.Controller;
using PlayerManagerMVC2.View;

namespace PlayerManagerMVC2
{

    public class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Please provide the filename as a command line argument.");
                return;
            }
            string filename = args[0];
            PlayerView view = new PlayerView();
            PlayerController controller = new PlayerController(view, filename);
            controller.Start();
        }
    }
}
