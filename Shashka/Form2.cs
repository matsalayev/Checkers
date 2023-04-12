namespace Shashka
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (label3.Text == "‍🙎‍♂️") Class1.dastur = false;
            if (label3.Text == "💻") Class1.dastur = true;
            if(radioButton1.Checked == true) Class1.oq = true;
            if (radioButton2.Checked == true) Class1.oq = false;
            Class1.esc= false;
            this.Close();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            label3.Text = (label3.Text == "💻") ? label3.Text = "‍🙎‍♂️" : "💻";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Class1.esc = true;
            this.Close();
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true) 
            {
                label1.ForeColor =Color.FromArgb(30, 25, 14);
                label3.ForeColor = Color.White;
            }
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label3.ForeColor = Color.FromArgb(30, 25, 14);
                label1.ForeColor = Color.White;
            }
        }
    }
}
