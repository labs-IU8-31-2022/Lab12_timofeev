using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor.Context;
using Razor.Entities;

namespace Razor.Areas.Grades.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Razor.Context.University _context;

        public IndexModel(Razor.Context.University context)
        {
            _context = context;
        }

        public IList<Grade> Grade { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Grades != null)
            {
                Grade = await _context.Grades
                .Include(g => g.Students).ToListAsync();
            }
        }
    }
}
