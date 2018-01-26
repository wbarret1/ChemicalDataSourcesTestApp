using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChemicalDataSourcesTestApp
{
    public partial class Form3 : Form
    {
        int m_Selected = -1;

        public Form3()
        {
            InitializeComponent();
            this.imageList1.Images.Clear();
            this.listView1.Columns.Add("Name", - 2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("CAS Number", - 2, HorizontalAlignment.Left);
        }


        public SynonymChemical[] chemicals
        {
            set
            {
               this.AddChemicalsToList(value);
            }
        }

        public string SelectedChemical
        {
            get
            {
                if (m_Selected < 0) return string.Empty;
                return this.listView1.Items[m_Selected].Text;
            }
        }

        void AddChemicalsToList(SynonymChemical[] chemicals)
        {
            int i = 0;
            foreach (SynonymChemical chemical in chemicals)
            {
                ListViewItem item = new ListViewItem(chemical.Name, i++);
                item.SubItems.Add(chemical.CAS);
                this.listView1.Items.Add(item);
                this.imageList1.Images.Add(Form1.PUGGetCompoundImage(chemical.Name, chemical.CAS));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Selected = -1;
            if (this.listView1.SelectedIndices.Count != 0)
                m_Selected = this.listView1.SelectedIndices[0];
            this.Close();
        }
    }
}
