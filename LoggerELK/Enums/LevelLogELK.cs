using System.ComponentModel;

namespace LoggerELK.Enums;

public enum LevelLogELK 
{
    [Description("INFO")]
    INFO = 0,
    [Description("ERROR")]
    ERROR = 1,
    [Description("DEBUG")]
    DEBUG = 2
}
