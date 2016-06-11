using SpaNotes.Data.Repositories;
using SpaNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SpaNotes.Services.Services
{
    public class NoteService : INoteService
    {
        private IRepository<Note> _noteRepository;

        public NoteService(IRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public string UserId { get; set; }

        public Note GetNoteById(int noteId)
        {
            return _noteRepository.GetSingle(n => n.Id == noteId && n.UserId == UserId);
        }

        public IEnumerable<Note> GetAllNotes()
        {
            return _noteRepository.GetMany(n => n.UserId == UserId).ToList();
        }

        public IEnumerable<Note> GetFilteredNotes(string name, string text, DateTime? date, int page, int notesPerPage, out int notesFound)
        {
            IQueryable<Note> notes = null;
            Expression<Func<Note, bool>> condition = null;

            if (String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId;
            }
            else if (!String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name);
            }
            else if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(text) && date == null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name) && n.Text.Contains(text);
            }
            else if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name) && n.Text.Contains(text) && n.Date == date.Value;
            }
            else if (String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Text.Contains(text) && n.Date == date.Value;
            }
            else if (String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Date == date.Value;
            }
            else if (!String.IsNullOrWhiteSpace(name) && String.IsNullOrWhiteSpace(text) && date != null)
            {
                condition = n => n.UserId == UserId && n.Name.Contains(name);
            }
            else
            {
                condition = n => n.UserId == UserId && n.Text.Contains(text);
            }

            notes = _noteRepository.GetMany(condition);
            notesFound = notes.Count();

            return notes.OrderBy(note => note.Id).
                Skip((page - 1) * notesPerPage).
                Take(notesPerPage).
                ToList();
        }

        public bool RemoveNoteById(int noteId)
        {
            bool noteExists = _noteRepository.Exists(n => n.Id == noteId && n.UserId == UserId);

            if (noteExists)
            {
                _noteRepository.Delete(new Note { Id = noteId });
                return true;
            }

            return false;
        }

        public bool UpdateNote(Note note)
        {
            bool noteExists = _noteRepository.Exists(n => n.Id == note.Id && n.UserId == UserId);

            if (noteExists)
            {
                note.UserId = UserId;
                _noteRepository.Update(note);
                return true;
            }

            return false;
        }

        public void AddNote(Note note)
        {
            note.UserId = UserId;
            _noteRepository.Add(note);
        }

        async public Task CommitAsync()
        {
            await _noteRepository.CommitAsync();
        }

        public void Commit()
        {
            _noteRepository.Commit();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_noteRepository != null)
                {
                    _noteRepository.Dispose();
                    _noteRepository = null;
                }
            }
        }
    }
}
