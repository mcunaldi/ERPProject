using ERPServer.Domain.Entities;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;

internal sealed class OrderDetailRepository : Repository<OrderDetail, ApplicationDbContext>, IRepository<OrderDetail>
{
    public OrderDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}

