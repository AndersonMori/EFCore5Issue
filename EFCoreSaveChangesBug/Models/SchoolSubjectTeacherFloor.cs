using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class SchoolSubjectTeacherFloor
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public long? SchoolSubjectTeacherID { get; set; }
        public int Floor { get; set; }
        public GradeEnum Grade { get; set; }
        public SchoolSubjectTeacher? SchoolSubjectTeacher { get; set; }

    }
}
