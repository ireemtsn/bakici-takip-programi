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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {

            if (txtUserNo.Text.Trim() == "")
                MessageBox.Show("Kullanıcı numarası boş", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (txtSifre.Text.Trim() == "")
                MessageBox.Show("şifre alanı boş", "Bakıcı Takip Sistemi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                List<BAKICI> list = BakiciBLL.BakiciGetir(Convert.ToInt32(txtUserNo.Text),txtSifre.Text);
                //listenin eleman sayısını kontrol ediyoruz.
                if (list.Count <= 0)
                  MessageBox.Show("Kullanıcı numarası veya şifre hatalı,tekrar deneyiniz.","Bakıcı Takip Sistemi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
               
                  
                else
                {
                    BAKICI bak = list.First();
                    UserStatic.BakiciID = bak.ID;
                    UserStatic.isAdmin = bak.isAdmin;
                    UserStatic.UserNo = bak.UserNo;
                    FrmMenü frm = new FrmMenü();
                    this.Hide();
                    frm.ShowDialog();
                  


                }
            }
           
        }

        private void txtBakiciNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtBakiciNo_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
