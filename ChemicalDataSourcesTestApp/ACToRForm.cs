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
    public partial class ACToRForm : Form
    {
        public ACToRForm(string casNo)
        {
            InitializeComponent();
            this.GetACToRData(casNo);
        }

        void GetACToRData(string casNo)
        {
            string url = "http://actorws.epa.gov/actorws/physchemdb/dev/properties/" + casNo + ".json";
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
            this.listView1.Columns.Add("Name", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Result Mean", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Result Min", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Result Max", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Result Unit", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Description", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Result Type", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Model Class", -2, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Source", -2, HorizontalAlignment.Left);
            for (int i = 0; i < actorPhysChem.DataList.list.Length; i++)
            {
                ListViewItem item = new ListViewItem(actorPhysChem.DataList.list[i].name);
                item.SubItems.Add(actorPhysChem.DataList.list[i].resultMean.ToString());
                item.SubItems.Add(actorPhysChem.DataList.list[i].resultMin.ToString());
                item.SubItems.Add(actorPhysChem.DataList.list[i].resultMax.ToString());
                item.SubItems.Add(actorPhysChem.DataList.list[i].resultUnit);
                item.SubItems.Add(actorPhysChem.DataList.list[i].description);
                item.SubItems.Add(actorPhysChem.DataList.list[i].resultType);
                item.SubItems.Add(actorPhysChem.DataList.list[i].modelClass);
                item.SubItems.Add(actorPhysChem.DataList.list[i].source);
                this.listView1.Items.Add(item);

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

            url = "http://actorws.epa.gov/actorws/dsstox/v02/properties.json?casrn=" + casNo;
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer dssToxjSerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorDSSTox.Rootobject));
            System.IO.Stream responseStream = response.GetResponseStream();
            ActorDSSTox.Rootobject dssToxData = (ActorDSSTox.Rootobject)dssToxjSerializer.ReadObject(response.GetResponseStream());
            //this.propertiesJSONtextBox.Text = responseString;
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

            url = "http://actorws.epa.gov/actorws/edsp21/v02/regulatory/" + casNo + ".json";
            request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            response = request.GetResponse();
            System.Runtime.Serialization.Json.DataContractJsonSerializer regulatorySerializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(ActorRegulatory.Rootobject));
            ActorRegulatory.Rootobject actorRegulatory = (ActorRegulatory.Rootobject)regulatorySerializer.ReadObject(response.GetResponseStream());
            string[] regulatoryNames = new string[actorRegulatory.DataList.list.Length];
            string[] regulatorySources = new string[actorRegulatory.DataList.list.Length];
            string[] regulatoryDescriptions = new string[actorRegulatory.DataList.list.Length];
            string[] regulatoryUrls = new string[actorRegulatory.DataList.list.Length];
            this.listView2.Columns.Add("Name", -2, HorizontalAlignment.Left);
            this.listView2.Columns.Add("Source", -2, HorizontalAlignment.Left);
            this.listView2.Columns.Add("Description", -2, HorizontalAlignment.Left);
            this.listView2.Columns.Add("URL", -2, HorizontalAlignment.Left);
            for (int i = 0; i < actorRegulatory.DataList.list.Length; i++)
            {
                ListViewItem item = new ListViewItem(actorRegulatory.DataList.list[i].name);
                item.SubItems.Add(actorRegulatory.DataList.list[i].source);
                item.SubItems.Add(actorRegulatory.DataList.list[i].description);
                item.SubItems.Add(actorRegulatory.DataList.list[i].url);
                this.listView2.Items.Add(item);
                regulatoryNames[i] = actorRegulatory.DataList.list[i].name;
                regulatorySources[i] = actorRegulatory.DataList.list[i].source;
                regulatoryDescriptions[i] = actorRegulatory.DataList.list[i].description;
                regulatoryUrls[i] = actorRegulatory.DataList.list[i].url;
            }
        }

    }
}
