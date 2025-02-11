using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class StagiaireStage
    {
        public long StagiaireId { get; set; }
        public long StageId { get; set; }
        public long SpecilaiteId { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }

        public virtual Specialite Specilaite { get; set; }
        public virtual Stagiaire Stagiaire { get; set; }
    }
}
