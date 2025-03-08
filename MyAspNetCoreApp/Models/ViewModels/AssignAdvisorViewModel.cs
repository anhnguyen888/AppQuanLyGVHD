using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAspNetCoreApp.Models.ViewModels
{
    public class AssignAdvisorViewModel
    {
        [Required]
        [Display(Name = "Sinh viên")]
        public int SelectedSinhVienId { get; set; }
        
        [Required]
        [Display(Name = "Giảng viên hướng dẫn")]
        public int SelectedGiangVienId { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Ngày bắt đầu")]
        public DateOnly NgayBatDau { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        
        [Display(Name = "Ghi chú")]
        public string? GhiChu { get; set; }
        
        public List<SinhVien> AvailableSinhViens { get; set; } = new List<SinhVien>();
        public List<GiangVien> AvailableGiangViens { get; set; } = new List<GiangVien>();
    }

    public class AdvisorAssignmentListViewModel
    {
        public List<SinhVienGiangVienHuongDan> Assignments { get; set; } = new List<SinhVienGiangVienHuongDan>();
    }
}
