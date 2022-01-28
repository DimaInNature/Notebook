namespace Services.Abstract.Notes;

public interface INoteFacadeService
{
    void Write(Note noteItem);
    void Erase(Note noteItem);
    IEnumerable<Note> GetAll();
    Note GetNoteById(int id);
}
