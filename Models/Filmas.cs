using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bandomoji_uzduotis.Models
{
    public class Filmas
    {
        [Key]
        public int ID { get; set; }
        public string Pavadinimas { get; set; }
        [DataType(DataType.Date)]
        public DateTime IsleidimoData { get; set; }
        public Zanras FilmoZanras { get; set; }
    }
}
