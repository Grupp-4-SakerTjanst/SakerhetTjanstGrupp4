namespace SakerhetTjanstGrupp4
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalAnv")]
    public partial class PersonalAnv
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Anvandarnamn { get; set; }

        [Required]
        [StringLength(50)]
        public string Losenord { get; set; }

        public int Behorighetsniva { get; set; }
    }
}
