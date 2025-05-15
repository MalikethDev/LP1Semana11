using PlayerManagerMVC.Controller;
using PlayerManagerMVC.View;

namespace PlayerManagerMVC
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
