using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    public record BidForCreationDto(
        [property: Required] string Bidder,
        int Amount
    );
}