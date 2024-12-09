﻿using Allup.Domain.Entities;
using Allup.Persistence.Context;
using Allup.Persistence.Repositories.Abstraction;
using Core.Persistence.Repositories;

namespace Allup.Persistence.Repositories.Implementations;

public class BasketRepository : EfRepositoryBase<Basket, AppDbContext>, IBasketRepository
{
    public BasketRepository(AppDbContext context) : base(context)
    {
    }
}
