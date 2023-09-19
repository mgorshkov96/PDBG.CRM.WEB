using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models.JsonEntities;
using PDBG.CRM.WEB.Models.Repositories;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models
{
    public class AmoService
    {
        private IAmoAuthRepository _amoAuthRepository;        
        static HttpClient httpClient = new HttpClient();
        private const string AMO_AUTH_URL = "https://gladilin.amocrm.ru/oauth2/access_token";
		private const string AMO_URL = "https://gladilin.amocrm.ru/api/v4/";

		
        public AmoService(IAmoAuthRepository amoAuthRepository)
        {
            _amoAuthRepository = amoAuthRepository;
        }

        public AmoContact? Contact { get; set; }

		public AmoInputLead? Lead { get; set; }

		public async Task RequestLeadAndContactAsync(int leadId)
        {
            string accessToken = await GetNewAccessTokenAsync();

            httpClient.DefaultRequestHeaders.Remove("Authorization");
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var amoLead = await httpClient.GetFromJsonAsync<AmoInputLead>($"{AMO_URL}leads/{leadId}?with=contacts");
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

            var amoContact = await httpClient.GetFromJsonAsync<AmoContact>($"{AMO_URL}contacts/{embeddedContact.Id}?with=contacts");
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

        public async Task<bool> SaveLeadAsync(Lead lead)
		{
            AmoOutputLead amoLead = new AmoOutputLead(lead.Id);
            
            switch (lead.StatusId)
            {
                case 2:
                    {
						amoLead.StatusId = 142;
						int sum = Decimal.ToInt32((decimal)lead.Sum);
                        amoLead.Price = sum;
                        break;
                    }
                case 3:
                    {
                        amoLead.StatusId = 143;
                        var amoCustomFields = new List<AmoOutputCustomField>();

                        var selectReason = new AmoOutputCustomField();
                        selectReason.FieldId = 625891;
                        selectReason.FieldName = "Причины отказа";
						var selectReasonValuesArr = new AmoOutputCustomFieldValue[1];
                        var selectReasonValue = new AmoOutputCustomFieldValue();
						selectReasonValue.Value = "Причина отказа у агента";
                        
                        selectReasonValuesArr[0] = selectReasonValue;
						selectReason.Values = selectReasonValuesArr;

						var descrReason = new AmoOutputCustomField();
                        descrReason.FieldId = 625941;
						descrReason.FieldName = "Причины отказа";
						var descrReasonValuesArr = new AmoOutputCustomFieldValue[1];
                        var descrReasonValue = new AmoOutputCustomFieldValue();
                        descrReasonValue.Value = lead.RejectionReason;
                        descrReasonValuesArr[0] = descrReasonValue;
						descrReason.Values = descrReasonValuesArr;

						amoCustomFields.Add(selectReason);
						amoCustomFields.Add(descrReason);

                        amoLead.CustomFieldsValues = amoCustomFields;
						break;
                    }
            }

			string accessToken = await GetNewAccessTokenAsync();

			httpClient.DefaultRequestHeaders.Remove("Authorization");
			httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

			JsonContent content = JsonContent.Create(amoLead);

            
            var result = await httpClient.PatchAsync($"{AMO_URL}leads/{lead.Id}", content);

            if (!result.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}
