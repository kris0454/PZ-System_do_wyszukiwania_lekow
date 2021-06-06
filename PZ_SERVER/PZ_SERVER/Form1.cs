using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PZ_SERVER
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            drugSystemDataSet.EnforceConstraints = false;
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.magazyny' . Możesz go przenieść lub usunąć.
            this.magazynyTableAdapter.Fill(this.drugSystemDataSet.magazyny);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.leki' . Możesz go przenieść lub usunąć.
            this.lekiTableAdapter.Fill(this.drugSystemDataSet.leki);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.apteki' . Możesz go przenieść lub usunąć.
            this.aptekiTableAdapter.Fill(this.drugSystemDataSet.apteki);

        }
        private void button1_Click(object sender, EventArgs e)
        {
            PZ_SERVER_LIBRARY.DataBase db = new PZ_SERVER_LIBRARY.DataBase();
            //dodaj lek,apteke, pozucje w magazynie
            if(textBox7.Text == "leki")
            {
                string nazwa = textBox1.Text;
                string opis = textBox2.Text;
                string sklad = textBox3.Text;
                float cena = float.Parse(textBox4.Text);
                string czy_oryginal = textBox5.Text;
                string lek_oryginalny = textBox6.Text;
                db.dodajLek(nazwa,opis,sklad,cena,czy_oryginal,lek_oryginalny);
            }
            else if(textBox7.Text == "apteki")
            {
                string nazwa = textBox1.Text;
                string ulica = textBox2.Text;
                string miasto = textBox3.Text;
                string telefon = textBox4.Text;
                db.dodajApteke(nazwa, ulica, miasto, telefon);
            }
            else
            {
                string nazwamagazynu = textBox7.Text;
                string nazwaleku = textBox1.Text;
                string datawaznosci = textBox2.Text;
                float cena = float.Parse(textBox3.Text);
                int ilosc = Int32.Parse(textBox4.Text);
                db.dodajLekdomagazynu(nazwamagazynu, nazwaleku, datawaznosci, cena, ilosc);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.magazyny' . Możesz go przenieść lub usunąć.
            this.magazynyTableAdapter.Fill(this.drugSystemDataSet.magazyny);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.leki' . Możesz go przenieść lub usunąć.
            this.lekiTableAdapter.Fill(this.drugSystemDataSet.leki);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.apteki' . Możesz go przenieść lub usunąć.
            this.aptekiTableAdapter.Fill(this.drugSystemDataSet.apteki);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PZ_SERVER_LIBRARY.DataBase db = new PZ_SERVER_LIBRARY.DataBase();
            //dodaj lek,apteke, pozucje w magazynie
            if (textBox7.Text == "leki")
            {
                string nazwa = textBox1.Text;
                db.usunLek(nazwa);
            }
            if (textBox7.Text == "apteki")
            {
                string nazwa = textBox1.Text;
                db.usunApteke(nazwa);
            }
            else
            {
                string nazwamagazynu = textBox7.Text;
                string nazwaleku = textBox1.Text;
                string datawaznosci = textBox2.Text;
                db.usunLekzmagazynu(nazwamagazynu,nazwaleku,datawaznosci);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }
    }
}
