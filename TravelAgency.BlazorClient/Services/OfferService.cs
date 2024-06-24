using TravelAgency.SharedKernel.Dto.Offer;
using Newtonsoft.Json;

namespace TravelAgency.BlazorClient.Services
{
    public class OfferService : IOfferService
    {
        private readonly HttpClient _httpClient;

        public OfferService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<OfferDto>> GetAll()
        {
            var response = await _httpClient.GetAsync("/offer");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var offers = JsonConvert.DeserializeObject<IEnumerable<OfferDto>>(content);

                return offers.ToList();
            }

            return new List<OfferDto>();

        }

        public async Task<OfferDto> GetById(int id)
        {
            var offers = await this.GetAll();
            var result = offers.FirstOrDefault(o => o.OfferId == id);
            return result;
        }
    }
}
