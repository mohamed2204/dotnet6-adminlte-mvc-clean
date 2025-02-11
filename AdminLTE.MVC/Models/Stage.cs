using System;
using System.Collections.Generic;

namespace AdminLTE.MVC.Models
{
    public partial class Stage
    {
        public Stage()
        {
            StagePhases = new HashSet<StagePhase>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Promotion { get; set; }

        public virtual ICollection<StagePhase> StagePhases { get; set; }
    }
}
