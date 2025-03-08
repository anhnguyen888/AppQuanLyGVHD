using System;
using System.Collections.Generic;

namespace MyAspNetCoreApp.Models;

public partial class SinhVien
{
    public int SinhVienId { get; set; }

    public string HoTen { get; set; } = null!;

    public string MaSinhVien { get; set; } = null!;

    public string Lop { get; set; } = null!;

    public virtual ICollection<SinhVienGiangVienHuongDan> SinhVienGiangVienHuongDans { get; set; } = new List<SinhVienGiangVienHuongDan>();
}
