using SpaNotes.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpaNotes.Services.Services
{
    public interface INoteService : IDisposable
    {
        string UserId { get; set; }
        Note GetNoteById(int noteId);
        IEnumerable<Note> GetAllNotes();
        IEnumerable<Note> GetFilteredNotes(string name, string text, DateTime? date, int page, int notesPerPage, out int notesFound);
        bool RemoveNoteById(int noteId);
        bool UpdateNote(Note note);
        void AddNote(Note note);
        Task CommitAsync();
        void Commit();
    }
}
