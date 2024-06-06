using ERPServer.Domain.Entities;
using ERPServer.Infrastructure.Context;
using GenericRepository;

namespace ERPServer.Infrastructure.Repositories;
internal sealed class OrderRepository : Repository<Order, ApplicationDbContext>, IRepository<Order>
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }
}

