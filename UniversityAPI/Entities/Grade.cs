using System;
using System.Collections.Generic;

namespace UniversityAPI.Controllers;

public class Grade
{
    public int GradeId { get; set; }

    public int StudentsId { get; set; }

    public int? Russian { get; set; }

    public int? English { get; set; }

    public int? DiscreteMath { get; set; }

    public int? Physics { get; set; }

    public int? Algorithms { get; set; }

    public virtual Student Students { get; set; } = null!;
}