using System;
using System.Collections.Generic;

namespace ProiectASP.Models
{
    public partial class Concediu
    {
        public int Id { get; set; }
        public int? TipConcediuId { get; set; }
        public DateTime DataInceput { get; set; }
        public DateTime DataSfarsit { get; set; }
        public int? InlocuitorId { get; set; }
        public string? Comentarii { get; set; }
        public int? StareConcediuId { get; set; }
        public int? AngajatId { get; set; }
        public int? ZileConcediu { get; set; }

        public virtual Angajat? Angajat { get; set; }
        public virtual Angajat? Inlocuitor { get; set; }
        public virtual StareConcediu? StareConcediu { get; set; }
        public virtual TipConcediu? TipConcediu { get; set; }

        public Concediu()
        {
            
        }

        public Concediu(int id, DateTime dataInceput, DateTime dataSfarsit, string comentarii, Angajat angajat, Angajat inlocuitor, TipConcediu tipConcediu, StareConcediu stareConcediu)
        {
           this.Id = id;
            this.DataInceput = dataInceput;
            this.DataSfarsit = dataSfarsit;
            this.Comentarii = comentarii;
            this.Angajat = angajat;
            this.Inlocuitor = inlocuitor;
            this.TipConcediu = tipConcediu;
            this.StareConcediu = stareConcediu;
           
        }
    }
}
