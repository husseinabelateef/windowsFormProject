using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HyperMarket1
{
    public partial class AdminForm : Form
    {
        Manager manager = new Manager();
       
         
        public AdminForm()
        {
            InitializeComponent();
           
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CashierButton_Click(object sender, EventArgs e)
        {
            CashierPanel.Show();
            CategoryPanel.Hide();
            BillPanel.Hide();
            ReportPanel.Hide();
            SupplierPanel.Hide();

        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            CashierPanel.Hide();
            CategoryPanel.Show();
            BillPanel.Hide();
            ReportPanel.Hide();
            SupplierPanel.Hide();
        }

        private void BillButton_Click(object sender, EventArgs e)
        {
            CashierPanel.Hide();
            CategoryPanel.Hide();
            BillPanel.Show();
            ReportPanel.Hide();
            SupplierPanel.Hide();
        }

        private void SupplierButton_Click(object sender, EventArgs e)
        {
            CashierPanel.Hide();
            CategoryPanel.Hide();
            BillPanel.Hide();
            ReportPanel.Hide();
            SupplierPanel.Show();
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            CashierPanel.Hide();
            CategoryPanel.Hide();
            BillPanel.Hide();
            ReportPanel.Show();
            SupplierPanel.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
                    }

        private void SupplierPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaAdvenceButton6_Click(object sender, EventArgs e)
        {

        }
         
        private void gunaAdvenceButton5_Click(object sender, EventArgs e)
        {

        }

        private void BillPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CashierAddButton_Click(object sender, EventArgs e)
        {
            List<string> msg = new List<string>();
            if (!IsValidPhone(CashierPhoneNumber.Text))
            {
                msg.Add("Phone");
            }
            if (!IsValidUserName(CashierUsrNameTxtBox.Text))
            {
                msg.Add("Username");
            }
            if (!IsValidSalary(CashierSalaryTxtBox.Text))
            {
                msg.Add("Salary");
            }
            if(CashierNameTxtBox.Text == "")
            {
                msg.Add("Name");
            }
            if(CashierPwdTxtBox.Text == "")
            {
                msg.Add("Password");
            }
            if (msg.Count==0)
            {
                Cashier cashier = new Cashier(CashierNameTxtBox.Text,CashierUsrNameTxtBox.Text, CashierPwdTxtBox.Text,(int) CahshierWorkingHourNumeric.Value, CashierPhoneNumber.Text, float.Parse(CashierSalaryTxtBox.Text));
                CashierIdTxtBox.Text = cashier.ID.ToString();
                Market.market.Cashiers.Add(cashier);
                CashierDataGridView.DataSource = null;
                CashierDataGridView.DataSource = Market.market.Cashiers;
            }
            else
            {
                empty();
                AlertError(msg);
            }
        }
        private void empty()
        {
            CashierIdTxtBox.Text = "";
            CashierSalaryTxtBox.Text = "";
            CashierPhoneNumber.Text = "";
            CashierNameTxtBox.Text = "";
            CashierPwdTxtBox.Text = "";
            CashierUsrNameTxtBox.Text = "";
        }

        public bool IsValidUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return false;
            foreach(Cashier ca in Market.market.Cashiers)
            {
                if (ca.Username == userName)
                    return false;
            }
            return true;
        }
        public bool IsValidPhone(string Phone)
        {
            if (string.IsNullOrEmpty(Phone))
                return false;
            Regex  r = new Regex(@"^[0-9]{11}$");
            return r.IsMatch(Phone);
        }
        public bool IsValidSalary(string Salary)
        {
            if (string.IsNullOrEmpty(Salary))
                return false;
            Regex r = new Regex(@"^[0-9]{4}$");
            return r.IsMatch(Salary);
        }
        private void AlertError(List<string> Msg)
        {
            string msg = "";
           for(int i = 0; i < Msg.Count; i++)
            {
                if(i < Msg.Count - 2)
                    msg += Msg[i] + " , ";
                else if (i < Msg.Count - 1)
                    msg += Msg[i] + " and ";
                else
                    msg += Msg[i];
            }
            if(Msg.Count==1)
            MessageBox.Show( "There is a problem with "+Msg[0]+" input" , "Warning Message");
            else
             MessageBox.Show("There is a problem with " + msg + " inputs", "Warning Message");

        }
        private void CashierDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
