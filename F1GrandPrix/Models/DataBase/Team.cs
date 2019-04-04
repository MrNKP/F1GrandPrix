namespace F1GrandPrix.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Team")]
    public partial class Team
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Driver_1 { get; set; }

        public int Driver_2 { get; set; }

        public virtual Driver Driver { get; set; }

        public virtual Driver Driver1 { get; set; }
    }
}
