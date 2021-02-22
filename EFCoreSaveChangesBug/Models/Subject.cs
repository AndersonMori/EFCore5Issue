using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class Subject
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public string Name { get; set; } = null!;
        public List<SchoolSubject>? SchoolSubjects { get; set; }

    }
}
