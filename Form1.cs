using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Default olarak alınan gap, match, mismatch değerleri
            this.textBox3.Text = "1";
            this.textBox4.Text = "-1";
            this.textBox5.Text = "-2";
        }

        static string seq11, seq22; //dosyadan alınan nükleotitler

        private string[] fileLines; //dosya okumak için

        //Hizalamaları tutan listeler(karakter karakter)
        List<char> SeqList1 = new List<char>();
        List<char> SeqList2 = new List<char>();

        //Hesaplama Butonu
        private void button2_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            //Okunan dosyadaki nükleotitler dışında nükleotit girilebilmesi için
            seq11 = "-" + this.textBox1.Text;
            seq22 = "-" + this.textBox2.Text;

            //Benzerlik ölçeği
            int match = int.Parse(this.textBox3.Text); //match
            int mismatch = int.Parse(this.textBox4.Text); //mismatch
            int gap = int.Parse(this.textBox5.Text); //gap

            //matris oluşturuyoruz
            Grid[,] matris = NWAlgoritması.Initialization_Adimi(seq11, seq22, match, mismatch, gap); //başlatma adımı
            //matrisi göstermek için GridView kullanıyoruz
            this.dataGridView1.ColumnCount = matris.GetLength(1) + 1;

            for (int i = 0; i < this.dataGridView1.ColumnCount; i++)
            {
                this.dataGridView1.Columns[i].Width = 25;
            }
            this.dataGridView1.RowHeadersWidth = 25;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.RowCount = matris.GetLength(0) + 1;

            for (int j = 1; j < matris.GetLength(0) + 1; j++)
            {
                this.dataGridView1.Rows[j].Cells[0].Value = seq22[j - 1];
            }

            for (int i = 1; i < matris.GetLength(1) + 1; i++)
            {
                this.dataGridView1.Rows[0].Cells[i].Value = seq11[i - 1];
            }

            for (int j = 1; j < matris.GetLength(0) + 1; j++)
            {
                for (int i = 1; i < matris.GetLength(1) + 1; i++)
                {
                    this.dataGridView1.Rows[j].Cells[i].Value = matris[j - 1, i - 1].GridScore;
                }

            }

            //Matrisin en sağ altından başla yüksek puanı alarak geriye doğru devam et, 0'a(başlangıç hücresi) ulaşıncaya kadar
            NWAlgoritması.Treaceback_Adimi(matris, seq11, seq22, SeqList1, SeqList2);

            //optimal global hizalama sonucunu görüntülemw
            //ters şekilde yapılan matristeki izi kaydediyoruz (geri izleme) (trace back) 
            for (int j = SeqList1.Count - 1; j >= 0; j--)
            {
                this.richTextBox1.AppendText(SeqList1[j].ToString());
            }

            this.richTextBox1.AppendText('\n'.ToString());

            for (int k = SeqList2.Count - 1; k >= 0; k--)
            {
                this.richTextBox1.AppendText(SeqList2[k].ToString());
            }

            watch.Stop();
            label7.Text = ($"Çalışma süresi: {watch.ElapsedMilliseconds} ms");


        }

        //Yeniden Başlatma Butonu
        private void button3_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms[0] == this)//Uygulamanin main form'u
            {
                //Ana formu yeniden başlatıyoruz yani uygulamayı yeniden başlatıyoruz
                Application.Restart();
            }
            else
            {
                Form1 f = new Form1();
                f.Show();
                this.Close();

            }
        }

        //Dosya Okuma Butonu
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dosya1 = new StreamReader("C:\\Users\\sevde\\OneDrive\\Masaüstü\\4.SINIF-BAHAR\\BİYOİNFORMATİK\\odev\\seq1.txt"))
                {
                    seq11 = dosya1.ReadToEnd();

                    fileLines = File.ReadAllLines("C:\\Users\\sevde\\OneDrive\\Masaüstü\\4.SINIF-BAHAR\\BİYOİNFORMATİK\\odev\\seq1.txt");
                    textBox1.Text = fileLines[1]; //seq1.txt dosyasının 2. satırını okuduk
                }

                using (var dosya2 = new StreamReader("C:\\Users\\sevde\\OneDrive\\Masaüstü\\4.SINIF-BAHAR\\BİYOİNFORMATİK\\odev\\seq2.txt"))
                {
                    seq22 = dosya2.ReadToEnd();
                    fileLines = File.ReadAllLines("C:\\Users\\sevde\\OneDrive\\Masaüstü\\4.SINIF-BAHAR\\BİYOİNFORMATİK\\odev\\seq2.txt");
                    textBox2.Text = fileLines[1]; //seq2.txt dosyasının 2. satırını okuduk
                }
            }
            catch (IOException)
            {
                label4.Text = "Dosya okunamadı";
            }
        }
    }
}
