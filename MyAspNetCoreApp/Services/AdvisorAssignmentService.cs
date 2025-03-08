using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyAspNetCoreApp.Models;

namespace MyAspNetCoreApp.Services
{
    public class AdvisorAssignmentService
    {
        private readonly ThesisManagementDbContext _context;

        public AdvisorAssignmentService(ThesisManagementDbContext context)
        {
            _context = context;
        }

        public async Task<SinhVienGiangVienHuongDan> AssignAdvisorToStudentAsync(int sinhVienId, int giangVienId, string ghiChu = null)
        {
            // Check if student and advisor exist
            var student = await _context.SinhViens.FindAsync(sinhVienId);
            if (student == null)
            {
                throw new ArgumentException("Sinh viên không tồn tại");
            }

            var advisor = await _context.GiangViens.FindAsync(giangVienId);
            if (advisor == null)
            {
                throw new ArgumentException("Giảng viên không tồn tại");
            }

            // Check if assignment already exists
            var existingAssignment = await _context.SinhVienGiangVienHuongDans
                .FirstOrDefaultAsync(x => x.SinhVienId == sinhVienId && x.GiangVienId == giangVienId);
            
            if (existingAssignment != null)
            {
                throw new InvalidOperationException("Giảng viên này đã được phân công hướng dẫn sinh viên này");
            }

            // Create new assignment
            var assignment = new SinhVienGiangVienHuongDan
            {
                SinhVienId = sinhVienId,
                GiangVienId = giangVienId,
                NgayBatDau = DateOnly.FromDateTime(DateTime.Today),
                GhiChu = ghiChu
            };

            await _context.SinhVienGiangVienHuongDans.AddAsync(assignment);
            await _context.SaveChangesAsync();

            return assignment;
        }
    }
}
