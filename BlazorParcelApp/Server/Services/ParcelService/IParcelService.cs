namespace BlazorParcelApp.Server.Services.ParcelService {
    public interface IParcelService {
        Task<ServiceResponse<List<ParcelDto>>> GetParcels();
        Task<ServiceResponse<ParcelDto>> GetParcel(int Id);
        Task<ServiceResponse<ParcelDto>> AddParcel(ParcelDto parcelDto);
        Task<ServiceResponse<ParcelDto>> UpdateParcel(ParcelDto parcelDto);
        Task<ServiceResponse<string>> DeleteParcel(int Id);
    }
}
