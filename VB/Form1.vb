Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports System
Imports System.Windows.Forms
Imports DevExpress.Utils.Behaviors.Common

Namespace WindowsApplication1

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            Dim radioGroup As RadioGroup = New RadioGroup() With {.Dock = DockStyle.Top}
            radioGroup.Height = 30
            Controls.Add(radioGroup)
            radioGroup.Properties.Columns = 2
            radioGroup.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Cell, "Cell"))
            radioGroup.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Column, "Column"))
            radioGroup.EditValue = ChangeMode.Cell
            AddHandler radioGroup.EditValueChanged, AddressOf RadioGroup_EditValueChanged
            gridControl1.DataSource = DataHelper.CreateTable(20)
            gridView1.OptionsSelection.MultiSelect = True
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
            behaviorManager1.Attach(Of MultiCellEditBehavior)(gridView1)
            Dim ri As RepositoryItemComboBox = New RepositoryItemComboBox()
            gridView1.Columns("Number").ColumnEdit = ri
            For i As Integer = 0 To 10 - 1
                ri.Items.Add(String.Format("Test{0}", i))
            Next
        End Sub

        Private Sub RadioGroup_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
            Dim radioGroup As RadioGroup = TryCast(sender, RadioGroup)
            If radioGroup IsNot Nothing Then
                Dim mode As ChangeMode = CType(radioGroup.EditValue, ChangeMode)
                If mode = ChangeMode.Cell Then
                    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
                Else
                    gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect
                End If
            End If
        End Sub
    End Class

    Public Enum ChangeMode
        Cell
        Column
    End Enum
End Namespace
