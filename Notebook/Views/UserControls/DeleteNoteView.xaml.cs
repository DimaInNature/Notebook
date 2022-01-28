using System.Windows.Controls;

namespace Notebook.Views.UserControls;

public partial class DeleteNoteView : UserControl
{
    public DeleteNoteView()
    {
        InitializeComponent();
        DataContext = (Application.Current as App)?
            .ServiceProvider.GetService<DeleteNoteViewModel>();
    }

    public DeleteNoteView(Note deletableNote) : this()
    {
        var vm = DataContext as DeleteNoteViewModel;
        vm.DeletableNote = deletableNote;
    }
}
