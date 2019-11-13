Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Linq
Imports System.Text

Namespace WindowsApplication1
	Public Class DataHelper
		Public Shared Function CreateTable(ByVal RowCount As Integer) As DataTable
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
	End Class
End Namespace
