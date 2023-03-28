using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Concurs.Domain;
using Problema5;
using Concurs.Repository;
using Concurs.Service;

namespace Concurs
{
    public partial class ConnectedUser : Form
    {
        Guid IdUser;
        string ConnectionString;
        UserService UserService;
        ChildService ChildService;
        EventService EventService;
        ParticipationService ParticipationService;

        public ConnectedUser(Guid id)
        {
            this.IdUser = id;
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

        private void LoadChildren()
        {
            dataGridView1.Rows.Clear();
            foreach (Event eventinlist in EventService.FindAll())
                dataGridView1.Rows.Add(eventinlist.Name, eventinlist.AgeToString(), eventinlist.Enrolled);
        }

        private void ConnectedUser_Load(object sender, EventArgs e)
        {
            ConnectionString = GetConnectionStringByName("P5DB");
            UserDBRepository userDBRepository = new UserDBRepository(ConnectionString);
            ChildDBRepository childDBRepository = new ChildDBRepository(ConnectionString);
            EventDBRepository eventDBRepository = new EventDBRepository(ConnectionString);
            ParticipationDBRepository participationDBRepository = new ParticipationDBRepository(ConnectionString, childDBRepository, eventDBRepository);
            UserService = new UserService(userDBRepository);
            ChildService = new ChildService(childDBRepository);
            EventService = new EventService(eventDBRepository);
            ParticipationService = new ParticipationService(participationDBRepository);

            LoadChildren();

            List<string> eventNames = new List<string>();
            List<string> eventCategories = new List<string>();
            foreach (Event eventinlist in EventService.FindAll())
            {
                eventNames.Add(eventinlist.Name);
                eventCategories.Add(eventinlist.AgeToString());
            }
            eventNames.Sort();
            eventCategories.Sort();

            HashSet<string> uniqueEventNames = new HashSet<string>(eventNames);
            HashSet<string> uniqueEventCategories = new HashSet<string>(eventCategories);


            foreach (string name in uniqueEventNames)
            {
                comboBox1.Items.Add(name);
                comboBox2.Items.Add(name);
            }

            foreach (string category in uniqueEventCategories)
                comboBox3.Items.Add(category);

            label6.Visible = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var firstSpaceIndex = textBox1.Text.IndexOf(" ");
            var firstString = textBox1.Text.Substring(0, firstSpaceIndex);
            var secondString = textBox1.Text.Substring(firstSpaceIndex + 1);
            int childAge = int.Parse(textBox2.Text);

            Child testChild =  ChildService.FindChildByNameAge(firstString,secondString,childAge);
            if(testChild == null)
            {
                Child newchild = new Child(firstString, secondString, childAge);
                ChildService.AddChild(newchild);
                Event newevent = EventService.FinyByNameAge(comboBox1.Text, int.Parse(textBox2.Text));
                Participation newparticipation = new Participation(newchild, newevent);
                ParticipationService.AddParticipation(newparticipation);
                EventService.AddEnrolledToEvent(newevent.Id);
                label6.Visible = false;
            }
            else
            {
                int countParticipations = ParticipationService.ParticipationCountChild(testChild.Id);
                if(countParticipations >=2)
                {
                    label6.Visible=true;
                }
                else
                {
                    Event newevent = EventService.FinyByNameAge(comboBox1.Text, int.Parse(textBox2.Text));
                    Participation newparticipation = new Participation(testChild, newevent);
                    ParticipationService.AddParticipation(newparticipation);
                    EventService.AddEnrolledToEvent(newevent.Id);
                    label6.Visible = false;
                }
            }
            LoadChildren();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            string nume = comboBox2.SelectedItem.ToString();
            string categorie = comboBox3.SelectedItem.ToString();
            foreach(Participation p in ParticipationService.FindAll())
            {
                if(p.Event.Name == nume && p.Event.AgeToString() == categorie)
                    dataGridView2.Rows.Add(p.Child.FirstName, p.Child.LastName, p.Child.Age);
            }
        }
    }
}
