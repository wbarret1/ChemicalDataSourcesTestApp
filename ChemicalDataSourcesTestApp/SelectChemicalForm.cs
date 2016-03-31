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

    public partial class SelectChemicalForm : Form
    {
        List<SpellAidChemical> chemicalList;
        int selected;

        public SelectChemicalForm(SpellAid aid, string desiredCompoundName)
        {
            InitializeComponent();
            chemicalList = new List<SpellAidChemical>();
            foreach (SpellAidChemical chemical in aid.Chemical)
            {
                chemicalList.Add(chemical);
            }
            this.dataGridView1.DataSource = chemicalList;
            this.label1.Text = "Select a chemical below that matches: " + desiredCompoundName;
        }

        public SpellAidChemical SelectedChemical
        {
            get
            {
                return chemicalList[selected];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count != 0)
            {
                selected = this.dataGridView1.SelectedCells[0].RowIndex;
                this.Close();
            }
        }
    }
}
