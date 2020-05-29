namespace SakerhetTjanstGrupp4
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SakerhetsDB : DbContext
    {
        public SakerhetsDB()
            : base("name=SakerhetsDB")
        {
        }

        public virtual DbSet<Anvandare> Anvandares { get; set; }
        public virtual DbSet<PersonalAnv> PersonalAnvs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
