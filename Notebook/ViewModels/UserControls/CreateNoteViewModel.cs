namespace Notebook.ViewModels.UserControls;

public class CreateNoteViewModel : BaseViewModel
{
    private readonly INoteFacadeService _noteService;
    private readonly IActiveSessionService _activeSessionService;

    public CreateNoteViewModel(INoteFacadeService noteService,
        IActiveSessionService activeSessionService)
    {
        _noteService = noteService;
        _activeSessionService = activeSessionService;

        InitializeCommands();
    }

    #region Properties

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    private string _title;

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            OnPropertyChanged(nameof(Text));
        }
    }

    private string _text;

    #endregion

    #region Commands

    public ICommand CreateNoteCommand { get; private set; }

    public ICommand ClearTextCommand { get; private set; }

    #endregion

    #region Command Methods

    #region Execute

    private void ExecuteCreateNote(object obj)
    {
        var note = new Note(title: Title,
            text: Text,
            creator: _activeSessionService.GetUser());

        _noteService.Write(noteItem: note);

        Text = Title = string.Empty;

        MessageBox.Show(messageBoxText: "Запись создана", caption: "Успех",
            button: MessageBoxButton.OK, icon: MessageBoxImage.Information);
    }

    private void ExecuteClearText(object obj) =>
        Text = Title = string.Empty;

    #endregion

    #region CanExecute

    private bool CanExecuteCreateNote(object obj) =>
        StringHelper.StrIsNotNullOrWhiteSpace(Text, Title);

    private bool CanExecuteClearText(object obj) =>
        StringHelper.StrIsNotNullOrWhiteSpace(Text) ||
        StringHelper.StrIsNotNullOrWhiteSpace(Title);

    #endregion

    #endregion

    #region Other logic

    private void InitializeCommands()
    {
        CreateNoteCommand = new RelayCommand(executeAction: ExecuteCreateNote,
            canExecuteFunc: CanExecuteCreateNote);

        ClearTextCommand = new RelayCommand(executeAction: ExecuteClearText,
            canExecuteFunc: CanExecuteClearText);
    }

    #endregion

}
