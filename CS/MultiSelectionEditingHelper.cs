using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace WindowsApplication1
{
    public class MultiSelectionEditingHelper
    {
        public MultiSelectionEditingHelper(GridView view)
        {
            this.view = view;
            this.view.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused;
            this.view.MouseUp += view_MouseUp;
            this.view.CellValueChanged += view_CellValueChanged;
            this.view.MouseDown += view_MouseDown;
        }

        private GridView view;

        void view_MouseDown(object sender, MouseEventArgs e)
        {
            if (GetInSelectedCell(e))
            {
                GridHitInfo hi = view.CalcHitInfo(e.Location);
                if (view.FocusedRowHandle == hi.RowHandle)
                {
                    view.FocusedColumn = hi.Column;
                    DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        void view_CellValueChanged(object sender, CellValueChangedEventArgs e)
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
                view.BeginUpdate();
                GridCell[] cells = view.GetSelectedCells();
                foreach (GridCell cell in cells)
                    view.SetRowCellValue(cell.RowHandle, cell.Column, value);
            }
            catch (Exception ex) { }
            finally { view.EndUpdate(); }
        }

        private bool GetInSelectedCell(MouseEventArgs e)
        {
            GridHitInfo hi = view.CalcHitInfo(e.Location);
            return hi.InRowCell && view.IsCellSelected(hi.RowHandle, hi.Column);
        }

        void view_MouseUp(object sender, MouseEventArgs e)
        {
            bool inSelectedCell = GetInSelectedCell(e);
            if (inSelectedCell)
            {
                DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                view.ShowEditorByMouse();
            }
        }
    }
}
