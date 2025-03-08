using System;
using System.Collections.Generic;

namespace MyAspNetCoreApp.Models;

public partial class GiangVien
{
    public int GiangVienId { get; set; }

    public string HoTen { get; set; } = null!;

    public string MaGiangVien { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string BoMon { get; set; } = null!;

    public virtual ICollection<SinhVienGiangVienHuongDan> SinhVienGiangVienHuongDans { get; set; } = new List<SinhVienGiangVienHuongDan>();
}
