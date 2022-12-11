using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Razor.Context;
using Razor.Entities;

namespace Razor.Areas.Groups.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Razor.Context.University _context;

        public IndexModel(Razor.Context.University context)
        {
            _context = context;
        }

        public IList<Group> Group { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Groups != null)
            {
                Group = await _context.Groups.ToListAsync();
                var temp = new List<Group>();
                temp.AddRange(Group);
                //var temp = Group;
                Group.Clear();
                Group.AddRange(temp.OrderBy(group => group.GroupName));
                //Array.Sort(Group, ((Entities.Group a, Entities.Group b) => a.GroupName < b.GroupName;));
            }
        }
    }
}
