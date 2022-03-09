using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;
using System.IO;

namespace Bakici_TakipSistemi
{
    public partial class FrmBakiciBilgileri : Form
    {
        public FrmBakiciBilgileri()
        {
            InitializeComponent();
        }

        private void txtMaas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        BakiciDTO dto = new BakiciDTO();
        public BakiciDetayDTO detay = new BakiciDetayDTO();
        public bool isUpdate = false;
        string resim2 = "";
        private void FrmBakiciBilgileri_Load(object sender, EventArgs e)
        {
            dto = BakiciBLL.GetAll();
            cmbDepartman.DataSource = dto.Departmanlar;
            cmbDepartman.DisplayMember = "DepartmanAd";
            cmbDepartman.ValueMember = "ID";
            cmbDepartman.SelectedIndex = -1;
            cmbPozisyonAd.DataSource = dto.Pozisyonlar;
            cmbPozisyonAd.DisplayMember = "PozisyonAd";
            cmbPozisyonAd.ValueMember = "ID";
            cmbPozisyonAd.SelectedIndex = -1;
            if (dto.Departmanlar.Count > 0)
                combofull = true;
            if(isUpdate)
            {
                txtAd.Text = detay.Ad;
                txtAdress.Text = detay.Adres;
                txtMaas.Text = detay.Maas.ToString();
                txtPassword.Text = detay.Password;
                txtSoyAd.Text = detay.Soyad;
                txtUserNo.Text = detay.UserNo.ToString();
                chisAdmin.Checked = detay.isAdmin;
                cmbDepartman.SelectedValue = detay.DepartmanID;
                cmbPozisyonAd.SelectedValue = detay.PozisyonID;
                resim2 = Application.StartupPath + "\\resimler\\" + detay.Resim;
                txtResim.Text = resim2;
                pictureBox1.Load(resim2);


                if(!UserStatic.isAdmin)
                {
                    txtAd.Enabled = false;
                    txtSoyAd.Enabled = false;
                    txtMaas.Enabled = false;
                    txtUserNo.Enabled = false;
                    chisAdmin.Enabled= false;
                    cmbDepartman.Enabled = false;
                    cmbPozisyonAd.Enabled = false;
                   

                }
            }

        }

        bool combofull = false;
        string resimad = "";
        private void cmbDepartman_SelectedIndexChanged(object sender, EventArgs e)
        {
            //combobox filtreleme işlemi
            if (combofull)
            {
                int departmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                cmbPozisyonAd.DataSource = dto.Pozisyonlar.Where(x => x.DepartmanID == departmanID).ToList();
            }
        }

        private void btnGözat_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtResim.Text = openFileDialog1.FileName;
                resimad = Guid.NewGuid().ToString();
                resimad += openFileDialog1.SafeFileName;

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //EKLEME İŞLEMİ
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("Bakici NO alanı boş geçilemez","Dikkat",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            else if (!isUpdate && BakiciBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
            {
                MessageBox.Show("bu bakıcı  numarasını başka bakıcı kullanıyor.Lütfen değiştiriniz.");
            }

            else if (txtAd.Text.Trim() == "")
                MessageBox.Show("Ad alanı boş geçilemez");
            else if (txtSoyAd.Text.Trim() == "")
                MessageBox.Show("Soyad alanı boş geçilemez");
            else if (txtMaas.Text.Trim() == "")
                MessageBox.Show("Maaş alanı boş geçilemez");
            else if (txtPassword.Text.Trim() == "")
                MessageBox.Show("Şifre alanı boş geçilemez");
            else if (txtResim.Text.Trim() == "")
                MessageBox.Show("Resim boş geçilemez");
            else if (cmbPozisyonAd.SelectedIndex == -1)
                MessageBox.Show("Pozisyon alanı boş geçilemez");
            else if (cmbDepartman.SelectedIndex == -1)
                MessageBox.Show("Departman alanı boş geçilemez");
            else
            {
                if (isUpdate)
                {
                    DialogResult result = MessageBox.Show(" Güncellemek istediğinize emin misiniz?", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(result==DialogResult.Yes)
                    {
                        BakiciDetayDTO bk = new BakiciDetayDTO();
                        bk.BakiciID = detay.BakiciID;
                        bk.UserNo = Convert.ToInt32(txtUserNo.Text);
                        bk.Ad = txtAd.Text;
                        bk.Soyad = txtSoyAd.Text;
                        bk.Maas = Convert.ToInt32(txtMaas.Text);
                        bk.isAdmin = chisAdmin.Checked;
                        bk.Password = txtPassword.Text;
                        bk.PozisyonID = Convert.ToInt32(cmbPozisyonAd.SelectedValue);
                        bk.DepartmanID= Convert.ToInt32(cmbDepartman.SelectedValue);
                        bk.DogumTarihi = dateTimePicker1.Value;
                        bk.Adres = txtAdress.Text;

                        if (resim2 != txtResim.Text)
                        {
                            bk.Resim = resimad;
                            if (File.Exists(resim2))
                                File.Delete(resim2);
                            File.Copy(txtResim.Text, @"resimler\\" + resimad);
                        }

                        else
                            bk.Resim = detay.Resim;
                        BakiciBLL.BakiciGüncelle(bk);
                        MessageBox.Show("Güncellendi,Bakıcı işlemleri bölümünden kontrol edebilirsiniz..","BAKICI TAKİP SİSTEMİ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();
                        FrmMenü frm = new FrmMenü();
                        frm.Show();
                      

                    }
                }

                else
                {

                    BAKICI bkc = new BAKICI();
                    bkc.UserNo = Convert.ToInt32(txtUserNo.Text);
                    bkc.Ad = txtAd.Text;
                    bkc.Soyad = txtSoyAd.Text;
                    bkc.Maas = Convert.ToInt32(txtMaas.Text);
                    bkc.isAdmin = chisAdmin.Checked;
                    bkc.Password = txtPassword.Text;
                    bkc.PozisyonID = Convert.ToInt32(cmbPozisyonAd.SelectedValue);
                    bkc.DepartmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                    bkc.DogumGunu = dateTimePicker1.Value;
                    bkc.Adres = txtAdress.Text;
                    bkc.Resim = Path.GetFileName(txtResim.Text);

                    string destFilePath = @"resimler\\" + bkc.Resim;
                    if (!File.Exists(destFilePath))
                    {
                        File.Copy(txtResim.Text, destFilePath);
                    }

                    BakiciBLL.BakiciEkle(bkc);
                    
                    MessageBox.Show("Bakıcı Eklendi.Bakıcı işlemleri bölümünden kontrol edebilirsiniz..", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Clear form.
                    txtUserNo.Text = "";
                    txtAd.Text = "";
                    txtSoyAd.Text = "";
                    txtMaas.Text = "";
                    chisAdmin.Checked = false;
                    txtPassword.Text = "";
                    cmbPozisyonAd.SelectedIndex = -1;
                    cmbPozisyonAd.DataSource = dto.Pozisyonlar;
                    cmbDepartman.SelectedIndex = -1;
                    dateTimePicker1.Value = DateTime.Today;
                    txtAdress.Clear();
                    txtResim.Text = "";
                    resimad = "";
                    this.Close();
                    FrmMenü frm = new FrmMenü();
                    frm.Show();
                }
            }
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("Bakıcı No boş");
            else if (BakiciBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
            {
                MessageBox.Show("Bu Bakıcı  Numarasını Başka Bakıcı Kullanıyor.Lütfen Değiştiriniz.","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Bu Bakıcı Numarası Kullanılabilir.","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

        }
    }
}
