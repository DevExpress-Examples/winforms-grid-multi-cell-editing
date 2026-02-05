using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Windows.Forms;
using DevExpress.Utils.Behaviors.Common;

namespace WindowsApplication1 {
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            RadioGroup radioGroup = new RadioGroup() { Dock = DockStyle.Top };
            radioGroup.Height = 30;
            Controls.Add(radioGroup);
            radioGroup.Properties.Columns = 2;
            radioGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Cell, "Cell"));
            radioGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Column, "Column"));
            radioGroup.EditValue = ChangeMode.Cell;
            radioGroup.EditValueChanged += RadioGroup_EditValueChanged;
            gridControl1.DataSource = DataHelper.CreateTable(20);
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            behaviorManager1.Attach<MultiCellEditBehavior>(gridView1);
            RepositoryItemComboBox ri = new RepositoryItemComboBox();
            gridView1.Columns["Number"].ColumnEdit = ri;

            for (int i = 0; i < 10; i++)
                ri.Items.Add(String.Format("Test{0}", i));
        }
        private void RadioGroup_EditValueChanged(object sender, EventArgs e)
        {
            RadioGroup radioGroup = sender as RadioGroup;
            if (radioGroup != null)
            {
                ChangeMode mode = (ChangeMode)radioGroup.EditValue;
                if (mode == ChangeMode.Cell)
                {
                    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
                }
                else
                {
                    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                }
            }
        }
    }
    public enum ChangeMode
    {
        Cell,
        Column
    }
}