using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TuttorPars
{
    public partial class Form1 : Form
    {
        class JsonUser
        {
            public int userId = 0;
            public int Id = 0;
            public string title = "null";
            public string body = "null";

            public JsonUser(int UserId_, int Id_, string Title_, string Body_)
            {
                userId = UserId_;
                Id = Id_;
                title = Title_;
                body = Body_;
            }

            public string Print()
            {
                return $"UserID - {userId};\nId - {Id};\nTitle - {title};\nBody - {body}\n\n";
            }
        }

        public Form1()
        {
            InitializeComponent();
            label1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string jtext;
            const string userIdPt = @"""userId""\:\s\d+";
            const string idPt = @"""id""\:\s\d+";
            const string titlePt = @"""title""\:\s""\D+"",";
            const string bodyPt = @"""body""\:\s""\D+""\n";

            Regex regex = new Regex(userIdPt);
            Regex regex2 = new Regex(idPt);
            Regex regex3 = new Regex(titlePt);
            Regex regex4 = new Regex(bodyPt);

            Random random = new Random();

            JsonUser[] jsonUsers = new JsonUser[3];
            using (WebClient wc = new WebClient()) 
            {
                byte[] myDataBuffer = wc.DownloadData(@"https://jsonplaceholder.typicode.com/posts");
                jtext = Encoding.ASCII.GetString(myDataBuffer);

                MatchCollection userIdC = regex.Matches(jtext);
                MatchCollection idC = regex2.Matches(jtext);
                MatchCollection titleC = regex3.Matches(jtext);
                MatchCollection bodyC = regex4.Matches(jtext);

                for (int i = 0; i < 3; i++)
                {
                    int r = random.Next(0, 100);
                    int val1 = Convert.ToInt32(userIdC[r].Value.Remove(0, 10));
                    int val2 = Convert.ToInt32(idC[r].Value.Remove(0, 5));
                    string val3 = titleC[r].Value.Remove(0, 9);
                    string val4 = bodyC[r].Value.Remove(0, 8);
                    jsonUsers[i] = new JsonUser(val1, val2, val3, val4);
                }
                foreach (var item in jsonUsers)
                {
                    label1.Text += item.Print();
                }
            }
        }
    }
}
