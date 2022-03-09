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
using DAL;

namespace Bakici_TakipSistemi
{
    public partial class FrmIsBilgileri : Form
    {
        public FrmIsBilgileri()
        {
            InitializeComponent();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtBakiciNo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBakiciNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        IsDTO dto = new IsDTO();
        bool combofull = false;

        public bool isUpdate = false;
        public IsDetayDTO detay = new IsDetayDTO();
        private void FrmIsBilgileri_Load(object sender, EventArgs e)
        {
            cmbisDurum.Visible = false;
            label9.Visible = false;
            dto = IsBLL.GetAll();

            gridControl1.DataSource = dto.Bakicilar;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Kullanıcı No";
            gridView1.Columns[2].Caption = "Ad";
            gridView1.Columns[3].Caption = "Soy Ad";
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Visible = false;
            gridView1.Columns[9].Visible = false;
            gridView1.Columns[10].Visible = false;
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

            if (isUpdate)
            {
                cmbisDurum.Visible = false;
                label9.Visible = false;
                txtAd.Text = detay.Ad;
                txtSoyAd.Text = detay.Soyad;
                txtUserNo.Text = detay.UserNo.ToString();
                txtIcerik.Text = detay.Icerik;
                txtBaslik.Text = detay.Baslik;
                cmbisDurum.DataSource = dto.Durumlar;
                cmbisDurum.DisplayMember = "IsDurumAd";
                cmbisDurum.ValueMember = "ID";
                cmbisDurum.SelectedValue = detay.IsDurumID;
            }
        }

        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                int departmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                cmbPozisyonAd.DataSource = dto.Pozisyonlar.Where(x => x.DepartmanID == departmanID).ToList();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            iss = new I();
            iss.BakiciID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BakiciID"));
            txtUserNo.Text = gridView1.GetFocusedRowCellValue("UserNo").ToString();
            txtAd.Text = gridView1.GetFocusedRowCellValue("Ad").ToString();
            txtSoyAd.Text = gridView1.GetFocusedRowCellValue("Soyad").ToString();

        }

        I iss = new I();
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (iss.BakiciID == 0)
                MessageBox.Show("Personel Seciniz...", "Bakici Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtBaslik.Text.Trim() == "")
                MessageBox.Show("Baslik Boş...", "Bakici Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtIcerik.Text.Trim() == "")
                MessageBox.Show("İçerik Alanı Boş...", "Bakici Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Güncelleme yapmak istediğinizden emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        IsDetayDTO dtoo = new IsDetayDTO();
                        if (Convert.ToInt32(txtUserNo.Text) != detay.UserNo)
                            dtoo.UserNo = iss.BakiciID;
                        else
                        dtoo.BakiciID = detay.BakiciID;
                        dtoo.Baslik = txtBaslik.Text;
                        dtoo.Icerik = txtIcerik.Text;
                        dtoo.IsDurumID = Convert.ToInt32(cmbisDurum.SelectedValue);
                        dtoo.IsID = detay.IsID;
                        IsBLL.IsGuncelle(dtoo);
                        MessageBox.Show("Güncellendi","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();

                    }
                }


                else
                {
                    iss.Baslik = txtBaslik.Text;
                    iss.Icerik = txtIcerik.Text;
                    iss.IsDurumID = 1;
                    iss.IsBaslamaTarih = DateTime.Today;
                    IsBLL.IsEkle(iss);
                    MessageBox.Show("iş eklendi.Bakıcı işleri bölümünden kontrol edebilirsiniz.", "Bakici Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtBaslik.Text = "";
                    txtIcerik.Text = "";
                    this.Close();
                    FrmMenü frm = new FrmMenü();
                    frm.Show();
                }
            }
        }
        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {

        }
    }
}
