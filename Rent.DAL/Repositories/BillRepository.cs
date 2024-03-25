using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class BillRepository(RentContext context) : RepositoryBase<Bill>(context), IBillRepository
{
    public async Task CreateWithProcedure(BillToCreateDto bill)
    {
        await context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Bill_Insert] @TenantId = '{bill.TenantId}', @AssetId = '{bill.AssetId}', @Amount = {bill.BillAmount}, @EndDate = '{bill.EndDate}', @IssueDate = '{bill.IssueDate}'");
    }
}