using PlayerManagerMVC2.Controller;
using PlayerManagerMVC2.View;

namespace PlayerManagerMVC2
{

    public class Program
    {
        private static void Main(string[] args)
        {
            // Create the view
            PlayerView view = new PlayerView();
            // Create the controller
            PlayerController controller = new PlayerController(view);
            // Start the controller
            controller.Start();
        }
    }
}
