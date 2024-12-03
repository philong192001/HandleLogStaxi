using System.Net;

namespace LoggerELK.Models;

public class LogEntry
{
    public DateTime DateTime { get; set; }
    public string Level { get; set; }
    public string App { get; set; } = Dns.GetHostEntry(Dns.GetHostName()).ToString();
    public string Host { get; set; }
    public string IP { get; set; }
    public string Logger { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }
    public string TypeApi { get; set; }
}


public class RootObject
{
    public IndexInfo index { get; set; }
}

public class IndexInfo
{
    public string _index { get; set; }
}

