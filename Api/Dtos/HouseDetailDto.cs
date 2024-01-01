namespace Api.Dtos
{
    public record HouseDetailDto(
        int Id,
        string? Address,
        string? Country,
        int Price,
        string? Description,
        string? Photo);
}