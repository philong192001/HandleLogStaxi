using LoggerELK.Enums;
using LoggerELK.Helpers;
using LoggerELK.Models;

namespace TestRefLog;

public class Program
{
    static List<string> _logBatch = new List<string>();
    static object _lock = new object();
    static int batchSize = 20;
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var message = "longpv2 demo 1234123";
        var level = LevelLogELK.INFO;
        var appName = "demoliblog";
        var elkSettings = new ELKSettings()
        {
            Password = "changeme",
            UserName = "elastic",
            Path = "/products/_bulk?pretty",
            Url = "http://10.0.10.220:9200"
        };
        //method 1 - log ngon
        //var a = ELKHelper.SendLog(message, level, appName, elkSettings).Result;
        //Console.WriteLine(a);
        //method 2 - log ngon
        //var messages = new List<string>();
        //messages.Add("longpv12");
        //messages.Add("longpv22");
        //messages.Add("longpv32");
        //messages.Add("longpv42");
        //messages.Add("longpv52");
        //messages.Add("longpv62");
        ////var b = ELKHelper.SendLog(messages,level, appName, elkSettings).Result;
        ////Console.WriteLine(b);
        ////method 3  - log ngon
        ////var c = ELKHelper.SendLogBatch(messages, level, appName, elkSettings).Result;
        ////Console.WriteLine(c);
        //////method 4 
        //ELKHelper.Add("lònng1");
        //ELKHelper.Add("lònng2");
        //ELKHelper.Add("lònng3");
        //ELKHelper.Add("lònng4");
        //var d = ELKHelper.SaveChanges(level, appName, elkSettings).Result;
        //Console.WriteLine(d);

        //test method 5 - gửi 1 list log - call api 1 lần
        AddLogToBatch(message, LevelLogELK.INFO, "/longpv2DEMO", "REQUEST", "longpv2-logging");

        //Tính lãi suất ngân hàng
        //1 tỷ
        var P = 1000000000;
        var r = 0.08 / 12;//Lãi suất ngân hàng
        var M = 30000000; //Số tiền trả góp hàng tháng

        //Tính số tháng
        var n = Math.Log(M / (M - P * r)) / Math.Log(1 + r);
        var n_months = Math.Ceiling(n);

        //Chuyển đổi sang năm và tháng
        var years = n_months / 12;
        var months = n_months % 12;

        Console.WriteLine($"Bạn sẽ phải trả góp trong khoảng {years} năm và {months} tháng.");
    }

    static void AddLogToBatch(string message, LevelLogELK logLevel, string path, string typeApi, string nameApp)
    {
        lock (_lock)
        {
            _logBatch.Add(FormatLog(message, logLevel, path, typeApi, nameApp));

            if (_logBatch.Count >= batchSize)
            {
                SendBatchLog();
            }
        }
    }

    static string FormatLog(string message, LevelLogELK logLevel, string path, string typeApi, string nameApp)
    {
        // Tạo log định dạng phù hợp
        return ELKHelper.CreateLogMessage(message, logLevel, path, typeApi, nameApp, ConfigELK());
    }

    static void SendBatchLog()
    {
        // Gửi log lên ELK
        var result = ELKHelper.SendBulkLog(_logBatch, ConfigELK()).Result;

        // Xóa batch sau khi gửi
        _logBatch.Clear();
    }
}
