using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AdminLTE.MVC.Migrations;
using Microsoft.Extensions.Hosting;

namespace AdminLTE.MVC.Models
{
    public partial class Matiere
    {
        public int Id { get; set; }

        public string Name { get; set; }

       
    }

    public partial class Stage
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Promotion { get; set; }

        public List<StagePhase> StagePhases { get; }


    }

    public partial class Phase
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<StagePhase> StagePhases { get; }

    }

    public partial class StagePhase
    {
     
        public int StageId { get; set; }

        public int PhaseId { get; set; }

        public Stage Stage { get; set; } = null!;

        public Phase Phase { get; set; } = null!;

    }
}
