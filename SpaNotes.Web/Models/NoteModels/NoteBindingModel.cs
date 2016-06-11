using System;
using System.ComponentModel.DataAnnotations;

namespace SpaNotes.Web.Models
{
    // Used as parameters to NotesController actions.
    public class NoteBindingModel
    {
        public int Id { get; set; }
        [Required, StringLength(32)]
        public string Name { get; set; }
        [Required, StringLength(256)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
