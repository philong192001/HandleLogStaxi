using ManageVoyage.Common;
using ManageVoyage.Models;
using ManageVoyage.Repositories;
using ManageVoyage.Settings;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ManageVoyage.BackgroundTasks;

public class GetVoyageBackgroundTasks : BackgroundService
{
    private readonly AppSetting _appSetting;
    private readonly ILogger<GetVoyageBackgroundTasks> _logger;
    private Timer? timer;
    private readonly IHttpClientFactory _factory;
    private TimeSpan nextDelay;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private string token;

    public GetVoyageBackgroundTasks(ILogger<GetVoyageBackgroundTasks> logger, IHttpClientFactory factory, AppSetting appSetting, IServiceScopeFactory serviceScopeFactory)
    {
        _appSetting = appSetting;
        _logger = logger;  
        _factory = factory; 
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        _serviceScopeFactory = serviceScopeFactory;
    }

    private TimeSpan GenerateRandomDelay()
    {
        var random = new Random();
        var second = random.Next(1, 10);
        return TimeSpan.FromMinutes(second);
    }

    private string GetVoyage(HttpClient httpClient, string token)
    {
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            try
            {

                // Lấy thời gian hiện tại
                DateTime currentTime = DateTime.UtcNow;

                DateTimeOffset dateTimeOffset = new DateTimeOffset(currentTime);
                long timestampTo = dateTimeOffset.ToUnixTimeSeconds();

                // Trừ đi 30 phút
                DateTime newTime = currentTime.AddMinutes(-30);
                // Chuyển đổi thành timestamp
                long timestampFrom = new DateTimeOffset(newTime).ToUnixTimeSeconds();

                var url = $"{_appSetting.CaroAccount.Url}{string.Format(Constants.UrlGetVoyage, 2000, timestampTo, timestampFrom)}";
                // Thêm thông tin xác thực vào header "Authorization"
                httpClient.DefaultRequestHeaders.Add("Authorization", token);
                //response từ API
                var response = httpClient.GetAsync(url).Result;
                string jsonData = response.Content.ReadAsStringAsync().Result;
                var data = JsonSerializer.Deserialize<VoyageCaroRes>(jsonData, _jsonSerializerOptions);
                
                if(data == null || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return "Fail";
                }
                var caroBookRepository = scope.ServiceProvider.GetRequiredService<ICaroBookRepository>();
                caroBookRepository.SaveRange(data);

                return "Ok";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return "Fail";
            }
           
        }
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        nextDelay = GenerateRandomDelay();

        timer = new Timer(o => {

            HttpClient client = _factory.CreateClient();
            token = Utils.GetToken(client, _appSetting, _jsonSerializerOptions);
            var voyageData = GetVoyage(client, token);

            //Khi thiếu token hoặc token hết hạn
            if(voyageData == "Fail")
            {
                token = Utils.GetToken(client,_appSetting, _jsonSerializerOptions);
            }

            nextDelay = GenerateRandomDelay();
            Task.Delay(nextDelay, stoppingToken);

            _logger.LogInformation("Get Route Caro " + DateTime.Now);
        },
             null,
             TimeSpan.Zero,
             TimeSpan.FromMinutes(1));

        return Task.CompletedTask;
    }

   
}
