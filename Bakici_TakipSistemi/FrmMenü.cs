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
    public partial class FrmMenü : Form
    {
        public FrmMenü()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(!UserStatic.isAdmin)
            {
                FrmBakiciBilgileri frm = new FrmBakiciBilgileri();
                BakiciDTO dto = new BakiciDTO();
                dto = BakiciBLL.GetAll();
                BakiciDetayDTO detay = new BakiciDetayDTO();
                detay = dto.Bakicilar.First(x => x.BakiciID == UserStatic.BakiciID);
                frm.isUpdate = true;
                frm.detay = detay;
                frm.MdiParent = this;
                frm.Show();
                //frm.ShowDialog();
                this.Visible = true;
            }
            else
            {
                FrmBakiciListesi frm = new FrmBakiciListesi();
                frm.MdiParent = this;
                frm.Show();
                this.Visible = true;
            }
           
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmDepartmanListesi frm = new FrmDepartmanListesi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPozisyonListesi frm = new FrmPozisyonListesi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmIsListesi frm = new FrmIsListesi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmMaasListesi frm = new FrmMaasListesi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmIzinListesi frm = new FrmIzinListesi();
            frm.MdiParent = this;
            frm.Show();
        }

        private void btnCikis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void btnOturumKapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form1 frm = new Form1();
            this.Hide();
            frm.Show();
        }

        private void FrmMenü_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrmMenü_Load(object sender, EventArgs e)
        {
            if (!UserStatic.isAdmin)
            {

                btnDepartmanlar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnPozisyonlar.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                ribbonPage1.Text = "BAKICI MENÜ SAYFASI";


            }
            else
                ribbonPage1.Text = "ADMİN MENÜ SAYFASI";
        }
    }
}
