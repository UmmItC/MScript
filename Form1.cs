namespace CS_SME_MScript
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("你確定要關閉本程式嗎?", "Close?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Bye Bye", "Exit");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "unknown" && textBox2.Text == "unknown")
            {
                this.Hide();
                Main main_ = new Main();
                main_.Show();
                label4.Visible = true;
                label4.Text = "已成功登入 ! !";
            }

            else
            {
                label4.Visible = true;
                MessageBox.Show("Error", "Error");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Text = "unknown";
                textBox2.Text = "unknown";
            }

            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Text = "unknown";
                textBox2.Text = "unknown";
            }

            else
            {
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
    }
}