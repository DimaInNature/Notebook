using System.Windows.Controls;

namespace Notebook.Views.UserControls;

public partial class NoteView : UserControl
{
    public NoteView()
    {
        InitializeComponent();
        DataContext = (Application.Current as App)?
            .ServiceProvider.GetService<NoteViewModel>();
    }
}
