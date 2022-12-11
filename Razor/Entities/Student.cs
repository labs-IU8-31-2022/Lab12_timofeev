using System;
using System.Collections.Generic;

namespace Razor.Entities;

public partial class Student
{
    public int StudentId { get; set; }

    public string? StudentName { get; set; }

    public string? Type { get; set; }

    public int? GroupsId { get; set; }

    public virtual ICollection<Grade> Grades { get; } = new List<Grade>();

    public virtual Group? Groups { get; set; }
}
