using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.MVC.Models
{
    public class Matiere
    {
        public int Id { get; set; }

        public string Name { get; set; }


    }

    //public class Stage
    //{
    //    public int Id { get; set; }
    //    [Required]
    //    public string Name { get; set; }
    //    [Required]
    //    public string Promotion { get; set; }

    //    public List<StagePhase> StagePhases { get; set; }


    //}

    //public class Phase
    //{
    //    public int Id { get; set; }
    //    [Required]
    //    public string Name { get; set; }

    //    public List<StagePhase> StagePhases { get; set; }

    //}

    //public class StagePhase
    //{

    //    public int StageId { get; set; }
    //    public Stage Stage { get; set; } = null!;

    //    public int PhaseId { get; set; }        

    //    public Phase Phase { get; set; } = null!;

    //    public DateTime AddedOn { get; set; }

    //    /*
    //    public int StudentId { get; set; }      // Foreign Key to Student
    //    public Student Student { get; set; }    // Navigation property
    //    public int CourseId { get; set; }       // Foreign Key to Course
    //    public Course Course { get; set; }      // Navigation property
    //    public DateTime EnrollmentDate { get; set; } // Additional Property
    //    */
    //}
}
