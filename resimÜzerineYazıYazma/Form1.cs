using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace resimÜzerineYazıYazma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string resim;
        Bitmap bmp;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnResimSec_Click(object sender, EventArgs e)
        {
            // Resim seçilmediğinde hata vermemesi için kontrol eklendi
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                resim = openFileDialog1.FileName;
                bmp = new Bitmap(resim);
                pictureBox1.Image = bmp;  // Seçilen resmi direkt olarak göster
            }
            else
            {
                MessageBox.Show("Lütfen bir resim dosyası seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        Color renk = Color.Black; // Varsayılan renk siyah
        private void btnRenkSec_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                renk = colorDialog1.Color;
            }
        }

        private void btnYazdir_Click(object sender, EventArgs e)
        {
            if (bmp == null)
            {
                MessageBox.Show("Lütfen önce bir resim dosyası seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtMetin.Text) )
            {
                MessageBox.Show("Lütfen yazdırılacak metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Font boyutunu kontrol et ve varsayılan olarak 12 ayarla
            int fontSize = 12;
            if (!int.TryParse(txtBoyut.Text, out fontSize) || fontSize <= 0)
            {
                MessageBox.Show("Geçerli bir font boyutu girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fontSize = 12; // Hatalı giriş olursa varsayılan font boyutu kullanılır
            }

            // Grafik üzerinde yazıyı çiz
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                Font font = new Font("Segoe UI", fontSize, FontStyle.Bold);
                SizeF stringSize1 = gr.MeasureString(txtMetin.Text, font);
              

                // İlk yazıyı ortalamak için x ve y hesaplama
                float x1 = (bmp.Width - stringSize1.Width) / 2;
                float y1 = (bmp.Height - stringSize1.Height) / 2;

              

                // İlk yazıyı çiz
                gr.DrawString(txtMetin.Text, font, new SolidBrush(renk), x1, y1);

            }

            pictureBox1.Image = bmp;  // Güncellenmiş resmi göster
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (bmp == null)
            {
                MessageBox.Show("Önce bir resim üzerine metin yazdırın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;

                // Dosya uzantısını kontrol et, yoksa varsayılan olarak ".png" ekle
                if (!fileName.EndsWith(".png", StringComparison.OrdinalIgnoreCase) &&
                    !fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) &&
                    !fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase))
                {
                    fileName += ".png"; // Varsayılan olarak PNG formatı
                }

                try
                {
                    bmp.Save(fileName);
                    MessageBox.Show("Resim başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya kaydedilirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
