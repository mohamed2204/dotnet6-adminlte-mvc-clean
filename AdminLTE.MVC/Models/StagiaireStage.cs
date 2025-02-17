using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class StagiaireStage
    {
        public long StagiaireId { get; set; }
        public long StageId { get; set; }
        public long SpecialiteId { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }

        public virtual Specialite Specialite { get; set; }
        public virtual Stagiaire Stagiaire { get; set; }
        //public object Specialite { get; internal set; }
    }
}
