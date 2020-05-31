using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
namespace Oopptoject
{
   
        public partial class Form2 : Form
    {
        public static List<Sportsmen> sportsmens = new List<Sportsmen>();

        [Serializable]
        public class Sportsmen
        {

            public string Name { get; set; }
            public int Age { get; set; }
            public string Personallife { get; set; }
            public string Record { get; set; }
            public string Citizenship { get; set; }
            public string Club { get; set; }
            public int Height { get; set; }
            public int Weight { get; set; }
            public int Rating { get; set; }
            public string KindofSport { get; set; }



            public Sportsmen()
            {
               
            }
            public Sportsmen(string name, int age, string personallife, string record, string cirizenship, string club, int rating, int height, int weight, string kindofsport)
            {
                Name = name;
                Age = age;
                Personallife = personallife;
                Record = record;
                Citizenship = cirizenship;
                Club = club;
                Height = height;
                Weight = weight;
                KindofSport = kindofsport;
                Rating = rating;

            }
            
            public void LoadData() //загрузка данных с xml
            {

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("Catalogofsportsmens1.xml");
                XmlElement xRoot = xDoc.DocumentElement;
                foreach (XmlElement xnode in xRoot)
                {
                    Sportsmen sportsmen = new Sportsmen();
                    XmlNode attr = xnode.Attributes.GetNamedItem("name");
      
                  
                    foreach (XmlNode childnode in xnode.ChildNodes)
                    {
                        if (childnode.Name == "Name")
                            sportsmen.Name = childnode.InnerText;

                        if (childnode.Name == "Personallife")
                            sportsmen.Personallife = childnode.InnerText;

                        if (childnode.Name == "Age")
                            sportsmen.Age = Int32.Parse(childnode.InnerText);

                        if (childnode.Name == "Height")
                            sportsmen.Height = Int32.Parse(childnode.InnerText);

                        if (childnode.Name == "Weight")
                            sportsmen.Weight = Int32.Parse(childnode.InnerText);

                        if (childnode.Name == "Club")
                            sportsmen.Club = childnode.InnerText;

                        if (childnode.Name == "Citizenship")
                            sportsmen.Citizenship = childnode.InnerText;

                        if (childnode.Name == "Record")
                            sportsmen.Record = childnode.InnerText;

                        if (childnode.Name == "KindofSport")
                            sportsmen.KindofSport = childnode.InnerText;
                        if (childnode.Name == "Rating")
                            sportsmen.Rating = Int32.Parse(childnode.InnerText);

                    }
                    sportsmens.Add(sportsmen);
                }
                Save();
               

            }

            public static string SportsmenFile= "Catalogofsportsmens2.xml"; //xml файл


            public static Sportsmen GetSportsmen()
            {
                Sportsmen sportsmen = null;
                string filename = SportsmenFile;

                if (File.Exists(filename))
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Open))
                    {


                        XmlSerializer xser = new XmlSerializer(typeof(Sportsmen));
                        sportsmen = (Sportsmen)xser.Deserialize(fs);
                        fs.Close();
                    }
                }
                else sportsmen = new Sportsmen();
                return sportsmen;

            }

            public void Save() //сохранения спортсмена через сериализацию
            {
           
                if (File.Exists(SportsmenFile)) File.Delete(SportsmenFile);

                XmlSerializer formatter = new XmlSerializer(typeof(List <Sportsmen>));
                TextWriter fs = new StreamWriter(SportsmenFile);
            
                    
                        formatter.Serialize(fs, sportsmens);
             

                fs.Close();

            }
         



        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Sportsmen Test = new Sportsmen();
            var list = sportsmens;
            dataGridView1.DataSource = list;
            Test.LoadData();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Index!= null)
            {
                Sportsmen change = new Sportsmen();
                int count = dataGridView1.CurrentRow.Index;
                string s = dataGridView1[0, count].Value.ToString();
                Form3 form3 = new Form3();
                index = 0;
                foreach(Sportsmen sport in sportsmens)
                {
                    index++;
                    if(sport.Name == s)
                    {
                        change = sport;
                        break;
                    }
                    
                }
                form3.Change(change);
                form3.ShowDialog();
            }
             
        }
        public static int index = 0;//индексатор

        private void button3_Click(object sender, EventArgs e)

        {
            string type = comboBox1.SelectedItem.ToString();
            Sportsmen NewSportsmen = new Sportsmen(textBox4.Text, Int32.Parse(numericUpDown2.Value.ToString()), textBox3.Text, textBox2.Text, maskedTextBox1.Text,textBox1.Text, Int32.Parse(numericUpDown1.Value.ToString()), Int32.Parse(numericUpDown4.Value.ToString()), Int32.Parse(numericUpDown3.Value.ToString()),type);
            sportsmens.Add(NewSportsmen);
            NewSportsmen.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows != null)
            {
               
                
                    Sportsmen del = new Sportsmen();
                    int count = dataGridView1.CurrentRow.Index;
                    string s = dataGridView1[0, count].Value.ToString();
               
                    index = 0;
                    foreach (Sportsmen sport in sportsmens)
                    {
                        index++;
                        if (sport.Name == s)
                        {
                           
                            break;
                        }
                    }
                    sportsmens.RemoveAt(index);
                del.Save(); 
            }
                   
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Sportsmen> sort = sportsmens;
            List<Sportsmen> sort1 = new List<Sportsmen>();
            string str = comboBox2.SelectedItem.ToString();
            if ( str == "Показать всех")
            {
                dataGridView1.DataSource = sportsmens;
            }
            else
            {
                foreach (Sportsmen s in sort)
                {
                    if (s.KindofSport == str)
                    {
                        sort1.Add(s);
                    }
                }
                dataGridView1.DataSource = sort1;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Sportsmen> sport = sportsmens;
            List<Sportsmen> sport1 = new List<Sportsmen>();
            if (comboBox2.SelectedItem != null) { 
                 string str = comboBox2.SelectedItem.ToString();
                 if (str == "Показать всех")
                 {
                    sport1 = sportsmens;
                 }
                 else
                 {
                    foreach (Sportsmen s in sport)
                    {
                        if (s.KindofSport == str)
                        {
                            sport1.Add(s);
                        }
                    }

                 }
                var sorted = sport1.OrderBy(x => x.Rating).ToList();

                dataGridView1.DataSource = sorted;
            }

        }
  
 
        public void SaveChanges(Sportsmen chs) // Функция сохранения изменений 
        {
            Sportsmen changesave = new Sportsmen();
            sportsmens.RemoveAt(index);
            sportsmens.Insert(index, chs);
            changesave.Save();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
