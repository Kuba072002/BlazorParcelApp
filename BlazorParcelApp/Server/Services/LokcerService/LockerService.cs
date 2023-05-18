using BlazorParcelApp.Server.Data;
using BlazorParcelApp.Shared;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System;

namespace BlazorParcelApp.Server.Services.LokcerService {
    public class LockerService : ILockerService {
        private readonly DataContext _context;
        public LockerService(DataContext context) {
            _context = context;
        }
        public async Task<ServiceResponse<LockerDto>> AddLocker(LockerDto lockerDto) {
            var responce = new ServiceResponse<LockerDto>();
            var locker = new Locker
            {
                Address = lockerDto.Address,
                City = lockerDto.City,
                Name = await CreateName(lockerDto.City)
            };
            _context.Lockers.Add(locker);
            await _context.SaveChangesAsync();
            
            responce.Success = true;
            responce.Message = "Added locker";
            responce.Data = lockerDto;

            return responce;
        }

        public async Task<ServiceResponse<LockerDto>> GetLocker(int Id) {
            var locker = await _context.Lockers.FirstOrDefaultAsync(x => x.Id == Id);
            var responce = new ServiceResponse<LockerDto>();
            if (locker != null) {
                var lockerDto = new LockerDto {
                    Name = locker.Name,
                    Address = locker.Address,
                    City = locker.City,
                };
                responce.Success = true;
                responce.Message = "Get success";
                responce.Data = lockerDto;
                return responce;
            }
            responce.Success = false;
            responce.Message = "Not found";
            return responce;

        }

        public async Task<ServiceResponse<List<LockerDto>>> GetLockers() {
            var response = new ServiceResponse<List<LockerDto>>();
            List<LockerDto> responceList = new();
            var lockers = await _context.Lockers.ToListAsync();

            foreach (var l in lockers) {
                LockerDto sd = new LockerDto {
                    Id = l.Id,
                    Name = l.Name,
                    Address = l.Address,
                    City = l.City,
                };
                responceList.Add(sd);
            }
            response.Success = true;
            response.Message = "Successfully get lockers";
            response.Data = responceList;

            return response;
        }

        private async Task<string> CreateName(string CityName)
        {
            var numberLockerInCity = await _context.Lockers.CountAsync(l => l.City.Equals(CityName));
            if(numberLockerInCity < 10)
                return string.Concat(CityName.AsSpan(0, Math.Min(CityName.Length, 3)), "0", (numberLockerInCity+1).ToString());
            return string.Concat(CityName.AsSpan(0, Math.Min(CityName.Length, 3)), (numberLockerInCity + 1).ToString());
        }
    }
}
