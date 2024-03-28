using Microsoft.Extensions.DependencyInjection;
using Rent.BLL;
using Rent.console.Handles;

namespace Rent.console;

internal class Program
{
    public static bool Working = true;

    public static ServiceProvider Services = BllServiceProvider.ServiceConfiguration();

    private static async Task Main(string[] args)
    {
        do
        {
            try
            {
                await MenuHandle.menuHandle[MenuHandle.MainMenuSelector]();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong! Try again");
                MenuHandle.MainMenuSelector = MenuHandle.PrevSelector;
            }
        } while (Working);
    }
}