namespace Api.Dtos
{
    public record HouseDto(
        int Id,
        string? Address,
        string? Country,
        int Price);
}