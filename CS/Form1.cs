using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = DataHelper.CreateTable(20);
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            RepositoryItemComboBox ri = new RepositoryItemComboBox();
            gridView1.Columns["Number"].ColumnEdit = ri;

            for (int i = 0; i < 10; i++)
                ri.Items.Add(String.Format("Test{0}", i));

            new MultiSelectionEditingHelper(gridView1);
        }
    }
}