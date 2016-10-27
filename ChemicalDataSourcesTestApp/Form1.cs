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

    struct NistFormationEnthlpyOrEntropy
    {
        public string quantity;
        public string value;
        public string units;
        public string method;
        public string reference;
        public string comment;
    }

    struct NistCP
    {
        public string temperature;
        public string value;
        public string reference;
        public string comment;
    }

    public partial class Form1 : Form
    {
        List<SpellAidChemical> chemicalList;
        string m_CompoundName = string.Empty;
        string m_casNo = string.Empty;
        string m_molecularFormula;
        string m_molecularFormulareference;


        int m_CID = 0;
        string ecClass = string.Empty;
        string ecClassReference = string.Empty;
        string rPhrase = string.Empty;
        string rPhraseReference = string.Empty;
        string MAKcarcinogenCategory = string.Empty;
        string MAKcellMutantGroup = string.Empty;
        string MAKppmValue = string.Empty;
        string MAmgm3Value = string.Empty;
        string MAKReference = string.Empty;
        string bpValue = string.Empty;
        string bpUnit = string.Empty;
        string bpPressure = string.Empty;
        string bpReference = string.Empty;
        string mpValue = string.Empty;
        string mpUnit = string.Empty;
        string mpReference = string.Empty;
        string densityValue = string.Empty;
        string densityUnit = string.Empty;
        string relativeDensityValue = string.Empty;
        string densityTemperature = string.Empty;
        string densityTemperatureUnit = string.Empty;
        string densityReference = string.Empty;
        string vaporPressUnit = string.Empty;
        string vaporPressTemp = string.Empty;
        string vaporPressTempUnit = string.Empty;
        string vaporPress = string.Empty;
        string vaporPressReference = string.Empty;
        string flashPtValue = string.Empty;
        string flashPtUnit = string.Empty;
        string flashPtReference = string.Empty;
        string nfpaHealth = string.Empty;
        string nfpaHealthReference = string.Empty;
        string nfpaFire = string.Empty;
        string nfpaFireReference = string.Empty;
        string nfpaReactivity = string.Empty;
        string nfpaReactivityReference = string.Empty;
        string logKow = string.Empty;
        string logKowReference = string.Empty;
        string m_ERPG2 = string.Empty;
        string m_ERPG3 = string.Empty;
        string m_IDLH = string.Empty;
        bool hazardous = false;
        bool triList = false;
        bool triPBTList = false;
        int numCarbon = 0;
        int numHydrogen = 0;
        int numNitrogen = 0;
        int numChlorine = 0;
        int numSodium = 0;
        int numOxygen = 0;
        int numPhosphorous = 0;
        int numSulfur = 0;
        string m_HeatOfCombustion = string.Empty;
        string m_HeatOfCombustionUnit = string.Empty;
        string m_HeatOfCombustionConditions = string.Empty;
        string m_HeatOfCombustionReference = string.Empty;
        string m_HeatOfVaporization = string.Empty;
        string m_HeatOfVaporizationUnit = string.Empty;
        string m_heatOfVaporizationConditions = string.Empty;
        string m_heatOfVaporizationReference = string.Empty;
        string m_hsdbDocumentURL = string.Empty;
        string m_iloChemicalSafetyCardURL = string.Empty;
        string m_NioshChemicalSafetyCardURL = string.Empty;
        string enthalpy = string.Empty;
        string entropy = string.Empty;
        string cp = string.Empty;
        string wgk = string.Empty;


        //string url = string.Empty;

        static private System.Collections.Generic.List<Species> speciesList;
        public Form1()
        {
            InitializeComponent();
            this.label2.Text = string.Empty;
            var source = new AutoCompleteStringCollection();
            source.AddRange(NISTChemicalList.CompoundNames);
            this.chemicalTextBox.AutoCompleteCustomSource = source;
            this.chemicalTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.chemicalTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }


        private void findButton_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            this.pictureBox1.Visible = false;
            this.pictureBox2.Visible = false;
            this.pictureBox3.Visible = false;
            this.pictureBox4.Visible = false;
            this.pictureBox5.Visible = false;
            this.pictureBox6.Visible = false;
            this.pictureBox7.Visible = false;
            this.label22.Text = string.Empty;
            this.label2.Visible = false;
            this.toolTip1.SetToolTip(this.label2, string.Empty);
            this.label22.Visible = false;
            this.label4.Visible = false;
            this.label5.Visible = false;
            this.label6.Visible = false;
            this.label7.Visible = false;
            this.label8.Visible = false;
            this.label9.Visible = false;
            this.label10.Visible = false;
            this.label11.Visible = false;
            this.label12.Visible = false;
            this.label13.Visible = false;
            this.label14.Visible = false;
            this.label15.Visible = false;
            this.label16.Visible = false;
            this.label17.Visible = false;
            this.label18.Visible = false;
            this.label19.Visible = false;
            this.label20.Visible = false;
            this.nfpaFlammabilityLabel.Visible = false;
            this.nfpaHealthLabel.Visible = false;
            this.nfpaInstabilityLabel.Visible = false;

            if (string.IsNullOrEmpty(this.chemicalTextBox.Text)) return;
            m_CompoundName = this.chemicalTextBox.Text;
            string pattern = "(?<1>\\d+-\\d{2}-\\d)";
            System.Text.RegularExpressions.Match m1 = System.Text.RegularExpressions.Regex.Match(m_CompoundName, pattern,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                TimeSpan.FromSeconds(1));
            if (m1.Groups.Count > 1)
            {
                if (this.validateCasNumber(m1.Groups[0].Value))
                {
                    m_casNo = m1.Groups[0].Value;
                    string url = "http://actorws.epa.gov/actorws/actor/2015q3/preferredName.json?casrn=" + m_casNo;
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    System.Net.WebResponse response = request.GetResponse();
                    System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                    //string output = reader.ReadToEnd();
                    System.Runtime.Serialization.Json.DataContractJsonSerializer jSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(Rootobject));
                    Rootobject chemicalName = (Rootobject)jSerializer.ReadObject(response.GetResponseStream());
                    m_CompoundName = chemicalName.DataRow.preferredName;
                }
                else
                {
                    this.label2.Text = "The CAS Number you entered " + m1.Groups[0].Value + " was not valid. Please check it and re-enter it.";
                    this.toolTip1.SetToolTip(this.label2, "The CAS number did not comply with its format. Either the value was not properly formatted (2 to 7 digits followed by a dash followed by two digits followed by a dash, folloed by one digit), or the digits checksum did not match.");
                    this.label2.Visible = true;
                    return;
                }
            }
            this.findCompound(ref m_CompoundName, ref m_casNo);
            this.AddChemicalInformation(m_CompoundName, m_casNo);
            string[] pictograms = EuropeanChemicalInventory.Pictograms(m_casNo);
        }

        bool validateCasNumber(string casNo)
        {
            // split the CAS Number into parts separated by a dash...
            string[] parts = casNo.Split('-');
            // check that there were three pieces of the cas number...if not, the cas number is improperly formatted and return false
            if (parts.Length != 3) return false;

            // Check to see if a numbers were submitted...
            int part1 = 0;
            if (!int.TryParse(parts[0], out part1))
            {
                // if not, return false
                return false;
            }
            // Check to see if a numbers were submitted...
            int part2 = 0;
            if (!int.TryParse(parts[1], out part2))
            {
                // if not, return false
                return false;
            }
            // Check to see if a numbers were submitted...
            int part3 = 0;
            if (!int.TryParse(parts[2], out part3))
            {
                // if not, return false
                return false;
            }

            //initialize the checksum
            int checksum = 0;

            // handle part 2...
            checksum = (part2 % 10);
            checksum = checksum + (part2 / 10)*2;

            // now handle part 1, it can have between 2 and 7 digits...
            int n = 3;
            while (part1 > 10)
            {
                checksum = checksum + (part1 % 10) * n++;
                part1 = part1 / 10;
            }
            checksum = checksum + part1*n;

            // number is valid if the last digit equals the remainder from dividing 
            // the checksum by 10.
            if (checksum % 10 == part3)
            {
                // throw exception if the checksum does not work...
                return true;
            }
            return false;

        }

        private void findCompound(ref string compoundName, ref string CasNo)
        {
            gov.nih.nlm.chemspell.SpellAidService service = new gov.nih.nlm.chemspell.SpellAidService();
            string response = service.getSugList(compoundName, "All databases");
            response = response.Replace("&", "&amp;");
            var XMLReader = new System.Xml.XmlTextReader(new System.IO.StringReader(response));
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(Synonym));
            if (serializer.CanDeserialize(XMLReader))
            {
                // Synonyms means more than one name for the same chemical/CAS Number.
                Synonym synonym = (Synonym)serializer.Deserialize(XMLReader);
                CasNo = synonym.Chemical[0].CAS;
                this.listBox1.BeginUpdate();
                foreach (SynonymChemical chemical in synonym.Chemical)
                {
                    this.listBox1.Items.Add(chemical.Name);
                    if (CasNo != chemical.CAS)
                    {
                        System.Windows.Forms.MessageBox.Show(compoundName + "has a synonym with a different CAS Number.");
                        return;
                    }
                }
                this.listBox1.EndUpdate();
                return;
            }
            serializer = new System.Xml.Serialization.XmlSerializer(typeof(SpellAid));
            if (serializer.CanDeserialize(XMLReader))
            {
                SpellAid aid = (SpellAid)serializer.Deserialize(XMLReader);
                Form3 selector = new Form3();
                selector.chemicals = aid;
                selector.ShowDialog();
                compoundName = selector.SelectedChemical;
                this.findCompound(ref compoundName, ref CasNo);
                return;
            }
            CasNo = string.Empty;
        }

        void AddChemicalInformation(string compoundName, string casNo)
        {
            this.GetPUGInformation(compoundName, casNo);
            this.GetICSCInformation(compoundName, casNo);
            this.GetTOXNETInformation(compoundName, casNo);
            this.label2.Text = "Chemical Name: " + compoundName;
            this.label2.Visible = true;
            this.label22.Text = "CAS Number: " + casNo;
            this.label22.Visible = true;
            this.label11.Text = "Molecular Formula = " + this.m_molecularFormula;
            this.label11.Visible = true;
            this.label4.Text = "Boiling Point = " + this.bpValue + " " + this.bpUnit;
            this.label4.Visible = true;
            this.label5.Text = "Melting Point = " + this.mpValue + " " + this.mpUnit;
            this.label5.Visible = true;
            this.label6.Text = "Flash Point = " + this.flashPtValue + " " + this.flashPtUnit;
            this.label6.Visible = true;
            this.pictureBox1.Image = Form1.PUGGetCompoundImage(compoundName, casNo);
            this.pictureBox1.Visible = true;
            if (nfpaHealth != string.Empty)
            {
                this.pictureBox2.Visible = true;
                this.nfpaHealthLabel.Text = nfpaHealth;
                this.nfpaHealthLabel.Visible = true;
                this.nfpaFlammabilityLabel.Text = nfpaFire;
                this.nfpaFlammabilityLabel.Visible = true;
                this.nfpaInstabilityLabel.Text = nfpaReactivity;
                this.nfpaInstabilityLabel.Visible = true;
                this.pictureBox2.SendToBack();
            }
            this.label12.Text = "Signal Word: " + EuropeanChemicalInventory.SignalWord(m_casNo);
            this.label12.Visible = true;
            string[] pictograms = EuropeanChemicalInventory.Pictograms(casNo);
            if (pictograms.Length > 0)
            {
                this.pictureBox3.Image = this.imageList2.Images[pictograms[0]];
                this.pictureBox3.Visible = true;
                this.label13.Visible = true;
                this.label13.Text = pictograms[0];
            }
            if (pictograms.Length > 1)
            {
                this.pictureBox4.Image = this.imageList2.Images[pictograms[1]];
                this.pictureBox4.Visible = true;
                this.label14.Visible = true;
                this.label14.Text = pictograms[1];
            }
            if (pictograms.Length > 2)
            {
                this.pictureBox5.Image = this.imageList2.Images[pictograms[2]];
                this.pictureBox5.Visible = true;
                this.label15.Visible = true;
                this.label15.Text = pictograms[2];
            }
            if (pictograms.Length > 3)
            {
                this.pictureBox6.Image = this.imageList2.Images[pictograms[3]];
                this.pictureBox6.Visible = true;
                this.label16.Visible = true;
                this.label16.Text = pictograms[3];
            }
            if (pictograms.Length > 4)
            {
                this.pictureBox7.Image = this.imageList2.Images[pictograms[4]];
                this.pictureBox7.Visible = true;
                this.label17.Visible = true;
                this.label17.Text = pictograms[4];
            }
            this.GetEuropeanInfo(m_casNo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] compoundNames = {
                 "N-Butanol",
                "Maleic Anhydride",
                "Water",
                "Carbon Monoxide",
                "Carbon Dioxide",
                "Phthalic Anhydride",
                "Acrylic Acid",
                "Acetic Acid",
                "Nitrogen",
                "Oxygen",
                "(E)-2-Butene",
                "Formaldehyde"
            };
            string[] casNos = {
                "71-36-3",
                "108-31-6",
                "7732-18-5",
                "630-08-0",
                "124-38-9",
                "85-44-9",
                "79-10-7",
                "64-19-7",
                "7727-37-9",
                "7782-44-7",
                "624-64-6",
                "50-00-0"
            };

            for (int i = 0; i < compoundNames.Length; i++)
            {
                m_casNo = string.Empty;
                m_CompoundName = string.Empty;
                ecClass = string.Empty;
                ecClassReference = string.Empty;
                rPhrase = string.Empty;
                rPhraseReference = string.Empty;
                MAKcarcinogenCategory = string.Empty;
                MAKcellMutantGroup = string.Empty;
                MAKppmValue = string.Empty;
                MAmgm3Value = string.Empty;
                MAKReference = string.Empty;
                bpValue = string.Empty;
                bpUnit = string.Empty;
                bpPressure = string.Empty;
                bpReference = string.Empty;
                mpValue = string.Empty;
                mpUnit = string.Empty;
                mpReference = string.Empty;
                densityValue = string.Empty;
                densityUnit = string.Empty;
                relativeDensityValue = string.Empty;
                densityTemperature = string.Empty;
                densityTemperatureUnit = string.Empty;
                densityReference = string.Empty;
                vaporPressUnit = string.Empty;
                vaporPressTemp = string.Empty;
                vaporPressTempUnit = string.Empty;
                vaporPress = string.Empty;
                vaporPressReference = string.Empty;
                flashPtValue = string.Empty;
                flashPtUnit = string.Empty;
                flashPtReference = string.Empty;
                nfpaHealth = string.Empty;
                nfpaHealthReference = string.Empty;
                nfpaFire = string.Empty;
                nfpaFireReference = string.Empty;
                nfpaReactivity = string.Empty;
                nfpaReactivityReference = string.Empty;
                logKow = string.Empty;
                logKowReference = string.Empty;
                m_ERPG2 = string.Empty;
                m_ERPG3 = string.Empty;
                m_IDLH = string.Empty;
                hazardous = false;
                triList = false;
                triPBTList = false;
                numCarbon = 0;
                numHydrogen = 0;
                numNitrogen = 0;
                numChlorine = 0;
                numSodium = 0;
                numOxygen = 0;
                numPhosphorous = 0;
                numSulfur = 0;
                m_HeatOfVaporization = string.Empty;
                m_HeatOfVaporizationUnit = string.Empty;
                m_HeatOfCombustion = string.Empty;
                m_HeatOfCombustionUnit = string.Empty;
                enthalpy = string.Empty;
                entropy = string.Empty;
                cp = string.Empty;
                wgk = string.Empty;

                this.GetLocalInformation(compoundNames[i], casNos[i]);
                this.GetPUGInformation(compoundNames[i], casNos[i]);
                this.GetICSCInformation(compoundNames[i], casNos[i]);
                this.GetTOXNETInformation(compoundNames[i], casNos[i]);
                this.GetToxData(compoundNames[i], casNos[i]);
                this.GetNISTLiquidData(casNos[i], ref enthalpy, ref entropy, ref cp);
                this.GetNISTGasData(casNos[i]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string[] casNos = {
                "71-36-3",
                "108-31-6",
                "7732-18-5",
                "630-08-0",
                "124-38-9",
                "85-44-9",
                "79-10-7",
                "64-19-7",
                "7727-37-9",
                "7782-44-7",
                "624-64-6",
                "50-00-0"
            };
            foreach (string casNo in casNos)
            {

                string url = "https://ends2.epacdxnode.net/RestProxy/query?Node=NGNTest2.0&Dataflow=SRS&Request=GetSubstanceByCAS&Params=CASRegistryNumber|" + casNo;
                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                System.Net.WebResponse response = request.GetResponse();
                string responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                var XMLReader = System.Xml.XmlReader.Create(responseString);
                //System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SubstanceInformationList));
                //if (serializer.CanDeserialize(XMLReader))
                //{
                //    SubstanceInformationList qResponse = (SubstanceInformationList)serializer.Deserialize(XMLReader);
                //    //foreach (SynonymChemical chemical in )
                //    //{
                //    //    int result = String.Compare(compoundName, chemical.Name, true);
                //    //    if (result == 0)
                //    //    {
                //    //        retVal[0] = chemical.CAS;
                //    //        retVal[1] = chemical.Name;
                //    //        return retVal;
                //    //    }
                //    //}

                //}

            }
            //WindowsFormsApplication1.HSDB.DocListDoc doc = new HSDB.DocListDoc();
            //doc.DOCNO = 35;
        }
        void GetACToRData(string casNo)
        {
            string url = "http://actorws.epa.gov/actorws/physchemdb/dev/properties/" + m_casNo + ".json";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer jSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorPhysicalChemicalProperties.Rootobject));
            ActorPhysicalChemicalProperties.Rootobject actorPhysChem = (ActorPhysicalChemicalProperties.Rootobject)jSerializer.ReadObject(response.GetResponseStream());
            string[] names = new string[actorPhysChem.DataList.list.Length];
            string[] sources = new string[actorPhysChem.DataList.list.Length];
            string[] descriptions = new string[actorPhysChem.DataList.list.Length];
            string[] modelClasses = new string[actorPhysChem.DataList.list.Length];
            string[] resultTypes = new string[actorPhysChem.DataList.list.Length];
            string[] rawResults = new string[actorPhysChem.DataList.list.Length];
            float[] resultMeans = new float[actorPhysChem.DataList.list.Length];
            float[] resultMins = new float[actorPhysChem.DataList.list.Length];
            float[] resultMaxs = new float[actorPhysChem.DataList.list.Length];
            string[] resultUnits = new string[actorPhysChem.DataList.list.Length];
            for (int i = 0; i < actorPhysChem.DataList.list.Length; i++)
            {
                names[i] = actorPhysChem.DataList.list[i].name;
                sources[i] = actorPhysChem.DataList.list[i].source;
                descriptions[i] = actorPhysChem.DataList.list[i].description;
                modelClasses[i] = actorPhysChem.DataList.list[i].modelClass;
                resultTypes[i] = actorPhysChem.DataList.list[i].resultType;
                rawResults[i] = actorPhysChem.DataList.list[i].rawResult;
                resultMeans[i] = actorPhysChem.DataList.list[i].resultMean;
                resultMins[i] = actorPhysChem.DataList.list[i].resultMin;
                resultMaxs[i] = actorPhysChem.DataList.list[i].resultMax;
                resultUnits[i] = actorPhysChem.DataList.list[i].resultUnit;
            }

            url = "http://actorws.epa.gov/actorws/dsstox/v02/properties.json?casrn=" + m_casNo;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer dssToxjSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorDSSTox.Rootobject));
            ActorDSSTox.Rootobject dssToxData = (ActorDSSTox.Rootobject)dssToxjSerializer.ReadObject(response.GetResponseStream());
            int gsid = dssToxData.DataRow.gsid;
            string casrn = dssToxData.DataRow.casrn;
            string imageURL = dssToxData.DataRow.imageURL;
            string preferredName = dssToxData.DataRow.preferredName;
            string casType = dssToxData.DataRow.casType;
            string smiles = dssToxData.DataRow.smiles;
            string chemtype = dssToxData.DataRow.chemtype;
            string inchi = dssToxData.DataRow.inchi;
            string inchiKey = dssToxData.DataRow.inchiKey;
            string chiralStereo = dssToxData.DataRow.chiralStereo;
            string dblStereo = dssToxData.DataRow.dblStereo;
            string organicForm = dssToxData.DataRow.organicForm;
            string iupac = dssToxData.DataRow.iupac;
            string molFormula = dssToxData.DataRow.molFormula;

            url = "http://actorws.epa.gov/actorws/edsp21/v02/regulatory/" + m_casNo + ".json";
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer regulatorySerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorRegulatory.Rootobject));
            ActorRegulatory.Rootobject actorRegulatory = (ActorRegulatory.Rootobject)regulatorySerializer.ReadObject(response.GetResponseStream());
            string[] regulatoryNames = new string[actorRegulatory.DataList.list.Length];
            string[] regulatorySources = new string[actorRegulatory.DataList.list.Length];
            string[] regulatoryDescriptions = new string[actorRegulatory.DataList.list.Length];
            string[] regulatoryUrls = new string[actorRegulatory.DataList.list.Length];
            for (int i = 0; i < actorRegulatory.DataList.list.Length; i++)
            {
                regulatoryNames[i] = actorRegulatory.DataList.list[i].name;
                regulatorySources[i] = actorRegulatory.DataList.list[i].source;
                regulatoryDescriptions[i] = actorRegulatory.DataList.list[i].description;
                regulatoryUrls[i] = actorRegulatory.DataList.list[i].url;
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {
            string url = "http://actorws.epa.gov/actorws/physchemdb/dev/properties/" + m_casNo + ".json";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer jSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorPhysicalChemicalProperties.Rootobject));
            ActorPhysicalChemicalProperties.Rootobject actorPhysChem = (ActorPhysicalChemicalProperties.Rootobject)jSerializer.ReadObject(response.GetResponseStream());
            string[] names = new string[actorPhysChem.DataList.list.Length];
            string[] sources = new string[actorPhysChem.DataList.list.Length];
            string[] descriptions = new string[actorPhysChem.DataList.list.Length];
            string[] modelClasses = new string[actorPhysChem.DataList.list.Length];
            string[] resultTypes = new string[actorPhysChem.DataList.list.Length];
            string[] rawResults = new string[actorPhysChem.DataList.list.Length];
            float[] resultMeans = new float[actorPhysChem.DataList.list.Length];
            float[] resultMins = new float[actorPhysChem.DataList.list.Length];
            float[] resultMaxs = new float[actorPhysChem.DataList.list.Length];
            string[] resultUnits = new string[actorPhysChem.DataList.list.Length];
            for (int i = 0; i < actorPhysChem.DataList.list.Length; i++)
            {
                names[i] = actorPhysChem.DataList.list[i].name;
                sources[i] = actorPhysChem.DataList.list[i].source;
                descriptions[i] = actorPhysChem.DataList.list[i].description;
                modelClasses[i] = actorPhysChem.DataList.list[i].modelClass;
                resultTypes[i] = actorPhysChem.DataList.list[i].resultType;
                rawResults[i] = actorPhysChem.DataList.list[i].rawResult;
                resultMeans[i] = actorPhysChem.DataList.list[i].resultMean;
                resultMins[i] = actorPhysChem.DataList.list[i].resultMin;
                resultMaxs[i] = actorPhysChem.DataList.list[i].resultMax;
                resultUnits[i] = actorPhysChem.DataList.list[i].resultUnit;
            }

            url = "http://actorws.epa.gov/actorws/dsstox/v02/properties.json?casrn=" + m_casNo;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer dssToxjSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorDSSTox.Rootobject));
            ActorDSSTox.Rootobject dssToxData = (ActorDSSTox.Rootobject)dssToxjSerializer.ReadObject(response.GetResponseStream());
            int gsid = dssToxData.DataRow.gsid;
            string casrn = dssToxData.DataRow.casrn;
            string imageURL = dssToxData.DataRow.imageURL;
            string preferredName = dssToxData.DataRow.preferredName;
            string casType = dssToxData.DataRow.casType;
            string smiles = dssToxData.DataRow.smiles;
            string chemtype = dssToxData.DataRow.chemtype;
            string inchi = dssToxData.DataRow.inchi;
            string inchiKey = dssToxData.DataRow.inchiKey;
            string chiralStereo = dssToxData.DataRow.chiralStereo;
            string dblStereo = dssToxData.DataRow.dblStereo;
            string organicForm = dssToxData.DataRow.organicForm;
            string iupac = dssToxData.DataRow.iupac;
            string molFormula = dssToxData.DataRow.molFormula;

            url = "http://actorws.epa.gov/actorws/edsp21/v02/regulatory/" + m_casNo + ".json";
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer regulatorySerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorRegulatory.Rootobject));
            ActorRegulatory.Rootobject actorRegulatory = (ActorRegulatory.Rootobject)regulatorySerializer.ReadObject(response.GetResponseStream());
            string[] regulatoryNames = new string[actorRegulatory.DataList.list.Length];
            string[] regulatorySources = new string[actorRegulatory.DataList.list.Length];
            string[] regulatoryDescriptions = new string[actorRegulatory.DataList.list.Length];
            string[] regulatoryUrls = new string[actorRegulatory.DataList.list.Length];
            for (int i = 0; i < actorRegulatory.DataList.list.Length; i++)
            {
                regulatoryNames[i] = actorRegulatory.DataList.list[i].name;
                regulatorySources[i] = actorRegulatory.DataList.list[i].source;
                regulatoryDescriptions[i] = actorRegulatory.DataList.list[i].description;
                regulatoryUrls[i] = actorRegulatory.DataList.list[i].url;
            }
        }

        void GetNISTLiquidData(string casNo, ref string enthalpy, ref string entropy, ref string cp)
        {
            List<NistFormationEnthlpyOrEntropy> enthalpies = new List<NistFormationEnthlpyOrEntropy>();
            List<NistFormationEnthlpyOrEntropy> entropies = new List<NistFormationEnthlpyOrEntropy>();
            List<NistCP> cps = new List<NistCP>();
            string url = "http://webbook.nist.gov/cgi/cbook.cgi?ID=C" + casNo.Replace("-", string.Empty) + "&Units=SI&Mask=2#Thermo-Condensed";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StringReader reader = new System.IO.StringReader(new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd());


            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load(reader);
            foreach (HtmlAgilityPack.HtmlNode bodyNode in document.DocumentNode.ChildNodes.Nodes())
            {
                if (bodyNode.Name == "body")
                {
                    foreach (HtmlAgilityPack.HtmlNode tableNode in bodyNode.ChildNodes)
                    {
                        if (tableNode.Name == "table")
                        {
                            foreach (HtmlAgilityPack.HtmlNode rowNode in tableNode.ChildNodes)
                            {
                                if (rowNode.Name == "tr")
                                {
                                    if (rowNode.ChildNodes.Count == 6)
                                    {
                                        if (rowNode.ChildNodes[0].InnerText == "fH&deg;liquid" || rowNode.ChildNodes[0].InnerText == "fH&deg;solid")
                                        {
                                            NistFormationEnthlpyOrEntropy newVal = new NistFormationEnthlpyOrEntropy();
                                            newVal.quantity = rowNode.ChildNodes[0].InnerText;
                                            newVal.value = rowNode.ChildNodes[1].InnerText;
                                            newVal.units = rowNode.ChildNodes[2].InnerText;
                                            newVal.method = rowNode.ChildNodes[3].InnerText;
                                            newVal.reference = rowNode.ChildNodes[4].InnerText;
                                            newVal.comment = rowNode.ChildNodes[5].InnerText;
                                            enthalpies.Add(newVal);
                                        }
                                        if (rowNode.ChildNodes[0].InnerText == "S&deg;liquid" || rowNode.ChildNodes[0].InnerText == "S&deg;solid")
                                        {
                                            NistFormationEnthlpyOrEntropy newVal = new NistFormationEnthlpyOrEntropy();
                                            newVal.quantity = rowNode.ChildNodes[0].InnerText;
                                            newVal.value = rowNode.ChildNodes[1].InnerText;
                                            newVal.units = rowNode.ChildNodes[2].InnerText;
                                            newVal.method = rowNode.ChildNodes[3].InnerText;
                                            newVal.reference = rowNode.ChildNodes[4].InnerText;
                                            newVal.comment = rowNode.ChildNodes[5].InnerText;
                                            entropies.Add(newVal);
                                        }
                                    }
                                    if (rowNode.ChildNodes.Count == 4)
                                    {
                                        if (rowNode.ChildNodes[0].InnerText != "Cp,liquid (J/mol*K)")
                                        {
                                            NistCP testVal = new NistCP();
                                            testVal.value = rowNode.ChildNodes[0].InnerText;
                                            testVal.temperature = rowNode.ChildNodes[1].InnerText;
                                            testVal.reference = rowNode.ChildNodes[2].InnerText;
                                            testVal.comment = rowNode.ChildNodes[3].InnerText;
                                            cps.Add(testVal);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void GetNISTGasData(string casNo)
        {
            List<NistFormationEnthlpyOrEntropy> enthalpies = new List<NistFormationEnthlpyOrEntropy>();
            List<NistFormationEnthlpyOrEntropy> entropies = new List<NistFormationEnthlpyOrEntropy>();
            List<NistCP> cps = new List<NistCP>();
            string url = "http://webbook.nist.gov/cgi/cbook.cgi?ID=C" + casNo.Replace("-", string.Empty) + "&Units=SI&Mask=1#Thermo-Gas";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StringReader reader = new System.IO.StringReader(new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd());
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load(reader);
            foreach (HtmlAgilityPack.HtmlNode bodyNode in document.DocumentNode.ChildNodes.Nodes())
            {
                if (bodyNode.Name == "body")
                {
                    bool NextTableShomate = false;
                    foreach (HtmlAgilityPack.HtmlNode tableNode in bodyNode.ChildNodes)
                    {
                        if (tableNode.InnerText == "Gas Phase Heat Capacity (Shomate Equation)")
                        {
                            NextTableShomate = true;
                        }
                        if (tableNode.Name == "table")
                        {
                            foreach (HtmlAgilityPack.HtmlNode rowNode in tableNode.ChildNodes)
                            {
                                if (rowNode.Name == "tr")
                                {
                                    if (rowNode.ChildNodes.Count == 6)
                                    {
                                        if (rowNode.ChildNodes[0].InnerText == "fH&deg;gas")
                                        {
                                            NistFormationEnthlpyOrEntropy enthalpy = new NistFormationEnthlpyOrEntropy();
                                            enthalpy.quantity = rowNode.ChildNodes[0].InnerText;
                                            enthalpy.value = rowNode.ChildNodes[1].InnerText;
                                            enthalpy.units = rowNode.ChildNodes[2].InnerText;
                                            enthalpy.method = rowNode.ChildNodes[3].InnerText;
                                            enthalpy.reference = rowNode.ChildNodes[4].InnerText;
                                            enthalpy.comment = rowNode.ChildNodes[5].InnerText;
                                            enthalpies.Add(enthalpy);
                                        }
                                        if (rowNode.ChildNodes[0].InnerText == "S&deg;gas")
                                        {
                                            NistFormationEnthlpyOrEntropy entropy = new NistFormationEnthlpyOrEntropy();
                                            entropy.quantity = rowNode.ChildNodes[0].InnerText;
                                            entropy.value = rowNode.ChildNodes[1].InnerText;
                                            entropy.units = rowNode.ChildNodes[2].InnerText;
                                            entropy.method = rowNode.ChildNodes[3].InnerText;
                                            entropy.reference = rowNode.ChildNodes[4].InnerText;
                                            entropy.comment = rowNode.ChildNodes[5].InnerText;
                                            entropies.Add(entropy);
                                        }
                                    }
                                    if (rowNode.ChildNodes.Count == 4)
                                    {
                                        if (rowNode.ChildNodes[0].InnerText != "Cp,gas (J/mol*K)")
                                        {
                                            {
                                                NistCP cp = new NistCP();
                                                cp.value = rowNode.ChildNodes[0].InnerText;
                                                cp.temperature = rowNode.ChildNodes[1].InnerText;
                                                cp.reference = rowNode.ChildNodes[2].InnerText;
                                                cp.comment = rowNode.ChildNodes[3].InnerText;
                                                cps.Add(cp);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (NextTableShomate && tableNode.Name == "table")
                        {

                        }
                    }
                }
            }
        }


        void GetLocalInformation(string compoundName, string casNo)
        {
            m_ERPG2 = AIHA.ERPG2(casNo);
            m_ERPG3 = AIHA.ERPG3(casNo);
            m_IDLH = IDLH.RevisedIDLH(compoundName);
            hazardous = ListOfLists.IsHAzardous(casNo);
            triList = TRIList.IsTRIChemical(casNo);
            triPBTList = TRIList.IsPBTChemical(casNo);
            wgk = GermanWGKSubstanceList.WGK(casNo);
        }

        void GetPUGInformation(string compoundName, string casNo)
        {
            string atomsReference = "http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/" + compoundName + "/JSON";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(atomsReference);
            System.Net.WebResponse response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer pugSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(pugRest.Rootobject));
            pugRest.Rootobject pugChem = (pugRest.Rootobject)pugSerializer.ReadObject(response.GetResponseStream());
            m_CID = pugChem.PC_Compounds[0].id.id.cid;

            numCarbon = 0;
            numHydrogen = 0;
            numNitrogen = 0;
            numChlorine = 0;
            numSodium = 0;
            numOxygen = 0;
            numPhosphorous = 0;
            numSulfur = 0;

            foreach (int atom in pugChem.PC_Compounds[0].atoms.element)
            {
                if (atom == 6) numCarbon = numCarbon + 1;
                if (atom == 1) numHydrogen = numHydrogen + 1;
                if (atom == 7) numNitrogen = numNitrogen + 1;
                if (atom == 17) numChlorine = numChlorine + 1;
                if (atom == 11) numSodium = numSodium + 1;
                if (atom == 8) numOxygen = numOxygen + 1;
                if (atom == 15) numPhosphorous = numPhosphorous + 1;
                if (atom == 16) numSulfur = numSulfur + 1;
            }
        }

        public static Image PUGGetCompoundImage(string compoundName, string casNo)
        {
            string imageReference = "http://pubchem.ncbi.nlm.nih.gov/rest/pug/compound/name/" + compoundName + "/PNG";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(imageReference);
            try
            {
                System.Net.WebResponse response = request.GetResponse();
                return Image.FromStream(response.GetResponseStream());
            }
            catch (System.Exception p_Ex)
            {
                return Properties.Resources.Image1;
            }
        }

        void GetICSCInformation(string compoundName, string casNo)
        {
            string icscNumber = string.Empty;

            System.Collections.Generic.List<string> ICSCnumbers = new System.Collections.Generic.List<string>(0);
            System.Collections.Generic.List<string> caNoss = new System.Collections.Generic.List<string>(0);
            try
            {
                System.IO.StringReader strReader = new System.IO.StringReader(Properties.Resources.ICSCnumberByCAS);
                string nextLine = strReader.ReadLine();
                while (!string.IsNullOrEmpty(nextLine))
                {
                    string[] split = nextLine.Split('*');
                    ICSCnumbers.Add(split[0]);
                    caNoss.Add(split[1].Remove(0, 1));
                    if (split[1].Remove(0, 1) == casNo) icscNumber = split[0];
                    nextLine = strReader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }

            if (!string.IsNullOrEmpty(icscNumber))
            {
                m_iloChemicalSafetyCardURL = "http://www.ilo.org/dyn/icsc/showcard.display?p_lang=en&p_card_id=" + icscNumber + "&p_version=1";
                m_NioshChemicalSafetyCardURL = "http://www.cdc.gov/niosh/ipcsneng/neng" + icscNumber + ".html";

                System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(m_iloChemicalSafetyCardURL);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
                string output = reader.ReadToEnd();

                string pattern1 = "Symbol: (?<1>\\S+)</span>";
                System.Text.RegularExpressions.Match m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                ecClass = m1.Groups[1].Value;
                ecClassReference = m_iloChemicalSafetyCardURL;


                pattern1 = "R: (?<1>\\S+)</span>;";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                rPhrase = m1.Groups[1].Value;
                rPhraseReference = m_iloChemicalSafetyCardURL;

                pattern1 = "MAK: Carcinogen category: (?<1>\\S+);</span> Germ cell mutagen group: (?<2>\\S+);</span>";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                MAKcarcinogenCategory = m1.Groups[1].Value;
                MAKcellMutantGroup = m1.Groups[2].Value;

                pattern1 = "MAK: (?<1>\\S+) ppm</span>, (?<2>\\S+) mg/m³;";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                MAKppmValue = m1.Groups[1].Value;
                MAmgm3Value = m1.Groups[2].Value;

                if (m1.Groups.Count == 1)
                {
                    pattern1 = "MAK \\(respirable fraction\\)</span>: (?<1>\\S+) ppm</span>, (?<2>\\S+) mg/m³;</span>";
                    m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                               System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                              TimeSpan.FromSeconds(1));
                    MAKppmValue = m1.Groups[1].Value;
                    MAmgm3Value = m1.Groups[2].Value;
                }
                MAKReference = m_iloChemicalSafetyCardURL;

                pattern1 = "Boiling point: (?<1>\\S+)</span>°(?<2>\\S+) <br />";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                bpValue = m1.Groups[1].Value;
                bpUnit = m1.Groups[2].Value;
                bpReference = m_iloChemicalSafetyCardURL;

                pattern1 = "Melting point: (?<1>\\S+)</span>°(?<2>\\S+) <br />";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                mpValue = m1.Groups[1].Value;
                mpUnit = m1.Groups[2].Value;
                mpReference = m_iloChemicalSafetyCardURL;

                pattern1 = "Density: (?<1>\\S+)</span> (?<2>\\S+)<br />";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                densityValue = m1.Groups[1].Value;
                densityUnit = m1.Groups[2].Value;

                pattern1 = "Relative density \\(water = 1\\): (?<1>\\S+)</span>";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                relativeDensityValue = m1.Groups[1].Value;
                densityReference = m_iloChemicalSafetyCardURL;

                pattern1 = "Vapour pressure, (?<1>\\S+) at (?<2>\\S+)</span>°C: (?<3>\\S+)</span> ";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                vaporPressUnit = m1.Groups[1].Value;
                vaporPressTemp = m1.Groups[2].Value;
                vaporPress = m1.Groups[3].Value;
                vaporPressReference = m_iloChemicalSafetyCardURL;

                pattern1 = "Flash point: (?<1>\\S+)</span>°(?<2>\\S+) c.c.<br />";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                flashPtValue = m1.Groups[1].Value;
                flashPtUnit = m1.Groups[2].Value;
                flashPtReference = m_iloChemicalSafetyCardURL;

                pattern1 = "NFPA Code: H(?<1>\\S+); F(?<2>\\S+); R(?<3>\\S+)</span>";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                if (m1.Groups.Count > 1)
                {
                    nfpaHealth = m1.Groups[1].Value;
                    nfpaHealthReference = m_iloChemicalSafetyCardURL;
                    nfpaFire = m1.Groups[2].Value;
                    nfpaFireReference = m_iloChemicalSafetyCardURL;
                    nfpaReactivity = m1.Groups[3].Value;
                    nfpaReactivityReference = m_iloChemicalSafetyCardURL;
                }
                pattern1 = "Octanol/water partition coefficient as log Pow: (?<1>\\S+)</span>";
                m1 = System.Text.RegularExpressions.Regex.Match(output, pattern1,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                logKow = m1.Groups[1].Value;
                logKowReference = m_iloChemicalSafetyCardURL;
            }
        }

        void GetTOXNETInformation(string compoundName, string casNo)
        {
            // http://toxnet.nlm.nih.gov/cgi-bin/sis/search2/f?./temp/~oiB60G:1
            string uriString = "https://toxgate.nlm.nih.gov/cgi-bin/sis/search2";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString);
            string postData = "queryxxx=" + casNo;
            postData += "&chemsyn=1";
            postData += "&database=hsdb";
            postData += "&Stemming=1";
            postData += "&and=1";
            postData += "&second_search=1";
            postData += "&gateway=1";
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            string responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            string s1 = responseString.Replace("<br>", "");
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.Load(new System.IO.StringReader(s1));
            string tempFileName = document.FirstChild["TemporaryFile"].InnerText;
            uriString = "http://toxnet.nlm.nih.gov/cgi-bin/sis/search2/f?" + tempFileName;

            //// Whole Document
            m_hsdbDocumentURL = "http://toxgate.nlm.nih.gov/cgi-bin/sis/search2/r?dbs+hsdb:@term+@DOCNO+" + document.FirstChild["Id"].InnerText.Split(' ')[0];

            // Molecular Formula 
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:mf");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            string mfResposne = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            string pattern = "Molecular Formula:</h3>\n<br>\n\\s*(?<1>\\S+)\\s*<br><code><NOINDEX>(?<2>[^\\n]*)</NOINDEX>";
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(mfResposne, pattern,
                       System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                      TimeSpan.FromSeconds(1));
            this.m_molecularFormula = m.Groups[1].Value;
            this.m_molecularFormulareference = m.Groups[2].Value;


            // AutoIgnition temperature 
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:auto");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            string autoResposne = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            pattern = "Autoignition Temperature:</h3>\n<br>\n\\s*(?<1>\\S+)\\s*Deg\\s*(?<2>\\S+)\\s*\\((?<4>\\S+)\\s*deg\\s*(?<5>\\S+)\\s*\\)<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
            m = System.Text.RegularExpressions.Regex.Match(autoResposne, pattern,
                       System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                      TimeSpan.FromSeconds(1));
            if (m.Groups.Count == 1)
            {
                pattern = "Autoignition Temperature:</h3>\n<br>\n\\s*(?<1>\\S+)\\s*Deg\\s*(?<2>\\S+)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(autoResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
            }

            // Chemical Properties 
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:cpp");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            string propertiesResposne = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            // Chemical Properties 
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:ph");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            propertiesResposne = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            pattern = "pH:\\s*(?<1>\\S+)\\s*(?<2>[^\\n]*)\\s*<NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
            m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                       System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                      TimeSpan.FromSeconds(1));
            string m_pH = m.Groups[1].Value;
            string m_pHAdditional = m.Groups[2].Value;
            string m_pHReference = m.Groups[3].Value;

            // heat of vaporization: 
            pattern = "Heat of Vaporization:</h3>\\s*<br>\\s*Latent:\\s*(?<1>\\S+)\\s*(?<2>\\S+)\\s*(?<4>[^\\n]*)<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
            m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                       System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                      TimeSpan.FromSeconds(1));
            if (m.Groups.Count == 1)
            {
                pattern = "Heat of Vaporization:</h3>\\s*<br>\\s*Enthalpy of vaporization:\\s*(?<1>\\S+)\\s*(?<2>\\S+)\\s*(?<4>[^\\n]*)<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
            }
            if (m.Groups.Count == 1)
            {
                pattern = "Heat of Vaporization:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*(?<2>\\S+)\\s*(?<4>[^\\n]*)<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
            }
            m_HeatOfVaporization = m.Groups[1].Value;
            m_HeatOfVaporizationUnit = m.Groups[2].Value;
            if (m_HeatOfVaporizationUnit.ToLower() == "kj/g")
            {
                m_HeatOfVaporization = (Convert.ToDouble(m_HeatOfVaporization) * 1000).ToString();
                m_HeatOfVaporizationUnit = "kJ/kg";
            }
            if (m_HeatOfVaporizationUnit.ToLower() == "btu/lb")
            {
                m_HeatOfVaporization = (Convert.ToDouble(m_HeatOfVaporization) * 2.32599999962).ToString();
                m_HeatOfVaporizationUnit = "kJ/kg";
            }
            if (m_HeatOfVaporizationUnit.ToLower() == "g-cal/g")
            {
                m_HeatOfVaporization = (Convert.ToDouble(m_HeatOfVaporization) * 4.184 * 1000).ToString();
                m_HeatOfVaporizationUnit = "kJ/kg";
            }
            if (m_HeatOfVaporizationUnit.ToLower() == "kcal/mole")
            {
                m_HeatOfVaporization = (Convert.ToDouble(m_HeatOfVaporization) * 4.184).ToString();
                m_HeatOfVaporizationUnit = "kJ/kg";
            }
            if (m_HeatOfVaporizationUnit.ToLower() == "kj/mole" || m_HeatOfVaporizationUnit.ToLower() == "kj/mol")
            {
                m_HeatOfVaporization = (Convert.ToDouble(m_HeatOfVaporization) * 1000).ToString();
                m_HeatOfVaporizationUnit = "kJ/kg";
            }
            if (m_HeatOfVaporizationUnit.ToLower() == "gcal/gmole")
            {
                m_HeatOfVaporization = (Convert.ToDouble(m_HeatOfVaporization) * 4.184).ToString();
                m_HeatOfVaporizationUnit = "kJ/kg";
            }
            m_heatOfVaporizationReference = m.Groups[3].Value;
            if (m_casNo == "64-19-7")
            {
                m_HeatOfVaporization = "1219055.6";
                m_HeatOfVaporizationUnit = "kJ/kg";
                m_heatOfVaporizationReference = "[Haynes, W.M. (ed.). CRC Handbook of Chemistry and Physics. 94th Edition. CRC Press LLC, Boca Raton: FL 2013-2014, p. 6-132] **PEER REVIEWED** ";

            }

            // heat of combustion: 
            pattern = "Heat of Combustion:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*(?<2>\\S+)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
            m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                       System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                      TimeSpan.FromSeconds(1));
            if (m.Groups.Count == 1)
            {
                pattern = "Heat of Combustion:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*(?<2>\\S+)\\s*(?<3>[^\\n]*)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
            }
            m_HeatOfCombustion = m.Groups[1].Value;
            m_HeatOfCombustionUnit = m.Groups[2].Value;
            if (m_HeatOfCombustionUnit.ToLower() == "kj/g")
            {
                m_HeatOfCombustion = (Convert.ToDouble(m_HeatOfCombustion) * 1000).ToString();
                m_HeatOfCombustionUnit = "kJ/kg";
            }
            if (m_HeatOfCombustionUnit.ToLower() == "btu/lb")
            {
                m_HeatOfCombustion = (Convert.ToDouble(m_HeatOfCombustion) * 2.32599999962).ToString();
                m_HeatOfCombustionUnit = "kJ/kg";
            }
            if (m_HeatOfCombustionUnit.ToLower() == "j/kmol")
            {
                m_HeatOfCombustion = (Convert.ToDouble(m_HeatOfCombustion) * 1000 * 1000).ToString();
                m_HeatOfCombustionUnit = "kJ/kg";
            }
            if (m_HeatOfCombustionUnit.ToLower() == "kj/mole" || m_HeatOfCombustionUnit.ToLower() == "kj/mol")
            {
                m_HeatOfCombustion = (Convert.ToDouble(m_HeatOfCombustion) * 1000).ToString();
                m_HeatOfCombustionUnit = "kJ/kg";
            }
            if (m_HeatOfCombustionUnit.ToLower() == "kj/kg")
            {
                m_HeatOfCombustion = (Convert.ToDouble(m_HeatOfCombustion) * 1000).ToString();
                m_HeatOfCombustionUnit = "kJ/kg";
            }
            m_HeatOfCombustionReference = m.Groups[3].Value;

            // Flash Point: 
            if (string.IsNullOrEmpty(flashPtValue))
            {
                // Chemical Properties 
                pattern = "Flash Point:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*deg\\s*(?<2>\\S+),\\s*(?<5>\\S+)\\s*deg\\s*(?<6>\\S+)\\s*(?<4>[^\\n]*)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                               System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                              TimeSpan.FromSeconds(1));
                if (m.Groups.Count == 1)
                {
                    pattern = "Flash Point:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*deg\\s*(?<2>\\S+)\\s*(?<4>[^\\n]*)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                    m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                                   System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                                  TimeSpan.FromSeconds(1));
                }
                if (m.Groups[2].Value == "C")
                {
                    flashPtValue = m.Groups[1].Value;
                    flashPtUnit = m.Groups[2].Value;
                }
                else if (m.Groups[4].Value == "F")
                {
                    flashPtValue = ((Convert.ToDouble(m.Groups[1].Value) - 32) * 5 / 9).ToString();
                    flashPtUnit = "C";
                }
                flashPtReference = m.Groups[3].Value;
            }

            // Vapor Pressure: 
            if (string.IsNullOrEmpty(vaporPress))
            {
                pattern = "Vapor Pressure:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*(?<2>[^\"']*) at (?<3>\\S+) deg (?<4>\\S+)\\s*(?<5>[^\\n]*)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                vaporPress = m.Groups[1].Value.ToLower();
                if (vaporPress.Contains("x"))
                {
                    vaporPress = vaporPress.Replace("x10", "x");
                    string[] splits = vaporPress.Split('x');
                    vaporPress = (Convert.ToDouble(splits[0]) * Math.Pow(10, Convert.ToDouble(splits[1]))).ToString();
                }
                vaporPressUnit = m.Groups[2].Value;
                vaporPressTemp = m.Groups[3].Value;
                vaporPressTempUnit = m.Groups[4].Value;
            }

            // boilingPoint: 
            if (string.IsNullOrEmpty(bpValue))
            {
                pattern = "Boiling Point:</h3>\\s*<br>\\s*(?<1>\\S+) deg (?<2>\\S+)<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                if (m.Groups.Count == 1)
                {
                    pattern = "Boiling Point:</h3>\\s*<br>\\s*(?<1>\\S+) deg (?<2>\\S+)\\s*(?<4>[^\\n]*)<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                    m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                               System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                              TimeSpan.FromSeconds(1));
                }
                bpValue = m.Groups[1].Value;
                bpUnit = m.Groups[2].Value;
                bpReference = m.Groups[3].Value;
            }

            // meltingPoint: 
            if (string.IsNullOrEmpty(mpValue))
            {
                pattern = "Melting Point:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*deg\\s*(?<2>\\S+)\\s*(?<4>[^\\n]*)\\s*<br><code><NOINDEX>(?<3>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                mpValue = m.Groups[1].Value;
                mpUnit = m.Groups[2].Value;
                mpReference = m.Groups[3].Value;
            }

            // density: 
            if (string.IsNullOrEmpty(densityValue) || string.IsNullOrEmpty(relativeDensityValue))
            {
                pattern = "Density/Specific Gravity:</h3>\\s*<br>\\s*Gas:\\s*(?<1>\\S+)\\s*(?<2>[^\"']*)\\s*at\\s*(?<4>[^\\n]*)\\s*<br><code><NOINDEX>(?<4>[^\\n]*)</NOINDEX>";
                m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                           System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                          TimeSpan.FromSeconds(1));
                if (m.Groups.Count == 1)
                {
                    pattern = "Density/Specific Gravity:</h3>\\s*<br>\\s*Absolute density:\\s*(?<1>\\S+)\\s*(?<2>[^\"']*)\\s*at\\s*(?<4>[^\\n]*)\\s*<br><code><NOINDEX>(?<4>[^\\n]*)</NOINDEX>";
                    m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                               System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                              TimeSpan.FromSeconds(1));
                }
                if (m.Groups.Count == 1)
                {
                    pattern = "Density/Specific Gravity:</h3>\\s*<br>\\s*(?<1>\\S+)\\s*(?<2>[^\"']*)\\s*at\\s*(?<4>[^\\n]*)\\s*<br><code><NOINDEX>(?<4>[^\\n]*)</NOINDEX>";
                    m = System.Text.RegularExpressions.Regex.Match(propertiesResposne, pattern,
                               System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                              TimeSpan.FromSeconds(1));
                }
                densityValue = m.Groups[1].Value;
                densityUnit = m.Groups[2].Value;
                densityTemperature = m.Groups[3].Value;
                densityTemperatureUnit = m.Groups[4].Value;
                densityReference = m.Groups[5].Value;
            }

            // Chemical safety 
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:csha");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.StringReader safetyReader = new System.IO.StringReader(new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd());

            if (string.IsNullOrEmpty(nfpaFire))
            {
                // NFPA: 
                request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:nfpa");
                response = (System.Net.HttpWebResponse)request.GetResponse();
                string nfpaResponseString = safetyReader.ReadToEnd();
                int index = nfpaResponseString.IndexOf("Health: ");
                if (index > 0)
                {
                    nfpaHealth = nfpaResponseString.Substring(index + 8, 1);
                }
                index = nfpaResponseString.IndexOf("Flammability: ");
                if (index > 0)
                {
                    nfpaFire = nfpaResponseString.Substring(index + 14, 1);
                }
                index = nfpaResponseString.IndexOf("Reactivity: ");
                if (index > 0)
                {
                    nfpaReactivity = nfpaResponseString.Substring(index + 12, 1);
                }
                if (index > 0)
                {
                    index = nfpaResponseString.IndexOf("instability: ");
                    if (index == -1)
                    {
                        nfpaReactivity = nfpaResponseString.Substring(index + 13, 1);
                    }
                }
            }
        }

        void GetToxData(string compoundName, string casNo)
        {
            // http://toxnet.nlm.nih.gov/cgi-bin/sis/search2/f?./temp/~oiB60G:1
            string uriString = "https://toxgate.nlm.nih.gov/cgi-bin/sis/search2";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString);
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString);
            string postData = "queryxxx=" + casNo;
            postData += "&chemsyn=1";
            postData += "&database=hsdb";
            postData += "&Stemming=1";
            postData += "&and=1";
            postData += "&second_search=1";
            postData += "&gateway=1";
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            System.Net.WebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            string responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            string s1 = responseString.Replace("<br>", "");
            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.Load(new System.IO.StringReader(s1));
            string tempFileName = document.FirstChild["TemporaryFile"].InnerText;
            uriString = "https://toxgate.nlm.nih.gov/cgi-bin/sis/search2/f?" + tempFileName;

            // EcoTox
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:etxv");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            string ecoToxResposne = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            string pattern = "LC50; Species:\\s*(?<1>[^\\n]*)\\s*[,;] Conditions:\\s*(?<2>[^\\n]*)\\s*[,;] Concentration:\\s*(?<3>\\S+)\\s*(?<4>\\S+)\\s*for\\s*(?<5>[^\\n]*)\\s*<br><code><NOINDEX>\\s*(?<6>[^\\n]*)\\s*</NOINDEX>";
            System.Text.RegularExpressions.MatchCollection matchColl = System.Text.RegularExpressions.Regex.Matches(ecoToxResposne, pattern,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                TimeSpan.FromSeconds(1));

            double lc50 = 1000000.0;
            System.Text.RegularExpressions.Match lc50Match = null;
            foreach (System.Text.RegularExpressions.Match match in matchColl)
            {
                string lc50STR = match.Groups[3].Value;
                lc50STR = lc50STR.Replace("&gt;", "");
                lc50STR = lc50STR.Replace("&lt;", "");
                double conc = Convert.ToDouble(lc50STR);
                string unit = match.Groups[4].Value;
                if (string.Compare(unit, "mg/l", true) == 0)
                {
                    if (conc < lc50)
                    {
                        lc50 = conc;
                        lc50Match = match;
                    }
                }
                else if (string.Compare(unit, "ug/l", true) == 0)
                {
                    {
                        if (conc / 1000 < lc50) lc50 = conc / 1000;
                        lc50Match = match;

                    }
                }
            }
            string lc50Value = string.Empty;
            string lc50Reference = string.Empty;
            if (lc50Match != null)
            {
                lc50Value = lc50Match.Groups[3].Value;
                lc50Reference = "Species: " + lc50Match.Groups[1].Value + "; Conditions: " + lc50Match.Groups[2].Value + "; Time: " + lc50Match.Groups[5].Value + "; Reference: " + lc50Match.Groups[6].Value;
            }
            //pattern = "EC50; Species:\\s*(?<1>[^\\n]*)\\s*[,;] Conditions:\\s*(?<2>[^\\n]*)\\s*[,;] Concentration:\\s*(?<3>[^\\n]*)\\s*for\\s*(?<4>[^\\n]*)\\s*[,;] Effect: \\s*(?<5>[^\\n]*)\\s*<br><code><NOINDEX>\\s*(?<6>[^\\n]*)\\s*</NOINDEX>";
            //matchColl = System.Text.RegularExpressions.Regex.Matches(ecoToxResposne, pattern,
            //    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
            //    TimeSpan.FromSeconds(1));

            // NonHuman Tox
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString + ":1:ntxv");
            response = (System.Net.HttpWebResponse)request.GetResponse();
            string nonHumanToxResposne = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            pattern = "LD50\\s*(?<1>\\S+)\\s*oral\\s*[(&gt;)\\s*]\\s*(?<2>\\S+)\\s*(?<3>\\S+)\\s*<br><code><NOINDEX>\\s*(?<4>[^\\n]*)\\s*</NOINDEX>";
            matchColl = System.Text.RegularExpressions.Regex.Matches(nonHumanToxResposne, pattern,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                TimeSpan.FromSeconds(1));

            double ld50Oral = 1000000.0;
            System.Text.RegularExpressions.Match ld50OralMatch = null;
            foreach (System.Text.RegularExpressions.Match match in matchColl)
            {
                string ld50STR = match.Groups[2].Value;
                ld50STR = ld50STR.Replace("&gt;", "");
                ld50STR = ld50STR.Replace("&lt;", "");
                double conc = 0;
                if (!double.TryParse(ld50STR, out conc))
                {
                    string[] values = ld50STR.Split('-');
                    double.TryParse(values[0], out conc);
                }
                string unit = match.Groups[3].Value;
                if (string.Compare(unit, "mg/kg", true) == 0)
                {
                    if (conc < ld50Oral)
                    {
                        ld50Oral = conc;
                        ld50OralMatch = match;
                    }
                }
                else if (string.Compare(unit, "g/kg", true) == 0)
                {
                    {
                        if (conc * 1000 < ld50Oral) ld50Oral = conc * 1000;
                        ld50OralMatch = match;

                    }
                }
            }
            string ld50OralSpecies = string.Empty;
            string ld50OralValue = string.Empty;
            string ld50OralReference = string.Empty;
            if (ld50OralMatch != null)
            {
                ld50OralSpecies = ld50OralMatch.Groups[1].Value;
                ld50OralValue = ld50Oral.ToString();
                ld50OralReference = ld50OralMatch.Groups[4].Value;
            }

            double ld50Dermal = 1000000.0;
            System.Text.RegularExpressions.Match ld50DermalMatch = null;
            string[] dermalSynomoyms =
            {
                "dermal",
                "percutaneous",
                "sc",
                "skin"
            };
            foreach (string synonym in dermalSynomoyms)
            {
                pattern = "LD50\\s*(?<1>\\S+)\\s*" + synonym + "\\s*[(&gt;)\\s*]\\s*(?<2>\\S+)\\s*(?<3>\\S+)\\s*<br><code><NOINDEX>\\s*(?<4>[^\\n]*)\\s*</NOINDEX>";
                matchColl = System.Text.RegularExpressions.Regex.Matches(nonHumanToxResposne, pattern,
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                    TimeSpan.FromSeconds(1));

                foreach (System.Text.RegularExpressions.Match match in matchColl)
                {
                    string ld50STR = match.Groups[2].Value;
                    ld50STR = ld50STR.Replace("&gt;", "");
                    ld50STR = ld50STR.Replace("&lt;", "");
                    double conc = Convert.ToDouble(ld50STR);
                    string unit = match.Groups[3].Value;
                    if (string.Compare(unit, "mg/kg", true) == 0)
                    {
                        if (conc < ld50Dermal)
                        {
                            ld50Dermal = conc;
                            ld50DermalMatch = match;
                        }
                    }
                    else if (string.Compare(unit, "g/kg", true) == 0)
                    {
                        {
                            if (conc * 1000 < ld50Dermal) ld50Dermal = conc * 1000;
                            ld50DermalMatch = match;

                        }
                    }
                }
            }
            pattern = "LD50\\s*(?<1>\\w*)\\s*[(&gt;)\\s*]\\s*(?<2>\\S+)\\s*(?<3>\\S+)\\s*dermal[.\\s+]\\s*\\s*<br><code><NOINDEX>\\s*(?<4>[^\\n]*)\\s*</NOINDEX>";
            matchColl = System.Text.RegularExpressions.Regex.Matches(nonHumanToxResposne, pattern,
                System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
                TimeSpan.FromSeconds(1));

            foreach (System.Text.RegularExpressions.Match match in matchColl)
            {
                string ld50STR = match.Groups[2].Value;
                ld50STR = ld50STR.Replace("&gt;", "");
                ld50STR = ld50STR.Replace("&lt;", "");
                double conc = Convert.ToDouble(ld50STR);
                string unit = match.Groups[3].Value;
                if (string.Compare(unit, "mg/l", true) == 0)
                {
                    if (conc < ld50Dermal)
                    {
                        ld50Dermal = conc;
                        ld50DermalMatch = match;
                    }
                }
                else if (string.Compare(unit, "ug/l", true) == 0)
                {
                    {
                        if (conc / 1000 < ld50Dermal) ld50Dermal = conc / 1000;
                        ld50DermalMatch = match;

                    }
                }
            }

            string ld50DermalSpecies = string.Empty;
            string ld50DermalValue = string.Empty;
            string ld50DermalReference = string.Empty;
            if (ld50DermalMatch != null)
            {
                ld50DermalSpecies = ld50DermalMatch.Groups[1].Value;
                ld50DermalValue = ld50Dermal.ToString();
                ld50DermalReference = ld50DermalMatch.Groups[4].Value;
            }
        }

        private void nioshCDSCButton_Click(object sender, EventArgs e)
        {
            string icscNumber = string.Empty;

            System.Collections.Generic.List<string> ICSCnumbers = new System.Collections.Generic.List<string>(0);
            System.Collections.Generic.List<string> caNoss = new System.Collections.Generic.List<string>(0);
            try
            {
                System.IO.StringReader strReader = new System.IO.StringReader(Properties.Resources.ICSCnumberByCAS);
                string nextLine = strReader.ReadLine();
                while (!string.IsNullOrEmpty(nextLine))
                {
                    string[] split = nextLine.Split('*');
                    ICSCnumbers.Add(split[0]);
                    caNoss.Add(split[1].Remove(0, 1));
                    if (split[1].Remove(0, 1) == m_casNo)
                    {
                        icscNumber = split[0];
                        break;
                    }
                    nextLine = strReader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }

            if (!string.IsNullOrEmpty(icscNumber))
            {
                System.Diagnostics.Process.Start("http://www.cdc.gov/niosh/ipcsneng/neng" + icscNumber + ".html");
                return;
            }
            System.Windows.Forms.MessageBox.Show("There is no NIOSH Chemical Safety Data Card for " + m_CompoundName);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToxnetHSDBButton_Click_1(object sender, EventArgs e)
        {
            string uriString = "https://toxgate.nlm.nih.gov/cgi-bin/sis/search2";
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString);
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriString);
            string postData = "queryxxx=" + m_casNo;
            postData += "&chemsyn=1";
            postData += "&database=hsdb";
            postData += "&Stemming=1";
            postData += "&and=1";
            postData += "&second_search=1";
            postData += "&gateway=1";
            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            System.Net.WebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            string responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
            string s1 = responseString.Replace("<br>", "");
            var XMLReader = new System.Xml.XmlTextReader(new System.IO.StringReader(s1));
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(QueryResult));
            if (serializer.CanDeserialize(XMLReader))
            {
                // Synonyms means more than one name for the same chemical/CAS Number.
                QueryResult result = (QueryResult)serializer.Deserialize(XMLReader);
                uriString = "https://toxgate.nlm.nih.gov/cgi-bin/sis/search2/f?" + result.TemporaryFile;
                string[] ids = result.Id.Split(' ');
                System.Diagnostics.Process.Start("http://toxgate.nlm.nih.gov/cgi-bin/sis/search2/r?dbs+hsdb:@term+@DOCNO+" + ids[0]);
            }
        }

        private void internationalChemSafetyDataCardutton_Click(object sender, EventArgs e)
        {
            string icscNumber = string.Empty;

            System.Collections.Generic.List<string> ICSCnumbers = new System.Collections.Generic.List<string>(0);
            System.Collections.Generic.List<string> caNoss = new System.Collections.Generic.List<string>(0);
            try
            {
                System.IO.StringReader strReader = new System.IO.StringReader(Properties.Resources.ICSCnumberByCAS);
                string nextLine = strReader.ReadLine();
                while (!string.IsNullOrEmpty(nextLine))
                {
                    string[] split = nextLine.Split('*');
                    ICSCnumbers.Add(split[0]);
                    caNoss.Add(split[1].Remove(0, 1));
                    if (split[1].Remove(0, 1) == m_casNo)
                    {
                        icscNumber = split[0];
                        break;
                    }
                    nextLine = strReader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }

            if (!string.IsNullOrEmpty(icscNumber))
            {
                System.Diagnostics.Process.Start("http://www.ilo.org/dyn/icsc/showcard.display?p_lang=en&p_card_id=" + icscNumber + "&p_version=1");
                return;
            }
            System.Windows.Forms.MessageBox.Show("There is no International Chemical Safety Data Card for " + m_CompoundName);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ACToRForm form = new ACToRForm(m_casNo);
            form.ShowDialog();
        }

        void GetEuropeanInfo(string casNo)
        {
            this.label18.Visible = true;
            this.label19.Visible = true;
            this.label20.Visible = true;
            this.label18.Text = "Risk Phrases: ";
            foreach (string str in europeanChemicalList.Rphrase(m_casNo))
            {
                this.label18.Text = this.label18.Text + str + " ";
            }
            this.label19.Text = "Safety Phrases: ";
            foreach (string str in europeanChemicalList.sPhrase(m_casNo))
            {
                this.label19.Text = this.label19.Text + str + " ";
            }
            this.label20.Text = "Indications of Danger: ";
            foreach (string str in europeanChemicalList.IndicationOfDanger(m_casNo))
            {
                this.label20.Text = this.label20.Text + str + " ";
            }
            //string url = europeanChemicalList.Link(m_casNo);
            //System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            //System.Net.WebResponse response = request.GetResponse();
            //System.IO.StringReader reader = new System.IO.StringReader(new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd());
            //HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            //document.Load(reader);
            //System.Xml.XPath.XPathNavigator navigator = document.CreateNavigator();
            //System.Xml.XPath.XPathNavigator node = navigator.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[2]/table[1]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[2]");
            //string[] rPhrases = node.Value.Trim().Split(' ');
            //node = navigator.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[2]/table[1]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[3]");
            //string[] sPhrases = node.Value.Trim().Split(' ');
            //node = navigator.SelectSingleNode("/html[1]/body[1]/div[1]/div[1]/div[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[2]/table[1]/div[1]/div[2]/table[1]/tbody[1]/tr[1]/td[4]");
            //string[] indicationOfDanger = node.Value.Trim().Split(' ');
            ////string pattern = "(?<1>R\\d+[/\\d+]*)";
            ////System.Text.RegularExpressions.MatchCollection matchColl = System.Text.RegularExpressions.Regex.Matches(text, pattern,
            ////    System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled,
            ////    TimeSpan.FromSeconds(1));
            ////bool c = text.Contains(" C ");
            ////bool t = text.Contains("T");
            ////bool tPlus = text.Contains("T+");
            ////bool xi = text.Contains("Xi");
            ////bool xn = text.Contains("Xn");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(europeanChemicalList.Link(m_casNo));
            return;
        }
    }

    public class Rootobject
    {
        public Datarow DataRow { get; set; }
    }

    public class Datarow
    {
        public int genericChemicalId { get; set; }
        public string preferredName { get; set; }
        public string casrn { get; set; }
    }
}
