using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL.DTO;
using BLL;
using System.IO;

namespace Bakici_TakipSistemi
{
    public partial class FrmBakiciListesi : Form
    {
        public FrmBakiciListesi()
        {
            InitializeComponent();
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            FrmBakiciBilgileri frm = new FrmBakiciBilgileri();
            frm.MdiParent = ParentForm;
            frm.isUpdate = false;
            frm.Show();
            combofull = false;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmBakiciBilgileri frm = new FrmBakiciBilgileri();
            frm.MdiParent = ParentForm;
            frm.isUpdate = true;
            frm.detay = detay;
            frm.Show();
            combofull = false;
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        BakiciDTO dto = new BakiciDTO();
        BakiciDetayDTO detay = new BakiciDetayDTO();
        bool combofull = false;
        private void FrmBakiciListesi_Load(object sender, EventArgs e)
        {
            doldur();
        }

        private void doldur()
        {
            dto = BakiciBLL.GetAll();


            gridControl1.DataSource = dto.Bakicilar;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Kullanıcı No";
            gridView1.Columns[2].Caption = "Ad";
            gridView1.Columns[3].Caption = "Soy Ad";
            gridView1.Columns[4].Caption = "Departman Ad";
            gridView1.Columns[5].Caption = "Pozisyon Ad";
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Caption = "Maas";
            gridView1.Columns[9].Visible = false;
            gridView1.Columns[10].Caption = "Admin";
            //gridView1.Columns[10].Visible = false;
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Visible = false;

            gridControl1.DataSource = dto.Bakicilar;
            cmbDepartman.DataSource = dto.Departmanlar;
            cmbDepartman.DisplayMember = "DepartmanAd";
            cmbDepartman.ValueMember = "ID";
            cmbDepartman.SelectedIndex = -1;
            if (dto.Departmanlar.Count > 0)
                combofull = true;
            cmbPozisyonAd.DataSource = dto.Pozisyonlar;
            cmbPozisyonAd.DisplayMember = "PozisyonAd";
            cmbPozisyonAd.ValueMember = "ID";
            cmbPozisyonAd.SelectedIndex = -1;
        }

        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                int departmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                cmbPozisyonAd.DataSource = dto.Pozisyonlar.Where(x => x.DepartmanID == departmanID).ToList();
            }
        }

        List<BakiciDetayDTO> listt = new List<BakiciDetayDTO>();
        //ARAMA İŞLEMİ !!!
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            listt = dto.Bakicilar;
            if (txtUserNo.Text.Trim() != "")
                listt = listt.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            if (txtAd.Text.Trim() != "")
                listt = listt.Where(x => x.Ad.Contains(txtAd.Text)).ToList();
            if (txtSoyAd.Text.Trim() != "")
                listt = listt.Where(x => x.Soyad.Contains(txtSoyAd.Text)).ToList();
            if (cmbDepartman.SelectedIndex != -1)
                listt = listt.Where(x => x.DepartmanID == Convert.ToInt32(cmbDepartman.SelectedValue)).ToList();
            if (cmbPozisyonAd.SelectedIndex != -1)
                listt = listt.Where(x => x.PozisyonID == Convert.ToInt32(cmbPozisyonAd.SelectedValue)).ToList();

               gridControl1.DataSource = listt;
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void Temizle()
        {
            txtAd.Text = "";
            txtSoyAd.Text = "";
            txtUserNo.Text = "";
            cmbDepartman.SelectedIndex = -1;
            cmbPozisyonAd.DataSource = dto.Pozisyonlar;
            cmbPozisyonAd.SelectedIndex = -1;
            gridControl1.DataSource = dto.Bakicilar;
        }

        //detay textlerin içlerini doldurma yeri!!
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            detay.BakiciID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BakiciID"));
            detay.Ad = gridView1.GetFocusedRowCellValue("Ad").ToString();
            detay.Soyad = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            detay.UserNo = Convert.ToInt32(gridView1.GetFocusedRowCellValue("UserNo"));
            detay.DepartmanID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("DepartmanID"));
            detay.PozisyonID= Convert.ToInt32(gridView1.GetFocusedRowCellValue("PoZisyonID"));
            detay.Maas = Convert.ToInt32(gridView1.GetFocusedRowCellValue("Maas"));
            detay.Password = gridView1.GetFocusedRowCellValue("Password").ToString();
            detay.isAdmin = Convert.ToBoolean(gridView1.GetFocusedRowCellValue("IsAdmin"));
            detay.Resim= gridView1.GetFocusedRowCellValue("Resim").ToString();
            detay.Adres = gridView1.GetFocusedRowCellValue("Adres").ToString();
            detay.DogumTarihi =Convert.ToDateTime(gridView1.GetFocusedRowCellValue("DoğumTarihi"));
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bakıcıyı silmek istediğinizden emin misiniz?", "Dikkat", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                BakiciBLL.BakiciSil(detay.BakiciID);
                string resimyol = Application.StartupPath + "\\resimler\\" + detay.Resim;
                File.Delete(resimyol);
                MessageBox.Show("Bakıcı silindi", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                combofull = false;
                Temizle();
                doldur();
               
            }
        }

        private void btnexcel_Click(object sender, EventArgs e)
        {
            //ExcelExport.ExportExcel(gridControl1);
        }
    }
}
