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
    public partial class FrmIsListesi : Form
    {
        public FrmIsListesi()
        {
            InitializeComponent();
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            FrmIsBilgileri frm = new FrmIsBilgileri();
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

        IsDTO dto = new IsDTO();
        private bool combofull;
        IsDetayDTO detay = new IsDetayDTO();

        private void FrmIsListesi_Load(object sender, EventArgs e)
        {
            MessageBox.Show(UserStatic.BakiciID.ToString() + "" + UserStatic.isAdmin.ToString());
            doldur();
            if(!UserStatic.isAdmin)
            {
                btEkle.Visible = false;
                btnGuncelle.Visible = false;
                btnSil.Visible = false;
                btnOnayla.Location = new Point(253, 4);
                btnKapat.Location = new Point(390, 4);
                pnlforAdmin.Visible = false;
                dto.Isler = dto.Isler.Where(x => x.BakiciID == UserStatic.BakiciID).ToList();
                //gridControl1.DataSource = dto.Isler;
                btnOnayla.Text = "Tamamla";
            }
        }

        private void doldur()
        {
            dto = IsBLL.GetAll();
            gridControl1.DataSource = dto.Isler;
            gridView1.Columns[0].Caption = "Başlık";
            gridView1.Columns[1].Caption = "Kullanıcı No";
            gridView1.Columns[2].Caption = "Ad";
            gridView1.Columns[3].Caption = "Soy Ad";
            gridView1.Columns[4].Caption = "Departman Ad";
            gridView1.Columns[5].Caption = "Pozisyon Ad";
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Visible = false;
            gridView1.Columns[9].Caption = "Icerik";
            //gridView1.Columns[10].Visible = false;
            gridView1.Columns[10].Caption = "Durumu";
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Visible = false;
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
            cmbIsDurum.DataSource = dto.Durumlar;
            cmbIsDurum.DisplayMember = "IsDurumAd";
            cmbIsDurum.ValueMember = "ID";
            cmbIsDurum.SelectedIndex = -1;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmIsBilgileri frm = new FrmIsBilgileri();
            frm.MdiParent = this.ParentForm;
            frm.isUpdate = true;
            frm.detay = detay;
            frm.Show();
            combofull = false;
            doldur();
            temizle();
        }

        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                int departmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                cmbPozisyonAd.DataSource = dto.Pozisyonlar.Where(x => x.DepartmanID == departmanID).ToList();
            }
        }

        List<IsDetayDTO> listt = new List<IsDetayDTO>();
        private void btnArama_Click(object sender, EventArgs e)
        {
            listt = dto.Isler;
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
                listt = listt.Where(x => x.IsBaslamaTarihi > Convert.ToDateTime(dpBaslama.Value)
                  && x.IsBaslamaTarihi < Convert.ToDateTime(dpBitis.Value)).ToList();
            if (rbBaslamaTarihi.Checked)
                listt = listt.Where(x => x.IsBitisTarihi > Convert.ToDateTime(dpBaslama.Value)
                  && x.IsBitisTarihi < Convert.ToDateTime(dpBitis.Value)).ToList();
            gridControl1.DataSource = listt;
        }

        private void rbBaslamaTarihi_CheckedChanged(object sender, EventArgs e)
        {

        }

        void temizle()
        {
            txtAd.Text = "";
            txtSoyAd.Text = "";
            txtUserNo.Text = "";
            cmbDepartman.SelectedIndex = -1;
            cmbPozisyonAd.DataSource = dto.Pozisyonlar;
            cmbPozisyonAd.SelectedIndex = -1;
            cmbIsDurum.SelectedIndex = -1;
            dpBaslama.Value = DateTime.Today;
            dpBitis.Value = DateTime.Today;
            gridControl1.DataSource = dto.Isler;
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            detay.IsID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("IsID"));
            detay.UserNo = Convert.ToInt32(gridView1.GetFocusedRowCellValue("UserNo"));
            detay.BakiciID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BakiciID"));
            detay.IsDurumID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("IsDurumID"));
            detay.Baslik = gridView1.GetFocusedRowCellValue("Baslik").ToString();
            detay.Icerik = gridView1.GetFocusedRowCellValue("Icerik").ToString();
            detay.Ad = gridView1.GetFocusedRowCellValue("Ad").ToString();
            detay.Soyad = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            detay.IsBitisTarihi = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("BaslamaTarihi"));
            detay.IsBitisTarihi = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("BitisTarihi"));




        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silinsin mi?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                IsBLL.IsSil(detay.IsID);
                MessageBox.Show("silindi");
                combofull = false;
                doldur();
                temizle();
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            if (UserStatic.isAdmin && detay.IsDurumID == OnayStatic.Onaylandı)
                MessageBox.Show("Bu iş onaylanmış..", "BAKICI TAKİP SİSTEMİ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (UserStatic.isAdmin && detay.IsDurumID == OnayStatic.Bakıcıda &&detay.BakiciID!=UserStatic.BakiciID)
                MessageBox.Show("işin önce tamamlanmış olması gerekiyor.","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else if (!UserStatic.isAdmin && detay.IsDurumID == OnayStatic.Tamamlandı)
                MessageBox.Show("iş zaten tamamlanmış","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else
            {
                IsBLL.IsGuncelle(detay.IsID);
                MessageBox.Show("Onaylandı", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                combofull = false;
                doldur();
                temizle();
            }
        }
    }
}
