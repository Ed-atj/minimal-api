using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.Dto.Request.admin;
using MinimalApi.Domain.Dto.Request.login;
using MinimalApi.Domain.Dto.Response.admin;
using MinimalApi.Domain.Entities;
using MinimalApi.Interfaces.Admin;
using MinimalApi.mappers;

namespace MinimalApi.Endpoints;

public static class AdministradorEndpoint
{
    public static void MapAdministradorEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/administradores").WithTags("Administradores");
        var mapper = new AdminMapper();
        var logger = 

        group.MapPost("/login", (LoginDto loginDto, 
            IAdministradorServico administradorServico) =>
        {
            AdministradorLogado administrador = administradorServico.Login(loginDto);
            return Results.Ok(administrador);
        }).AllowAnonymous();

        group.MapGet("/", (IAdministradorServico administradorServico) =>
        {
            var administradores = administradorServico.Listar();
            return Results.Ok(administradores);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" });

        group.MapGet("/{id:int}", ([FromRoute] int id, IAdministradorServico administradorServico) =>
        {
            var administrador = administradorServico.BuscaPorId(id);
            if(administrador == null) return Results.NotFound();

            var administradorDto = mapper.ToAdministradorDto(administrador);
            return Results.Ok(administradorDto);
        })
        .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" });

        group.MapPost("/create",
            ([FromBody] AdministradorDto administradorDto, IAdministradorServico administradorServico) =>
            {
                Administrador administrador = mapper.ToAdministrador(administradorDto);
                Administrador newAdmin = administradorServico.Salvar(administrador);
                return Results.Created($"/create/{newAdmin.Id}", mapper.ToAdministradorDto(newAdmin));
            })
        .AllowAnonymous();
    }
}