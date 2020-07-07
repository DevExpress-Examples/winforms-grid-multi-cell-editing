using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Windows.Forms;

namespace WindowsApplication1 {
    public partial class Form1 : Form
    {
        MultiSelectionEditingHelper helper;
        public Form1()
        {
            InitializeComponent();
            RadioGroup radioGroup = new RadioGroup() { Dock = DockStyle.Top };
            radioGroup.Height = 30;
            Controls.Add(radioGroup);
            radioGroup.Properties.Columns = 3;
            radioGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.All, "All"));
            radioGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Row, "Row"));
            radioGroup.Properties.Items.Add(new DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Column, "Column"));
            radioGroup.EditValue = ChangeMode.All;
            gridControl1.DataSource = DataHelper.CreateTable(20);
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            RepositoryItemComboBox ri = new RepositoryItemComboBox();
            gridView1.Columns["Number"].ColumnEdit = ri;

            for (int i = 0; i < 10; i++)
                ri.Items.Add(String.Format("Test{0}", i));

            helper = new MultiSelectionEditingHelper(gridView1, radioGroup);
        }
        protected override void OnFormClosing(FormClosingEventArgs e) {
            helper.Disable();
            helper = null;
            base.OnFormClosing(e);
        }
    }
}