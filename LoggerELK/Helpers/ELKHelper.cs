using LoggerELK.Enums;
using LoggerELK.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace LoggerELK.Helpers;

public static class ELKHelper
{
    private const int BatchSize = 20; // Kích thước của batch
    private static List<string> _messages = new List<string>();

    public static string CreateLogMessage(string message, LevelLogELK logLevel, string path, string typeApi)
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        var log = new LogEntry
        {
            DateTime = DateTime.UtcNow, 
            Level = logLevel.ToString(),
            App = hostEntry.HostName,
            Host = hostEntry.HostName,
            IP = ipv4Address?.ToString() ?? "IPv4 Address Not Found",
            Message = message,
            Path = path,
            TypeApi = typeApi
        };
        return JsonSerializer.Serialize(log);
    }

    // Send list log
    public static async Task<string> SendBulkLog(List<string> logs, string appName, ELKSettings elkSettings)
    {
        if (logs == null || logs.Count == 0)
        {
            return "No logs to send.";
        }
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        var combinedJsonBuilder = new StringBuilder();

        foreach (var log in logs)
        {
            // Kết hợp các chuỗi JSON
            var indexJson = JsonSerializer.Serialize(new RootObject()
            {
                index = new IndexInfo { _index = appName.ToLower() }
            });

            combinedJsonBuilder.AppendLine(indexJson);
            combinedJsonBuilder.AppendLine(log);
        }

        var combinedJson = combinedJsonBuilder.ToString();

        HttpClient client = new HttpClient();
        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{elkSettings.UserName}:{elkSettings.Password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        client.BaseAddress = new Uri(elkSettings.Url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsync(elkSettings.Path, new StringContent(combinedJson, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }
        else
        {
            return response.ToString();
        }
    }

    /// <summary>
    /// Send 1 log
    /// </summary>
    /// <param name="message"></param>
    /// <param name="level"></param>
    /// <param name="appName"></param>
    /// <param name="eLKSettings"></param>
    /// <returns></returns>
    public async static Task<string> SendLog(string message, LevelLogELK level, string path, string typeApi, string appName, ELKSettings eLKSettings)
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        // Tạo chuỗi JSON cho log
        var logJson = JsonSerializer.Serialize(new LogEntry
        {
            DateTime = DateTime.Now,
            Level = level.GetDescription(),
            App = hostEntry.HostName,
            Host = hostEntry.HostName,
            IP = ipv4Address?.ToString() ?? "IPv4 Address Not Found",
            Message = message,
            TypeApi = typeApi,
            Path = path
        });
        // Kết hợp các chuỗi JSON
        var indexJson = JsonSerializer.Serialize(new RootObject()
        {
            index = new IndexInfo { _index = appName.ToLower() }
        });

        var combinedJson = $"{indexJson}\n{logJson}\n \n";

        HttpClient client = new HttpClient();
        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eLKSettings.UserName}:{eLKSettings.Password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        client.BaseAddress = new Uri(eLKSettings.Url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsync(eLKSettings.Path, new StringContent(combinedJson, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }
        else
        {
            return response.ToString();
        }
    }

    /// <summary>
    /// Tu lấy appName
    /// </summary>
    /// <param name="message"></param>
    /// <param name="level"></param>
    /// <param name="appName"></param>
    /// <param name="eLKSettings"></param>
    /// <returns></returns>
    public async static Task<string> SendLog(string message, string path, string typeApi, LevelLogELK level, ELKSettings eLKSettings)
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);

        // Tạo chuỗi JSON cho log
        var logJson = JsonSerializer.Serialize(new LogEntry
        {
            DateTime = DateTime.Now,
            Level = level.GetDescription(),
            App = hostEntry.HostName,
            Host = hostEntry.HostName,
            IP = ipv4Address?.ToString() ?? "IPv4 Address Not Found",
            Message = message,
            TypeApi = typeApi,
            Path = path
        });
        // Kết hợp các chuỗi JSON
        var indexJson = JsonSerializer.Serialize(new RootObject()
        {
            index = new IndexInfo { _index = Dns.GetHostEntry(Dns.GetHostName()).ToString().ToLower() }
        });

        var combinedJson = $"{indexJson}\n{logJson}\n \n";

        HttpClient client = new HttpClient();
        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eLKSettings.UserName}:{eLKSettings.Password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        client.BaseAddress = new Uri(eLKSettings.Url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsync(eLKSettings.Path, new StringContent(combinedJson, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var contents = await response.Content.ReadAsStringAsync();
            return contents;
        }
        else
        {
            return response.ToString();
        }
    }
    /// <summary>
    /// Send 1 list log
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="level"></param>
    /// <param name="appName"></param>
    /// <param name="eLKSettings"></param>
    /// <returns></returns>
    public async static Task<string> SendLog(List<string> messages, LevelLogELK level, string path, string typeApi, string appName, ELKSettings eLKSettings)
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        var count = 0;
        var msg = "";
        foreach (var message in messages)
        {
            // Tạo chuỗi JSON cho log
            var logJson = JsonSerializer.Serialize(new LogEntry
            {
                DateTime = DateTime.Now,
                Level = level.GetDescription(),
                App = hostEntry.HostName,
                Host = hostEntry.HostName,
                IP = ipv4Address?.ToString() ?? "IPv4 Address Not Found",
                Message = message,
                Path = path,
                TypeApi = typeApi
            });
            // Kết hợp các chuỗi JSON
            var indexJson = JsonSerializer.Serialize(new RootObject()
            {
                index = new IndexInfo { _index = appName.ToLower() }
            });

            var combinedJson = $"{indexJson}\n{logJson}\n \n";

            HttpClient client = new HttpClient();
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eLKSettings.UserName}:{eLKSettings.Password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            client.BaseAddress = new Uri(eLKSettings.Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync(eLKSettings.Path, new StringContent(combinedJson, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                count++;
            }
            else
            {
                msg += $"{response.RequestMessage} --";
            }
        }

        if (string.IsNullOrEmpty(msg))
        {
            return $"Send thành công {count} log - Những request send lỗi {msg}";

        }
        return $"Send thành công {count} log";
    }

    /// <summary>
    /// Send log theo batch - Đủ batch thì mới gửi
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="level"></param>
    /// <param name="appName"></param>
    /// <param name="eLKSettings"></param>
    /// <returns></returns>
    public async static Task<string> SendLogBatch(List<string> messages, LevelLogELK level, string path, string typeApi, string appName, ELKSettings eLKSettings)
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        var count = 0;
        var msg = "";
        var batchedMessages = Batch(messages, BatchSize);

        foreach (var batch in batchedMessages)
        {
            var bulkJson = new StringBuilder();
            foreach (var message in batch)
            {
                // Tạo chuỗi JSON cho log
                var logJson = JsonSerializer.Serialize(new LogEntry
                {
                    DateTime = DateTime.Now,
                    Level = level.GetDescription(),
                    App = hostEntry.HostName,
                    Host = hostEntry.HostName,
                    IP = ipv4Address?.ToString() ?? "IPv4 Address Not Found",
                    Message = message,
                    Path = path,
                    TypeApi = typeApi
                });
                // Kết hợp các chuỗi JSON
                var indexJson = JsonSerializer.Serialize(new RootObject()
                {
                    index = new IndexInfo { _index = appName.ToLower() }
                });

                var combinedJson = $"{indexJson}\n{logJson}\n \n";
                bulkJson.Append(combinedJson);
            }
            HttpClient client = new HttpClient();
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eLKSettings.UserName}:{eLKSettings.Password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            client.BaseAddress = new Uri(eLKSettings.Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync(eLKSettings.Path, new StringContent(bulkJson.ToString(), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var contents = await response.Content.ReadAsStringAsync();
                count += batch.Count();
            }
            else
            {
                msg += $"{response.RequestMessage} --";
            }
        }

        if (string.IsNullOrEmpty(msg))
        {
            return $"Send thành công {count} log - Những request send lỗi {msg}";

        }
        return $"Send thành công {count} log";
    }

    private static IEnumerable<IEnumerable<T>> Batch<T>(IEnumerable<T> source, int size)
    {
        while (source.Any())
        {
            yield return source.Take(size);
            source = source.Skip(size);
        }
    }

    /// <summary>
    /// Add vào batch
    /// </summary>
    /// <param name="message"></param>
    public static void Add(string message)
    {
        _messages.Add(message);
    }

    /// <summary>
    /// Send log theo batch đã add
    /// </summary>
    /// <param name="level"></param>
    /// <param name="appName"></param>
    /// <param name="eLKSettings"></param>
    /// <returns></returns>
    public static async Task<string> SaveChanges(LevelLogELK level, string path, string typeApi, string appName, ELKSettings eLKSettings)
    {
        var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        var ipv4Address = hostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        var count = 0;
        var msg = "";

        var batchSize = _messages.Count;
        var batchLogJson = "";
        foreach (var message in _messages)
        {
            // Tạo chuỗi JSON cho log
            var logJson = JsonSerializer.Serialize(new LogEntry
            {
                DateTime = DateTime.Now,
                Level = level.GetDescription(),
                App = hostEntry.HostName,
                Host = hostEntry.HostName,
                IP = ipv4Address?.ToString() ?? "IPv4 Address Not Found",
                Message = message,
                Path = path,
                TypeApi = typeApi
            });
            // Kết hợp các chuỗi JSON
            var indexJson = JsonSerializer.Serialize(new RootObject()
            {
                index = new IndexInfo { _index = appName.ToLower() }
            });

            var combinedJson = $"{indexJson}\n{logJson}\n \n";
            batchLogJson += combinedJson;
        }

        HttpClient client = new HttpClient();
        var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eLKSettings.UserName}:{eLKSettings.Password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
        client.BaseAddress = new Uri(eLKSettings.Url);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage response = await client.PostAsync(eLKSettings.Path, new StringContent(batchLogJson, Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var contents = await response.Content.ReadAsStringAsync();
            count += batchSize;
        }
        else
        {
            msg += $"{response.RequestMessage} --";
        }

        _messages.Clear();

        if (string.IsNullOrEmpty(msg))
        {
            return $"Send thành công {count} log - Những request send lỗi {msg}";
        }
        return $"Send thành công {count} log";
    }
}
