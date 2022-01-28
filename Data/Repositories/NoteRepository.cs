namespace Data.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly ApplicationContext _context;

    public NoteRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task AddNoteAsync(Note noteItem)
    {
        await _context.Notes.AddAsync(entity: noteItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteNote(Note noteItem)
    {
        _context.Notes.Attach(entity: noteItem);
        _context.Notes.Remove(entity: noteItem);
        await _context.SaveChangesAsync();
    }

    public IEnumerable<Note> GetAll() => _context.Notes;

    public Note? GetNoteById(int id) =>
        _context.Notes.FirstOrDefault(note => note.Id == id);
}
