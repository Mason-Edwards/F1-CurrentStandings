using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace F1_CurrentStandings
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {   
            // Get Standings Data
            //string test = getStandingsData().Result;
            var client = new RestClient("http://ergast.com/api/f1/current/driverStandings");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);

            // Display API data
            //label1.Text = response.Content;

            // Load data into xml document 
            XmlDocument xmlData = new XmlDocument();
            xmlData.LoadXml(response.Content);
            
            // Get all drivers in list
            XmlNodeList nodes = xmlData.GetElementsByTagName("Driver");

            //Doesnt display drivers in the correct order,
            //Loop though 10 Drivers and add them to the 10 labels
            int count = 0;

            // Set Names
            foreach(Label label in panel1.Controls)
            {
                // Get drivers name
                string name = nodes[count].Attributes["driverId"].Value;

                // Get parent node to get standing position
                XmlNode parent = nodes[count].ParentNode;

                // Set label text
                label.Text = name.Replace("_", " ");

                count++;
            }

            count = 0;

            // Set Positions
            foreach(Label label in panel2.Controls)
            {
                // Get parent node to get standing position
                XmlNode parent = nodes[count].ParentNode;

                //Get Position 
                string pos = parent.Attributes["position"].Value;

                // Set label text
                label.Text = pos;

                count++;
            }

            count = 0;

            // Set Points
            foreach(Label label in panel3.Controls)
            {
                // Get parent node to get standing position
                XmlNode parent = nodes[count].ParentNode;

                //Get Position 
                string points = parent.Attributes["points"].Value;

                label.Text = points;
                count++;
            }

            count = 0;

            // Set Wins
            foreach(Label label in panel4.Controls)
            {
                // Get parent node to get standing position
                XmlNode parent = nodes[count].ParentNode;

                //Get Position 
                string wins = parent.Attributes["wins"].Value;

                label.Text = wins;
                count++;
            }

            count = 0;
            

            /*
            label2.Text = nodes[0].Attributes["driverId"].Value;
            label3.Text = nodes[1].Attributes["driverId"].Value;
            label4.Text = nodes[2].Attributes["driverId"].Value;
            label5.Text = nodes[3].Attributes["driverId"].Value;
            label6.Text = nodes[4].Attributes["driverId"].Value;
            label7.Text = nodes[5].Attributes["driverId"].Value;
            label8.Text = nodes[6].Attributes["driverId"].Value;
            label9.Text = nodes[7].Attributes["driverId"].Value;
            label10.Text = nodes[8].Attributes["driverId"].Value;
            label11.Text = nodes[9].Attributes["driverId"].Value;
            */
        }


    }
}
