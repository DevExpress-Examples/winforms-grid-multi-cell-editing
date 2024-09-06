<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128628324/24.2.1%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2779)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# WinForms Data Grid - Simultaneous editing of several cell values

This example shows how to edit the values ​​in selected cells at the same time.

![Edit Values in Selected Cells - WinForms Data Grid](https://raw.githubusercontent.com/DevExpress-Examples/how-to-edit-multiple-values-in-gridview-at-the-same-time-e2779/13.1.4+/media/ff30315e-29e4-4c71-9772-bd893bb6bab2.png)

```csharp
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
    try {
        view.BeginUpdate();
        GridCell[] cells = view.GetSelectedCells();
        ChangeMode mode = (ChangeMode)radioGroup.EditValue;
        foreach(GridCell cell in cells) {
            int rowHandle = cell.RowHandle;
            GridColumn column = cell.Column;
            switch(mode) {
                case ChangeMode.All:
                    break;
                case ChangeMode.Column:
                    column = view.FocusedColumn;
                    break;
                case ChangeMode.Row:
                    rowHandle = view.FocusedRowHandle;
                    break;
            }
            view.SetRowCellValue(rowHandle, column, value);
        }
    }
    catch(Exception ex) { }
    finally { view.EndUpdate(); }
}
```


## Files to Review

* [Form1.cs](./CS/MultiSelectionEditingHelper.cs) (VB: [Form1.vb](./VB/MultiSelectionEditingHelper.vb))
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-multi-cell-editing&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=winforms-grid-multi-cell-editing&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
