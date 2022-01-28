namespace Notebook.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IActiveSessionService _activeSession;
    private readonly INoteFacadeService _noteFacadeService;

    public MainViewModel(IActiveSessionService activeSessionService, INoteFacadeService noteFacadeService)
    {
        _noteFacadeService = noteFacadeService;
        _activeSession = activeSessionService;

        InitializeCommands();
        InitializeData();
    }

    #region Properties

    #region View

    #region Frame

    public Visibility FrameVisibility
    {
        get => _frameVisibility;
        set
        {
            _frameVisibility = value;
            OnPropertyChanged(nameof(FrameVisibility));
        }
    }

    private Visibility _frameVisibility;

    public object? FrameSource
    {
        get => _frameSource;
        set
        {
            _frameSource = value;
            OnPropertyChanged(nameof(FrameSource));

            MenuIsVisible = value switch
            {
                null => Visibility.Visible,
                _ => Visibility.Collapsed,
            };
        }
    }

    private object _frameSource;

    #endregion

    public Visibility MenuIsVisible
    {
        get => _menuIsVisible;
        set
        {
            _menuIsVisible = value;
            OnPropertyChanged(nameof(MenuIsVisible));
        }
    }

    private Visibility _menuIsVisible;

    #endregion

    #region Data

    public Note SelectedDeleteNote
    {
        get => _selectedDeleteNote;
        set
        {
            _selectedDeleteNote = value;
            OnPropertyChanged(nameof(SelectedDeleteNote));
        }
    }

    private Note _selectedDeleteNote;

    public List<Note> Notes
    {
        get => _notes is null
            ? new List<Note>()
            : _notes;
        set
        {
            _notes = value is null
                ? new List<Note>()
                : value;
            OnPropertyChanged(nameof(Notes));
        }
    }

    private List<Note> _notes;

    #endregion

    #endregion

    #region Commands

    public ICommand LogoutCommand { get; private set; }

    public ICommand SelectDeleteItemCommand { get; private set; }

    public ICommand ShowDeletePageCommand { get; private set; }

    public ICommand ShowViewPageCommand { get; private set; }

    public ICommand ShowCreatePageCommand { get; private set; }

    public ICommand ShowSettingsPageCommand { get; private set; }

    public ICommand ShowMenuPageCommand { get; private set; }

    public ICommand UpdateDataCollectionCommand { get; private set; }

    public ICommand CloseApplicationCommand { get; private set; }

    #endregion

    #region Command Logic

    #region Execute

    private void ExecuteShowViewPage(object obj)
    {
        FrameSource = new NoteView();
    }

    private void ExecuteUpdateDataCollection(object obj) => InitializeData();

    private void ExecuteShowMenuPage(object obj)
    {
        FrameSource = null;
    }

    private void ExecuteSelectDeleteItem(object obj)
    {
        FrameSource = new DeleteNoteView(deletableNote: SelectedDeleteNote);
    }

    private void ExecuteShowCreatePage(object obj)
    {
        FrameSource = new CreateNoteView();
    }

    private void ExecuteShowDeletePage(object obj)
    {
        FrameSource = new DeleteNoteView();
    }

    private void ExecuteShowSettingsPage(object obj)
    {
        FrameSource = new SettingsView();
    }

    private static void ExecuteCloseApplication(object obj) =>
      Application.Current.Shutdown();

    private void ExecuteLogout(object obj)
    {
        new LoginView().Show();

        _activeSession.EndSession();

        (obj as MainView)?.Close();
    }

    #endregion

    #region CanExecute

    private bool CanExecuteShowMenuPage(object obj) =>
        !(FrameSource is null && MenuIsVisible is Visibility.Visible);

    private bool CanExecuteShowViewPage(object obj) =>
        (FrameSource is not NoteView);

    private bool CanExecuteShowCreatePage(object obj) =>
        FrameSource is not CreateNoteView;

    private bool CanExecuteShowDeletePage(object obj) =>
        !(FrameSource is DeleteNoteView && SelectedDeleteNote is null);

    private bool CanExecuteShowSettingsPage(object obj) =>
        FrameSource is not SettingsView;

    private bool CanExecuteSelectDeleteItem(object obj) =>
        SelectedDeleteNote is not null;

    #endregion

    #endregion

    #region Other Logic

    private void InitializeCommands()
    {
        ShowMenuPageCommand = new RelayCommand(executeAction: ExecuteShowMenuPage,
            canExecuteFunc: CanExecuteShowMenuPage);

        ShowViewPageCommand = new RelayCommand(executeAction: ExecuteShowViewPage,
            canExecuteFunc: CanExecuteShowViewPage);

        ShowCreatePageCommand = new RelayCommand(executeAction: ExecuteShowCreatePage,
            canExecuteFunc: CanExecuteShowCreatePage);

        ShowDeletePageCommand = new RelayCommand(executeAction: ExecuteShowDeletePage,
            canExecuteFunc: CanExecuteShowDeletePage);

        ShowSettingsPageCommand = new RelayCommand(executeAction: ExecuteShowSettingsPage,
            canExecuteFunc: CanExecuteShowSettingsPage);

        SelectDeleteItemCommand = new RelayCommand(executeAction: ExecuteSelectDeleteItem,
            canExecuteFunc: CanExecuteSelectDeleteItem);

        LogoutCommand = new RelayCommand(executeAction: ExecuteLogout);

        CloseApplicationCommand = new RelayCommand(executeAction: ExecuteCloseApplication);

        UpdateDataCollectionCommand = new RelayCommand(executeAction: ExecuteUpdateDataCollection);
    }

    private void InitializeData() =>
        Notes = _noteFacadeService.GetAll()
        .Where(note => note.CreatorId == _activeSession.GetUser().Id)
        .ToList();

    #endregion

}
