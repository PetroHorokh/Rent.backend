namespace Rent.console.Handles;

public class MenuHandle
{
    public delegate Task MenuDelegate();
    public static List<MenuDelegate> menuHandle;

    public static int MainMenuSelector;
    public static int PrevSelector;

    static MenuHandle()
    {
        menuHandle = new List<MenuDelegate>() { MainMenu, TenantMenu, ViewMenu, async () =>
        {
            Console.WriteLine("Bye! Take care!"); Program.Working = false; } };
        MainMenuSelector = 0;
        PrevSelector = 0;
    }

    private static async Task MainMenu()
    {
        PrevSelector = 0;
        Console.WriteLine("\nMain menu\n1.Tenant menu\n2.View menu\n3.Exit");
        
        Console.Write("Select an option: ");
        string input = Console.ReadLine()!;

        _ = int.TryParse(input, out int select);

        MainMenuSelector = select;
    }

    private static async Task TenantMenu()
    {
        PrevSelector = 1;
        Console.WriteLine(
            "\nTenant menu\n1.Get all tenants\n2.Get tenant by id\n3.Get tenant by name\n4.Get tenant address information\n5.Create tenant\n6.Update tenant\n7.Delete tenant\n8.Exit");

        Console.Write("Select an option: ");
        string input = Console.ReadLine()!;
        _ = int.TryParse(input, out int select);
        
        await TenantHandle.tenantHandle[select - 1]();
    }


    private static async Task ViewMenu()
    {
        PrevSelector = 2;
        Console.WriteLine(
            "\nView menu\n1.Certificate for tenant\n2.See room occupation in give date\n3.Get general information for tenant\n4.Exit");

        Console.Write("Select an option: ");
        string input = Console.ReadLine()!;
        _ = int.TryParse(input, out int select);

        await ViewHandle.viewHandle[select - 1]();
    }
        
}