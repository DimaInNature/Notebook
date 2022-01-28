namespace Services.Notes;

public class NoteRepositoryService : INoteRepositoryService
{
    private readonly INoteRepository _repository;

    public NoteRepositoryService(INoteRepository repository)
    {
        _repository = repository;
    }

    public void AddNote(Note noteItem) =>
        _repository.AddNoteAsync(noteItem);

    public void DeleteNote(Note noteItem) =>
        _repository.DeleteNote(noteItem);

    public IEnumerable<Note> GetAll() =>
        _repository.GetAll();

    public Note GetNoteById(int id) =>
        _repository.GetNoteById(id);
}
