namespace BlazorParcelApp.Client.Services {
    public interface IParcelService {

        public Task<List<ParcelDto>> GetParcels();
        public Task<List<ParcelDto>> GetParcelsByUser(string username);
    }
}
