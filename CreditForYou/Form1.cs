using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CreditForYou
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Locates and reads the file "credit.txt"
        private void readFile(string path)
        {
            string filePath;

            if (path == "")
            {
                string applicationPath = Application.StartupPath;
                filePath = applicationPath + @"\credit.txt";
            }

            else
            {
                filePath = path;
            }

            StreamReader streamFile = new StreamReader(filePath);

            string lineOfText;
            string loadedContent = "";
            bool finishedReadingFile = false;

            while (!finishedReadingFile)
            {
                lineOfText = streamFile.ReadLine();

                if (lineOfText == null)
                {
                    finishedReadingFile = true;
                }

                else
                {
                    loadedContent += lineOfText + Environment.NewLine;
                }
            }

            streamFile.Close();
            //Sets the text in the textbox to be the content from "credit.txt"
            textBox1.Text = loadedContent;

        }

       //When the button is clicked, run readFile
        private void button1_Click(object sender, EventArgs e)
        {
            readFile("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        //When the other button is clicked, determines whether applicants are accepted or not
        private void button2_Click(object sender, EventArgs e)
        {
            string[] applicants = textBox1.Text.Split('\n');
            string[][] applicantsAsArrayOfString = new string[applicants.Length][];
            for (int i = 0; i < applicants.Length; i++)
            {
                string[] applicant = applicants[i].Split(';');
                applicantsAsArrayOfString[i] = applicant;
            }

            int[,] applicantsAsArrayOfInt = new int[applicants.Length,5];
            for (int i = 0; i < applicants.Length; i++)
            {
                for (int j = 0; j < applicantsAsArrayOfString[i].Length; j++)
                {
                    // Error check
                    if (applicantsAsArrayOfString[i][j] == null || applicantsAsArrayOfString[i][j] == "")
                    {
                        continue;
                    }
                    applicantsAsArrayOfInt[i,j] = Convert.ToInt32(applicantsAsArrayOfString[i][j]);
                }
            }
            // Declares all the variables in the credit program
            string[] applicantsResultString = new string[applicantsAsArrayOfInt.Length];
            for (int i = 0; i < applicantsAsArrayOfInt.GetLength(0); i++)
            {
                int applicantNumber = applicantsAsArrayOfInt[i,0],
                    yearlySalary = applicantsAsArrayOfInt[i,1],
                    yearsEmployed = applicantsAsArrayOfInt[i,2],
                    monthlyRent = applicantsAsArrayOfInt[i,3],
                    yearsDomiciled = applicantsAsArrayOfInt[i,4];
                
                //Establishes boundaries for getting accepted
                bool isAcceptedForCreditCard =
                    (
                        yearlySalary >= 250_000 ||
                        (yearlySalary >= 200_000 && monthlyRent < yearlySalary / 48) ||
                        (yearlySalary >= 150_000 && yearsDomiciled > 5) ||
                        (yearlySalary >= 100_000 && yearsDomiciled >= 5 && yearsEmployed >= 3)
                    );

                string applicantResult = $"{applicantNumber};{yearlySalary};{yearsEmployed};{monthlyRent};{yearsDomiciled};{isAcceptedForCreditCard}";
                // Spagetthi fix for my spaghetti code
                if (applicantResult != "0;0;0;0;0;False")
                {
                    applicantsResultString[i] = applicantResult;
                }
        }

            string macncheese  = applicantsResultString.Aggregate("", (acc, next) => acc + next + Environment.NewLine);
            textBox1.Text = macncheese;
        }
    }
}