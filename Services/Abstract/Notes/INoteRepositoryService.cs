namespace Services.Abstract.Notes;

public interface INoteRepositoryService
{
    void AddNote(Note noteItem);
    void DeleteNote(Note noteItem);
    IEnumerable<Note> GetAll();
    Note GetNoteById(int id);
}
