using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;
using System;
using System.Net;
using Dapper;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;
using Microsoft.Extensions.Logging;

namespace Rent.DAL.Repositories;

public class TenantRepository(RentContext context, IConfiguration configuration, ILogger<TenantRepository> logger) : RepositoryBase<Tenant>(context), ITenantRepository
{
    private readonly ILogger<TenantRepository> _logger = logger;

    public async Task CreateWithProcedure(TenantToCreateDto tenant)
    {
        _logger.LogInformation("Entering TenantRepository, method CreateWithProcedure");

        await using var connection = new SqlConnection(configuration["ConnectionStrings:RentDatabase"]);
        await connection.OpenAsync();

        var storedProcedureName = "sp_Tenant_Insert";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("AddressId", tenant.AddressId);
        parameters.Add("Name", tenant.Name);
        parameters.Add("BankName", tenant.BankName);
        parameters.Add("Director", tenant.Director);
        parameters.Add("Description", tenant.Description);

        _logger.LogInformation("Querying 'sp_Tenant_Insert' stored procedures");
        _logger.LogInformation($"Parameters: @AddressId = {tenant.AddressId}, @Name = {tenant.Name}, @BankName = {tenant.BankName}, @Director = {tenant.Director}, @Description = {tenant.Description}");
        await connection.QueryAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
        _logger.LogInformation("Queried stored procedure successfully");

        await connection.CloseAsync();

        _logger.LogInformation("Leaving TenantRepository, method CreateWithProcedure");
    }
}