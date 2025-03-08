using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Models;
using MyAspNetCoreApp.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MyAspNetCoreApp.Controllers
{
    public class AdvisorAssignmentController : Controller
    {
        private readonly ThesisManagementDbContext _context;

        public AdvisorAssignmentController(ThesisManagementDbContext context)
        {
            _context = context;
        }

        // GET: AdvisorAssignment/Index
        public async Task<IActionResult> Index()
        {
            var assignments = await _context.SinhVienGiangVienHuongDans
                .Include(s => s.SinhVien)
                .Include(s => s.GiangVien)
                .ToListAsync();

            var viewModel = new AdvisorAssignmentListViewModel
            {
                Assignments = assignments
            };

            return View(viewModel);
        }

        // GET: AdvisorAssignment/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new AssignAdvisorViewModel
            {
                AvailableSinhViens = await _context.SinhViens
                    .OrderBy(sv => sv.MaSinhVien)
                    .ToListAsync(),
                AvailableGiangViens = await _context.GiangViens
                    .OrderBy(gv => gv.MaGiangVien)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // POST: AdvisorAssignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AssignAdvisorViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the assignment already exists
                var existingAssignment = await _context.SinhVienGiangVienHuongDans
                    .FirstOrDefaultAsync(a => 
                        a.SinhVienId == model.SelectedSinhVienId && 
                        a.GiangVienId == model.SelectedGiangVienId);

                if (existingAssignment != null)
                {
                    ModelState.AddModelError("", "Sinh viên này đã được phân công cho giảng viên này.");
                }
                else
                {
                    var assignment = new SinhVienGiangVienHuongDan
                    {
                        SinhVienId = model.SelectedSinhVienId,
                        GiangVienId = model.SelectedGiangVienId,
                        NgayBatDau = model.NgayBatDau,
                        GhiChu = model.GhiChu
                    };

                    _context.Add(assignment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // If we got this far, something failed, redisplay form
            model.AvailableSinhViens = await _context.SinhViens
                .OrderBy(sv => sv.MaSinhVien)
                .ToListAsync();
            model.AvailableGiangViens = await _context.GiangViens
                .OrderBy(gv => gv.MaGiangVien)
                .ToListAsync();

            return View(model);
        }

        // GET: AdvisorAssignment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.SinhVienGiangVienHuongDans
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (assignment == null)
            {
                return NotFound();
            }

            var viewModel = new AssignAdvisorViewModel
            {
                SelectedSinhVienId = assignment.SinhVienId,
                SelectedGiangVienId = assignment.GiangVienId,
                NgayBatDau = assignment.NgayBatDau,
                GhiChu = assignment.GhiChu,
                AvailableSinhViens = await _context.SinhViens
                    .OrderBy(sv => sv.MaSinhVien)
                    .ToListAsync(),
                AvailableGiangViens = await _context.GiangViens
                    .OrderBy(gv => gv.MaGiangVien)
                    .ToListAsync()
            };

            ViewData["AssignmentId"] = id;
            return View(viewModel);
        }

        // POST: AdvisorAssignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AssignAdvisorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var assignment = await _context.SinhVienGiangVienHuongDans.FindAsync(id);
                    if (assignment == null)
                    {
                        return NotFound();
                    }

                    assignment.SinhVienId = model.SelectedSinhVienId;
                    assignment.GiangVienId = model.SelectedGiangVienId;
                    assignment.NgayBatDau = model.NgayBatDau;
                    assignment.GhiChu = model.GhiChu;

                    _context.Update(assignment);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            model.AvailableSinhViens = await _context.SinhViens
                .OrderBy(sv => sv.MaSinhVien)
                .ToListAsync();
            model.AvailableGiangViens = await _context.GiangViens
                .OrderBy(gv => gv.MaGiangVien)
                .ToListAsync();

            ViewData["AssignmentId"] = id;
            return View(model);
        }

        // GET: AdvisorAssignment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignment = await _context.SinhVienGiangVienHuongDans
                .Include(a => a.SinhVien)
                .Include(a => a.GiangVien)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (assignment == null)
            {
                return NotFound();
            }

            return View(assignment);
        }

        // POST: AdvisorAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var assignment = await _context.SinhVienGiangVienHuongDans.FindAsync(id);
            if (assignment != null)
            {
                _context.SinhVienGiangVienHuongDans.Remove(assignment);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool AssignmentExists(int id)
        {
            return _context.SinhVienGiangVienHuongDans.Any(e => e.Id == id);
        }
    }
}
