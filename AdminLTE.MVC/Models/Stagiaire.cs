using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class Stagiaire
    {
        public int Id { get; set; }

        public string Grade { get; set; }

        public string Prenom { get; set; }

        public string Nom { get; set; }

        public string Mle { get; set; }

        public string Cin { get; set; }

        public string NomAr { get; set; }

        public string PrenomAr { get; set; }

        public string Spec { get; set; }

        public string Branche { get; set; }
    }
}
