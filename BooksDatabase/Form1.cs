using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooksDatabase
{
    public partial class Form1 : Form
    {
        OleDbConnection conn;
        OleDbCommand Title_command;
        OleDbDataAdapter Titles_Adapter;
        DataTable Table_Titles;
        CurrencyManager TitlesManager;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var connstring = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Alia\Desktop\Books.accdb;
Persist Security Info = False; ";
            conn = new OleDbConnection(connstring);
            conn.Open();
            Title_command = new OleDbCommand("Select * from Titles", conn);
            Titles_Adapter = new OleDbDataAdapter();
            Titles_Adapter.SelectCommand = Title_command;
            Table_Titles = new DataTable();
            Titles_Adapter.Fill(Table_Titles);

            //Data Binding control

            txt_title.DataBindings.Add("Text", Table_Titles, "Title");
            txt_year.DataBindings.Add("Text", Table_Titles, "Year_Published");
            txt_ISBN.DataBindings.Add("Text", Table_Titles, "ISBN");
            txt_PubID.DataBindings.Add("Text", Table_Titles, "PubId");

            //establish currency manager

            TitlesManager = (CurrencyManager)BindingContext[Table_Titles];
            // can't implicitly therefore will cast it 
            // bound data passed to currency manager- creates an array with the data

            conn.Close();
            conn.Dispose();
            Titles_Adapter.Dispose();
            Table_Titles.Dispose();



            
            

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            TitlesManager.Position = 0;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            TitlesManager.Position--; ;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            TitlesManager.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            TitlesManager.Position = TitlesManager.Count + 1;
            //Array starts at ZERO
        }
    }
}
