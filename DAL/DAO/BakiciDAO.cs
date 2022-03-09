using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class BakiciDAO : BakiciDataContext
    {
        public static void BakiciEkle(BAKICI bkc)
        {
            try
            {
                db.BAKICIs.InsertOnSubmit(bkc);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<BAKICI> BakiciGetir(int v)
        {
            return db.BAKICIs.Where(x => x.UserNo == v).ToList();
        }

        public static List<BakiciDetayDTO> BakiciGetir()
        {
            List<BakiciDetayDTO> liste = new List<BakiciDetayDTO>();


            var list = (from b in db.BAKICIs
                        join d in db.DEPARTMANs on b.DepartmanID equals d.ID
                        join pz in db.POZISYONs on b.PozisyonID equals pz.ID
                        select new
                        {
                            bakiciID = b.ID,
                            ad = b.Ad,
                            soyad = b.Soyad,
                            password = b.Password,
                            departman = d.DepartmanAd,
                            pozisyon = pz.PozisyonAd,
                            departmanId = d.ID,
                            pozisyonID = b.PozisyonID,
                            isAdmin = b.isAdmin,
                            maas = b.Maas,
                            resim = b.Resim,
                            dogumtarihi = b.DogumGunu,
                            adres = b.Adres,
                            bakıcıNo = b.UserNo,

                        }).OrderBy(x => x.bakiciID).ToList();
            foreach (var item in list)
            {
                BakiciDetayDTO dto = new BakiciDetayDTO();
                dto.BakiciID = item.bakiciID;
                dto.Ad = item.ad;
                dto.Soyad = item.soyad;
                dto.Password = item.password;
                dto.DepartmanAd = item.departman;
                dto.PozisyonAd = item.pozisyon;
                dto.DepartmanID = item.departmanId;
                dto.PozisyonID = item.pozisyonID;
                dto.isAdmin = item.isAdmin;
                dto.Maas = item.maas;
                dto.Resim = item.resim;
                dto.DogumTarihi = item.dogumtarihi;
                dto.Adres = item.adres;
                dto.UserNo = item.bakıcıNo;
                liste.Add(dto);
            }
            return liste;


                //dto.DepartmanID = item.departmanId;
               
           
        }

        public static void BakiciGuncelle(PozisyonDetay detay)
        {
            List<BAKICI> list = db.BAKICIs.Where(x => x.PozisyonID == detay.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmanID = detay.DepartmanID;
            }
            db.SubmitChanges();
        }

        public static void BakiciSil(int bakiciID)
        {
            try
            {
                List<IZIN> iz = db.IZINs.Where(x => x.BakiciID == bakiciID).ToList();
                db.IZINs.DeleteAllOnSubmit(iz);
                db.SubmitChanges();
                List<I> iss = db.Is.Where(x => x.BakiciID == bakiciID).ToList();
                db.Is.DeleteAllOnSubmit(iss);
                db.SubmitChanges();
                List<MAA> maas = db.MAAs.Where(x => x.BakiciID == bakiciID).ToList();
                db.MAAs.DeleteAllOnSubmit(maas);
                db.SubmitChanges();
                BAKICI bk = db.BAKICIs.First(x => x.ID == bakiciID);
                db.BAKICIs.DeleteOnSubmit(bk);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void BakiciGuncelle(BakiciDetayDTO bk)
        {
            try
            {
                BAKICI bak = db.BAKICIs.First(x => x.ID == bk.BakiciID);
                bak.UserNo = bk.UserNo;
                bak.Ad = bk.Ad;
                bak.Adres = bk.Adres;
                bak.DepartmanID = bk.DepartmanID;
                bak.DogumGunu = bk.DogumTarihi;
                bak.isAdmin = bk.isAdmin;
                bak.Maas = bk.Maas;
                bak.Password = bk.Password;
                bak.PozisyonID = bk.PozisyonID;
                bak.Resim = bk.Resim;
                bak.Soyad = bk.Soyad;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void BakiciMaasGüncelle(MaasDetayDTO maas)
        {
            try
            {
                BAKICI bk = db.BAKICIs.First(x => x.ID == maas.BakiciID);
                bk.Maas = maas.MaasMiktar;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<BAKICI> BakiciGetir(int v, string text)
        {
            return db.BAKICIs.Where(x => x.UserNo == v && x.Password == text).ToList();
        }
    }
}
