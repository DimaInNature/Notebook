namespace Notebook.ViewModels.UserControls;

public class DeleteNoteViewModel : BaseViewModel
{
    private readonly INoteFacadeService _noteService;

    public DeleteNoteViewModel(INoteFacadeService noteService)
    {
        _noteService = noteService;

        InitializeCommands();
    }

    #region Properties

    public Note? DeletableNote
    {
        get => _deletableNote;
        set
        {
            _deletableNote = value;
            OnPropertyChanged(nameof(DeletableNote));
        }
    }

    private Note? _deletableNote;

    #endregion

    #region Commands

    public ICommand DeleteNoteCommand { get; private set; }

    #endregion

    #region Command Methods

    #region Execute

    private void ExecuteDeleteNote(object obj)
    {
        _noteService.Erase(noteItem: DeletableNote);

        DeletableNote = null;

        MessageBox.Show(messageBoxText: "Запись удалена", caption: "Успех",
            button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
    }

    #endregion

    #region CanExecute

    private bool CanExecuteDeleteNote(object obj) =>
        DeletableNote is not null;

    #endregion

    #endregion

    #region Other logic

    private void InitializeCommands()
    {
        DeleteNoteCommand = new RelayCommand(executeAction: ExecuteDeleteNote,
            canExecuteFunc: CanExecuteDeleteNote);
    }

    #endregion
}
