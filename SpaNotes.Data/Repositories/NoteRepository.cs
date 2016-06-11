using SpaNotes.Entities;
using System.Data.Entity;

namespace SpaNotes.Data.Repositories
{
    public class NoteRepository : RepositoryBase<Note>
    {
        public NoteRepository(DbContext _dbContext)
            : base(_dbContext)
        { }
    }
}
