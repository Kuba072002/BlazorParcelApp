﻿namespace BlazorParcelApp.Server.Services.LokcerService {
    public interface ILockerService {
        Task<ServiceResponse<List<LockerDto>>> GetLockers();
        Task<ServiceResponse<LockerDto>> GetLocker(int Id);
        Task<ServiceResponse<LockerDto>> AddLocker(LockerDto lockerDto);
        Task<ServiceResponse<LockerDto>> UpdateLocker(LockerDto lockerDto);
        Task<ServiceResponse<LockerDto>> DeleteLocker(int lockerId);
    }
}
