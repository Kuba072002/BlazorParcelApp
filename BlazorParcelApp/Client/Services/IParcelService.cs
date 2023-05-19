namespace BlazorParcelApp.Client.Services {
    public interface IParcelService {

        public Task<List<ParcelDto>> GetParcels();
    }
}
