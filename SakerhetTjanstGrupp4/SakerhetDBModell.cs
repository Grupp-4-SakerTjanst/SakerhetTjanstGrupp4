namespace SakerhetTjanstGrupp4
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SakerhetDBModell : DbContext
    {
        public SakerhetDBModell()
            : base("name=SakerhetDBModell")
        {
        }

        public virtual DbSet<Anvandare> Anvandares { get; set; }
 

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
