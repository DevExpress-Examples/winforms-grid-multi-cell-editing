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
                private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("Number", typeof(string));
            tbl.Columns.Add("Test", typeof(string));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, "Test" });
            return tbl;
        }


        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            gridView1.OptionsSelection.MultiSelect = true;
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
            RepositoryItemComboBox ri = new RepositoryItemComboBox();
            gridView1.Columns["Number"].ColumnEdit = ri;
            for (int i = 0; i < 10; i++)
            {
                ri.Items.Add(String.Format("Test{0}", i));
            }
            gridView1.ShowButtonMode = ShowButtonModeEnum.ShowAlways;
            new MultiSelectionEditingHelper(gridView1);
        }
    }

    public class MultiSelectionEditingHelper
    {

        private GridView _View;
        public MultiSelectionEditingHelper(GridView view)
        {
            _View = view;
            _View.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            _View.MouseUp += _View_MouseUp;
            _View.CellValueChanged += new CellValueChangedEventHandler(_View_CellValueChanged);
            _View.MouseDown += new MouseEventHandler(_View_MouseDown);
        }

        void _View_MouseDown(object sender, MouseEventArgs e)
        {
            if (GetInSelectedCell(e))
            {
                  GridHitInfo hi = _View.CalcHitInfo(e.Location);
                  if (_View.FocusedRowHandle == hi.RowHandle)
                  {
                      _View.FocusedColumn = hi.Column;
                      DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                  }
            }
                
        }

        void _View_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            OnCellValueChanged(e);
        }

        bool lockEvents;
        private void OnCellValueChanged(CellValueChangedEventArgs e)
        {
            if (lockEvents)
                return;
            lockEvents = true;
            SetSelectedCellsValues(e.Value);
            lockEvents = false;
        }
        
        private void SetSelectedCellsValues(object value)
        {
            try
            {
                _View.BeginUpdate();
                GridCell[] cells = _View.GetSelectedCells();
                foreach (GridCell cell in cells)
                {
                    _View.SetRowCellValue(cell.RowHandle, cell.Column, value);
                }
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                _View.EndUpdate(); 
            }
           
        }
        private bool GetInSelectedCell(MouseEventArgs e)
        {
            GridHitInfo hi = _View.CalcHitInfo(e.Location);
            return hi.InRowCell && hi.InRowCell && _View.IsCellSelected(hi.RowHandle, hi.Column);
        }

        void _View_MouseUp(object sender, MouseEventArgs e)
        {
            bool inSelectedCell = GetInSelectedCell(e);
            if (inSelectedCell)
            {
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                _View.ShowEditorByMouse();
            }
        }
    }
}