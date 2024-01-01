namespace Api.Dtos
{
    public record BidDto(
        int Id,
        int HouseId,
        string Bidder,
        int Amount
    );
}