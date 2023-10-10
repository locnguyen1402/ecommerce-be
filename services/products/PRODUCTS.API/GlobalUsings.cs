global using Microsoft.IdentityModel.Tokens;

global using AutoMapper;
global using MediatR;

global using FluentValidation;

global using Microsoft.AspNetCore.Http.HttpResults;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;

global using ECommerce.Products.Api.Application.Queries;
global using ECommerce.Products.Api.Application.Responses;
global using ECommerce.Products.Api.Application.Requests;

global using ECommerce.Products.Domain.AggregatesModels;

global using ECommerce.Products.Infrastructure.DbContexts;
global using ECommerce.Products.Infrastructure.Repositories;

global using ECommerce.Shared.Common.Controllers;
global using ECommerce.Shared.Common.Domain.AggregatesModels;
global using ECommerce.Shared.Common.AppSettings;
global using ECommerce.Shared.Common.Domain.Queries;
global using ECommerce.Shared.Common.Exceptions;
global using ECommerce.Shared.Common.Middlewares;

global using ECommerce.Shared.Libs.Extensions;

global using ECommerce.Shared.Integration.Extensions;
global using ECommerce.Shared.Integration.RestClients;
global using ECommerce.Shared.Integration.Application.Queries;
