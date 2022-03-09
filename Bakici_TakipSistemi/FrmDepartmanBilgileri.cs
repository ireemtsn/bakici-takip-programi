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
    public partial class FrmDepartmanBilgileri : Form
    {
        public FrmDepartmanBilgileri()
        {
            InitializeComponent();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtDepartmanAd.Text.Trim() == "")
            MessageBox.Show("Lütfen Departman Adı Griniz", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                DEPARTMAN dpt = new DEPARTMAN();

                if(isUpdate)
                {
                    DialogResult result = MessageBox.Show("emin misiniz", "dikkat", MessageBoxButtons.YesNo);
                    if(result==DialogResult.Yes)
                    {
                        dpt.DepartmanAd = txtDepartmanAd.Text;
                        dpt.ID = detay.ID;
                        DepartmanBLL.DepartmanGuncelle(dpt);
                        MessageBox.Show("Güncellendi");
                        this.Close();
                    }
                }

                else
                {
                    dpt.DepartmanAd = txtDepartmanAd.Text;
                    DepartmanBLL.DepartmanEkle(dpt);
                    MessageBox.Show("Departman Eklendi.Departmanlar bölümünden kontrol edebilirsiniz.", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDepartmanAd.Text = "";
                    this.Close();
                    FrmMenü frm = new FrmMenü();
                    //FrmDepartmanListesi frm = new FrmDepartmanListesi();
                    frm.Show();

                    //FrmDepartmanListesi frm = new FrmDepartmanListesi();
                    //frm.MdiParent = this.ParentForm;
                    //frm.Show();
                }



            }
            
        }
        public bool isUpdate = false;
        public DEPARTMAN detay = new DEPARTMAN();
        private void FrmDepartmanBilgileri_Load(object sender, EventArgs e)
        {
            if (isUpdate)
                txtDepartmanAd.Text = detay.DepartmanAd;
        }
    }
}
