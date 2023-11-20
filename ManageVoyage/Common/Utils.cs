using ManageVoyage.Models;
using ManageVoyage.Settings;
using System.Text;
using System.Text.Json;

namespace ManageVoyage.Common;

public class Utils
{
    public static string GetToken(HttpClient client, AppSetting appSetting, JsonSerializerOptions jsonSerializerOptions)
    {
        var userLogin = new UserCaro()
        {
            Email = appSetting.CaroAccount.Email,
            Password = appSetting.CaroAccount.Password
        };

        // Chuyển đối tượng data thành chuỗi JSON
        var jsonData = JsonSerializer.Serialize(userLogin);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = client.PostAsync($"{appSetting.CaroAccount.Url}{Constants.UrlGetToken}", content);
        var responseBody = response.Result.Content.ReadAsStringAsync();
        var trackingRouteResponse = JsonSerializer.Deserialize<UserCaroRes>(responseBody.Result, jsonSerializerOptions);
        return trackingRouteResponse.Token;
    }
}
