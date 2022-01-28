using Enums;
using Models.Serialize;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;

namespace Services;

public class ApplicationConfiguratorService
{
    public const string ConfigurationFilePath = @"ApplicationConfig.json";

    public ThemeType Theme
    {
        get
        {
            if (File.Exists(path: ConfigurationFilePath))
            {
                string json = File.ReadAllText(path: ConfigurationFilePath);

                ApplicationSettings? settings = new();

                if (!string.IsNullOrWhiteSpace(json))
                    settings = JsonConvert.DeserializeObject<ApplicationSettings>(json);

                return settings.Theme;
            }
            else
                return ThemeType.Classic;
        }
        set
        {
            switch (value)
            {
                case ThemeType.Classic or ThemeType.Dark:

                    var data = new ApplicationSettings()
                    {
                        Theme = value,
                        DataBaseConnection = DataBaseConnection
                    };

                    var serializer = new JsonSerializer();
                    serializer.Converters.Add(item: new JavaScriptDateTimeConverter());
                    serializer.NullValueHandling = NullValueHandling.Ignore;

                    using (var stream = new StreamWriter(path: ConfigurationFilePath))
                    {
                        using var writer = new JsonTextWriter(textWriter: stream);
                        serializer.Serialize(jsonWriter: writer, value: data);
                    }
                    break;
            }
        }
    }

    public string DataBaseConnection
    {
        get
        {
            string configurationFilePath = ConfigurationFilePath;

            if (File.Exists(path: configurationFilePath))
            {
                string json = File.ReadAllText(path: configurationFilePath);

                ApplicationSettings? settings = new();

                if (!string.IsNullOrWhiteSpace(json))
                    settings = JsonConvert.DeserializeObject<ApplicationSettings>(json);

                return settings?.DataBaseConnection
                    ?? throw new FileNotFoundException("Connection string is not founded");
            }
            else
                throw new FileNotFoundException("Not found configuration file");
        }
        set
        {
            ApplicationSettings data = new()
            {
                Theme = Theme,
                DataBaseConnection = value
            };

            var serializer = new JsonSerializer();
            serializer.Converters.Add(item: new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using StreamWriter stream = new(path: ConfigurationFilePath);
            using JsonTextWriter writer = new(textWriter: stream);
            serializer.Serialize(jsonWriter: writer, value: data);
        }
    }
}
