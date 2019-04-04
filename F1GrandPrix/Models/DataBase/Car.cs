namespace F1GrandPrix.Models.DataBase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Car")]
    public partial class Car
    {
        public Car()
        {
            Drivers = new HashSet<Driver>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Engine { get; set; }

        public int MaxSpeed { get; set; }

        public virtual ICollection<Driver> Drivers { get; set; }
    }
}
