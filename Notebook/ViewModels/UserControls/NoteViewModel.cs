namespace Notebook.ViewModels.UserControls;

public class NoteViewModel : BaseViewModel
{
    private readonly IActiveSessionService _activeSessionService;
    private readonly INoteFacadeService _noteFacadeService;

    public NoteViewModel(IActiveSessionService activeSessionService,
        INoteFacadeService noteFacadeService)
    {
        _noteFacadeService = noteFacadeService;
        _activeSessionService = activeSessionService;

        InitializeData();
    }

    #region Properties

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

    public Note SelectedNote
    {
        get => _selectedNote;
        set
        {
            _selectedNote = value;
            OnPropertyChanged(nameof(SelectedNote));
        }
    }

    private Note _selectedNote;

    #endregion

    #region Other logic

    private void InitializeData()
    {
        Notes = _noteFacadeService.GetAll()
            .Where(note => note.CreatorId == _activeSessionService.GetUser().Id)
            .ToList();
    }

    #endregion

}
