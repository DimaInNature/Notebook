namespace Data.Repositories.Abstract;

public interface INoteRepository
{
    Task AddNoteAsync(Note noteItem);
    Task DeleteNote(Note noteItem);
    Note GetNoteById(int id);
    IEnumerable<Note> GetAll();
}
