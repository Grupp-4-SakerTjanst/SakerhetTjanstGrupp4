namespace SakerhetTjanstGrupp4
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AnvandarModel : DbContext
    {
        public AnvandarModel()
            : base("name=AnvandarModel")
        {
        }

        public virtual DbSet<Anvandare> Anvandare { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
