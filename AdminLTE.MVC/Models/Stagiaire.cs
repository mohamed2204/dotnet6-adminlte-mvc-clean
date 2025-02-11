using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class Stagiaire
    {
        public Stagiaire()
        {
            StagiaireStages = new HashSet<StagiaireStage>();
        }

        public long Id { get; set; }
        public string Grade { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Mle { get; set; }
        public string Cin { get; set; }
        public string NomAr { get; set; }
        public string PrenomAr { get; set; }
        public long? SpecialiteId { get; set; }
        public string Branche { get; set; }
        public string Promotion { get; set; }

        public virtual Specialite Specialite { get; set; }
        public virtual ICollection<StagiaireStage> StagiaireStages { get; set; }
    }
}
