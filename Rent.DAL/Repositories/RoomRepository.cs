using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;
using System.Data;
using System.Net;

namespace Rent.DAL.Repositories;

public class RoomRepository(RentContext context, IConfiguration configuration, ILogger<TenantRepository> logger) : RepositoryBase<Room>(context), IRoomRepository
{
    public async Task<RepositoryResponseDto> CreateWithProcedure(RoomToCreateDto room)
    {
        logger.LogInformation("Entering RoomRepository, method CreateWithProcedure");

        SqlException? error = null;

        await using var connection = new SqlConnection(configuration["ConnectionStrings:RentDatabase"]);
        await connection.OpenAsync();

        var storedProcedureName = "sp_Room_Insert";

        DynamicParameters parameters = new ();
        parameters.Add("Number", room.Number);
        parameters.Add("Area", room.Area);
        parameters.Add("RoomTypeId", room.RoomTypeId);
        try
        {
            logger.LogInformation("Querying 'sp_Room_Insert' stored procedures");
            logger.LogInformation($"Parameters: @Number = {room.Number}, @Area = {room.Area}, @RoomTypeId = {room.RoomTypeId}");
            var response = await connection.QueryAsync(storedProcedureName, parameters, commandType: CommandType.StoredProcedure);
            logger.LogInformation("Queried stored procedure successfully");
        }
        catch (SqlException ex)
        {
            logger.LogInformation($"An error occured while inserting room entity: {ex.Message}");
            error = ex;
        }
        await connection.CloseAsync();

        logger.LogInformation("Leaving RoomRepository, method CreateWithProcedure");

        return new RepositoryResponseDto() { DateTime = DateTime.Now, Error = error };
    }
}