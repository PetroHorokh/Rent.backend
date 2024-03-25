using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories.Contracts;

public interface ITenantRepository : IRepositoryBase<Tenant>
{
    Task CreateWithProcedure(TenantToCreateDto tenant);
}