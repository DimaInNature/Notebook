using Enums;
using System;

namespace Models.Serialize;

[Serializable]
public class ApplicationSettings
{
    public ThemeType Theme { get; set; }

    public string DataBaseConnection { get; set; }
}