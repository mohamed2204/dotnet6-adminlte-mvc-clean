using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class Specialite
    {
        public Specialite()
        {
            StagePhases = new HashSet<StagePhase>();
            StagiaireStages = new HashSet<StagiaireStage>();
            Stagiaires = new HashSet<Stagiaire>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StagePhase> StagePhases { get; set; }
        public virtual ICollection<StagiaireStage> StagiaireStages { get; set; }
        public virtual ICollection<Stagiaire> Stagiaires { get; set; }
    }
}
