using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.DeleteDepotById;
internal sealed class DeleteDepotByIdCommandHandler(
    IDepotRepository depotRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepotByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepotByIdCommand request, CancellationToken cancellationToken)
    {
        Depot depot = await depotRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);
        if (depot == null)
        {
            return Result<string>.Failure("Güncellenecek kayıt bulunamadı");
        }

        depotRepository.Delete(depot);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Depot silme işlemi başarılı");
    }
}
