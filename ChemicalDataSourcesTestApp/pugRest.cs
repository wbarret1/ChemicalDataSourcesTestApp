using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalDataSourcesTestApp
{
    namespace pugRest
    {

        public class Rootobject
        {
            public PC_Compounds[] PC_Compounds { get; set; }
        }

        public class PC_Compounds
        {
            public Id id { get; set; }
            public Atoms atoms { get; set; }
            public Bonds bonds { get; set; }
            public Coord[] coords { get; set; }
            public int charge { get; set; }
            public Prop[] props { get; set; }
            public Count count { get; set; }
        }

        public class Id
        {
            public Id1 id { get; set; }
        }

        public class Id1
        {
            public int cid { get; set; }
        }

        public class Atoms
        {
            public int[] aid { get; set; }
            public int[] element { get; set; }
        }

        public class Bonds
        {
            public int[] aid1 { get; set; }
            public int[] aid2 { get; set; }
            public int[] order { get; set; }
        }

        public class Count
        {
            public int heavy_atom { get; set; }
            public int atom_chiral { get; set; }
            public int atom_chiral_def { get; set; }
            public int atom_chiral_undef { get; set; }
            public int bond_chiral { get; set; }
            public int bond_chiral_def { get; set; }
            public int bond_chiral_undef { get; set; }
            public int isotope_atom { get; set; }
            public int covalent_unit { get; set; }
            public int tautomers { get; set; }
        }

        public class Coord
        {
            public int[] type { get; set; }
            public int[] aid { get; set; }
            public Conformer[] conformers { get; set; }
        }

        public class Conformer
        {
            public float[] x { get; set; }
            public float[] y { get; set; }
            public Style style { get; set; }
        }

        public class Style
        {
            public int[] annotation { get; set; }
            public int[] aid1 { get; set; }
            public int[] aid2 { get; set; }
        }

        public class Prop
        {
            public Urn urn { get; set; }
            public Value value { get; set; }
        }

        public class Urn
        {
            public string label { get; set; }
            public string name { get; set; }
            public int datatype { get; set; }
            public string release { get; set; }
            public string implementation { get; set; }
            public string version { get; set; }
            public string software { get; set; }
            public string source { get; set; }
            public string parameters { get; set; }
        }

        public class Value
        {
            public int ival { get; set; }
            public float fval { get; set; }
            public string binary { get; set; }
            public string sval { get; set; }
        }
    }
}

