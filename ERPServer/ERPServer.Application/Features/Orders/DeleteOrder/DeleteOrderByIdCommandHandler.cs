﻿using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.DeleteOrder;

internal sealed class DeleteOrderByIdCommandHandler(
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteOrderByIdCommand request, CancellationToken cancellationToken)
    {
        Order order = await orderRepository.GetByExpressionWithTrackingAsync(p=> p.Id == request.Id, cancellationToken);
        if (order is null)
        {
            return Result<string>.Failure("Sipariş bulunamadı");
        }

        orderRepository.Delete(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Sipariş başarıyla silindi";
    }
}
