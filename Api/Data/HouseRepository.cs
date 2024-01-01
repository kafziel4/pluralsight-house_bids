using Api.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class HouseRepository : IHouseRepository
    {
        private readonly HouseDbContext _context;

        public HouseRepository(HouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<HouseDto>> GetAll()
        {
            return await _context.Houses
                .Select(h => new HouseDto(
                    h.Id, h.Address, h.Country, h.Price))
                .ToListAsync();
        }

        public async Task<HouseDetailDto?> Get(int id)
        {
            var entity = await _context.Houses.FindAsync(id);

            if (entity == null)
                return null;

            return EntityToDetailDto(entity);
        }

        public async Task<HouseDetailDto> Add(HouseForCreationDto dto)
        {
            var entity = new HouseEntity();
            DtoToEntity(dto, entity);

            _context.Houses.Add(entity);
            await _context.SaveChangesAsync();

            return EntityToDetailDto(entity);
        }

        public async Task<HouseDetailDto?> Update(int id, HouseForUpdateDto dto)
        {
            var entity = await _context.Houses.FindAsync(id);
            if (entity == null)
                return null;

            DtoToEntity(dto, entity);

            _context.Update(entity);
            await _context.SaveChangesAsync();

            return EntityToDetailDto(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _context.Houses.FindAsync(id);
            if (entity == null)
                return false;

            _context.Houses.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static void DtoToEntity(HouseForCreationDto dto, HouseEntity entity)
        {
            entity.Address = dto.Address;
            entity.Country = dto.Country;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Photo = dto.Photo;
        }

        private static void DtoToEntity(HouseForUpdateDto dto, HouseEntity entity)
        {
            entity.Address = dto.Address;
            entity.Country = dto.Country;
            entity.Description = dto.Description;
            entity.Price = dto.Price;
            entity.Photo = dto.Photo;
        }

        private static HouseDetailDto EntityToDetailDto(HouseEntity entity)
        {
            return new HouseDetailDto(
                entity.Id,
                entity.Address,
                entity.Country,
                entity.Price,
                entity.Description,
                entity.Photo
            );
        }
    }
}