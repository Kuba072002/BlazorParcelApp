using BlazorParcelApp.Server.Data;
using BlazorParcelApp.Server.Services.UserService;
using BlazorParcelApp.Shared;
using Microsoft.EntityFrameworkCore;
using static BlazorParcelApp.Shared.Parcel;

namespace BlazorParcelApp.Server.Services.ParcelService {
    public class ParcelService : IParcelService
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ParcelService(DataContext context, IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<ServiceResponse<ParcelDto>> AddParcel(ParcelDto parcelDto)
        {
            var response = new ServiceResponse<ParcelDto>();
            var user = _context.Users.Where(u => u.Username.Equals(_userService.GetUserName())).FirstOrDefault();
            //var sender = await _context.Users.FindAsync();
            var receiver = await _context.Users.Where(u => u.Username == parcelDto.Receiver).FirstOrDefaultAsync();
            var lockerSrc = await _context.Lockers.Where(u => u.Id == parcelDto.SrcLocker.Id).FirstOrDefaultAsync();
            var lockerDest = await _context.Lockers.Where(u => u.Id == parcelDto.DestLocker.Id).FirstOrDefaultAsync();

            var newShipment = new Parcel
            {
                Name = Guid.NewGuid().ToString("N").Substring(0, 8),
                CurrentState = State.Posted,
                Sender = user,
                Receiver = receiver,
                SrcLocker = lockerSrc,
                DestLocker = lockerDest
            };
            _context.Parcels.Add(newShipment);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Successfully add shipment";
            response.Data = parcelDto;
            return response;
        }

        public Task<ServiceResponse<string>> DeleteParcel(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ParcelDto>> GetParcel(int Id)
        {
            var response = new ServiceResponse<ParcelDto>();

            var parcel = await _context.Parcels.Include(x => x.SrcLocker).Include(x => x.DestLocker).Include(x => x.Receiver).FirstOrDefaultAsync(i => i.Id == Id);
            if (parcel == null)
            {
                response.Success = false;
                response.Message = "Parcel not found";
                return response;

            }

            ParcelDto sd = new ParcelDto
            {
                Id = parcel.Id,
                Name = parcel.Name,
                CurrentState = parcel.CurrentState,
                Sender = parcel.Sender.Username,
                Receiver = parcel.Receiver.Username,
                DestLocker = new LockerDto
                {
                    Id = parcel.DestLocker.Id,
                    Name = parcel.DestLocker.Name,
                    City = parcel.DestLocker.City,
                    Address = parcel.DestLocker.Address
                },
                SrcLocker = new LockerDto
                {
                    Id = parcel.SrcLocker.Id,
                    Name = parcel.SrcLocker.Name,
                    City = parcel.SrcLocker.City,
                    Address = parcel.SrcLocker.Address
                }
            };
            response.Message = "Parcel successfully get";
            response.Data = sd;
            return response;
        }

        public async Task<ServiceResponse<List<ParcelDto>>> GetParcels()
        {
            var responce = new ServiceResponse<List<ParcelDto>>();
            List<ParcelDto> parcelsDtoList = new();
            //var userId = _userService.GetUserId();
            var userName = _userService.GetUserName();
            if (_contextAccessor.HttpContext != null)
            {
                userName = _contextAccessor.HttpContext.User?.Identity?.Name;//.FindFirstValue(ClaimTypes.Name);
            }
            //string name = principal.Identity.Name;
            var userRole = _userService.GetUserRole();
            var parcels = await _context.Parcels
                .Include(x => x.SrcLocker)
                .Include(x => x.DestLocker)
                .Include(x => x.Receiver)
                .Include(x => x.Sender)
                //.Where(u => u.Sender.Username.Equals(userName) || u.Receiver.Username.Equals(userName))
                .ToListAsync();
            foreach (var p in parcels)
            {
                ParcelDto pdto = new ParcelDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CurrentState = p.CurrentState,
                    Sender = p.Sender.Username,
                    Receiver = p.Receiver.Username,
                    DestLocker = new LockerDto
                    {
                        Id = p.DestLocker.Id,
                        Name = p.DestLocker.Name,
                        City = p.DestLocker.City,
                        Address = p.DestLocker.Address
                    },
                    SrcLocker = new LockerDto
                    {
                        Id = p.SrcLocker.Id,
                        Name = p.SrcLocker.Name,
                        City = p.SrcLocker.City,
                        Address = p.SrcLocker.Address
                    }
                };
                parcelsDtoList.Add(pdto);
            }
            responce.Success = true;
            responce.Message = "Success, got parcels";
            responce.Data = parcelsDtoList;

            return responce;
        }

        public async Task<ServiceResponse<ParcelDto>> UpdateParcelState(int parcelId)
        {
            var parcel = await _context.Parcels.FirstOrDefaultAsync(i => i.Id == parcelId);
            if (parcel == null)
                return new ServiceResponse<ParcelDto>
                { Success = false, Message = "Shipment not found" };
            int currentIndex = (int)parcel.CurrentState;
            if (currentIndex + 1 < Enum.GetValues(typeof(State)).Length)
            {
                parcel.CurrentState = (State)currentIndex + 1;
                _context.Parcels.Update(parcel);
                await _context.SaveChangesAsync();
            }
            return new ServiceResponse<ParcelDto>
            { Success = true, Message = "Parcel already received" };
        }

        public async Task<ServiceResponse<List<ParcelDto>>> GetParcelsByUser(string username)
        {
            var response = new ServiceResponse<List<ParcelDto>> { Success = true, Message = "Success" };
            response.Data = await _context.Parcels
                .Include(p => p.SrcLocker)
                .Include(p => p.DestLocker)
                .Include(p => p.Receiver)
                .Include(p => p.Sender)
                .Where(p => p.Sender.Username == username || p.Receiver.Username == username)
                .Select(p => new ParcelDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CurrentState = p.CurrentState,
                    Sender = p.Sender.Username,
                    Receiver = p.Receiver.Username,
                    DestLocker = new LockerDto 
                    { 
                        Id = p.DestLocker.Id,
                        Name = p.DestLocker.Name,
                        City= p.DestLocker.City,
                        Address = p.DestLocker.Address
                    },
                    SrcLocker = new LockerDto
                    {
                        Id = p.SrcLocker.Id,
                        Name = p.SrcLocker.Name,
                        City = p.SrcLocker.City,
                        Address = p.SrcLocker.Address
                    }
                }).ToListAsync();
            return response;
        }
    }
}
