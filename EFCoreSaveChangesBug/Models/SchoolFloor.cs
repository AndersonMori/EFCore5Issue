using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSaveChangesBug.Models
{
    public class SchoolFloor
    {
        public long Id { get; set; }
        public byte[]? RowVersion { get; set; }
        public long? SchoolID { get; set; }
        public int Floor { get; set; }
        #region Navigations
        public School? School { get; set; }
        #endregion
    }
}
