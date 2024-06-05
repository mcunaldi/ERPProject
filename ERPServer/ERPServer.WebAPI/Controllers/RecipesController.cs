using ERPServer.Application.Features.Recipes.CreateRecipe;
using ERPServer.Application.Features.Recipes.DeleteRecipeById;
using ERPServer.Application.Features.Recipes.GetAllRecipe;
using ERPServer.Application.Features.Recipes.GetRecipeByIdWithDetails;
using ERPServer.WebAPI.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERPServer.WebAPI.Controllers;

public sealed class RecipesController : ApiController
{
    public RecipesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRecipeCommand request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllRecipeQuery request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteById(DeleteRecipeByIdCommand request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetRecipeByIdWithDetails(GetRecipeByIdWithDetailsQuery request, CancellationToken cancellation)
    {
        var response = await _mediator.Send(request, cancellation);
        return StatusCode(response.StatusCode, response);
    }
}
