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

namespace Bakici_TakipSistemi
{
    public partial class FrmIzinListesi : Form
    {
        public FrmIzinListesi()
        {
            InitializeComponent();
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            FrmIzinBilgileri frm = new FrmIzinBilgileri();
            frm.MdiParent = this.ParentForm;
            frm.isUpdate = false;
            frm.Show();
            combofull = false;
            doldur();
            temizle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBakiciNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        IzinDTO dto = new IzinDTO();
        private bool combofull;

        void doldur()
        {
        
            dto = IzinBLL.GetAll();
            gridControl1.DataSource = dto.Izinler;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Kullanıcı No";
            gridView1.Columns[2].Caption = "Ad";
            gridView1.Columns[3].Caption = "Soy Ad";
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Caption = "Baslama Tarihi";
            gridView1.Columns[9].Caption = "Bitiş Tarihi";
            gridView1.Columns[10].Visible = false;
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Caption = "İzin Durum";
            //gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Visible = false;
            gridView1.Columns[14].Visible = false;
           
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
            cmbIzinDurum.DataSource = dto.IzinDurumlar;
            cmbIzinDurum.DisplayMember = "IzinDurumAd";
            cmbIzinDurum.ValueMember = "ID";
            cmbIzinDurum.SelectedIndex = -1;
        }

        
        private void FrmIzinListesi_Load(object sender, EventArgs e)
        {
            doldur();
            if(!UserStatic.isAdmin)
            {
                //sadece giriş yapan bakıcının iznini görüntülemek için!!
               
                dto.Izinler = dto.Izinler.Where(x => x.BakiciID == UserStatic.BakiciID).ToList();
                gridControl1.DataSource = dto.Izinler;
                panelControl1.Visible = false;
                btnOnayla.Visible = false;
                btnReddet.Visible = false;
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (detay.IzinID == 0)
                MessageBox.Show("Lütfen İzin Giriniz.", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (detay.IzinDurumID == ComboStatic.Onayla || detay.IzinDurumID == ComboStatic.Reddedildi)
                MessageBox.Show("Onaylanmış veya Reddedilmiş izinler güncellenemez.", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                FrmIzinBilgileri frm = new FrmIzinBilgileri();
                frm.MdiParent = this.ParentForm;
                frm.isUpdate = true;
                frm.detay = detay;
                frm.Show();
                combofull = false;
                doldur();
                temizle();
            }
           
            
          
        }

        private void cmbPozisyonAd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (combofull)
            {
                int departmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                cmbPozisyonAd.DataSource = dto.Pozisyonlar.Where(x => x.DepartmanID == departmanID).ToList();
            }
        }

        List<IzinDetayDTO> listt = new List<IzinDetayDTO>();
        private void btnArama_Click(object sender, EventArgs e)
        {
            listt = dto.Izinler;
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
            if (rbBaslamaTarihi.Checked)
                listt = listt.Where(x => x.BaslamaTarihi >= Convert.ToDateTime(dpBaslama.Value) && x.BaslamaTarihi < Convert.ToDateTime(dpBaslama.Value)).ToList();
            if (rbTeslimTarihi.Checked)
                listt = listt.Where(x => x.BitisTarihi >= Convert.ToDateTime(dpBaslama.Value) && x.BitisTarihi < Convert.ToDateTime(dpBitis.Value)).ToList();
            if (cmbIzinDurum.SelectedIndex != -1)
                listt = listt.Where(x => x.IzinDurumID == Convert.ToInt32(cmbIzinDurum.SelectedValue)).ToList();
            if (txtSure.Text.Trim() != "")
                listt = listt.Where(x => x.Sure == Convert.ToInt32(txtSure.Text)).ToList();
             gridControl1.DataSource = listt;


        }

       void temizle()
        {
            txtAd.Text = "";
            txtSoyAd.Text = "";
            txtUserNo.Text = "";
            cmbDepartman.SelectedIndex = -1;
            cmbPozisyonAd.DataSource = dto.Pozisyonlar;
            cmbPozisyonAd.SelectedIndex = -1;
            gridControl1.DataSource = dto.Izinler;
            rbBaslamaTarihi.Checked = false;
            rbTeslimTarihi.Checked = false;
            txtSure.Text = "";
            cmbIzinDurum.SelectedIndex = -1;
            dpBaslama.Value = DateTime.Today;
            dpBitis.Value = DateTime.Today;
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {

        }

        //Textlere gridden veri çekme işlemi yapıyoruz.
        IzinDetayDTO detay = new IzinDetayDTO();
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            detay.IzinID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("IzinID"));
            detay.BaslamaTarihi = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("BaslamaTarihi"));
            detay.BitisTarihi = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("BitisTarihi"));
            detay.UserNo = Convert.ToInt32(gridView1.GetFocusedRowCellValue("UserNo"));
            detay.Sure= Convert.ToInt32(gridView1.GetFocusedRowCellValue("Sure"));
            detay.IzinID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("IzinID"));
            detay.Aciklama= gridView1.GetFocusedRowCellValue("Aciklama").ToString();
            detay.IzinDurumID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("IzinDurumID"));
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (detay.IzinID == 0)
                MessageBox.Show("Lütfen izin seçiniz", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                IzinBLL.IzinGuncelle(detay.IzinID, ComboStatic.Onayla);
                MessageBox.Show("Onaylandı","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                temizle();
                doldur();
            }
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {

            if (detay.IzinID == 0)
                MessageBox.Show("Lütfen izin seçiniz", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                IzinBLL.IzinGuncelle(detay.IzinID, ComboStatic.Reddedildi);
                MessageBox.Show("Reddedildi", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                temizle();
                doldur();
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silinsin mi?", "Dikkat", MessageBoxButtons.YesNo);
            if(result==DialogResult.Yes)
            {
                if (detay.IzinDurumID == ComboStatic.Onayla || detay.IzinDurumID == ComboStatic.Reddedildi)
                    MessageBox.Show("ONAYLI YA DA REDDEDİLMİŞ İZİNLER SİLİNEMEZ","BAKICI TAKİP SİSTEMİ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                else
                {
                    IzinBLL.IzinSil(detay.IzinID);
                    MessageBox.Show("silindi"," BAKICI TAKİP SİSTEMİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    combofull = false;
                    doldur();
                    temizle();
                }
            }
        }
    }
}
