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
    public partial class FrmMaasListesi : Form
    {
        public FrmMaasListesi()
        {
            InitializeComponent();
        }

        MaasDTO dto = new MaasDTO(); //yeni bir instance oluşturduk
        private bool combofull;
        MaasDetayDTO detay = new MaasDetayDTO();

        private void FrmMaasListesi_Load(object sender, EventArgs e)
        {
            doldur();
            if(!UserStatic.isAdmin)
            {
                btEkle.Visible = false;
                btnGuncelle.Visible = false;
                btnSil.Visible = false;
                dto.Maaslar = dto.Maaslar.Where(x => x.BakiciID == UserStatic.BakiciID).ToList();
                gridControl1.DataSource = dto.Maaslar;
                panelControl3.Visible = false;
                btnKapat.Location = new Point(293, 29);
            }

        }

        private void doldur()
        {
            dto = MaasBLL.GetAll();
            gridControl1.DataSource = dto.Maaslar;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Kullanıcı No";
            gridView1.Columns[2].Caption = "Ad";
            gridView1.Columns[3].Caption = "Soy Ad";
            //gridView1.Columns[4].Caption = "Departman Ad";
            //gridView1.Columns[5].Caption = "Pozisyon Ad";
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Caption = "Yıl";
            gridView1.Columns[9].Caption = "Ay";
            gridView1.Columns[10].Caption = "Maas";
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Visible = false;
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
            cmbAylar.DataSource = dto.Aylar;
            cmbAylar.DisplayMember = "Ay";
            cmbAylar.ValueMember = "ID";
            cmbAylar.SelectedIndex = -1;
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            FrmMaasBilgileri frm = new FrmMaasBilgileri();
            frm.MdiParent = this.ParentForm;
            frm.isUpdate = false;
            frm.Show();
            temizle();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmMaasBilgileri frm = new FrmMaasBilgileri();
            frm.MdiParent = this.ParentForm;
            frm.isUpdate = true;
            frm.detay = detay;
            frm.Show();
            combofull = false;
            doldur();
            temizle();
        }

        private void btnGuncelle_Click_1(object sender, EventArgs e)
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

        private void cmbAylar_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        List<MaasDetayDTO> listt = new List<MaasDetayDTO>();
        private void simpleButton1_Click(object sender, EventArgs e) // MAAS ARAMA İŞLEMİ 
        {
            listt = dto.Maaslar;
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
            if (cmbAylar.SelectedIndex != -1)
                listt = listt.Where(x => x.MaasAyID == Convert.ToInt32(cmbAylar.SelectedValue)).ToList();

            if (txtYil.Text.Trim() != "")
                listt = listt.Where(x => x.UserNo == Convert.ToInt32(txtYil.Text)).ToList();
            if (rbBuyuk.Checked)
                listt = listt.Where(x => x.MaasMiktar > Convert.ToInt32(txtMaas.Text)).ToList();

            else if (rbKucuk.Checked)
                listt = listt.Where(x => x.MaasMiktar < Convert.ToInt32(txtMaas.Text)).ToList();

            else if (rbEsit.Checked)
                listt = listt.Where(x => x.MaasMiktar == Convert.ToInt32(txtMaas.Text)).ToList();
            gridControl1.DataSource = listt;

        }

        void temizle()
        {
            gridControl1.DataSource = dto.Maaslar;
            txtAd.Text = "";
            txtSoyAd.Text = "";
            txtUserNo.Text = "";
            txtMaas.Text = "";
            txtYil.Text = "";
            cmbAylar.SelectedIndex = -1;
            cmbDepartman.SelectedIndex = -1;
            cmbPozisyonAd.DataSource = dto.Pozisyonlar;
            cmbPozisyonAd.SelectedIndex = -1;
            rbBuyuk.Checked = false;
            rbKucuk.Checked = false;
            rbEsit.Checked = false;
        }
        private void btnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void gridControl1_FocusedViewChanged(object sender, DevExpress.XtraGrid.ViewFocusEventArgs e)
        {

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            detay.MaasID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MaasID"));
            detay.BakiciID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BakiciID"));
            detay.MaasAyID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MaasAyID"));
            detay.MaasYil = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MaasYil"));
            detay.MaasMiktar = Convert.ToInt32(gridView1.GetFocusedRowCellValue("MaasMiktar"));
            detay.UserNo = Convert.ToInt32(gridView1.GetFocusedRowCellValue("UserNo"));
            detay.Ad = gridView1.GetFocusedRowCellValue("Ad").ToString();
            detay.Soyad = gridView1.GetFocusedRowCellValue("Soyad").ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silinsin mi?", "Dikkat", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                MaasBLL.maasSil(detay.MaasID);
                MessageBox.Show("silindi");
                combofull = false;
                doldur();
                temizle();
            }
        }
    }
}
