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
using DAL;
using BLL;

namespace Bakici_TakipSistemi
{
    public partial class FrmIzinBilgileri : Form
    {
        public FrmIzinBilgileri()
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

        private void txtSure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        TimeSpan sure = new TimeSpan();
        public bool isUpdate = false;
        public IzinDetayDTO detay = new IzinDetayDTO();
        private void FrmIzinBilgileri_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStatic.UserNo.ToString();
            if (isUpdate)
            {
                dpBaslama.Value = detay.BaslamaTarihi;
                dpBitis.Value = detay.BitisTarihi;
                txtSure.Text = detay.Sure.ToString();
                txtAciklama.Text = detay.Aciklama;

            }
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            if (txtSure.Text.Trim() == "")
                MessageBox.Show("süre alanı boş geçilemez", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (Convert.ToInt32(txtSure.Text) <= 0)
                MessageBox.Show("İzin süresi geçersiz.", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtAciklama.Text.Trim() == "")
                MessageBox.Show("Aciklama alanını lütfen doldurunuz.", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {

                if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Güncelleme yapmak istediğinizden emin misiniz?", "Dikkat", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        IzinDetayDTO detaydto = new IzinDetayDTO();
                        detaydto.IzinID = detay.IzinID;
                        detaydto.Aciklama = txtAciklama.Text;
                        detaydto.Sure = Convert.ToInt32(txtSure.Text);
                        detaydto.BaslamaTarihi = dpBaslama.Value;
                        detaydto.BitisTarihi = dpBitis.Value;
                        IzinBLL.IzinGuncelle(detaydto);
                        MessageBox.Show("Güncellendi","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {


                    IZIN iz = new IZIN();
                    iz.BakiciID = UserStatic.BakiciID;
                    iz.IzinDurumID = 1;
                    iz.IzinBitisTarihi = dpBitis.Value;
                    iz.IzinBaslamaTarihi = dpBaslama.Value;
                    iz.Sure = Convert.ToInt32(sure.TotalDays);
                    iz.Acıklama = txtAciklama.Text;
                    IzinBLL.IzinEkle(iz);
                    MessageBox.Show("izin eklendi..Bakıcı izinleri bölümünden kontrol edebilirsiniz...", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    FrmMenü frm = new FrmMenü();
                    frm.Show();
                    dpBaslama.Value = DateTime.Today;
                    dpBitis.Value = DateTime.Today;
                    txtSure.Text = "";
                    txtAciklama.Text = "";
                }
            }
        }

        private void dpBaslama_ValueChanged(object sender, EventArgs e)
        {
            sure = dpBitis.Value.Date - dpBaslama.Value.Date;
            txtSure.Text = sure.TotalDays.ToString();
        }

        private void dpBitis_ValueChanged(object sender, EventArgs e)
        {
            sure = dpBitis.Value.Date - dpBaslama.Value.Date;
            txtSure.Text = sure.TotalDays.ToString();
        }
    }
}
