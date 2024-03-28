using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface IPaymentRepository : IRepositoryBase<Payment>
{
    Task CreateWithProcedure(PaymentToCreateDto payment);
}