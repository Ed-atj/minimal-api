using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.Domain.Dto.Request.veiculo;
using MinimalApi.Interfaces.Veiculo;
using MinimalApi.mappers;

namespace MinimalApi.Endpoints;

public static class VeiculoEndpoint
{
    public static void MapVeiculoEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/veiculos").WithTags("Veiculos");
        var veiculoMapper = new VeiculoMapper();

        group.MapPost("/veiculos", ([FromBody] VeiculoDto veiculoDto
                , IVeiculoServico veiculoServico) =>
            {
                var veiculo = veiculoMapper.ToVeiculo(veiculoDto);
                veiculoServico.Salvar(veiculo);
                return Results.Created($"/veiculo/{veiculo.Id}",
                    veiculoMapper.ToVeiculoDto(veiculo));
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veiculos");

        group.MapGet("/veiculos", (IVeiculoServico veiculoServico) =>
        {
            var veiculosList = veiculoServico.Listar();
            return Results.Ok(veiculosList);
        }).RequireAuthorization().WithTags("Veiculos");

        group.MapGet("/veiculos/{id:int}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
            {
                var veiculoFound = veiculoServico.BuscarPorId(id);
                return Results.Ok(veiculoMapper.ToVeiculoDto(veiculoFound));
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm,Editor" })
            .WithTags("Veículos");

        group.MapPut("/veiculos/{id:int}",
                ([FromRoute] int id, [FromBody] VeiculoDto veiculoDto, IVeiculoServico veiculoServico) =>
                {
                    var veiculoToUpdate = veiculoMapper.ToVeiculo(veiculoDto);
                    veiculoServico.Atualizar(id, veiculoToUpdate);
                    return Results.Ok();
                })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Veiculos");

        group.MapDelete("/veiculos/{id:int}", ([FromRoute] int id, IVeiculoServico veiculoServico) =>
            {
                veiculoServico.Apagar(id);
                return Results.NoContent();
            })
            .RequireAuthorization()
            .RequireAuthorization(new AuthorizeAttribute { Roles = "Adm" })
            .WithTags("Veiculos");
    }
}