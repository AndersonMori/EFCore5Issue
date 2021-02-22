using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class SchoolSubjectTeacher
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public long? SchoolID { get; set; }
        public long? ClassRoomTeacherID { get; set; }
        public School? School { get; set; }
        public ClassRoomTeacher? ClassRoomTeacher { get; set; }
        public List<SchoolSubjectTeacherFloor>? SchoolSubjectTeacherFloor { get; set; }

    }
}
