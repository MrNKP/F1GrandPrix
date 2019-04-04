namespace F1GrandPrix.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Driver")]
    public partial class Driver
    {
        public Driver()
        {
            Teams = new HashSet<Team>();
            Teams1 = new HashSet<Team>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        public int Car_ID { get; set; }

        public virtual Car Car { get; set; }

        public virtual ICollection<Team> Teams { get; set; }

        public virtual ICollection<Team> Teams1 { get; set; }
    }
}
