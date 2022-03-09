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
    public class MaasBLL
    {
        public static MaasDTO GetAll()
        {
            MaasDTO dto = new MaasDTO();
            dto.Departmanlar = DepartmanDAO.DepartmanGetir();
            dto.Pozisyonlar = PozisyonDAO.PozisyonGeitr();
            //böyle bir metodumuz zaten var overload edicez..
            //bir önceki metot user no kontrolü içindi.şimdiki ise tüm personeller için.
            dto.Bakicilar = BakiciDAO.BakiciGetir();
            dto.Aylar = MaasDAO.getAylar();
            dto.Maaslar = MaasDAO.MaasGetir();
            return dto;
        }

        public static void MaasEkle(MAA maas,bool control)
        {
            MaasDAO.MaasEkle(maas);
           
            if (control)
            {
                MaasDetayDTO dto = new MaasDetayDTO();
                dto.BakiciID = maas.BakiciID;
                dto.MaasMiktar = maas.Miktar;
                BakiciDAO.BakiciMaasGüncelle(dto);
            }
        }

        public static void MaasGuncelle(MaasDetayDTO maas, bool control)
        {
            MaasDAO.MaasGuncelle(maas);
            if (control)
                BakiciDAO.BakiciMaasGüncelle(maas);
        }

        public static void maasSil(int maasID)
        {
            MaasDAO.maasSil(maasID);
        }
    }
}
