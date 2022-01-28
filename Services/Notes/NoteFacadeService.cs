namespace Services.Notes;

public class NoteFacadeService : INoteFacadeService
{
    private readonly INoteRepositoryService _repositoryService;

    public NoteFacadeService(INoteRepositoryService repository)
    {
        _repositoryService = repository;
    }

    public void Write(Note noteItem) =>
        _repositoryService.AddNote(noteItem);

    public void Erase(Note noteItem) =>
        _repositoryService.DeleteNote(noteItem);

    public IEnumerable<Note> GetAll() =>
        _repositoryService.GetAll();

    public Note GetNoteById(int id) =>
        _repositoryService.GetNoteById(id);
}
