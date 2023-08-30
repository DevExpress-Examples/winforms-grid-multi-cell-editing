Imports System
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Columns

Namespace WindowsApplication1

    Public Class MultiSelectionEditingHelper

        Private view As GridView

        Private radioGroup As RadioGroup

        Public Sub New(ByVal view As GridView, ByVal radioGroup As RadioGroup)
            Me.radioGroup = radioGroup
            Me.view = view
            Me.view.OptionsBehavior.EditorShowMode = EditorShowMode.MouseDownFocused
            AddHandler Me.view.MouseUp, AddressOf view_MouseUp
            AddHandler Me.view.CellValueChanged, AddressOf view_CellValueChanged
            AddHandler Me.view.MouseDown, AddressOf view_MouseDown
        End Sub

        Private Sub view_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            If GetInSelectedCell(e) Then
                Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
                If view.FocusedRowHandle = hi.RowHandle Then
                    view.FocusedColumn = hi.Column
                    DXMouseEventArgs.GetMouseArgs(e).Handled = True
                End If
            End If
        End Sub

        Private Sub view_CellValueChanged(ByVal sender As Object, ByVal e As CellValueChangedEventArgs)
            OnCellValueChanged(e)
        End Sub

        Private lockEvents As Boolean

        Private Sub OnCellValueChanged(ByVal e As CellValueChangedEventArgs)
            If lockEvents Then Return
            lockEvents = True
            SetSelectedCellsValues(e.Value)
            lockEvents = False
        End Sub

        Private Sub SetSelectedCellsValues(ByVal value As Object)
            Try
                view.BeginUpdate()
                Dim cells As GridCell() = view.GetSelectedCells()
                Dim mode As ChangeMode = CType(radioGroup.EditValue, ChangeMode)
                For Each cell As GridCell In cells
                    Dim rowHandle As Integer = cell.RowHandle
                    Dim column As GridColumn = cell.Column
                    Select Case mode
                        Case ChangeMode.All
                        Case ChangeMode.Column
                            column = view.FocusedColumn
                        Case ChangeMode.Row
                            rowHandle = view.FocusedRowHandle
                    End Select

                    view.SetRowCellValue(rowHandle, column, value)
                Next
            Catch ex As Exception
            Finally
                view.EndUpdate()
            End Try
        End Sub

        Private Function GetInSelectedCell(ByVal e As MouseEventArgs) As Boolean
            Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
            Return hi.InRowCell AndAlso view.IsCellSelected(hi.RowHandle, hi.Column)
        End Function

        Private Sub view_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim inSelectedCell As Boolean = GetInSelectedCell(e)
            If inSelectedCell Then
                DXMouseEventArgs.GetMouseArgs(e).Handled = True
                view.ShowEditorByMouse()
            End If
        End Sub

        Public Sub Disable()
            RemoveHandler view.MouseUp, AddressOf view_MouseUp
            RemoveHandler view.CellValueChanged, AddressOf view_CellValueChanged
            RemoveHandler view.MouseDown, AddressOf view_MouseDown
            view = Nothing
        End Sub
    End Class

    Public Enum ChangeMode
        All
        Row
        Column
    End Enum
End Namespace
