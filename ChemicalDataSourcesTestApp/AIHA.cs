using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{
    static class AIHA
    {
        static private System.Collections.Generic.List<ERPGData> aihaData;

        static AIHA()
        {
            aihaData = new System.Collections.Generic.List<ERPGData>();
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources._2015_ERPG_Levels);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    aihaData.Add(new ERPGData(nextLine));
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }

        //string m_CompoundName;
        //string m_CasNo;
        static public string ERPG1(string CASNumber)
        {
            foreach (ERPGData data in aihaData)
                if (CASNumber == data.CASNumber) return data.ERPG1;
            return string.Empty;
        }

        static public string ERPG2(string CASNumber)
        {
            foreach (ERPGData data in aihaData)
                if (CASNumber == data.CASNumber) return data.ERPG2;
            return string.Empty;
        }

        static public string ERPG3(string CASNumber)
        {
            foreach (ERPGData data in aihaData)
                if (CASNumber == data.CASNumber) return data.ERPG3;
            return string.Empty;
        }

        static public string LEL(string CASNumber)
        {
            foreach (ERPGData data in aihaData)
                if (CASNumber == data.CASNumber) return data.LEL;
            return string.Empty;
        }
    }
    class ERPGData
    {
        string m_CompoundName;
        string m_CasNo;
        string m_ERPG1;
        string m_ERPG2;
        string m_ERPG3;
        string m_LEL;

        public ERPGData(string line)
        {
            char tab = '\t';
            string[] splits = line.Split(tab);
            m_CompoundName = splits[0];
            m_CasNo = splits[1];
            m_ERPG1 = splits[2];
            m_ERPG2 = splits[3];
            m_ERPG3 = splits[4];
            m_LEL = splits[5];
        }

        public string CASNumber
        {
            get
            {
                return m_CasNo;
            }
        }

        public string ERPG1
        {
            get
            {
                return m_ERPG1;
            }
        }

        public string ERPG2
        {
            get
            {
                return m_ERPG2;
            }
        }

        public string ERPG3
        {
            get
            {
                return m_ERPG3;
            }
        }

        public string LEL
        {
            get
            {
                return m_LEL;
            }
        }
    }
}
