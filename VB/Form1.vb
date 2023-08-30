Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Windows.Forms

Namespace WindowsApplication1

    Public Partial Class Form1
        Inherits Form

        Private helper As MultiSelectionEditingHelper

        Public Sub New()
            InitializeComponent()
            Dim radioGroup As RadioGroup = New RadioGroup() With {.Dock = DockStyle.Top}
            radioGroup.Height = 30
            Controls.Add(radioGroup)
            radioGroup.Properties.Columns = 3
            radioGroup.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.All, "All"))
            radioGroup.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Row, "Row"))
            radioGroup.Properties.Items.Add(New DevExpress.XtraEditors.Controls.RadioGroupItem(ChangeMode.Column, "Column"))
            radioGroup.EditValue = ChangeMode.All
            gridControl1.DataSource = DataHelper.CreateTable(20)
            gridView1.OptionsSelection.MultiSelect = True
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
            Dim ri As RepositoryItemComboBox = New RepositoryItemComboBox()
            gridView1.Columns("Number").ColumnEdit = ri
            For i As Integer = 0 To 10 - 1
                ri.Items.Add(String.Format("Test{0}", i))
            Next

            helper = New MultiSelectionEditingHelper(gridView1, radioGroup)
        End Sub

        Protected Overrides Sub OnFormClosing(ByVal e As FormClosingEventArgs)
            helper.Disable()
            helper = Nothing
            MyBase.OnFormClosing(e)
        End Sub
    End Class
End Namespace
