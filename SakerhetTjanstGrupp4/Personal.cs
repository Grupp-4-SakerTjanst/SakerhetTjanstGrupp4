namespace SakerhetTjanstGrupp4
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Personal")]
    public partial class Personal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AnvandarNamn { get; set; }

        [Required]
        [StringLength(50)]
        public string Losenord { get; set; }

        [Required]
        [StringLength(50)]
        public string Fornamn { get; set; }

        [Required]
        [StringLength(50)]
        public string Efternamn { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public int Behorighetniva { get; set; }
    }
}
