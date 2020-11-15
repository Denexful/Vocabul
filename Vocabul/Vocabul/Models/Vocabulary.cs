using SQLite;
using System;
using System.Collections.Generic;
using System.Text;


namespace Vocabul.Models
{
    [Table("Vocabulary")]
    public class Vocabulary
    {
        [PrimaryKey, Column("VocableID"), AutoIncrement]
        public int VocableID { get; set; }
        [Column("Counter")]
        public int Counter { get; set; }
        [Column("German")]
        public string German { get; set; }
        [Column("English")]
        public string English { get; set; }
        [Column("ModulID")]
        public int ModulID { get; set; }

        public Vocabulary ShallowCopy()
        {
            return (Vocabulary)this.MemberwiseClone();
        }
        public void test()
        {

        }
    }
}
