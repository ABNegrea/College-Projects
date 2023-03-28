using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Concurs.Domain;
using Concurs.Repository;
using System.Configuration;
using Concurs.Service;
using Concurs;

namespace Problema5
{
    public partial class Login : Form
    {

        UserService UserService;

        public Login()
        {
            InitializeComponent();
        }

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            UserDBRepository Userrepo = new UserDBRepository(GetConnectionStringByName("P5DB"));
            UserService = new UserService(Userrepo);
            label4.Visible = false;
            textBox2.UseSystemPasswordChar = true;
            textBox2.PasswordChar = '*';

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = UserService.FindByEmailPassword(textBox1.Text, textBox2.Text);
            UserDBRepository userDBRepository = new UserDBRepository(GetConnectionStringByName("P5DB"));
            userDBRepository.Save(new User("andrei", "andrei", "andrei", "andrei", 1));
            if(user != null)
            {
                ConnectedUser connectedUser = new ConnectedUser(user.Id);
                connectedUser.Show();
                this.Hide();
            }
            else
            {
                label4.Visible = true;
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
    }
}
