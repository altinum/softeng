using System.Globalization;
using System.Text;

namespace Microsimulation
{
    public partial class Form1 : Form
    {
        List<Individual> individuals = new List<Individual>();
        List<ProbabilityOfDeath> pDeaths = new List<ProbabilityOfDeath>();
        List<ProbabilityOfBirth> pBirths = new List<ProbabilityOfBirth>();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"census.csv");
            while (!sr.EndOfStream)
            {
                string[] s = sr.ReadLine().Split(';');

                Individual sz = new Individual();
                sz.YearOfBirth = int.Parse(s[0]);
                sz.Sex = (Sex)int.Parse(s[1]);
                sz.NumberOfChildren = byte.Parse(s[2]);

                individuals.Add(sz);
            }
            sr.Close();


            //var engine = new FileHelperEngine<Individual>();
            //var Személyek = engine.ReadFile(@"c:\temp\census.csv");

            StreamReader sr1 = new StreamReader(@"fertality.csv");
            sr1.ReadLine(); //Van fejléc-sor is
            while (!sr1.EndOfStream)
            {
                string[] s = sr1.ReadLine().Split(';');
                ProbabilityOfBirth szv = new ProbabilityOfBirth();
                szv.Age = byte.Parse(s[0]);
                szv.NumberOfChildren = byte.Parse(s[2]);
                szv.PGivingBirth = double.Parse(s[3]);
                pBirths.Add(szv);
            }
            sr1.Close();


            //var engine = new FileHelperEngine<ProbabilityOfDeath>();
            //var pDeaths = engine.ReadFile(@"c:\temp\mortality.csv");

            StreamReader sr2 = new StreamReader(@"mortality.csv", Encoding.Default);
            sr2.ReadLine();
            while (!sr2.EndOfStream)
            {
                string[] s = sr2.ReadLine().Split(';');
                ProbabilityOfDeath hv = new ProbabilityOfDeath();
                hv.Sex = (Sex)byte.Parse(s[0]);
                //hv.Nem = (Nem)(s[0]=="Férfi"?1:2);
                hv.Age = byte.Parse(s[1]);
                hv.PDeath = double.Parse(s[2], CultureInfo.GetCultureInfo("HU-hu"));
                pDeaths.Add(hv);
            }
            sr2.Close();

            MessageBox.Show("Ok.");
        }
    }
}
