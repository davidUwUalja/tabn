using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanacsadoLib
{
    public class Talalkozo
    {
        public int TalalkozoId { get; set; }
        public int TanacsadoId { get; set; }
        public int UgyfelId { get; set; }
        public DateTime Datum { get; set; }
        public TimeSpan Idopont { get; set; }
        public int Idotartam { get; set; }
    }
}
