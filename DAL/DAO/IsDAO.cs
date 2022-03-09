using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class IsDAO : BakiciDataContext
    {
        public static List<ISDURUM> DurumGetir()
        {
            return db.ISDURUMs.ToList();
        }

        public static void IsEkle(I iss)
        {
            try
            {
                db.Is.InsertOnSubmit(iss);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<IsDetayDTO> IsGetir()
        {
            List<IsDetayDTO> liste = new List<IsDetayDTO>();
            var list = (from i in db.Is
                        join b in db.BAKICIs on i.BakiciID equals b.ID
                        join d in db.DEPARTMANs on b.DepartmanID equals d.ID
                        join pz in db.POZISYONs on b.PozisyonID equals pz.ID
                        join durum in db.ISDURUMs on i.IsDurumID equals durum.ID
                        select new
                        {
                            isID = i.ID,
                            baslik = i.Baslik,
                            icerik = i.Icerik,
                            baslamatarihi = i.IsBaslamaTarih,
                            bitiştarihi = i.IsBitisTarih,
                            bakicino = b.UserNo,
                            ad = b.Ad,
                            soyad = b.Soyad,
                            departman = d.DepartmanAd,
                            pozisyon = pz.PozisyonAd,
                            departmanID = b.DepartmanID,
                            pozisyonId = b.PozisyonID,
                            isdurumu = durum.IsDurumAd,
                            isdurumID = i.IsDurumID,
                            bakiciID = i.BakiciID,
                        }
                      ).OrderBy(x => x.baslamatarihi).ToList();
            foreach (var item in list)
            {
                IsDetayDTO dto = new IsDetayDTO();
                dto.IsID = item.isID;
                dto.Baslik = item.baslik;
                dto.Icerik = item.icerik;
                dto.IsBaslamaTarihi = item.baslamatarihi;
                dto.IsBitisTarihi = item.baslamatarihi;
                dto.UserNo = item.bakicino;
                dto.Ad = item.ad;
                dto.Soyad = item.soyad;
                dto.DepartmanAd = item.departman;
                dto.PozisyonAd = item.pozisyon;
                dto.DepartmanID = item.departmanID;
                dto.PozisyonID = item.pozisyonId;
                dto.IsDurumAd = item.isdurumu;
                dto.IsDurumID = item.isdurumID;
                dto.BakiciID = item.bakiciID;

                liste.Add(dto);
            }
            return liste;
        }

        public static void IsGuncellle(int isID)
        {
            I iss = db.Is.First(x => x.ID == isID);
            if (UserStatic.isAdmin)
                iss.IsDurumID = OnayStatic.Onaylandı;
            else
                iss.IsDurumID = OnayStatic.Tamamlandı;
            db.SubmitChanges();
        }

        public static void IsSil(int isID)
        {
            try
            {
                I iss = db.Is.First(x => x.ID == isID);
                db.Is.DeleteOnSubmit(iss);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void IsGuncell(IsDetayDTO dto)
        {
            try
            {
                I iss = db.Is.First(x => x.ID == dto.IsID);
                iss.Baslik = dto.Baslik;
                iss.Icerik = dto.Icerik;
                iss.BakiciID = dto.BakiciID;
                iss.IsDurumID = dto.IsDurumID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
