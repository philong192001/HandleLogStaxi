using LoggerELK.Enums;

namespace LoggerELK.Models;

public class ELKInfo
{
    public string Message { get; set; }
    public LevelLogELK Level { get; set; }
    public string AppName { get; set; }
}
