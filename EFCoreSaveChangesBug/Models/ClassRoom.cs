using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class ClassRoom
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public string Name { get; set; } = null!;
        public List<ClassRoomTeacher>? ClassRoomTeachers { get; set; }
    }
}
