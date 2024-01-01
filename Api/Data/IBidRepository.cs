using Api.Dtos;

namespace Api.Data
{
    public interface IBidRepository
    {
        Task<List<BidDto>> Get(int houseId);
        Task<BidDto> Add(int houseId, BidForCreationDto dto);
    }
}