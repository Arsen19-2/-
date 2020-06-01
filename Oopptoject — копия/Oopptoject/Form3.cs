using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Oopptoject
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
         
        }
        public void Change(Form2.Sportsmen sport) {
            textBox4.Text = sport.Name;
            textBox3.Text = sport.Personallife;
            maskedTextBox1.Text = sport.Citizenship;
            textBox2.Text = sport.Record;
            textBox1.Text = sport.Club;
            numericUpDown1.Value = sport.Rating;
            numericUpDown2.Value = sport.Age;
            numericUpDown3.Value = sport.Weight;
            numericUpDown4.Value = sport.Height;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null) { 
            string type = comboBox1.SelectedItem.ToString();
            Form2.Sportsmen NewSportsmen = new Form2.Sportsmen(textBox4.Text, Int32.Parse(numericUpDown2.Value.ToString()), textBox3.Text, textBox2.Text, maskedTextBox1.Text, textBox1.Text, Int32.Parse(numericUpDown1.Value.ToString()), Int32.Parse(numericUpDown4.Value.ToString()), Int32.Parse(numericUpDown3.Value.ToString()), type);
            Form2 form = new Form2();
            form.SaveChanges(NewSportsmen);
            form.ShowDialog();
            }
        }
        
    }
}
