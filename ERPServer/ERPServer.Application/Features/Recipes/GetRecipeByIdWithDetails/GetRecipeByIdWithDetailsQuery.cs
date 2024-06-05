using ERPServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Recipes.GetRecipeByIdWithDetails;
public sealed record GetRecipeByIdWithDetailsQuery(
    Guid Id) : IRequest<Result<Recipe>>;
