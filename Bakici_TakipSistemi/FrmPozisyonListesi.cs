using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
using DAL.DTO;

namespace Bakici_TakipSistemi
{
    public partial class FrmPozisyonListesi : Form
    {
        public FrmPozisyonListesi()
        {
            InitializeComponent();
        }

        List<PozisyonDTO> liste = new List<PozisyonDTO>();
        PozisyonDetay detay = new PozisyonDetay();
        private void FrmPozisyonListesi_Load(object sender, EventArgs e)
        {
            liste = PozisyonBLL.PozisyonGeitr();
            gridControl1.DataSource = liste;
            gridView3.Columns[0].Caption = "Departman Adı";
            gridView3.Columns[1].Visible = false;
            gridView3.Columns[2].Caption = "Pozisyon Adı";
            gridView3.Columns[3].Visible = false;


        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            FrmPozisyonBilgileri frm = new FrmPozisyonBilgileri();
            frm.MdiParent = this.ParentForm;
            frm.isUpdate = false;
            frm.Show();
            liste = PozisyonBLL.PozisyonGeitr();
            gridControl1.DataSource = liste;
            //this.Close();
           

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmPozisyonBilgileri frm = new FrmPozisyonBilgileri();
            frm.isUpdate = true;
            frm.detay = detay;
            frm.MdiParent = this.ParentForm;
            frm.Show();
            liste = PozisyonBLL.PozisyonGeitr();
            gridControl1.DataSource = liste;

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silinsin mi?", "Dikkat", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PozisyonBLL.PozisyonSil(detay.ID);
                MessageBox.Show("Silindi");
                liste = PozisyonBLL.PozisyonGeitr();
                gridControl1.DataSource = liste;

            }
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            detay.ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            detay.DepartmanID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("DepartmanID"));
            detay.EskiDepartmanID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("EskiDepartmanID"));
            detay.PozisyonAd = gridView3.GetFocusedRowCellValue("PozisyonAd").ToString();

        }
    }
}
