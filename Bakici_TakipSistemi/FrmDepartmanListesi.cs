using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BLL;
using DAL;

namespace Bakici_TakipSistemi
{
    public partial class FrmDepartmanListesi : Form
    {
        public FrmDepartmanListesi()
        {
            InitializeComponent();
        }

        private void btEkle_Click(object sender, EventArgs e)
        {
            FrmDepartmanBilgileri frm = new FrmDepartmanBilgileri();
            frm.isUpdate = false;
            frm.MdiParent = this.ParentForm;
           
            frm.Show();
            liste = DepartmanBLL.DepartmanGetir();
            gridControl1.DataSource = liste;
            //Refresh();
            //Invalidate(true);
            //gridControl1.Invalidate(true);
            //gridControl1.RefreshDataSource();
            //gridControl1.Refresh();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            Close();
            gridControl1.Invalidate(true);
            gridControl1.RefreshDataSource();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDepartmanBilgileri frm = new FrmDepartmanBilgileri();
            frm.MdiParent = this.ParentForm;
            frm.isUpdate = true;
            frm.detay = detay;

            frm.Show();
            liste = DepartmanBLL.DepartmanGetir();
            gridControl1.DataSource = liste;
        }

        List<DEPARTMAN> liste = new List<DEPARTMAN>();
        DEPARTMAN detay = new DEPARTMAN();
        private void FrmDepartmanListesi_Load(object sender, EventArgs e)
        {
            liste = DepartmanBLL.DepartmanGetir();
            gridControl1.DataSource = liste;
            gridView3.Columns[0].Visible = false;
            gridView3.Columns[1].Caption = "Departman Adı";
        }

        private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            detay.ID = Convert.ToInt32(gridView1.GetFocusedRowCellValue("ID"));
            detay.DepartmanAd = gridView3.GetFocusedRowCellValue("DepartmanAd").ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Silinsin mi?", "Dikkat", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                DepartmanBLL.DepartmanSil(detay.ID);
                MessageBox.Show("Silindi");
                liste = DepartmanBLL.DepartmanGetir();
                gridControl1.DataSource = liste;
                
            }
        }
    }
}