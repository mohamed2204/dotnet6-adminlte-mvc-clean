using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AdminLTE.MVC.Models;

public partial class Grade
{
    public Grade()
    {
        Stagiaires = new HashSet<Stagiaire>();
    }
    public long Id { get; set; }

    [Required(ErrorMessage = "Le Grade est requis")]
    [Display(Name = "Grade")]
    public string Name { get; set; }

    public virtual ICollection<Stagiaire> Stagiaires { get; set; }

}
