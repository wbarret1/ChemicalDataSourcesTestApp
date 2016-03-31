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
    public partial class Form2 : Form
    {
        List<SpellAidChemical> chemicalList;
        string compoundName = string.Empty;
        string casNo = string.Empty;

        public Form2()
        {
            InitializeComponent();
            label2.Text = string.Empty;
        }

        public string CompoundName
        {
            get
            {
                return compoundName;
            }
        }

        public string CASnumber
        {
            get
            {
                return casNo;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count != 0)
            {
                int selected = this.dataGridView1.SelectedCells[0].RowIndex;
                compoundName = chemicalList[selected].Name;
                casNo = chemicalList[selected].CAS;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Visible = false;
            compoundName = this.textBox1.Text;
            casNo = NISTChemicalList.casNumber(compoundName);
            if (string.IsNullOrEmpty(casNo))
            {
                gov.nih.nlm.chemspell.SpellAidService service = new gov.nih.nlm.chemspell.SpellAidService();
                string response = service.getSugList(compoundName, "All databases");
                var XMLReader = new System.Xml.XmlTextReader(new System.IO.StringReader(response));
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Synonym));
                if (serializer.CanDeserialize(XMLReader))
                {
                    // Synonyms means more than one name for the same chemical/CAS Number.
                    Synonym synonym = (Synonym)serializer.Deserialize(XMLReader);
                    casNo = synonym.Chemical[0].CAS;
                    foreach (SynonymChemical chemical in synonym.Chemical)
                    {
                        if (casNo != chemical.CAS)
                        {
                            System.Windows.Forms.MessageBox.Show(compoundName + "has a synonym with a different CAS Number.");
                            return;
                        }
                    }
                    label2.Text = "The CAS Number of the chemical is: " + casNo + ". Now select the finished button to exit.";
                    return;
                }
                serializer = new System.Xml.Serialization.XmlSerializer(typeof(SpellAid));
                if (serializer.CanDeserialize(XMLReader))
                {
                    SpellAid aid = (SpellAid)serializer.Deserialize(XMLReader);
                    //bool different = true;
                    chemicalList = new List<SpellAidChemical>();
                    foreach (SpellAidChemical chemical in aid.Chemical)
                    {
                        chemicalList.Add(chemical);
                    }
                    this.dataGridView1.DataSource = chemicalList;
                    this.dataGridView1.Visible = true;
                    this.label2.Text = "Select the desired chemical from the list below and click the 'Finished' button to exit.";
                    return;

                    //    retVal[0] = aid.Chemical[0].CAS;
                    //    retVal[1] = aid.Chemical[0].Name;
                    //    for (int i = 0; i < aid.Chemical.Length - 1; i++)
                    //    {
                    //        if (retVal[0] != aid.Chemical[i + 1].CAS)
                    //        {
                    //            different = false;
                    //            retVal[0] = aid.Chemical[i].CAS;
                    //            retVal[1] = aid.Chemical[i].Name;
                    //        }
                    //    }
                    //    if (!different)
                    //    {
                    //        foreach (SpellAidChemical chemical in aid.Chemical)
                    //        {
                    //            int result = String.Compare(compoundName, 0, chemical.Name, 0, compoundName.Length, true);
                    //            if (result == 0 && compoundName.Length >= chemical.Name.Length)
                    //            {
                    //                retVal[0] = chemical.CAS;
                    //                retVal[1] = chemical.Name;
                    //                return retVal;
                    //            }
                    //        }
                    //        SelectChemicalForm form = new SelectChemicalForm(aid, compoundName);
                    //        form.ShowDialog();
                    //        retVal[0] = form.SelectedChemical.CAS;
                    //        retVal[1] = form.SelectedChemical.Name;
                    //        return retVal;
                    //    }
                }
            }
            label2.Text = "The CAS Number of the chemical is: " + casNo + ". Now select the 'Finished' button to exit.";
            return;
        }
    }
}

