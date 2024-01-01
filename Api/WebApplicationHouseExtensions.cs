using Api.Data;
using Api.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using MiniValidation;

namespace Api
{
    public static class WebApplicationHouseExtensions
    {
        public static void MapHouseEndpoints(this WebApplication app)
        {
            app.MapGet("/houses", async Task<Ok<List<HouseDto>>> (IHouseRepository repository) =>
            {
                return TypedResults.Ok(await repository.GetAll());
            });

            app.MapGet("/houses/{id}", async Task<Results<NotFound, Ok<HouseDetailDto>>> (
                int id, IHouseRepository repository) =>
            {
                var house = await repository.Get(id);
                if (house == null)
                    return TypedResults.NotFound();

                return TypedResults.Ok(house);
            }).WithName("GetHouse");

            app.MapPost("/houses", async Task<Results<ValidationProblem, CreatedAtRoute<HouseDetailDto>>> (
                HouseForCreationDto dto, IHouseRepository repository) =>
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return TypedResults.ValidationProblem(errors);

                var newHouse = await repository.Add(dto);
                return TypedResults.CreatedAtRoute(
                    newHouse,
                    "GetHouse",
                    new { id = newHouse.Id }
                );
            });

            app.MapPut("/houses/{id}", async Task<Results<ValidationProblem, NotFound, Ok<HouseDetailDto>>> (
                int id, HouseForUpdateDto dto, IHouseRepository repository) =>
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return TypedResults.ValidationProblem(errors);

                var updatedHouse = await repository.Update(id, dto);
                if (updatedHouse == null)
                    return TypedResults.NotFound();

                return TypedResults.Ok(updatedHouse);
            });

            app.MapDelete("/houses/{id}", async Task<Results<NotFound, NoContent>> (
                int id, IHouseRepository repository) =>
            {
                var deletedHouse = await repository.Delete(id);
                if (!deletedHouse)
                    return TypedResults.NotFound();

                return TypedResults.NoContent();
            });
        }
    }
}