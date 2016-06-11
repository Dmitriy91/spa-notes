using SpaNotes.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SpaNotes.Data.Configurations
{
    public class NoteConfig : EntityTypeConfiguration<Note>
    {
        public NoteConfig()
        {
            HasKey(n => n.Id);
            Property(n => n.UserId).IsRequired();
            Property(n => n.Name).IsRequired().HasMaxLength(32);
            Property(n => n.Text).IsRequired().HasMaxLength(256);
            Property(n => n.Date).HasColumnType("date");
        }
    }
}
