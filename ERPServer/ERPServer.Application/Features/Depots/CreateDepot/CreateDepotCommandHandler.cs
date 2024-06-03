using AutoMapper;
using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Depots.CreateDepot;
internal sealed class CreateDepotCommandHandler(
    IDepotRepository depotRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateDepotCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateDepotCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await depotRepository.AnyAsync(p=> p.Name == request.Name, cancellationToken);
        if (isNameExists)
        {
            return Result<string>.Failure("Bu depo ismi daha önce kullanılmış");
        }

        Depot depot = mapper.Map<Depot>(request);
        await depotRepository.AddAsync(depot, cancellationToken);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Succeed("Depo kayıt işlemi başarılı");

    }
}
