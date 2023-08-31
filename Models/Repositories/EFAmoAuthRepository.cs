using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Policy;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFAmoAuthRepository : IAmoAuthRepository
    {
        private PDBGContext _context;
        static HttpClient httpClient = new HttpClient();
        private const string AMO_URL = "https://gladilin.amocrm.ru/oauth2/access_token";

        public EFAmoAuthRepository(PDBGContext context)
        {
            _context = context;
        }

        public IQueryable<AmoAuth> AmoAuthes => _context.AmoAuthes;

        //public async Task<string?> GetNewAccessTokenAsync()
        //{
        //    var amoAuthReq = await _context.AmoAuthes.FirstOrDefaultAsync();

        //    if (amoAuthReq.AccessToken == null)
        //    {
        //        amoAuthReq.GrantType = "authorization_code";
        //    }
        //    else
        //    {
        //        amoAuthReq.GrantType = "refresh_token";
        //    }

        //    JsonContent authContent = JsonContent.Create(amoAuthReq);
        //    using var authResponse = await httpClient.PostAsync(AMO_URL, authContent);
        //    var amoAuth = await authResponse.Content.ReadFromJsonAsync<AmoAuth>();

        //    if (amoAuth != null && amoAuth.AccessToken != null)
        //    {
        //        amoAuthReq.AccessToken = amoAuth.AccessToken;
        //        amoAuthReq.RefreshToken = amoAuth.RefreshToken;

        //        _context.AmoAuthes.Update(amoAuthReq);
        //        await _context.SaveChangesAsync();

        //        return amoAuth.AccessToken;
        //    }
        //    else
        //    {
        //        return null;
        //    }            
        //}

        public async Task UpdateAuthAsync(AmoAuth amoAuth)
        {
            amoAuth.Id = 1;
            _context.AmoAuthes.Update(amoAuth);
            await _context.SaveChangesAsync();
        }
    }
}
