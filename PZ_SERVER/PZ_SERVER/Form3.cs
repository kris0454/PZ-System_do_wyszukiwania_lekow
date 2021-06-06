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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PZ_SERVER_LIBRARY.Search szukaj = new PZ_SERVER_LIBRARY.Search();
            string nazwa = textBox1.Text;
            string lek1 = textBox2.Text;
            string lek2 = textBox3.Text;
            string lek = szukaj.porownajCene(nazwa,lek1,lek2);
            textBox4.Text = lek.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
