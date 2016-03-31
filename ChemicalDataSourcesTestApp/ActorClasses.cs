using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{
    namespace ActorPhysicalChemicalProperties
    {
        
        public class Rootobject
        {
            public Datalist DataList { get; set; }
        }

        public class Datalist
        {
            public List[] list { get; set; }
            public int count { get; set; }
        }

        public class List
        {
            public string name { get; set; }
            public string source { get; set; }
            public string description { get; set; }
            public string modelClass { get; set; }
            public string resultType { get; set; }
            public string rawResult { get; set; }
            public float resultMean { get; set; }
            public float resultMin { get; set; }
            public float resultMax { get; set; }
            public string resultUnit { get; set; }
        }
    }

    namespace ActorDSSTox
    {
        public class Rootobject
        {
            public Datarow DataRow { get; set; }
        }

        public class Datarow
        {
            public int gsid { get; set; }
            public string casrn { get; set; }
            public string imageURL { get; set; }
            public string preferredName { get; set; }
            public string casType { get; set; }
            public string smiles { get; set; }
            public string chemtype { get; set; }
            public string inchi { get; set; }
            public string inchiKey { get; set; }
            public string chiralStereo { get; set; }
            public string dblStereo { get; set; }
            public string organicForm { get; set; }
            public string iupac { get; set; }
            public string molFormula { get; set; }
        }
    }

    namespace ActorRegulatory
    {

        public class Rootobject
        {
            public Datalist DataList { get; set; }
        }

        public class Datalist
        {
            public List[] list { get; set; }
            public int count { get; set; }
        }

        public class List
        {
            public string name { get; set; }
            public string source { get; set; }
            public string description { get; set; }
            public string url { get; set; }
        }

    }
}
