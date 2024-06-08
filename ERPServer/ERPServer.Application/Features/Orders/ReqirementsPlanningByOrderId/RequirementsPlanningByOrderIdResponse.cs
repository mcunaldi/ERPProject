using ERPServer.Domain.Dtos;

namespace ERPServer.Application.Features.Orders.ReqirementsPlanningByOrderId;

public sealed record RequirementsPlanningByOrderIdResponse(
    DateOnly Date,
    string Title,
    List<ProductDto> Products);
