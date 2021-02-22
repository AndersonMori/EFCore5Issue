using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class Teacher
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public string Name { get; set; } = null!;
        public long SubjectID { get; set; }
        public Subject? Subject { get; set; }
        public List<ClassRoomTeacher>? ClassRoomTeacher { get; set; }
    }
}
