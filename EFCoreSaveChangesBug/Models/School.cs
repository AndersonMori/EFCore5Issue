using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class School
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public string Name { get; set; } = null!;
        #region Collections
        public List<SchoolSubject>? SchoolSubject { get; set; }
        public List<SchoolFloor>? Floors { get; set; }
        public List<SchoolSubjectTeacher>? SchoolSubjectTeacher { get; set; }
        #endregion
    }
}
