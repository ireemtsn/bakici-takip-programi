using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
   public class IzinDTO
    {
        public List<DEPARTMAN> Departmanlar { get; set; }
        public List<PozisyonDTO> Pozisyonlar { get; set; }
        //departman ve pozisyon bilgilerini beraber tutabilmek için
        public List<IZINDURUM> IzinDurumlar { get; set; }
        public List<IzinDetayDTO> Izinler { get; set; }
    }
}
