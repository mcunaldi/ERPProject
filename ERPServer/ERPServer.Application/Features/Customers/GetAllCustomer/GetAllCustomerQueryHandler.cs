﻿using ERPServer.Domain.Entities;
using ERPServer.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ERPServer.Application.Features.Customers.GetAllCustomer;

internal sealed class GetAllCustomerQueryHandler(
    ICustomerRepository customerRepository) : IRequestHandler<GetAllCustomerQuery, Result<List<Customer>>>
{
    async Task<Result<List<Customer>>> IRequestHandler<GetAllCustomerQuery, Result<List<Customer>>>.Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
    {
        List<Customer> customers = await customerRepository.GetAll().OrderBy(p => p.Name).ToListAsync(cancellationToken);

        return customers;
    }
}