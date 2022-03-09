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

namespace Bakici_TakipSistemi
{
    public partial class FrmPozisyonBilgileri : Form
    {
        public FrmPozisyonBilgileri()
        {
            InitializeComponent();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        List<DEPARTMAN> departmanlar = new List<DEPARTMAN>();
        public bool isUpdate = false;
        public PozisyonDetay detay = new PozisyonDetay();
        private void FrmPozisyonBilgileri_Load(object sender, EventArgs e)
        {
            departmanlar = DAL.DAO.DepartmanDAO.DepartmanGetir();
            cmbDepartman.DataSource = departmanlar;
            cmbDepartman.DisplayMember = "DepartmanAd";
            cmbDepartman.ValueMember = "ID";
            cmbDepartman.SelectedIndex = -1;

            if(isUpdate)
            {
                txtPozisyonAd.Text = detay.PozisyonAd;
                cmbDepartman.SelectedValue = detay.DepartmanID;
            }
           
        }

        private void btEkle_Click(object sender, EventArgs e)
        {

            if (txtPozisyonAd.Text.Trim() == "")
              MessageBox.Show("Lütfen Pozisyon Adı giriniz","Bakıcı Takip sistemi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            else if (cmbDepartman.SelectedIndex == -1)
                MessageBox.Show("Departman Seçiniz");
            else
            {
                if(isUpdate)
                {
                    DialogResult result = MessageBox.Show("Emin misiniz?", "DİKKAT", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if(result==DialogResult.Yes)
                    {
                        detay.PozisyonAd = txtPozisyonAd.Text;
                        detay.DepartmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                        bool control = false;
                        if (detay.EskiDepartmanID != detay.DepartmanID)
                            control = true;
                        PozisyonBLL.PozisyonGuncelle(detay, control);
                        MessageBox.Show("Güncellendi");
                        this.Close();
                        
                       
                    }
                }
                else
                {
                    POZISYON pz = new POZISYON();
                    pz.PozisyonAd = txtPozisyonAd.Text;
                    pz.DepartmanID = Convert.ToInt32(cmbDepartman.SelectedValue);
                    PozisyonBLL.PozisyonEkle(pz);
                    MessageBox.Show("Pozisyon Eklendi.Pozisyonlar bölümünden kontrol edebilirsiniz.", "Bakıcı Takip sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPozisyonAd.Text = "";
                    cmbDepartman.SelectedIndex = -1;
                    this.Close();
                    FrmMenü frm = new FrmMenü();
                    frm.Show();
                }
               
            }
        }
    }
}
