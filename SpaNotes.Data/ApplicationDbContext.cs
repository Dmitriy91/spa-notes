using Microsoft.AspNet.Identity.EntityFramework;
using SpaNotes.Data.Configurations;
using SpaNotes.Entities;
using System.Data.Entity;

namespace SpaNotes.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public virtual IDbSet<Note> Notes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfig());
            modelBuilder.Configurations.Add(new NoteConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
