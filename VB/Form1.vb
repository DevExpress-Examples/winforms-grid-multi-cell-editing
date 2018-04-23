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

        Public Sub New()
            InitializeComponent()
            gridControl1.DataSource = DataHelper.CreateTable(20)
            gridView1.OptionsSelection.MultiSelect = True
            gridView1.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect
            Dim ri As New RepositoryItemComboBox()
            gridView1.Columns("Number").ColumnEdit = ri

            For i As Integer = 0 To 9
                ri.Items.Add(String.Format("Test{0}", i))
            Next i

            Dim tempVar As New MultiSelectionEditingHelper(gridView1)
        End Sub
    End Class
End Namespace