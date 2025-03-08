using MyAspNetCoreApp.Models;
using System.Collections.Generic;

namespace MyAspNetCoreApp.ViewModels
{
    public class AdvisorAssignmentViewModel
    {
        public int StudentId { get; set; }
        public int AdvisorId { get; set; }
        public string Notes { get; set; }
        
        public IEnumerable<SinhVien> Students { get; set; }
        public IEnumerable<GiangVien> Advisors { get; set; }
    }
}
