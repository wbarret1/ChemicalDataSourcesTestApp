using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{
    static class IDLH
    {
        static private System.Collections.Generic.List<nioshSubstance> idlhData;

        static IDLH()
        {
            idlhData = new System.Collections.Generic.List<nioshSubstance>();
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources.idlh);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    idlhData.Add(new nioshSubstance(nextLine));
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }

        static public string OriginalIDLH(string compoundName)
        {
            foreach (nioshSubstance data in idlhData)
                if (compoundName.ToLower() == data.Substance.ToLower()) return data.OriginalIDLH;
            return string.Empty;
        }

        static public string RevisedIDLH(string compoundName)
        {
            foreach (nioshSubstance data in idlhData)
                if (compoundName.ToLower() == data.Substance.ToLower()) return data.RevisedIDLH;
            return string.Empty;
        }
    }

    class nioshSubstance
    {
        string m_Substance;
        string m_OriginalIDLH;
        string m_RevisedIDLH;
        public nioshSubstance(string newLine)
        {
            string[] splits = newLine.Split('\t');
            m_Substance = splits[0].Split('(')[0];
            m_OriginalIDLH = splits[1];
            m_RevisedIDLH = splits[2];
        }

        public string Substance
        {
            get
            {
                return m_Substance;
            }
        }

        public string OriginalIDLH
        {
            get
            {
                return m_OriginalIDLH;
            }
        }

        public string RevisedIDLH
        {
            get
            {
                return m_RevisedIDLH;
            }
        }
    }
}
