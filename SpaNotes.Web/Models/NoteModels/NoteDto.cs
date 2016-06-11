namespace SpaNotes.Web.Models
{
    // Returned by NotesController actions.
    public class NoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
