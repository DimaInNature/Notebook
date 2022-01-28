using Enums;
using System.Windows;

namespace Services;

public class ResourcesDictionaryManagerService
{
    public void SwitchTheme(ThemeType theme)
    {
        Application.Current.Resources
            .MergedDictionaries.Clear();

        var url = new Uri(
            uriString: $@"/Content/Theme/{theme}Theme.xaml",
            uriKind: UriKind.Relative);

        var resource = Application
            .LoadComponent(url) as ResourceDictionary;

        Application.Current.Resources
            .MergedDictionaries.Add(resource);
    }
}
