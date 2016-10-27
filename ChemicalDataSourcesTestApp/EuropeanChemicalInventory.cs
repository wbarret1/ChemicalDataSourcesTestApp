using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{

    class EuropeanChemicalInventory
    {
        static private System.Collections.Generic.List<inventoryChemical> chemicals;

        static EuropeanChemicalInventory()
        {
            chemicals = new System.Collections.Generic.List<inventoryChemical>();
            int maxGHS = 0;
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources.annex_vi_clp_table_en);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    inventoryChemical chemical = new inventoryChemical(nextLine);
                    chemicals.Add(chemical);
                    if (maxGHS < chemical.countPictograms) maxGHS = chemical.countPictograms;
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }


        static public string ChemicalIdentification(string CASNumber)
        {
            foreach (inventoryChemical data in chemicals)
                if (CASNumber == data.CASNumber) return data.InternationalChemicalIdentification;
            return string.Empty;
        }

        static public string[] Pictograms(string CASNumber)
        {
            foreach (inventoryChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.Pictograms;
            return new string[0];
        }

        static public string SignalWord(string CASNumber)
        {
            foreach (inventoryChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.SignalWord;
            return string.Empty;
        }

        static public string ecNumber(string CASNumber)
        {
            foreach (inventoryChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.ecNumber;
            return string.Empty;
        }
    }



    class inventoryChemical
    {
        string m_indexNumber;
        string m_internationalChemicalIdentification;
        string m_ECNo;
        string m_CasNo;
        string[] m_ClassificationHazCatCode;
        string[] m_ClassificationHazStatementCode;
        List<string> m_Pictograms;
        string m_SignalWord = "Warning";
        string[] m_LabaellingHazCatCode;
        string[] m_LabaellingSupHazStatementCode;
        string m_secificConcLimit;
        string m_Notes;
        string m_ATP;


        public inventoryChemical(string line)
        {
            char tab = '\t';
            string[] splits = line.Split(tab);
            m_indexNumber = splits[0];
            m_internationalChemicalIdentification = splits[1];
            m_ECNo = splits[2];
            m_CasNo = splits[3];
            m_Pictograms = new List<string>();
            if (splits[6].Contains("GHS01")) m_Pictograms.Add("GHS01");
            if (splits[6].Contains("GHS02")) m_Pictograms.Add("GHS02");
            if (splits[6].Contains("GHS03")) m_Pictograms.Add("GHS03");
            if (splits[6].Contains("GHS04")) m_Pictograms.Add("GHS04");
            if (splits[6].Contains("GHS05")) m_Pictograms.Add("GHS05");
            if (splits[6].Contains("GHS06")) m_Pictograms.Add("GHS06");
            if (splits[6].Contains("GHS07")) m_Pictograms.Add("GHS07");
            if (splits[6].Contains("GHS08")) m_Pictograms.Add("GHS08");
            if (splits[6].Contains("GHS09")) m_Pictograms.Add("GHS09");
            if (splits[6].Contains("GHS10")) m_Pictograms.Add("GHS10");
            if (splits[6].Contains("GHS11")) m_Pictograms.Add("GHS11");
            if (splits[6].Contains("Dgr")) m_SignalWord = "Danger";



        }

        public string CASNumber
        {
            get
            {
                return m_CasNo;
            }
        }
        public string InternationalChemicalIdentification
        {
            get
            {
                return m_internationalChemicalIdentification;
            }
        }

        public string[] Pictograms
        {
            get
            {
                return this.m_Pictograms.ToArray<string>();
            }
        }
        public int countPictograms
        {
            get
            {
                return this.m_Pictograms.ToArray<string>().Length;
            }
        }

        public string SignalWord
        {
            get
            {
                return m_SignalWord;
            }
        }

        public string ecNumber
        {
            get
            {
                return m_ECNo;
            }
        }

        //    public string LEL
        //    {
        //        get
        //        {
        //            return m_LEL;
        //        }
        //    }
        //}





    }
    class europeanChemicalList
    {
        static private System.Collections.Generic.List<europeanListChemical> chemicals;

        static europeanChemicalList()
        {
            chemicals = new System.Collections.Generic.List<europeanListChemical>();
            int maxGHS = 0;
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources.europeanChemicalList);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    europeanListChemical chemical = new europeanListChemical(nextLine);
                    if (!string.IsNullOrEmpty(chemical.indexNumber))
                    {
                        chemicals.Add(chemical);
                    }
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }

        static public string Link(string CASNumber)
        {
            foreach (europeanListChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.Link;
            return string.Empty;
        }

        static public string ecNumber(string CASNumber)
        {
            foreach (europeanListChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.ecNumber;
            return string.Empty;
        }
        static public string[] Rphrase(string CASNumber)
        {
            foreach (europeanListChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.Rphrase;
            return new string[0];
        }
        static public string[] sPhrase(string CASNumber)
        {
            foreach (europeanListChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.Sphrase;
            return new string[0];
        }
        static public string[] IndicationOfDanger(string CASNumber)
        {
            foreach (europeanListChemical chem in chemicals)
                if (CASNumber == chem.CASNumber) return chem.IndicationOfDanger;
            return new string[0];
        }
    }



    class europeanListChemical
    {
        string m_IndexNumber;
        string m_ECNo;
        string m_CasNo;
        string m_Name;
        string m_PageID;
        string m_link;
        bool finished = false;
        string[] m_rPhrases;
        string[] m_SPhrases;
        string[] m_IndicationOfDanger;
        string[] m_HazardCodes;
        string[] m_HazardTexts;

        public europeanListChemical(string line)
        {
            char tab = '\t';
            string[] splits = line.Split(tab);
            m_IndexNumber = splits[1];
            m_ECNo = splits[2];
            m_CasNo = splits[3];
            m_Name = splits[4];
            m_PageID = splits[5];
            m_link = splits[6];
        }

        void FinishCompund()
        {
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(m_link);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.StringReader reader = new System.IO.StringReader(new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd());
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.Load(reader);
            //System.Xml.XPath.XPathNavigator navigator = document.CreateNavigator();
            HtmlAgilityPack.HtmlNode mainNode = document.GetElementbyId("p_p_id_dissclinventory_WAR_dissclinventoryportlet_");
            HtmlAgilityPack.HtmlNode clpTableNode = mainNode.ChildNodes[1].ChildNodes[9].ChildNodes[3].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[4].ChildNodes[1];
            HtmlAgilityPack.HtmlNode dataNode = clpTableNode.ChildNodes[0].ChildNodes[11];
            HtmlAgilityPack.HtmlNode currentNode = dataNode.ChildNodes[5];
            int numCodes = (dataNode.ChildNodes.Count - 5) / 2;
            m_HazardCodes = new string[numCodes];
            m_HazardTexts = new string[numCodes];
            for (int i = 0; i < numCodes; i++)
            {
                m_HazardTexts[i] = dataNode.ChildNodes[5 + i * 2].ChildNodes[3].InnerText.Trim();
                m_HazardCodes[i] = dataNode.ChildNodes[5 + i * 2].ChildNodes[1].InnerText.Trim();
            }
            HtmlAgilityPack.HtmlNode dsdTableNode = mainNode.ChildNodes[1].ChildNodes[9].ChildNodes[3].ChildNodes[1].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[4].ChildNodes[4].ChildNodes[1].ChildNodes[3].ChildNodes[1].ChildNodes[1].ChildNodes[3];
            HtmlAgilityPack.HtmlNode rPhraseNode = dsdTableNode.ChildNodes[1].ChildNodes[3];
            m_rPhrases = rPhraseNode.InnerText.Trim().Split(' ');
            HtmlAgilityPack.HtmlNode sPhraseNode = dsdTableNode.ChildNodes[1].ChildNodes[5];
            m_SPhrases = sPhraseNode.InnerText.Trim().Split(' ');
            HtmlAgilityPack.HtmlNode indicationOfDangerNode = dsdTableNode.ChildNodes[1].ChildNodes[7];
            m_IndicationOfDanger = indicationOfDangerNode.InnerText.Trim().Split(' ');
            finished = true;
        }

        public string CASNumber
        {
            get
            {
                return m_CasNo;
            }
        }
        public string indexNumber
        {
            get
            {
                return m_IndexNumber;
            }
        }

        public string ecNumber
        {
            get
            {
                return m_ECNo;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }
         public string PageID
        {
            get
            {
                return m_PageID;
            }
        }

        public string Link
        {
            get
            {
                return m_link;
            }
        }
        public string[] Rphrase
        {
            get
            {
                if (!finished) FinishCompund();
                return m_rPhrases;
            }
        }
        public string[] Sphrase
        {
            get
            {
                if (!finished) FinishCompund();
                return m_SPhrases;
            }
        }
        public string[] IndicationOfDanger
        {
            get
            {
                if (!finished) FinishCompund();
                return m_IndicationOfDanger;
            }
        }
        public string[] HazardCodes
        {
            get
            {
                if (!finished) FinishCompund();
                return m_HazardCodes;
            }
        }
        public string[] HazardTexts
        {
            get
            {
                if (!finished) FinishCompund();
                return m_HazardTexts;
            }
        }
    }
}
