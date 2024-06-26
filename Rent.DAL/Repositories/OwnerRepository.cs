﻿using Microsoft.EntityFrameworkCore;
using Rent.DAL.DTO;
using Rent.DAL.Models;
using Rent.DAL.Repositories.Contracts;
using Rent.DAL.RepositoryBase;

namespace Rent.DAL.Repositories;

public class OwnerRepository(RentContext context) :RepositoryBase<Owner>(context), IOwnerRepository
{
    public async Task CreateWithProcedure(OwnerToCreateDto owner)
    {
        await Context.Database.ExecuteSqlAsync($"EXEC [dbo].[sp_Owner_Insert] @AddressId = '{owner.AddressId}', @Name = '{owner.Name}', , @CreatedBy = '{owner.CreatedBy}'");
    }
}