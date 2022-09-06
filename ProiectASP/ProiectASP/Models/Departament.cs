using System;
using System.Collections.Generic;

namespace ProiectASP.Models
{
    public partial class Departament
    {
        public Departament()
        {
            Angajats = new HashSet<Angajat>();
        }

        public int Id { get; set; }
        public string? Denumire { get; set; }

        public virtual ICollection<Angajat> Angajats { get; set; }
    }
}
