namespace Rent.console.Handles;

public class MenuHandle
{
    public delegate Task MenuDelegate();
    public static readonly List<MenuDelegate> menuHandle;

    public static int MainMenuSelector { get; set; }
    public static int PrevSelector { get; set; }

    static MenuHandle()
    {
        menuHandle = [
            MainMenu,
            TenantMenu,
            RoomMenu,
            ViewMenu,
            () => Task.Run(() =>
            {
                Program.Working = false;
                Console.WriteLine("Bye! Take care!");
            })
        ];
        MainMenuSelector = 0;
        PrevSelector = 0;
    }

    private static Task MainMenu()
    {
        PrevSelector = 0;
        Console.WriteLine("\nMain menu\n1.Tenant menu\n2.Room menu\n3.View menu\n4.Exit");
        
        Console.Write("\nSelect an option: ");
        string input = Console.ReadLine()!;

        _ = int.TryParse(input, out int select);

        MainMenuSelector = select;
        return Task.CompletedTask;
    }

    private static async Task TenantMenu()
    {
        PrevSelector = 1;
        Console.WriteLine(
            "\nTenant menu\n1.Get all tenants\n2.Get tenant by id\n3.Get tenant by name\n4.Get tenant address information\n5.Create tenant\n6.Update tenant\n7.Delete tenant\n8.Exit");

        Console.Write("\nSelect an option: ");
        string input = Console.ReadLine()!;
        _ = int.TryParse(input, out int select);
        
        await TenantHandle.TenantMenu[select - 1]();
    }

    private static async Task RoomMenu()
    {
        PrevSelector = 2;
        Console.WriteLine(
            "\nRoom menu" +
            "\n1.Get all rooms" +
            "\n2.Get all room types" +
            "\n3.Get all accommodations" +
            "\n4.Get room by room id" +
            "\n5.Get room by room number" +
            "\n6.Get room's accommodations" +
            "\n7.Create room" +
            "\n8.Add accommodation to a room" +
            "\n9.Change quantity of accommodation for room" +
            "\n10.Delete room" +
            "\n11.Exit");

        Console.Write("\nSelect an option: ");
        string input = Console.ReadLine()!;
        _ = int.TryParse(input, out int select);

        await RoomHandle.RoomMenu[select - 1]();
    }

    private static async Task ViewMenu()
    {
        PrevSelector = 3;
        Console.WriteLine(
            "\nView menu\n1.Certificate for tenant\n2.See room occupation in give date\n3.Get general information for tenant\n4.Exit");

        Console.Write("\nSelect an option: ");
        string input = Console.ReadLine()!;
        _ = int.TryParse(input, out int select);

        await ViewHandle.ViewMenu[select - 1]();
    }
}