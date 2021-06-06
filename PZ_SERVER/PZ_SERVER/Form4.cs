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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PZ_SERVER_LIBRARY.Search szukaj = new PZ_SERVER_LIBRARY.Search();
            string nazwa = textBox1.Text;
            string apteka = szukaj.szukajApteki(nazwa);
            textBox2.Text = apteka.ToString();
        }
    }
}
