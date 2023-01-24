﻿using BlazorParcelApp.Server.Data;
using BlazorParcelApp.Server.Services.UserService;
using BlazorParcelApp.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorParcelApp.Server.Services.ParcelService {
    public class ParcelService : IParcelService {
        private readonly DataContext _context;
        private readonly IUserService _userService;

        public ParcelService(DataContext context, IUserService userService) {
            _context = context;
            _userService = userService;
        }

        public async Task<ServiceResponse<ParcelDto>> AddParcel(ParcelDto parcelDto) {
            var response = new ServiceResponse<ParcelDto>();
            var user = _context.Users.Where(u => u.Username == _userService.GetUserName()).FirstOrDefault();
            //var sender = await _context.Users.FindAsync(2);
            var receiver = await _context.Users.Where(u => u.Username == parcelDto.Receiver).FirstOrDefaultAsync();
            var lockerSrc = await _context.Lockers.Where(u => u.Name == parcelDto.SrcLocker).FirstOrDefaultAsync();
            var lockerDest = await _context.Lockers.Where(u => u.Name == parcelDto.DestLocker).FirstOrDefaultAsync();

            var newShipment = new Parcel {
                Name = parcelDto.Name,
                State = parcelDto.State,
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

        public Task<ServiceResponse<string>> DeleteParcel(int Id) {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<ParcelDto>> GetParcel(int Id) {
            var response = new ServiceResponse<ParcelDto>();

            var parcel = await _context.Parcels.Include(x => x.SrcLocker).Include(x => x.DestLocker).Include(x => x.Receiver).FirstOrDefaultAsync(i => i.Id == Id);
            if (parcel == null) {
                response.Success = false;
                response.Message = "Parcel not found";
                return response;

            }
            
            ParcelDto sd = new ParcelDto {
                Id = parcel.Id,
                Name = parcel.Name,
                State = parcel.State,
                Sender = parcel.Sender.Username,
                Receiver = parcel.Receiver.Username,
                SrcLocker = parcel.SrcLocker.Name,
                DestLocker = parcel.DestLocker.Name
            };
            response.Message = "Parcel successfully get";
            response.Data = sd;
            return response;
        }

        public async Task<ServiceResponse<List<ParcelDto>>> GetParcels() {
            var responce = new ServiceResponse<List<ParcelDto>>();
            List<ParcelDto> parcelsDtoList = new();
            //var userId = _userService.GetUserId();
            var userName = _userService.GetUserName();
            var userRole = _userService.GetUserRole();
            var parcels = await _context.Parcels.Include(x => x.SrcLocker).Include(x => x.DestLocker)
                    .Include(x => x.Receiver).Where(u => u.Sender.Username == userName).ToListAsync();
            foreach(var p in parcels) {
                ParcelDto pdto = new ParcelDto {
                    Id = p.Id,
                    Name = p.Name,
                    State = p.State,
                    Sender = p.Sender.Username,
                    Receiver = p.Receiver.Username,
                    DestLocker = p.DestLocker.Name,
                    SrcLocker = p.SrcLocker.Name
                };
                parcelsDtoList.Add(pdto);
            }
            responce.Success = true;
            responce.Message = "Success, got parcels";
            responce.Data = parcelsDtoList;
            
            return responce;
        }

        public async Task<ServiceResponse<ParcelDto>> UpdateParcel(ParcelDto parcelDto) {
            var response = new ServiceResponse<ParcelDto>();

            var parcel = await _context.Parcels.FirstOrDefaultAsync(i => i.Id == parcelDto.Id);
            if (parcel == null) {
                response.Success = false;
                response.Message = "Shipment not found";
                return response;

            }

            parcel.State = parcelDto.State;
            _context.Parcels.Update(parcel);
            await _context.SaveChangesAsync();

            response.Data = parcelDto;
            return response;
        }
    }
}
