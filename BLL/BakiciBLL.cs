using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class BakiciBLL
    {
        public static BakiciDTO GetAll()
        {
            BakiciDTO dto  = new BakiciDTO();
            dto.Departmanlar = DepartmanDAO.DepartmanGetir();
            dto.Pozisyonlar = PozisyonDAO.PozisyonGeitr();
            //böyle bir metodumuz zaten var overload edicez..
            //bir önceki metot user no kontrolü içindi.şimdiki ise tüm personeller için.
            dto.Bakicilar = BakiciDAO.BakiciGetir();
            return dto;
        }

        public static void BakiciEkle(BAKICI bkc)
        {
            BakiciDAO.BakiciEkle(bkc);
        }

        public static List<BAKICI> BakiciGetir(int v, string text)
        {
            return BakiciDAO.BakiciGetir(v, text);
        }

        public static bool isUnique(int v)
        {
            List<BAKICI> list = BakiciDAO.BakiciGetir(v);
            if (list.Count > 0)
                return true;
            else
                return false;
        }

        public static void BakiciGüncelle(BakiciDetayDTO bk)
        {
            BakiciDAO.BakiciGuncelle(bk);
        }

        public static void BakiciSil(int bakiciID)
        {
            BakiciDAO.BakiciSil(bakiciID);
        }
    }
}
