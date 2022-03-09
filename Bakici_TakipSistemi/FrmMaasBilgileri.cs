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
    public partial class FrmMaasBilgileri : Form
    {
        public FrmMaasBilgileri()
        {
            InitializeComponent();
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

        MaasDTO dto = new MaasDTO();
        private bool combofull;
        public bool isUpdate = false;
        public MaasDetayDTO detay = new MaasDetayDTO();

        private void FrmMaasBilgileri_Load(object sender, EventArgs e)
        {
            dto = MaasBLL.GetAll();
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
            cmbAylar.DataSource = dto.Aylar;
            cmbAylar.DisplayMember = "Ay";
            cmbAylar.ValueMember = "ID";
            cmbAylar.SelectedIndex = -1;
            txtYil.Text = DateTime.Today.Year.ToString();

            if (isUpdate)
            {
                txtAd.Text = detay.Ad;
                txtSoyAd.Text = detay.Soyad;
                txtMaas.Text = detay.MaasMiktar.ToString();
                txtYil.Text = detay.MaasYil.ToString();
                txtUserNo.Text = detay.UserNo.ToString();
                cmbAylar.SelectedValue = detay.MaasAyID;

            }


        }

        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox filtreleme işlemi
            if (combofull)
            {
                int departmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                cmbPozisyonAd.DataSource = dto.Pozisyonlar.Where(x => x.DepartmanID == departmanID).ToList();
            }
        }

        MAA maas = new MAA();
        int maasmiktar = 0;
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            maas.BakiciID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BakiciID"));
            txtUserNo.Text = gridView1.GetFocusedRowCellValue("UserNo").ToString();
            txtAd.Text = gridView1.GetFocusedRowCellValue("Ad").ToString();
            txtSoyAd.Text = gridView1.GetFocusedRowCellValue("Soyad").ToString();
            txtMaas.Text = gridView1.GetFocusedRowCellValue("Maas").ToString();
            maasmiktar= Convert.ToInt32(gridView1.GetFocusedRowCellValue("MaasMiktar"));
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            if (maas.BakiciID == 0)
                MessageBox.Show("personel seçiniz", "bakıcı takip sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtMaas.Text.Trim() == "")
                MessageBox.Show("maas alanını doldurunuz", "bakıcı takip sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (labelYil.Text.Trim() == "")
                MessageBox.Show("yıl alanını doldurunuz", "bakıcı takip sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (cmbAylar.SelectedIndex == -1)
                MessageBox.Show("lütfen ay seçiniz","Bakıcı takip sistemi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            else
            {
                bool control = false;
                if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Güncelleme yapmak istediğinizden emin misiniz?", "Dikkat!", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        MaasDetayDTO maas = new MaasDetayDTO();
                        maas.MaasID = detay.MaasID;
                        maas.MaasAyID = Convert.ToInt32(cmbAylar.SelectedValue);
                        maas.MaasYil = Convert.ToInt32(txtYil.Text);
                        maas.EskiMaas = detay.MaasMiktar;
                        maas.BakiciID = detay.BakiciID;
                        maas.MaasMiktar = Convert.ToInt32(txtMaas.Text);

                       

                        if (maas.MaasMiktar > maas.EskiMaas)

                        MaasBLL.MaasGuncelle(maas, control);
                        MessageBox.Show("maas güncellendi,maaş bölümünden kontrol edebilirisiniz..");
                        this.Close();
                        FrmMenü frm = new FrmMenü();
                        frm.Show();
                    }
                }
                else
                {
                    if (Convert.ToInt32(txtMaas.Text) > maasmiktar)
                    control = true;
                    maas.Ay_ID = Convert.ToInt32(cmbAylar.SelectedValue);
                    maas.Miktar = Convert.ToInt32(txtMaas.Text);
                    maas.YIL = Convert.ToInt32(txtYil.Text);
                    MaasBLL.MaasEkle(maas,control);
                    MessageBox.Show("Maas eklendi,maaş bölümünden kontrol edebilirsiniz.", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMaas.Text = "";
                    this.Close();
                    FrmMenü frm = new FrmMenü();
                    frm.Show();
                    maas = new MAA();
                }

               
            }

        }
    }
}