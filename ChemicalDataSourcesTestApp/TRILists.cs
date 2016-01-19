using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{
    static class TRIList
    {
        static private System.Collections.Generic.List<triChemical> triChemicalListData;

        static TRIList()
        {
            triChemicalListData = new System.Collections.Generic.List<triChemical>();
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources.tri_chemical_list_for_ry15_11_5_2015_1);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    triChemicalListData.Add(new triChemical(nextLine));
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }

        static public bool IsTRIChemical(string CASnumber)
        {
            foreach (triChemical data in triChemicalListData)
            {
                if (CASnumber == data.CasNumber)
                    return true;
            }
            return false;
        }
        static public bool IsPBTChemical(string CASnumber)
        {
            foreach (triChemical data in triChemicalListData)
            {
                if (CASnumber == data.CasNumber)
                {
                    if (data.DeMinimis == "*") return true;
                    return false;
                }
            }
            return false;
        }
    }


    class triChemical
    {
        string m_CasNumber;
        string m_Name;
        string m_DeMinimis;
        string m_CategoryDesription;
        string m_CategoryMember;

        public triChemical(string newLine)
        {
            string[] splits = newLine.Split('\t');
            m_CasNumber = splits[0].Replace(" ", string.Empty);
            m_Name = splits[1].Replace(" ", string.Empty);
            m_DeMinimis = splits[2].Replace(" ", string.Empty);
            m_CategoryDesription = splits[3].Replace(" ", string.Empty);
            m_CategoryMember = splits[4].Replace(" ", string.Empty);
        }

        public string CasNumber
        {
            get
            {
                return m_CasNumber;
            }
        }
        public string DeMinimis
        {
            get
            {
                return m_DeMinimis;
            }
        }
    }

    static class ListOfLists
    {
        static private System.Collections.Generic.List<hazardousSubstance> hazardousSubstanceListData;

        static ListOfLists()
        {
            hazardousSubstanceListData = new System.Collections.Generic.List<hazardousSubstance>();
            try
            {
                System.IO.StringReader reader = new System.IO.StringReader(Properties.Resources.list_of_lists);
                string nextLine = reader.ReadLine();
                while (nextLine != null)
                {
                    hazardousSubstanceListData.Add(new hazardousSubstance(nextLine));
                    nextLine = reader.ReadLine();
                }
            }
            catch (System.Exception obj)
            {
                obj.GetType();
            }
        }

        static public bool IsHAzardous(string CASnumber)
        {
            foreach (hazardousSubstance data in hazardousSubstanceListData)
            {
                if (CASnumber == data.CasNumber)
                    return true;
            }
            return false;
        }
    }


    class hazardousSubstance
    {
        string m_Name;
        string m_NameIndex;
        string m_CasNumber;
        string m_CasNumber313;
        string m_Section302;
        string m_Section304;
        string m_CERCLARQ;
        string m_Section313;
        string m_RCRA;
        string m_CERCLA112;

        public hazardousSubstance(string newLine)
        {
            string[] splits = newLine.Split('\t');
            m_Name = splits[0].Replace(" ", string.Empty);
            m_NameIndex = splits[1].Replace(" ", string.Empty);
            m_CasNumber = splits[2].Replace(" ", string.Empty);
            m_CasNumber313 = splits[3].Replace(" ", string.Empty);
            m_Section302 = splits[4].Replace(" ", string.Empty);
            m_Section304 = splits[5].Replace(" ", string.Empty);
            m_CERCLARQ = splits[6].Replace(" ", string.Empty);
            m_Section313 = splits[7].Replace(" ", string.Empty);
            m_RCRA = splits[8].Replace(" ", string.Empty);
            m_CERCLA112 = splits[9].Replace(" ", string.Empty);
        }

        public string CasNumber
        {
            get
            {
                return m_CasNumber313;
            }
        }
    }
}
