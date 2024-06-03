using ERPServer.Application.Features.Products.CreateProduct;
using ERPServer.Application.Features.Products.DeleteProductById;
using ERPServer.Application.Features.Products.GetAllProduct;
using ERPServer.Application.Features.Products.UpdateProduct;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers;

public sealed class ProductsController : ApiController
{
    public ProductsController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductCommand request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateProductCommand request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllProductQuery request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteProductByIdCommand request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }
}
