using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupClient
{
    public class Info
    {
        public int ID { get; set; }
        public string ComputerName { get; set; }
        public bool Authorize { get; set; }
        public string BackupFolderSource { get; set; }
        public string BacklupFolderTarget { get; set; }
    }
}
