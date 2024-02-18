using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameworkPrj
{
    public partial class SiparisForm : Form
    {

        KullaniciEntities entities = new KullaniciEntities();

        public SiparisForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tumKayitlariGoster();
        }

        private void tumKayitlariGoster()
        {
          var siparisler = (from siparis in entities.Siparis
                            select new
                            {
                                siparis.SiparisNo,
                                siparis.Tarih,
                                siparis.MüsteriId,
                                siparis.UrunId, 
                                siparis.Adet
                           
                               }).ToList();
            dataGridView1.DataSource = siparisler;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Siparis siparis = new Siparis();
                siparis.Tarih = dateTimePicker1.Value;
                siparis.MusteriId = Convert.ToInt32(comboBoxMusteriId.SelectedValue.ToString());
                siparis.UrunId = Convert.ToInt32(comboBoxUrunId.SelectedValue.ToString());
                siparis.Adet = Convert.ToInt32(textBoxAdet.Text);
                entities.SaveChanges();

                tumKayitlariGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H021\n+" +
                                  ex.Message);
            }

        }

        private void SiparisForm_Load(object sender, EventArgs e)
        {
            tumKayitlariGoster();
            var musteriler = (from musteri in entities.Customers
                              select new
                              {
                                  musteri.MusteriId,
                                  musteri.Ad,
                                  musteri.Soyad
                              }).ToList();
            comboBoxMusteriId.ValueMember = "MusteriId";
            comboBoxMusteriId.DisplayMember = "Ad" + "Soyad";
            comboBoxMusteriId.DataSource = musteriler;

            var urunler = (from urun in entities.Uruns
                              select new
                              {
                                  urun.UrunId,
                                  urun.UrunAdi,
                                  urun.Fiyati,
                              }).ToList();
            comboBoxUrunId.ValueMember = "UrunId";
            comboBoxUrunId.DisplayMember = "Adi" + "Fiyati";
            comboBoxUrunId.DataSource = urunler;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex;
            textBoxSiparisNo.Text = dataGridView1.Rows[secilenSatir].Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[secilenSatir].Cells[1].Value.ToString();
            int musteriId = Convert.ToInt32(dataGridView1.Rows[secilenSatir].Cells[2].Value.ToString());
            comboBoxMusteriId.SelectedValue = musteriId;
            int urunId = Convert.ToInt32(dataGridView1.Rows[secilenSatir].Cells[3].Value.ToString());
            comboBoxUrunId.SelectedValue = urunId;
            textBoxAdet.Text = dataGridView1.Rows[secilenSatir].Cells[4].Value.ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                int siparisNo = Convert.ToInt32(textBoxSiparisNo.Text);
                var siparis = entities.Siparis.Find(siparisNo);
                siparis.Tarih = dateTimePicker1.Value;
                siparis.MusteriId = Convert.ToInt32(comboBoxMusteriId.SelectedValue.ToString());
                siparis.UrunId = Convert.ToInt32(comboBoxUrunId.SelectedValue.ToString());
                siparis.Adet = Convert.ToInt32(textBoxAdet.Text);
                entities.SaveChanges();
                tumKayitlariGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H022\n+" +
                                  ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                int siparisNo = Convert.ToInt32(textBoxSiparisNo.Text);
                var siparis = entities.Siparis.Find(siparisNo);
                entities.Siparis.Remove(siparis);
                entities.SaveChanges();
                tumKayitlariGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H020\n+" +
                                  ex.Message);
            }
        }
    }
        }





