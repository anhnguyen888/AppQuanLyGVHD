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

    // GET: SinhVien/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sinhVien = await _context.SinhViens
            .FirstOrDefaultAsync(m => m.SinhVienId == id);
        if (sinhVien == null)
        {
            return NotFound();
        }

        return View(sinhVien);
    }

    // GET: SinhVien/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SinhVien/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SinhVienId,HoTen,MaSinhVien,Lop")] SinhVien sinhVien)
    {
        if (ModelState.IsValid)
        {
            _context.Add(sinhVien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(sinhVien);
    }

    // GET: SinhVien/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien == null)
        {
            return NotFound();
        }
        return View(sinhVien);
    }

    // POST: SinhVien/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SinhVienId,HoTen,MaSinhVien,Lop")] SinhVien sinhVien)
    {
        if (id != sinhVien.SinhVienId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sinhVien);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SinhVienExists(sinhVien.SinhVienId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(sinhVien);
    }

    // GET: SinhVien/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sinhVien = await _context.SinhViens
            .FirstOrDefaultAsync(m => m.SinhVienId == id);
        if (sinhVien == null)
        {
            return NotFound();
        }

        return View(sinhVien);
    }

    // POST: SinhVien/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var sinhVien = await _context.SinhViens.FindAsync(id);
        if (sinhVien != null)
        {
            _context.SinhViens.Remove(sinhVien);
        }
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SinhVienExists(int id)
    {
        return _context.SinhViens.Any(e => e.SinhVienId == id);
    }
}