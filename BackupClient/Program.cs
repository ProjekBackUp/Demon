using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Backup backup = new Backup();
            backup.BackupData(); 
        }
    }
}
