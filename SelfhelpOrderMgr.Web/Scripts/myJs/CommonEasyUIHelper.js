
//删除DataGrid所有行
function ClearDataGridAllRow(datagridId) {
    var rows = $("#" + datagridId).datagrid("getRows");
    for (var i = rows.length-1; i>=0; i--) {
        $("#" + datagridId).datagrid("deleteRow", i);
    }
}

//更新DataGrid选中行信息
function UpdateDataGridSelectRow(datagridId,rowData) {
    var row = $("#" + datagridId).datagrid("getSelected");
    var rowIdx = $("#" + datagridId).datagrid('getRowIndex', row);
    $('#' + datagridId).datagrid('updateRow', {
        index: rowIdx,
        row: rowData
    });
}