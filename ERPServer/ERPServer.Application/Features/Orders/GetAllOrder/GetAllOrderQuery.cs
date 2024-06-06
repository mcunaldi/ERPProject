using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.GetAllOrder;
public sealed record GetAllOrderQuery() : IRequest<Result<List<Order>>>;

internal sealed class GetAllOrderQueryHandler(
    IOrderRepository orderRepository) : IRequestHandler<GetAllOrderQuery, Result<List<Order>>>
{
    public async Task<Result<List<Order>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        List<Order> orders = await orderRepository
            .GetAll()
            .Include(p=> p.Customer)
            .Include(p=> p.Details!)
            .ThenInclude(p=> p.Product)
            .OrderByDescending(p=> p.Date)
            .ToListAsync(cancellationToken);

        return orders;
    }
}
