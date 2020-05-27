namespace SakerhetTjanstGrupp4
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PersonalModel : DbContext
    {
        public PersonalModel()
            : base("name=PersonalModel1")
        {
        }

        public virtual DbSet<PersonalAnv> PersonalAnvs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
