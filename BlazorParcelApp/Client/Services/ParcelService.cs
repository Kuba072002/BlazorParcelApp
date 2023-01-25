using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorParcelApp.Client.Services {
    public class ParcelService : IParcelService {
        private readonly HttpClient _http;
        public ParcelService(HttpClient http) {
            _http = http;
        }

        public async Task<List<ParcelDto>> GetParcels() {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<ParcelDto>>>($"api/Parcel");
            return result.Data;
        }

        public async Task<IEnumerable<ParcelDto>> GetItems() {
            try {
                var response = await this._http.GetAsync("api/parcel/get2");

                if (response.IsSuccessStatusCode) {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return Enumerable.Empty<ParcelDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ParcelDto>>();
                }
                else {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception) {
                //Log exception
                throw;
            }
        }
    }
}
