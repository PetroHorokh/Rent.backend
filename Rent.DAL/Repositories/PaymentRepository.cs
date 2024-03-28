using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class PaymentRepository(RentContext context) : RepositoryBase<Payment>(context), IPaymentRepository
{
    public async Task CreateWithProcedure(PaymentToCreateDto payment)
    {
        await Context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Payment_Insert] @TenantId = '{payment.TenantId}', @BillId = '{payment.BillId}', @PaymentDay = '{payment.PaymentDay}', @Amount = '{payment.Amount}', @CreatedBy = '{payment.CreatedBy}'");
    }
}