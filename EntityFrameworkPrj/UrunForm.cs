using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EntityFrameworkPrj
{
    public partial class UrunForm : Form
     {
        KullaniciEntities entities = new KullaniciEntities();
        public UrunForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tumKayıtlarıGoster();
        }

        private void tumKayıtlarıGoster()
        {
            {
                var urunler = entities.Uruns.ToList();
                dataGridView1.DataSource = urunler;
                dataGridView1.ClearSelection();
                MetinKutulariniTemizle();
            }
        }

        private void MetinKutulariniTemizle()
        {
            textBoxAdi.Clear();
            textBoxFiyati.Clear();
            textBoxUrunId.Text = "0";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Urun urun = new Urun();
            urun.Adi = textBoxAdi.Text;
            urun.Fiyat = Convert.ToInt32(textBoxFiyati.Text);
            
            try
            {
               entities.Uruns.Add(urun);
                entities.SaveChanges();
                MessageBox.Show("Müşteri kaydı eklendi");
                tumKayıtlarıGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H0010\n+" +
                                  ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenSatir = dataGridView1.SelectedCells[0].RowIndex;
            textBoxUrunId.Text = dataGridView1.Rows[secilenSatir].Cells[0].Value.ToString();
            textBoxAdi.Text = dataGridView1.Rows[secilenSatir].Cells[1].Value.ToString();
            textBoxFiyati.Text = dataGridView1.Rows[secilenSatir].Cells[2].Value.ToString();
        }

        private void UrunForm_Load(object sender, EventArgs e)
        {
            tumKayıtlarıGoster();
            textBoxUrunId.Text = "0";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int urunId = Convert.ToInt32(textBoxUrunId.Text);
                var urun = entities.Uruns.Find(urunId);
                entities.Uruns.Remove(urun);
                entities.SaveChanges();
                MessageBox.Show("Ürün Kaydı silindi");
                tumKayıtlarıGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,Hata kodu H011\n+" +
                                  ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int urunId = Convert.ToInt32(textBoxUrunId.Text);
                var urun = entities.Uruns.Find(urunId);
                urun.Adi = textBoxAdi.Text;
                urun.Fiyat = Convert.ToInt32(textBoxFiyati.Text);
                entities.SaveChanges();
                MessageBox.Show("Ürün bilgileri güncellendi");
                tumKayıtlarıGoster();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri Tabanı işlemleri sırasında hata oluştu,HataKodu:H012\n+" +
                    ex.Message);
            }
        }
    }
    

}
