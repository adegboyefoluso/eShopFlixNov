using eShopflix.web.Models;
using System.Text;
using System.Text.Json;

namespace eShopflix.web.HttpClients
{
    public class AuthServiceClient
    {
         HttpClient _client;

        public AuthServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserModel> Login(LoginModel loginModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(loginModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "auth/login", content);
            if (response.IsSuccessStatusCode)
            {
                var data= await response.Content.ReadAsStringAsync();
                UserModel user= JsonSerializer.Deserialize<UserModel>(data);
                return user;
            }
            else return null;
        }

        public async Task<bool> SignUp(SignUpModel SignUpModel)
        {
            var content = new StringContent(JsonSerializer.Serialize(SignUpModel), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_client.BaseAddress + "/auth/Register", content);
            if (response.IsSuccessStatusCode)
            {
             
                return true;
            }
            else return false;
        }
    }
}

