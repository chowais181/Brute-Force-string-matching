using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace algoassignment3
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            
            frm.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
