using Microsoft.CodeAnalysis.Elfie.Serialization;
using PDBG.CRM.WEB.Models.JsonEntities;
using PDBG.CRM.WEB.Models.Repositories;

namespace PDBG.CRM.WEB.Models
{
    public class YaMaps
    {
        static HttpClient httpClient = new HttpClient();
        private IApiKeyRepository _apiKeyRepository;
        private string apiKey;

        public YaMaps(IApiKeyRepository apiKeyRepository)
        {
            _apiKeyRepository = apiKeyRepository;
            apiKey = apiKeyRepository.GetApiKeyByName("yandex_maps");
        }

        public async Task<string> GetCoordsAsync(string address)
        {
            string coords = "";
            if (apiKey != null)
            {
                address = address.Replace(" ", "+");
                var geocode = await httpClient.GetFromJsonAsync<YaGeocode>($"https://geocode-maps.yandex.ru/1.x/?apikey={apiKey}={address}&format=json");

                if (geocode != null)
                {
                    if (geocode.Response.GeoObjectCollection.FeatureMember.Length != 1)
                    {
                        List<GeoObject> objects = new List<GeoObject>();

                        foreach (var item in geocode.Response.GeoObjectCollection.FeatureMember)
                        {
                            if (String.Equals(item.GeoObject.MetaDataProperty.GeocoderMetaData.Address.CountryCode, "RU"))
                            {
                                objects.Add(item.GeoObject);
                            }
                        }

                        if (objects.Count == 1)
                        {
                            coords = objects.FirstOrDefault().Point.Pos;
                        }
                        else if (objects.Count > 1)
                        {
                            foreach (var item in objects)
                            {
                                foreach (var comp in item.MetaDataProperty.GeocoderMetaData.Address.Components)
                                {
                                    if (String.Equals(comp.Kind, "province") && String.Equals(comp.Name, "Санкт-Петербург") ||
                                        String.Equals(comp.Kind, "province") && String.Equals(comp.Name, "Ленинградская область"))
                                    {
                                        coords = item.Point.Pos;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        coords = geocode.Response.GeoObjectCollection.FeatureMember[0].GeoObject.Point.Pos;
                    }
                }
            }           
            return coords;
        }
    }
}
