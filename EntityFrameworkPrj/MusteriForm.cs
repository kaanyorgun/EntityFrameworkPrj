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
    public partial class MusteriForm : Form
    {
        KullaniciEntities entities = new KullaniciEntities();
        public MusteriForm()
        {
            InitializeComponent();
        }

        private void button1Listele_Click(object sender, EventArgs e)
        {
            tumKayıtlarıGoster();
        }

        private void tumKayıtlarıGoster()
        {
            var musterileri = entities.Customers.ToList();
            dataGridView1.DataSource = musterileri;
            dataGridView1.ClearSelection();
            MetinKutulariniTemizle();
        }

        private void MetinKutulariniTemizle()
        {
            textBoxAd.Clear();
            textBoxSoyad.Clear();
            textBoxSehir.Clear();
            textBoxMusteriId.Text = "0";
        }

        private void MusteriForm_Load(object sender, EventArgs e)
        {
            tumKayıtlarıGoster();
            textBoxMusteriId.Text = "0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.Ad = textBoxAd.Text;
            customer.Soyad = textBoxSoyad.Text;
            customer.Sehir = textBoxSehir.Text;
            try
            {
               entities.Customers.Add(customer);
                entities.SaveChanges();
                MessageBox.Show("Müşteri kaydı eklendi");
                tumKayıtlarıGoster();
            }
            catch(Exception ex)  
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H001\n+" +
                                  ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int musteriId = Convert.ToInt32(textBoxMusteriId.Text);
                var musteri = entities.Customers.Find(musteriId);
                entities.Customers.Remove(musteri);
                entities.SaveChanges();
                MessageBox.Show("Müşteri Kaydı silindi");
                tumKayıtlarıGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H002\n+" +
                                  ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex;
            textBoxMusteriId.Text = dataGridView1.Rows[secilenSatir].Cells[0].Value.ToString();
            textBoxAd.Text = dataGridView1.Rows[secilenSatir].Cells[1].Value.ToString();
            textBoxSoyad.Text = dataGridView1.Rows[secilenSatir].Cells[2].Value.ToString();
            textBoxSehir.Text = dataGridView1.Rows[secilenSatir].Cells[3].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int musteriNo = Convert.ToInt32(textBoxMusteriId.Text);
                var musteri = entities.Customers.Find(musteriNo);
                musteri.Ad = textBoxAd.Text;
                musteri.Soyad = textBoxSoyad.Text;
                musteri.Sehir = textBoxSehir.Text;
                entities.SaveChanges();
                MessageBox.Show("Müşteri bilgileri güncellendi");
                tumKayıtlarıGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,HataKodu:H003\n+"+
                    ex.Message);
            }
        }
    }
}
