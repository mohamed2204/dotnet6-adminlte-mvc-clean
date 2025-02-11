using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class StagePhase
    {
        public long StageId { get; set; }
        public long PhaseId { get; set; }
        public long SpecialileId { get; set; }
        public string DateDebut { get; set; }
        public string DateFin { get; set; }
        public string AddedOn { get; set; }

        public virtual Phase Phase { get; set; }
        public virtual Specialite Specialile { get; set; }
        public virtual Stage Stage { get; set; }
    }
}
