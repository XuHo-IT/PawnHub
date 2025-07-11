using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace BussinessObject.Services
{
    public class GoogleAuthService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public GoogleAuthService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            _clientId = configuration["Google:ClientId"];
            _clientSecret = configuration["Google:ClientSecret"];

            // Kiểm tra xem có đọc được config không
            if (string.IsNullOrEmpty(_clientId) || _clientId.Contains("YOUR_GOOGLE_CLIENT_ID"))
            {
                throw new InvalidOperationException("Google ClientId không được cấu hình đúng. Vui lòng kiểm tra appsettings.Development.json");
            }

            if (string.IsNullOrEmpty(_clientSecret) || _clientSecret.Contains("YOUR_GOOGLE_CLIENT_SECRET"))
            {
                throw new InvalidOperationException("Google ClientSecret không được cấu hình đúng. Vui lòng kiểm tra appsettings.Development.json");
            }
        }

        public async Task<GoogleJsonWebSignature.Payload> ValidateGoogleTokenAsync(string idToken)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _clientId }
                });

                return payload;
            }
            catch (InvalidJwtException)
            {
                return null;
            }
        }

        public string GetClientId()
        {
            return _clientId;
        }

        public string GetClientSecret()
        {
            return _clientSecret;
        }
    }

    public class GoogleUserInfo
    {
        public string GoogleId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public bool EmailVerified { get; set; }
    }
}
