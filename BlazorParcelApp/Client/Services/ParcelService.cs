using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace BlazorParcelApp.Client.Services {
    public class ParcelService : IParcelService {
        private readonly HttpClient _http;
        public ParcelService(HttpClient http) {
            _http = http;
        }

        public async Task<List<ParcelDto>> GetParcels() {
            try {
                var response = await this._http.GetAsync("api/parcel");

                if (response.IsSuccessStatusCode) {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent) {
                        return new List<ParcelDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<List<ParcelDto>>();
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
