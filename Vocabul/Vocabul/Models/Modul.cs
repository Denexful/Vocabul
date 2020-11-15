using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabul.Models
{
 [Table("Modul")]
    public class Modul
    {

        [PrimaryKey, Column("ModulID")]
        public int ModulID { get; set; }
        [Column("Modul_Name")]
        public string Modul_Name { get; set; }
       
    }
}
