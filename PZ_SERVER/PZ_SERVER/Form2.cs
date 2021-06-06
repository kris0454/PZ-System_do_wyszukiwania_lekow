using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PZ_SERVER_LIBRARY;

namespace PZ_SERVER
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PZ_SERVER_LIBRARY.Search szukaj = new PZ_SERVER_LIBRARY.Search();
            string nazwa = textBox1.Text;
            string lek = szukaj.znajdzLek(nazwa);
            textBox2.Text = lek.ToString();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            drugSystemDataSet.EnforceConstraints = false;
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'drugSystemDataSet.leki' . Możesz go przenieść lub usunąć.
            this.lekiTableAdapter.Fill(this.drugSystemDataSet.leki);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PZ_SERVER_LIBRARY.Search szukaj = new PZ_SERVER_LIBRARY.Search();
            string nazwa = textBox3.Text;
            string lek = szukaj.szukajZamiennik(nazwa);
            textBox4.Text = lek.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
