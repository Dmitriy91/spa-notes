using SpaNotes.Entities;
using System.Data.Entity.ModelConfiguration;

namespace SpaNotes.Data.Configurations
{
    class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfig()
        {
            HasMany(u => u.Notes)
            .WithRequired()
            .HasForeignKey(n => n.UserId);
        }
    }
}
