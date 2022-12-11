using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.Context;

namespace Razor.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    
    private Razor.Context.University _context = new University();

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        ViewData["Groups"] = _context.Groups.Count().ToString();
        ViewData["Students"] = _context.Students.Count();
        return Page();
    }
}