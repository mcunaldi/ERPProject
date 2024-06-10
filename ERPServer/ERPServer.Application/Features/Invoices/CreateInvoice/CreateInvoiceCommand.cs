using ERPServer.Application.Features.Orders.CreateOrder;
using ERPServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Invoices.CreateInvoice;
public sealed record CreateInvoiceCommand(
    Guid CustomerId,
    int Type,
    DateOnly Date,
    string InvoiceNumber,
    List<OrderDetailDto> Details) : IRequest<Result<string>>;
