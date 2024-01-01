using Api.Data;
using Api.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using MiniValidation;

namespace Api
{
    public static class WebApplicationBidExtensions
    {
        public static void MapBidEndpoints(this WebApplication app)
        {
            app.MapGet("/houses/{houseId}/bids", async Task<Results<NotFound, Ok<List<BidDto>>>> (
                int houseId, IHouseRepository houseRepository, IBidRepository bidRepository) =>
            {
                if (await houseRepository.Get(houseId) == null)
                    return TypedResults.NotFound();

                var bids = await bidRepository.Get(houseId);
                return TypedResults.Ok(bids);
            }).WithName("GetBids");

            app.MapPost("/houses/{houseId}/bids", async
                Task<Results<ValidationProblem, NotFound, CreatedAtRoute<BidDto>>> (
                int houseId, BidForCreationDto dto, IHouseRepository houseRepository, IBidRepository bidRepository) =>
            {
                if (!MiniValidator.TryValidate(dto, out var errors))
                    return TypedResults.ValidationProblem(errors);

                if (await houseRepository.Get(houseId) == null)
                    return TypedResults.NotFound();

                var newBid = await bidRepository.Add(houseId, dto);
                return TypedResults.CreatedAtRoute(
                    newBid,
                    "GetBids",
                    new { houseId = newBid.HouseId }
                );
            });
        }
    }
}