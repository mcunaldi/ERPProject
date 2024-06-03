using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.UpdateDepot;

internal sealed class UpdateDepotCommandHandler(
    IDepotRepository depotRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateDepotCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateDepotCommand request, CancellationToken cancellationToken)
    {
        Depot depot = await depotRepository.GetByExpressionWithTrackingAsync(p=> p.Id == request.Id, cancellationToken);
        if (depot == null)
        {
            return Result<string>.Failure("Güncellenecek kayıt bulunamadı");
        }

        if(request.Name.ToLower() == depot.Name.ToLower())
        {
            bool nameIsUnique = await depotRepository.AnyAsync(p => p.Name.ToLower() == request.Name.ToLower(), cancellationToken);
            if (nameIsUnique)
            {
                return Result<string>.Failure("Bu depo ismi daha önce kullanılmış");
            }
        }

        mapper.Map(request, depot);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Depo güncelleme işlemi başarılı.");
    }
}
