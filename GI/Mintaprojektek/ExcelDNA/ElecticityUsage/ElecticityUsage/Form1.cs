namespace ElecticityUsage
{
    public partial class Form1 : Form
    {
        List<PowerUsage> powerUsages = new List<PowerUsage>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines("c:\\temp\\power_usage.csv");

            // Az első sor a fejléc
            for (int i = 1; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(';');

                PowerUsage powerUsage = new PowerUsage
                {
                    Date = DateTime.Parse(parts[0]),
                    Appliance = parts[1],
                    HoursUsed = double.Parse(parts[2]),
                    PowerWatts = int.Parse(parts[3]),
                    EnergyKWh = double.Parse(parts[4])
                };

                powerUsages.Add(powerUsage);
            }

            dataGridView1.DataSource = powerUsages;

        }
    }
}
