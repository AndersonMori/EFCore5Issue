using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class SchoolSubject
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public long? SchoolID { get; set; }
        public long? SubjectID { get; set; }

        #region Navigations
        public School? School { get; set; }
        public Subject? Subject { get; set; }
        #endregion Navigations
    }
}
