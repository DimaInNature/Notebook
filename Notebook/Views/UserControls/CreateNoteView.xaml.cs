using System.Windows.Controls;

namespace Notebook.Views.UserControls;

public partial class CreateNoteView : UserControl
{
    public CreateNoteView()
    {
        InitializeComponent();
        DataContext = (Application.Current as App)?
            .ServiceProvider.GetService<CreateNoteViewModel>();
    }
}
