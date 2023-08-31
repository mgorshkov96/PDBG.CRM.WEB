using Azure.Core;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models.JsonEntities;
using PDBG.CRM.WEB.Models.Repositories;
using System.Net.Http;

namespace PDBG.CRM.WEB.Models
{
    public class AmoSync
    {
        private IAmoAuthRepository _amoAuthRepository;        
        static HttpClient httpClient = new HttpClient();
        private const string AMO_AUTH_URL = "https://gladilin.amocrm.ru/oauth2/access_token";

        public AmoSync(IAmoAuthRepository amoAuthRepository)
        {
            _amoAuthRepository = amoAuthRepository;
        }

        public AmoContact Contact { get; set; }

        public AmoLead Lead { get; set; }

        public async Task RequestLeadAndContactAsync(int leadId)
        {
            string accessToken = await GetNewAccessTokenAsync();

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var amoLead = await httpClient.GetFromJsonAsync<AmoLead>($"https://gladilin.amocrm.ru/api/v4/leads/{leadId}?with=contacts");
            Lead = amoLead;

            var amoContacts = amoLead.Embedded.Contacts;
            AmoContact? embeddedContact = amoContacts[0];

            foreach (var contact in amoContacts)
            {
                if (contact.IsMain == true)
                {
                    embeddedContact = contact;
                }
            }

            var amoContact = await httpClient.GetFromJsonAsync<AmoContact>($"https://gladilin.amocrm.ru/api/v4/contacts/{embeddedContact.Id}?with=contacts");
            Contact = amoContact;
        }

        public async Task<string?> GetNewAccessTokenAsync()
        {
            var amoAuthReq = _amoAuthRepository.AmoAuthes.FirstOrDefault();

            if (String.IsNullOrEmpty(amoAuthReq.AccessToken))
            {
                amoAuthReq.GrantType = "authorization_code";
            }
            else
            {
                amoAuthReq.GrantType = "refresh_token";
            }

            JsonContent authContent = JsonContent.Create(amoAuthReq);
            using var authResponse = await httpClient.PostAsync(AMO_AUTH_URL, authContent);
            var amoAuth = await authResponse.Content.ReadFromJsonAsync<AmoAuth>();            
            amoAuthReq.RefreshToken = amoAuth.RefreshToken;
            amoAuthReq.AccessToken = amoAuth.AccessToken;
            await _amoAuthRepository.UpdateAuthAsync(amoAuthReq);
            return amoAuth.AccessToken;            
        }
    }
}
