using ERPServer.Domain.Dtos;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Enums;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Orders.ReqirementsPlanningByOrderId;

internal sealed class RequirementsPlanningByOrderIdCommandHandler(
    IOrderRepository orderRepository,
    IStockMovementRepository stockMovementRepository,
    IRecipeRepository recipeRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RequirementsPlanningByOrderIdCommand, Result<RequirementsPlanningByOrderIdResponse>>
{
    public async Task<Result<RequirementsPlanningByOrderIdResponse>> Handle(RequirementsPlanningByOrderIdCommand request, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository
            .Where(p => p.Id == request.OrderId)
            .Include(p => p.Details!)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(cancellationToken);
        if (order is null)
        {
            return Result<RequirementsPlanningByOrderIdResponse>.Failure("Sipariş bulunamadı");
        }


        List<ProductDto> uretilmesiGerekenUrunListesi = new();
        List<ProductDto> requirementsPlanningProducts = new();

        if (order.Details is not null)
        {
            foreach (var item in order.Details)
            {
                var product = item.Product;
                List<StockMovement> movements =
                    await stockMovementRepository
                    .Where(p => p.ProductId == product!.Id)
                    .ToListAsync(cancellationToken);

                decimal stock = movements.Sum(p => p.NumberOfEntries) - movements.Sum(p => p.NumberOfOutputs);

                if (stock < item.Quantity)
                {
                    ProductDto uretilmesiGerekenUrun = new()
                    {
                        Id = item.ProductId,
                        Name = product!.Name,
                        Quantity = item.Quantity - stock,
                    };

                    uretilmesiGerekenUrunListesi.Add(uretilmesiGerekenUrun);
                }
            }

            foreach (var item in uretilmesiGerekenUrunListesi)
            {
                Recipe? recipe =
                    await recipeRepository
                    .Where(p => p.ProductId == item.Id)
                    .Include(p => p.Details!)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefaultAsync(cancellationToken);

                if (recipe is not null && recipe.Details is not null)
                {
                    foreach (var detail in recipe.Details)
                    {
                        List<StockMovement> urunMovements =
                                 await stockMovementRepository
                                 .Where(p => p.ProductId == detail.ProductId)
                                 .ToListAsync(cancellationToken);

                        decimal stock = urunMovements.Sum(p => p.NumberOfEntries) - urunMovements.Sum(p => p.NumberOfOutputs);

                        if (stock < detail.Quantity)
                        {
                            ProductDto ihtiyacOlanUrun = new()
                            {
                                Id = detail.ProductId,
                                Name = detail.Product!.Name,
                                Quantity = detail.Quantity - stock,
                            };

                            requirementsPlanningProducts.Add(ihtiyacOlanUrun);
                        }
                    }

                }
            }
        }

        requirementsPlanningProducts = requirementsPlanningProducts
            .GroupBy(p => p.Id)
            .Select(g => new ProductDto
            {
                Id = g.Key,
                Name = g.First().Name,
                Quantity = g.Sum(item => item.Quantity)
            }).ToList();

        order.Status = OrderStatusEnum.RequirementPlanWorked;
        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);    

        return new RequirementsPlanningByOrderIdResponse(
            DateOnly.FromDateTime(DateTime.Now), 
            order.Number + "Nolu Sipariş ihtiyaç planlaması", 
            new(requirementsPlanningProducts));
    }
}
