using System;
using System.Collections.Generic;


namespace App;

public class Group
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }
    
    public virtual ICollection<Student>? Students { get; } = new List<Student>();
    
    public Group() {}
}
