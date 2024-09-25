using BaseLibrary.DTO;
using ClientLibrary.Services.Contracts;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Helpers
{
    public class CustomHttpHandler(
        GetHttpClient getHttpClient,
        LocalStorageService localStorageService,
        IUserAccountService accountService) : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            bool loginUrl = request.RequestUri!.AbsoluteUri.Contains("login");
            bool registerUrl = request.RequestUri!.AbsoluteUri.Contains("register");
            bool refreshTokenUrl = request.RequestUri!.AbsoluteUri.Contains("refresh-token");

            if (loginUrl ||  registerUrl || refreshTokenUrl) return await base.SendAsync(request, cancellationToken);

            HttpResponseMessage result = await base.SendAsync(request, cancellationToken);
            if (result.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Get token from localStorage
                string stringToken = await localStorageService.GetToken();
                if (stringToken is null) return result;

                // Check if the header contains token
                string token = string.Empty;
                try { token = request.Headers.Authorization!.Parameter!; }
                catch { }

                UserSession deserializedToken = Serializations.DeserializeJsonString<UserSession>(stringToken);
                if (deserializedToken is null) return result;
                {
                    
                }

                if (string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", deserializedToken.Token);
                    return await base.SendAsync(request, cancellationToken);
                }

                // Call for refresh token
                var newJwtToken = await GetRefreshToken(deserializedToken.RefreshToken!);
                if (string.IsNullOrEmpty(newJwtToken)) return result;

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newJwtToken);
                return await base.SendAsync(request, cancellationToken);
            }
            return result;
        }

        private async Task<string> GetRefreshToken(string refreshToken)
        {
            var result = await accountService.RefreshTokenAsync(new RefreshToken() { Token = refreshToken });
            string serializedToken = Serializations.SerializeObj(new UserSession() { Token = result.Token, RefreshToken = result.RefreshToken });
            await localStorageService.SetToken(serializedToken);
            return result.Token;
        }
    }
}
