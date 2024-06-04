﻿using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Recipes.GetAllRecipe;
public sealed record GetAllRecipeQuery() : IRequest<Result<List<Recipe>>>;

internal sealed class GetAllRecipeQueryHandler(
    IRecipeRepository recipeRepository) : IRequestHandler<GetAllRecipeQuery, Result<List<Recipe>>>
{
    public async Task<Result<List<Recipe>>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
    {
        List<Recipe> recipes = await recipeRepository
            .GetAll()
            .Include(p=> p.Product)
            .OrderBy(p=> p.Product!.Name)
            .ToListAsync();
        return recipes;
    }

}
