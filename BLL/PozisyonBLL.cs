using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DAO;
using DAL.DTO;

namespace BLL
{
    public class PozisyonBLL
    {
        public static void PozisyonEkle(POZISYON pz)
        {
            PozisyonDAO.DepartmanEkle(pz);
        }

        public static List<PozisyonDTO> PozisyonGeitr()
        {
            return PozisyonDAO.PozisyonGeitr();
        }

        public static void PozisyonSil()
        {
            throw new NotImplementedException();
        }

        public static void PozisyonGuncelle(PozisyonDetay detay, bool control)
        {
            PozisyonDAO.PozisyonGuncelle(detay);
            if (control)
                BakiciDAO.BakiciGuncelle(detay);
        }

        public static void PozisyonSil(int id)
        {
            PozisyonDAO.PozisyonSil(id);
        }
    }
}
