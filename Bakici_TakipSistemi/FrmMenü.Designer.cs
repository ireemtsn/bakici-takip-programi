namespace Bakici_TakipSistemi
{
    partial class FrmMenü
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMenü));
            this.xtraTabbedMdiManager1 = new DevExpress.XtraTabbedMdi.XtraTabbedMdiManager(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnBakiciIslemleri = new DevExpress.XtraBars.BarButtonItem();
            this.btnDepartmanlar = new DevExpress.XtraBars.BarButtonItem();
            this.btnPozisyonlar = new DevExpress.XtraBars.BarButtonItem();
            this.btnBakiciIsleri = new DevExpress.XtraBars.BarButtonItem();
            this.btnBakiciIzinleri = new DevExpress.XtraBars.BarButtonItem();
            this.BtnBakiciMaasları = new DevExpress.XtraBars.BarButtonItem();
            this.btnOturumKapat = new DevExpress.XtraBars.BarButtonItem();
            this.btnCikis = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabbedMdiManager1
            // 
            this.xtraTabbedMdiManager1.MdiParent = this;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.btnBakiciIslemleri,
            this.btnDepartmanlar,
            this.btnPozisyonlar,
            this.btnBakiciIsleri,
            this.btnBakiciIzinleri,
            this.BtnBakiciMaasları,
            this.btnOturumKapat,
            this.btnCikis});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 10;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.Size = new System.Drawing.Size(1143, 176);
            // 
            // btnBakiciIslemleri
            // 
            this.btnBakiciIslemleri.Caption = "BAKICI İŞLEMLERİ";
            this.btnBakiciIslemleri.Id = 1;
            this.btnBakiciIslemleri.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBakiciIslemleri.ImageOptions.Image")));
            this.btnBakiciIslemleri.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBakiciIslemleri.ImageOptions.LargeImage")));
            this.btnBakiciIslemleri.Name = "btnBakiciIslemleri";
            this.btnBakiciIslemleri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // btnDepartmanlar
            // 
            this.btnDepartmanlar.Caption = "DEPARTMANLAR";
            this.btnDepartmanlar.Id = 2;
            this.btnDepartmanlar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDepartmanlar.ImageOptions.Image")));
            this.btnDepartmanlar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnDepartmanlar.ImageOptions.LargeImage")));
            this.btnDepartmanlar.Name = "btnDepartmanlar";
            this.btnDepartmanlar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // btnPozisyonlar
            // 
            this.btnPozisyonlar.Caption = "POZİSYONLAR";
            this.btnPozisyonlar.Id = 3;
            this.btnPozisyonlar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPozisyonlar.ImageOptions.Image")));
            this.btnPozisyonlar.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnPozisyonlar.ImageOptions.LargeImage")));
            this.btnPozisyonlar.Name = "btnPozisyonlar";
            this.btnPozisyonlar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // btnBakiciIsleri
            // 
            this.btnBakiciIsleri.Caption = "BAKICI İŞLERİ";
            this.btnBakiciIsleri.Id = 5;
            this.btnBakiciIsleri.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBakiciIsleri.ImageOptions.Image")));
            this.btnBakiciIsleri.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBakiciIsleri.ImageOptions.LargeImage")));
            this.btnBakiciIsleri.Name = "btnBakiciIsleri";
            this.btnBakiciIsleri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // btnBakiciIzinleri
            // 
            this.btnBakiciIzinleri.Caption = "BAKICI İZİNLERİ";
            this.btnBakiciIzinleri.Id = 6;
            this.btnBakiciIzinleri.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnBakiciIzinleri.ImageOptions.Image")));
            this.btnBakiciIzinleri.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnBakiciIzinleri.ImageOptions.LargeImage")));
            this.btnBakiciIzinleri.Name = "btnBakiciIzinleri";
            this.btnBakiciIzinleri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // BtnBakiciMaasları
            // 
            this.BtnBakiciMaasları.Caption = "BAKICI MAAŞLARI";
            this.BtnBakiciMaasları.Id = 7;
            this.BtnBakiciMaasları.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnBakiciMaasları.ImageOptions.Image")));
            this.BtnBakiciMaasları.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnBakiciMaasları.ImageOptions.LargeImage")));
            this.BtnBakiciMaasları.Name = "BtnBakiciMaasları";
            this.BtnBakiciMaasları.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem6_ItemClick);
            // 
            // btnOturumKapat
            // 
            this.btnOturumKapat.Caption = "OTURUMU KAPAT";
            this.btnOturumKapat.Id = 8;
            this.btnOturumKapat.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnOturumKapat.ImageOptions.Image")));
            this.btnOturumKapat.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnOturumKapat.ImageOptions.LargeImage")));
            this.btnOturumKapat.Name = "btnOturumKapat";
            this.btnOturumKapat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnOturumKapat_ItemClick);
            // 
            // btnCikis
            // 
            this.btnCikis.Caption = "ÇIKIŞ";
            this.btnCikis.Id = 9;
            this.btnCikis.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnCikis.ImageOptions.Image")));
            this.btnCikis.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("btnCikis.ImageOptions.LargeImage")));
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnCikis_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "MENÜ SAYFASI";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBakiciIslemleri);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDepartmanlar);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnPozisyonlar);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBakiciIsleri);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnBakiciIzinleri);
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnBakiciMaasları);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnOturumKapat);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnCikis);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // FrmMenü
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1143, 655);
            this.Controls.Add(this.ribbonControl1);
            this.IsMdiContainer = true;
            this.Name = "FrmMenü";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMenü";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMenü_FormClosed);
            this.Load += new System.EventHandler(this.FrmMenü_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabbedMdiManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraTabbedMdi.XtraTabbedMdiManager xtraTabbedMdiManager1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.BarButtonItem btnBakiciIslemleri;
        private DevExpress.XtraBars.BarButtonItem btnDepartmanlar;
        private DevExpress.XtraBars.BarButtonItem btnPozisyonlar;
        private DevExpress.XtraBars.BarButtonItem btnBakiciIsleri;
        private DevExpress.XtraBars.BarButtonItem btnBakiciIzinleri;
        private DevExpress.XtraBars.BarButtonItem BtnBakiciMaasları;
        private DevExpress.XtraBars.BarButtonItem btnOturumKapat;
        private DevExpress.XtraBars.BarButtonItem btnCikis;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
    }
}