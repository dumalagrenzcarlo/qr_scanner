using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    public class Attendance
    {
        public int? student_id { get; set; }
        public string? lrn { get; set; }
        public string? type { get; set; } //AM or PM
        public string? currentdate { get; set; }
        public string? time{ get; set; }
    }
}
