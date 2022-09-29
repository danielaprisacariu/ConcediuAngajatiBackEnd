using System;
using System.Collections.Generic;

namespace ProiectASP.Models
{
    public partial class Angajat
    {
        public Angajat()
        {
            ConcediuAngajats = new HashSet<Concediu>();
            ConcediuInlocuitors = new HashSet<Concediu>();
            InverseManager = new HashSet<Angajat>();
        }


        public int Id { get; set; }
        public string Nume { get; set; } = null!;
        public string Prenume { get; set; } = null!;
        public string? Email { get; set; }
        public string? Parola { get; set; }
        public DateTime DataAngajare { get; set; }
        public DateTime DataNasterii { get; set; }
        public string Cnp { get; set; } = null!;
        public string Serie { get; set; } = null!;
        public string No { get; set; } = null!;
        public string? NrTelefon { get; set; }
        public byte[]? Poza { get; set; }
        public bool? EsteAdmin { get; set; }
        public int? ManagerId { get; set; }
        public int? DepartamentId { get; set; }
        public int? FunctieId { get; set; }
        
        public bool? concediat { get; set; }

        public virtual Angajat? Manager { get; set; }

        public virtual Departament? Departament { get; set; }

        public virtual Functie? Functie { get; set; }
        public virtual ICollection<Concediu> ConcediuAngajats { get; set; }
        public virtual ICollection<Concediu> ConcediuInlocuitors { get; set; }
        public virtual ICollection<Angajat> InverseManager { get; set; }

        public Angajat(int id, Departament departament, Functie functie)
        {
            this.Id = id;
            this.Departament = departament;
            this.Functie = functie;
        }

        public Angajat(int id, string nume, string prenume, string email, Angajat manager,Departament departament)
        {
            this.Id = id;
            this.Nume = nume;
            this.Prenume = prenume;
            this.Email = email;
            this.Manager = manager;
            this.Departament = departament;
        }

        public Angajat(int id, string nume, string prenume, string email, DateTime dataAngajare, DateTime dataNastere, string cnp, string serie, string no, string nrTelefon, byte[] poza, int managerId,Departament departament, Functie functie)
        {
            this.Id = id;
            this.Nume = nume;
            this.Prenume = prenume;
            this.Email = email;
            this.DataAngajare = dataNastere;
            this.DataNasterii = dataAngajare;
            this.Cnp = cnp;
            this.Serie = serie;
            this.No = no;
            this.NrTelefon = nrTelefon;
            this.Poza = poza;
            this.Departament = departament;
            this.Functie = functie;
            this.ManagerId = managerId;
        }

        public Angajat(int id, string nume, string prenume, string email, DateTime dataAngajare, DateTime dataNastere, string cnp, string serie, string no, string nrTelefon, byte[] poza, Angajat manager, Departament departament, Functie functie)
        {
            this.Id = id;
            this.Nume = nume;
            this.Prenume = prenume;
            this.Email = email;
            this.DataAngajare = dataNastere;
            this.DataNasterii = dataAngajare;
            this.Cnp = cnp;
            this.Serie = serie;
            this.No = no;
            this.NrTelefon = nrTelefon;
            this.Manager = manager;
            this.Departament = departament;
            this.Functie = functie;
            this.Poza = poza;
        }


    }
}
