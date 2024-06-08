using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Orders.ReqirementsPlanningByOrderId;
public sealed record RequirementsPlanningByOrderIdCommand(
    Guid OrderId) : IRequest<Result<RequirementsPlanningByOrderIdResponse>>;
