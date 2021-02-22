using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class ClassRoomTeacher
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public long? TeacherID { get; set; }
        public long? ClassRoomID { get; set; }
        public Teacher? Teacher { get; set; }
        public ClassRoom? ClassRoom { get; set; }
        public List<SchoolSubjectTeacher>? SchoolSubjectTeachers { get; set; }
    }
}
