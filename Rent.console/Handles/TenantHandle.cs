using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Rent.BLL.Services;
using Rent.BLL.Services.Contracts;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories;

namespace Rent.console.Handles;

public static class TenantHandle
{
    public delegate Task TenantHandleDelegate();
    public static List<TenantHandleDelegate> TenantMenu { get; set; }

    private static readonly ITenantService TenantService;

    static TenantHandle()
    {
        TenantService = Program.Services.GetRequiredService<ITenantService>();
        TenantMenu =
        [ 
            GetAllTenants, 
            GetTenantById, 
            GetTenantByName, 
            GetTenantAddressByTenantId, 
            CreateTenant, 
            UpdateTenant, 
            DeleteTenant,
            () => Task.Run(() => MenuHandle.MainMenuSelector = 0),
        ];
    }

    private static async Task GetAllTenants()
    {
        var tenants = (await TenantService.GetAllTenantsAsync()).ToList();

        if (tenants.IsNullOrEmpty()) Console.WriteLine("There are no tenants");
        else
        {
            foreach (var tenant in tenants)
            {
                Console.WriteLine(tenant);
            }
        }
    }

    private static async Task GetTenantById()
    {
        Console.Write("\nPlease enter required tenant id: ");
        string input = Console.ReadLine()!;

        if (Guid.TryParse(input, out Guid tenantId))
        {
            var tenant = await TenantService.GetTenantByIdAsync(tenantId);

            Console.WriteLine( tenant != null ?
                tenant:
                "\nThere is no such tenant");
        }
        else
        {
            Console.WriteLine("\nWrong tenant id format");
        }
    }

    private static async Task GetTenantByName()
    {
        Console.WriteLine("\nPlease enter required tenant name: ");
        string tenantName = Console.ReadLine()!;

        var tenant = await TenantService.GetTenantByNameAsync(tenantName);

        Console.WriteLine(tenant != null ?
                tenant:
                "\nThere is no such tenant");
    }

    private static async Task GetTenantAddressByTenantId()
    {
        Console.WriteLine("\nPlease enter required tenant id: ");
        string input = Console.ReadLine()!;

        if (Guid.TryParse(input, out Guid tenantId))
        {
            var address = await TenantService.GetTenantAddressByTenantIdAsync(tenantId);

            Console.WriteLine(address != null ?
                $"\n{input} tenant address information\n" + address :
                "\nThere is no such tenant");
        }
        else
        {
            Console.WriteLine("\nWrong tenant id format");
        }
    }

    private static async Task CreateTenant()
    {
        Console.WriteLine("\nCreation of new tenant");

        string name, bankName, description, director, input;
        Guid addressId;

        do
        {
            Console.Write("Enter tenant's name: ");
            name = Console.ReadLine()!;
        } while (name.Length == 0);

        do
        {
            Console.Write("Enter tenant's bank name: ");
            bankName = Console.ReadLine()!;
        } while (bankName.Length == 0);

        do
        {
            Console.Write("Enter address id: ");
            input = Console.ReadLine()!;
        } while (!Guid.TryParse(input, out addressId));

        do
        {
            Console.Write("Enter description: ");
            description = Console.ReadLine()!;
        } while (description.Length == 0);

        do
        {
            Console.Write("Enter director: ");
            director = Console.ReadLine()!;
        } while (director.Length == 0);

        var tenant = new TenantToCreateDto()
        {
            Name = name,
            BankName = bankName,
            Description = description,
            Director = director,
            AddressId = addressId
        };

        await TenantService.CreateTenant(tenant);
    }

    private static async Task DeleteTenant()
    {
        Console.Write("\nPlease enter required tenant id for deletion: ");
        string input = Console.ReadLine()!;

        if (Guid.TryParse(input, out Guid tenantId))
        {
            var tenant = await TenantService.GetTenantByIdAsync(tenantId);

            if(tenant != null) await TenantService.DeleteTenant(tenantId);
        }
        else
        {
            Console.WriteLine("\nWrong tenant id format");
        }
    }

    private static async Task UpdateTenant()
    {
        Console.Write("\nPlease enter required tenant id for update: ");
        string input = Console.ReadLine()!;

        if (Guid.TryParse(input, out Guid tenantId))
        {
            var tenant = await TenantService.GetTenantByIdAsync(tenantId);

            if (tenant != null)
            {
                Console.WriteLine("Tenant edit");

                var tenantName = tenant.Name;
                EditProp(ref tenantName, "name");
                tenant.Name = tenantName;

                var tenantBankName = tenant.BankName;
                EditProp(ref tenantBankName, "bank");
                tenant.BankName = tenantBankName;

                var tenantDirector = tenant.Director;
                EditProp(ref tenantDirector, "director");
                tenant.Director = tenantDirector;

                var tenantDescription = tenant.Description;
                EditProp(ref tenantDescription, "description");
                tenant.Description = tenantDescription;

                var tenantAddressId = tenant.AddressId;
                EditGuidProp(ref tenantAddressId, "address id");
                tenant.AddressId = tenantAddressId;

                var tenantToUpdate = new TenantToUpdateDto()
                {
                    TenantId = tenantId,
                    Name = tenant.Name,
                    BankName = tenant.BankName,
                    Director = tenant.Director,
                    Description = tenant.Description,
                    AddressId = tenantAddressId
                };

                await TenantService.UpdateTenant(tenantToUpdate);
            }
            else
            {
                Console.WriteLine("\nThere is no such tenant");
            }
        }
        else
        {
            Console.WriteLine("\nWrong tenant id format");
        }
    }

    private static bool EditDecision(string input) =>
        string.CompareOrdinal(input.ToLower(), "y") == 0 || string.CompareOrdinal(input.ToLower(), "n") == 0 ||
        string.CompareOrdinal(input.ToLower(), "yes") == 0 || string.CompareOrdinal(input.ToLower(), "no") == 0;

    private static bool EditConfirm(string input) => string.CompareOrdinal(input.ToLower(), "y") == 0 ||
                                                     string.CompareOrdinal(input.ToLower(), "yes") == 0;
    private static void EditProp(ref string prop, string propName)
    {
        string input;
        Console.WriteLine($"Old tenant {propName}: {prop}");

        do
        {
            Console.Write($"Would you like to change tenant's {propName}? (Y/N): ");
            input = Console.ReadLine()!;
        } while (!EditDecision(input));

        if (EditConfirm(input))
        {
            Console.Write($"New tenant {propName}: ");
            input = Console.ReadLine()!;
            prop = input;
        }
    }

    private static void EditGuidProp(ref Guid prop, string propName)
    {
        string input;
        Console.WriteLine($"Old tenant {propName}: {prop}");

        do
        {
            Console.Write($"Would you like to change tenant's {propName}? (Y/N): ");
            input = Console.ReadLine()!;
        } while (!EditDecision(input));

        if (EditConfirm(input))
        {
            Guid newProp;
            do
            {
                Console.Write($"New tenant {propName}: ");
                input = Console.ReadLine()!;
            } while (!Guid.TryParse(input, out newProp));
            prop = newProp;
        }
    }
}