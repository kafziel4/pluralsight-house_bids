using Api.Dtos;

namespace Api.Data
{
    public interface IHouseRepository
    {
        Task<List<HouseDto>> GetAll();
        Task<HouseDetailDto?> Get(int id);
        Task<HouseDetailDto> Add(HouseForCreationDto dto);
        Task<HouseDetailDto?> Update(int id, HouseForUpdateDto dto);
        Task<bool> Delete(int id);
    }
}