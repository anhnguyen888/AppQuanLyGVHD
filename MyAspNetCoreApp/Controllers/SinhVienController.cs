using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Models;

public class SinhVienController : Controller
{
    private readonly ThesisManagementDbContext _context;

    public SinhVienController(ThesisManagementDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.SinhViens.ToListAsync());
    }
}