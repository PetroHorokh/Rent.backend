using Microsoft.Extensions.DependencyInjection;
using Rent.BLL.Services;
using Rent.BLL.Services.Contracts;
using Rent.DAL.Models;

namespace Rent.console.Handles;

public class ViewHandle
{
    public delegate Task ViewHandleDelegate();
    public static List<ViewHandleDelegate> viewHandle;

    private static readonly IViewService viewService;

    static ViewHandle()
    {
        viewService = Program.Services.GetRequiredService<IViewService>();
        viewHandle = new List<ViewHandleDelegate>() { CertificateForTenant, RoomsWithTenants, TenantAssetPayment,
            async () => { MenuHandle.MainMenuSelector = 0; }
        };
    }

    private static async Task CertificateForTenant()
    {
        Console.Write("\nPlease enter required tenant id: ");
        string input = Console.ReadLine()!;

        if (Guid.TryParse(input, out Guid tenantId))
        {
            var results = (await viewService.GetCertificateForTenant(tenantId)).ToList();

            if (!results.Any()) Console.WriteLine($"There are no results for tenant {tenantId}");
            else
            {
                string output = "";
                foreach (var result in results)
                {
                    output += $"\nCertificate for tenant {tenantId}\n" + result;
                }
                Console.WriteLine(output);
            }
        }
        else
        {
            Console.WriteLine("\nWrong tenant id format");
        }
    }

    private static async Task RoomsWithTenants()
    {
        Console.Write("\nPlease enter date: ");
        string input = Console.ReadLine()!;

        if (DateTime.TryParse(input, out DateTime dateTime))
        {
            var results = (await viewService.GetRoomsWithTenants(dateTime)).ToList();

            if (!results.Any()) Console.WriteLine("There are no results for provided date");
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"\nRoom occupation on {dateTime.Date}\n" + result);
                }
            }
        }
        else
        {
            Console.WriteLine("\nWrong format for date time( try yyyy-MM-dd HH:mm:ss)");
        }
    }

    private static async Task TenantAssetPayment()
    {
        Console.Write("\nPlease enter required tenant id: ");
        string input = Console.ReadLine()!;

        if (Guid.TryParse(input, out Guid tenantId))
        {
            var results = (await viewService.GetTenantAssetPayment(tenantId)).ToList();

            if (!results.Any()) Console.WriteLine($"There are no results for tenant {tenantId}");
            else
            {
                string output = "";
                foreach (var result in results)
                {
                    output += $"\nGeneral information for tenant {tenantId}\n" + result;
                }
                Console.WriteLine(output);
            }
        }
        else
        {
            Console.WriteLine("\nWrong tenant id format");
        }
    }
}