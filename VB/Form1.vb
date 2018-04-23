Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form
				Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(String))
			tbl.Columns.Add("Number", GetType(String))
			tbl.Columns.Add("Test", GetType(String))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, "Test" })
			Next i
			Return tbl
				End Function


		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			gridView1.OptionsSelection.MultiSelect = True
			gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
			Dim ri As New RepositoryItemComboBox()
			gridView1.Columns("Number").ColumnEdit = ri
			For i As Integer = 0 To 9
				ri.Items.Add(String.Format("Test{0}", i))
			Next i
			gridView1.ShowButtonMode = ShowButtonModeEnum.ShowAlways
			Dim TempMultiSelectionEditingHelper As MultiSelectionEditingHelper = New MultiSelectionEditingHelper(gridView1)
		End Sub
	End Class

	Public Class MultiSelectionEditingHelper

		Private _View As GridView
		Public Sub New(ByVal view As GridView)
			_View = view
			_View.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused
			AddHandler _View.MouseUp, AddressOf _View_MouseUp
			AddHandler _View.CellValueChanged, AddressOf _View_CellValueChanged
			AddHandler _View.MouseDown, AddressOf _View_MouseDown
		End Sub

		Private Sub _View_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
			If GetInSelectedCell(e) Then
				  Dim hi As GridHitInfo = _View.CalcHitInfo(e.Location)
				  If _View.FocusedRowHandle = hi.RowHandle Then
					  _View.FocusedColumn = hi.Column
					  DXMouseEventArgs.GetMouseArgs(e).Handled = True
				  End If
			End If

		End Sub

		Private Sub _View_CellValueChanged(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
			OnCellValueChanged(e)
		End Sub

		Private lockEvents As Boolean
		Private Sub OnCellValueChanged(ByVal e As CellValueChangedEventArgs)
			If lockEvents Then
				Return
			End If
			lockEvents = True
			SetSelectedCellsValues(e.Value)
			lockEvents = False
		End Sub

		Private Sub SetSelectedCellsValues(ByVal value As Object)
			Try
				_View.BeginUpdate()
				Dim cells() As GridCell = _View.GetSelectedCells()
				For Each cell As GridCell In cells
					_View.SetRowCellValue(cell.RowHandle, cell.Column, value)
				Next cell
			Catch ex As Exception

			Finally
				_View.EndUpdate()
			End Try

		End Sub
		Private Function GetInSelectedCell(ByVal e As MouseEventArgs) As Boolean
			Dim hi As GridHitInfo = _View.CalcHitInfo(e.Location)
			Return hi.InRowCell AndAlso hi.InRowCell AndAlso _View.IsCellSelected(hi.RowHandle, hi.Column)
		End Function

		Private Sub _View_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
			Dim inSelectedCell As Boolean = GetInSelectedCell(e)
			If inSelectedCell Then
				DXMouseEventArgs.GetMouseArgs(e).Handled = True
				_View.ShowEditorByMouse()
			End If
		End Sub
	End Class
End Namespace