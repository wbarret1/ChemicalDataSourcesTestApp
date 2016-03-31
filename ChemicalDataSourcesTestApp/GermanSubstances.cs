using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class QueryResult
    {

        private ushort countField;

        private byte dispMaxField;

        private string idField;

        private string temporaryFileField;

        private QueryResultTranslation translationField;

        /// <remarks/>
        public ushort Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }

        /// <remarks/>
        public byte DispMax
        {
            get
            {
                return this.dispMaxField;
            }
            set
            {
                this.dispMaxField = value;
            }
        }

        /// <remarks/>
        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string TemporaryFile
        {
            get
            {
                return this.temporaryFileField;
            }
            set
            {
                this.temporaryFileField = value;
            }
        }

        /// <remarks/>
        public QueryResultTranslation Translation
        {
            get
            {
                return this.translationField;
            }
            set
            {
                this.translationField = value;
            }
        }
    }


    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class QueryResultTranslation
    {

        private string[] bField;

        private string[] textField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("b")]
        public string[] b
        {
            get
            {
                return this.bField;
            }
            set
            {
                this.bField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string[] Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }
    }



    static class GermanWGKSubstanceList
    {
        static private System.Collections.Generic.List<GermanWGKSubstance> m_GermanWGKSubstances;

        static GermanWGKSubstanceList()
        {
            m_GermanWGKSubstances = new System.Collections.Generic.List<GermanWGKSubstance>();
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources.DE_Hazardous_Water_AID_1);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    m_GermanWGKSubstances.Add(new GermanWGKSubstance(nextLine));
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }

        static public string WGK(string casNo)
        {
            foreach (GermanWGKSubstance substance in m_GermanWGKSubstances)
            {
                if (substance.CASNumber == casNo) return substance.HazardClass;
            }
            return string.Empty;
        }
    }


    class GermanWGKSubstance
    {
        public GermanWGKSubstance(string line)
        {
            char tab = '\t';
            string[] parts = line.Split(tab);
            m_SubstanceName = parts[0];
            m_SubstanceID = parts[1];
            m_CASNumber = parts[2];
            m_AssayID = parts[3];
            m_HazardClass = parts[4];
            m_HazardClassDescription = parts[5];
        }

        private string m_SubstanceName;
        public string SubstanceName
        {
            get { return m_SubstanceName; }
        }
        private string m_SubstanceID;
        public string SubstanceID
        {
            get { return m_SubstanceID; }
        }

        private string m_CASNumber;
        public string CASNumber
        {
            get { return m_CASNumber; }
        }

        private string m_AssayID;
        public string AssayID
        {
            get { return m_AssayID; }
        }
        private string m_HazardClass;
        public string HazardClass
        {
            get { return m_HazardClass; }
        }

        private string m_HazardClassDescription;
        public string HazardClassDescription
        {
            get { return m_HazardClassDescription; }
        }
    }
}
