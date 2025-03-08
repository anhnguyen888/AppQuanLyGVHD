using System;
using System.Collections.Generic;

namespace MyAspNetCoreApp.Models;

public partial class SinhVienGiangVienHuongDan
{
    public int Id { get; set; }

    public int SinhVienId { get; set; }

    public int GiangVienId { get; set; }

    public DateOnly NgayBatDau { get; set; }

    public string? GhiChu { get; set; }

    public virtual GiangVien GiangVien { get; set; } = null!;

    public virtual SinhVien SinhVien { get; set; } = null!;
}
