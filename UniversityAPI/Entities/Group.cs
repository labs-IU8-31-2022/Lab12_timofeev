using System;
using System.Collections.Generic;

namespace UniversityAPI.Controllers;

public class Group
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }

    public virtual ICollection<Student>? Students { get; } = new List<Student>();

    public Group()
    {
    }
}