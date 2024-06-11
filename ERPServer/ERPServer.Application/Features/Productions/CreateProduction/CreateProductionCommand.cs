using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Productions.CreateProduction;
public sealed record CreateProductionCommand(
    Guid ProductId,
    Guid DepotId,
    decimal Quantity) : IRequest<Result<string>>;

internal sealed class CreateProductionCommandHandler(
    IProductionRepository productionRepository,
    IRecipeRepository recipeRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfwork,
    IMapper mapper) : IRequestHandler<CreateProductionCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateProductionCommand request, CancellationToken cancellationToken)
    {

        Production production = mapper.Map<Production>(request);

        Recipe? recipe = 
            await recipeRepository
            .Where(p => p.ProductId == request.ProductId)
            .Include(p => p.Details!)
            .ThenInclude(p=> p.Product)
            .FirstOrDefaultAsync(cancellationToken);

        if (recipe is not null && recipe.Details is not null)
        {
            var details = recipe.Details;

            foreach (var item in details)
            {
                List<StockMovement> movements = await stockMovementRepository.Where(p => p.ProductId == item.ProductId).ToListAsync(cancellationToken);

                List<Guid> depotIds = movements.GroupBy(p => p.DepotId)
                    .Select(g => g.Key)
                    .ToList();

                decimal stock = movements.Sum(p => p.NumberOfEntries) - movements.Sum(p => p.NumberOfOutputs);

                if(item.Quantity >  stock)
                {
                    return Result<string>
                        .Failure(item.Product!.Name + " ürünündenm üretim için yeterli miktar yok. Eksik miktar: " + (item.Quantity - stock));
                }

                foreach (var depotId in depotIds)
                {
                    decimal quantity = movements.Where(p => p.DepotId == depotId).Sum(s => s.NumberOfEntries - s.NumberOfOutputs);


                    StockMovement stockMovement = new()
                    {
                        ProductionId = production.Id,
                        DepotId = depotId
                    };

                    if (item.Quantity <= quantity)
                    {
                    }
                    else
                    {

                    }
                }

            }
        }


        await productionRepository.AddAsync(production);
        await unitOfwork.SaveChangesAsync(cancellationToken);

        return "Ürün başarıyla üretildi";
    }
}
