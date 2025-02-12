using System.Collections.Generic;
using AdminLTE.MVC.Models;

namespace AdminLTE.MVC.ViewModel
{
    public class StagiaireViewModel
    {
        public long StagiaireId { get; set; }
        public long? SpecialiteId { get; set; }
        public long? StageId { get; set; }
        public string Grade { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Mle { get; set; }
        public string Cni { get; set; }
        public string NomAr { get; set; }
        public string PrenomAr { get; set; }
        
        public string Branche { get; set; }
        public string Promotion { get; set; }
        //public virtual Specialite Specialite { get; set; }

        //public IEnumerable<string> Roles { get; set; }
    }
}