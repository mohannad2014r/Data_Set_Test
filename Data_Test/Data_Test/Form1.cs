using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
namespace Data_Test
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			using (System.IO.TextReader tr = File.OpenText((@"Data.pre")))
			{
				string line;
				//add new list of string arrey
				List<string[]> lststr = new List<string[]>();
				while ((line = tr.ReadLine()) != null)
				{

					string[] items = Regex.Split(line,"=");
					lststr.Add(items);
				}
				int col = lststr.Max(x => x.Length);
				if (dt.Columns.Count == 0)
				{
					// Create the data columns for the data table based on the number of items
					// on the first line of the file
					for (int i = 0; i < col; i++)
						dt.Columns.Add(new DataColumn("Column" + i, typeof(string)));
				}
				// loop the list 
				foreach (string[] item in lststr)
				{
					dt.Rows.Add(item);
				}
				gv.DataSource = dt;

				//set autosize mode
				gv.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				gv.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				gv.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

				//datagrid has calculated it's widths so we can store them
				for (int i = 0; i <= gv.Columns.Count - 1; i++)
				{
					//store autosized widths
					int colw = gv.Columns[i].Width;
					//remove autosizing
					gv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
					//set width to calculated by autosize
					gv.Columns[i].Width = colw;
				}
			}
			//show it in gridview 
			this.gv.DataSource = dt;
		}
	}
}
