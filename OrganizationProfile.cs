using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Try_Catch
{
    public partial class Form1 : Form
    {
        private string _FullName;
        private long _StudentNo;
        private int[] _Age;
        private long _ContactNo;
        
        
        /////return methods 
        public long StudentNumber(string studNum)
        {
            if (Regex.IsMatch(studNum, @"^[0-9]{12}$"))
            {
                _StudentNo = long.Parse(studNum);
            }
            else if (studNum ==null)
            {
                throw new ArgumentNullException("Missing Student Number.");
            }
            

            return _StudentNo;
        }

        public long ContactNo(string Contact)
        {
            if (Regex.IsMatch(Contact, @"^[0-9]{10,11}$"))
            {
                _ContactNo = long.Parse(Contact);
            }
            else
            {
                throw new OverflowException("Enter 10-11 digits only");
            }
            return _ContactNo;
        }

        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {
            if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$")  && 
                Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") &&
                Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
            {
                _FullName = LastName + ", " + FirstName + ", " + MiddleInitial;
            }
            else
            {
                throw new FormatException("Input letters only on each name field.");
            }
            return _FullName;
        }

        public int Age(string age)
        {
            _Age = new int[age.Length];
            string result=null;

            if (Regex.IsMatch(age, @"^[0-9]{1,3}$"))
            {
                for (int j =0; j<=age.Length-1; j++)
                {
                    
                    _Age[j]=Int32.Parse(age[j].ToString());

                    if (j+1 == age.Length)
                    {
                        result = _Age.Aggregate("", (s, i) => s + i.ToString());
                    }
                }     
            }
            else
            {
                throw new IndexOutOfRangeException("Please enter a realistic age.");
            }
            return Int32.Parse(result); 
            
           
        }

        public void formatChecker()
        {
            if(!Regex.IsMatch(txtStudentNo.Text, @"^[0-9]+$") || !Regex.IsMatch(cbProgram.Text, @"^[a-zA-Z\x20]+$") ||
                !Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z]+$") ||
                !Regex.IsMatch(txtMiddleInitial.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(txtAge.Text, @"^[0-9]+$") ||
                !Regex.IsMatch(cbGender.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(txtContactNo.Text, @"^[0-9]+$"))
            {
                throw new Exception("One of the field has an incorrect format or missing input.");
            }
        }

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]{
             "BS Information Technology",
             "BS Computer Science",
             "BS Information Systems",
             "BS in Accountancy",
             "BS in Hospitality Management",
             "BS in Tourism Management"
         };
         for (int i = 0; i < 6; i++)
                    {
                        cbProgram.Items.Add(ListOfProgram[i].ToString());
                    }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                
                StudentInformationClass.SetFullName = FullName(txtLastName.Text,
                txtFirstName.Text, txtMiddleInitial.Text);
                StudentInformationClass.SetStudentNo = Convert.ToInt64(StudentNumber(txtStudentNo.Text));
                StudentInformationClass.SetProgram = cbProgram.Text;

                StudentInformationClass.SetGender = cbGender.Text;
                StudentInformationClass.SetContactNo = Convert.ToInt64(ContactNo(txtContactNo.Text));
                StudentInformationClass.SetAge = Age(txtAge.Text);
                StudentInformationClass.SetBirthDay = datePickerBirthday.Value.ToString("yyyy-MM-dd");

                formatChecker();

                frmConfirmation frm = new frmConfirmation();
                if (frm.ShowDialog().Equals(DialogResult.OK))
                {
                    txtStudentNo.Clear();
                    cbProgram.ResetText();
                    txtLastName.Clear();
                    txtFirstName.Clear();
                    txtMiddleInitial.Clear();
                    txtAge.Clear();
                    cbGender.ResetText();
                    txtContactNo.Clear();
                }
            }
            catch (FormatException f1)
            {
                MessageBox.Show(f1.Message); 
            }
            catch (ArgumentNullException a1)
            {
                MessageBox.Show(a1.Message);
            }
            catch (OverflowException o1)
            {
                MessageBox.Show(o1.Message);
            }
            catch (IndexOutOfRangeException i1)
            {
                MessageBox.Show(i1.Message);
            }
            
        }
    }
}
