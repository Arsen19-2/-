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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        class User//класс входа
        {
            public string Login { get; set; } // Логин

            public string Password { get; set; } // Пароль
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            List<User> users = new List<User>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("loginpsrol.xml");
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement xnode in xRoot)
            {
                User user = new User();
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("login");
                    if (attr != null)
                        user.Login = attr.Value;
                }
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "password")
                    {
                        user.Password = childnode.InnerText;
                    }
                }
                users.Add(user);
            }

            foreach (User u in users)
            {
                if (u.Login == textBox1.Text)
                {
                    if (u.Password == textBox2.Text)
                    {
                        Form1 form1 = new Form1();
                        form1.Visible = false;
                        Form2 form2 = new Form2();
                        if (u.Login != "Admin")
                        {
                            form2.button2.Visible = false;
                            form2.button1.Visible = false;
                            form2.ShowDialog();
                            
                        }
                        if (u.Login == "Admin")
                        {
                            form2.button1.Enabled = true;
                            form2.button2.Visible = true;
                            form2.ShowDialog();
                        }


                    }
                    else
                    {
                        MessageBox.Show("Incorrect login or password");
                    }
                }
            }
            
         

         
        }
        //
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
        }
    }
}
