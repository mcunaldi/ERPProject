﻿using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;
internal sealed class StockMovementRepository : Repository<StockMovement, ApplicationDbContext>, IStockMovementRepository
{
    public StockMovementRepository(ApplicationDbContext context) : base(context)
    {
    }
}
