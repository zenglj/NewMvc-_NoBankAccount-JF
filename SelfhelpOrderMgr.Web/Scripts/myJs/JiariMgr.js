
$(function () {

    //$('#btnSave').linkbutton('disable');
    //$('#btnSave').attr('disabled',"disabled");

    //$('#FGtype').combobox({
    //    url: '../UI/Commons.ashx?action=GetGoodsType',
    //    valueField: 'Fcode',
    //    textField: 'Fname'
    //});

    $('#FSupplyer').combobox({
        url: '/Super/GetSupplyer',
        valueField: 'sCode',
        textField: 'sName'
    });


    $('#FGtype').combobox({
        url: '/Super/GetGoodsType',
        valueField: 'Fcode',
        textField: 'Fname'
    });

    $('#Ffreeflag').combobox({
        data: [{
            Fname: '是',
            Fcode: '1'
        }, {
            Fname: '否',
            Fcode: '0'
        }],
        valueField: 'Fcode',
        textField: 'Fname'
    });

    



    loadTableList();//载入队别列表数据
    
    $('#editWindows').window({
        modal: true,
        closed: true
    });


});

var SaleTypes = [{ "value": "春节", "text": "春节" }, { "value": "端午节", "text": "端午节" }, { "value": "中秋节", "text": "中秋节" }];


function unitformatter(value, rowData, rowIndex) {
    if (value == 0) {
        return;
    }

    for (var i = 0; i < SaleTypes.length; i++) {
        if (SaleTypes[i].value == value) {
            return SaleTypes[i].text;
        }
    }
}



function loadTableList() {
    //载入列表清单
    $('#test').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        //width: 750,
        height: $(window).height() * 0.9,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/BaseInfoMgr/GetChinaFestivalListMgr',
        sortName: 'id',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'id',
        pageSize: 10,
        pageList: [10, 20],
        pagination: true,
        columns: [[
					{ field: 'ck', checkbox: true },
					{ field: 'id', width: 50, sortable: true, title: '编号', editor: 'text' },
                    { field: 'FName', width: 50, sortable: true, title: '名称', editor: 'text' },
                    {
                        field: 'FDate', title: 'FDate', width: 100, sortable: true, editor: 'text',hidden:true, formatter: function (value, row, index) {
                            if (row.FDate != null) {
                                if (row.FDate != "") {
                                    if (value != "/Date(-62135596800000)/") {
                                        var dt = getLongTime(value);
                                        if (dt == 'Invalid Date') {
                                            return '';
                                        } else {
                                            return dt;
                                        }
                                    } else {
                                        return '';
                                    }
                                } else {
                                    return '';
                                }
                            } else {
                                return '';
                            }
                        }
                    },
                    {
                        field: 'FTextDate', title: '日期', width: 100, sortable: true, editor: 'datebox', formatter: function (value, row, index) {
                            if (row.FTextDate != null) {
                                if (row.FTextDate != "") {
                                    return value;
                                } else {
                                    return '';
                                }
                            } else {
                                return '';
                            }
                        }
                    },
                    {
                        field: 'Festival_Name', width: 60, sortable: true, title: '节日名称', formatter: unitformatter, editor: { type: 'combobox', options: { data: SaleTypes, valueField: "value", textField: "text" } }
                    },
                    { field: 'Remark', width: 80, title: '备注', editor: 'text' }
                    ]]
    });
}

//扩展Editer
$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            var ed = $(this).datagrid('getEditor', param);
            if (ed) {
                if ($(ed.target).hasClass('textbox-f')) {
                    $(ed.target).textbox('textbox').focus();
                } else {
                    $(ed.target).focus();
                }
            }
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    },
    enableCellEditing: function (jq) {
        return jq.each(function () {
            var dg = $(this);
            var opts = dg.datagrid('options');
            opts.oldOnClickCell = opts.onClickCell;
            opts.onClickCell = function (index, field) {
                if (opts.editIndex != undefined) {
                    if (dg.datagrid('validateRow', opts.editIndex)) {
                        dg.datagrid('endEdit', opts.editIndex);
                        opts.editIndex = undefined;
                    } else {
                        return;
                    }
                }
                dg.datagrid('selectRow', index).datagrid('editCell', {
                    index: index,
                    field: field
                });
                opts.editIndex = index;
                opts.oldOnClickCell.call(this, index, field);
            }
        });
    }
});

$(function () {
    $('#test').datagrid().datagrid('enableCellEditing');
})

var $dg = $("#test");

//批量保存数据
function BatchSaveDg(){
    endEdit();
    if ($dg.datagrid('getChanges').length) {
        var inserted = $dg.datagrid('getChanges', "inserted");
        var deleted = $dg.datagrid('getChanges', "deleted");
        var updated = $dg.datagrid('getChanges', "updated");
                         
        var effectRow = new Object();
        if (inserted.length) {
            effectRow["inserted"] = JSON.stringify(inserted);
        }
        if (deleted.length) {
            effectRow["deleted"] = JSON.stringify(deleted);
        }
        if (updated.length) {
            effectRow["updated"] = JSON.stringify(updated);
        }
 
        $.post("/BaseInfoMgr/Save_ChinaFestivalList", effectRow,
            function (data, status) {
                if ("success" != status) {
                    return false;
                }
                else{
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        UpdateDGInfo(words[1]);
                        $dg.datagrid('acceptChanges');
                        $.messager.alert("提示", "保存成功！");
                    }
                    else
                    {
                        $.messager.alert("提示", data);
                    }
                }                
            });
    }
}

//结束编辑
function endEdit() {
    var rows = $dg.datagrid('getRows');
    for (var i = 0; i < rows.length; i++) {
        $dg.datagrid('endEdit', i);
    }
}

//更新DataGrid记录信息

function UpdateDGInfo(word)
{
    var invs = $.parseJSON(word);
    //清空现有记录
    var item = $('#test').datagrid('getRows');
    if (item) {
        for (var i = item.length - 1; i >= 0; i--) {
            var index = $('#test').datagrid('getRowIndex', item[i]);
            $('#test').datagrid('deleteRow', index);
        }
    }
    for (var i = 0; i < invs.length; i++) {
        $('#test').datagrid('appendRow', {
            id: invs[i].id,
            FName: invs[i].FName,
            FDate: invs[i].FDate,
            FTextDate: invs[i].FTextDate,
            Festival_Name: invs[i].Festival_Name,
            Remark: invs[i].Remark
        });
    }
}




//添加类别商品信息
function AddTypeGoods()
{
    $('#test').datagrid('appendRow', {
        id: '',
        FName: "",
        FTextDate: "",
        FDate: "",
        Festival_Name: "",
        Remark:""
    });
}

//删除类别商品信息
function DeleteDeleleChinaFestivalById() {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');            
            if (row != null) {
                $.post("/BaseInfoMgr/DeleleChinaFestival", { "ID": row.id }
                    , function (data, status) {
                        if ("success" != status) {
                            return false;
                        } else {
                            if (data == "OK|删除成功") {
                                var index = $('#test').datagrid('getRowIndex', row);
                                $('#test').datagrid('deleteRow', index);
                                $.messager.alert('提示', '删除成功');
                            } else {
                                $.messager.alert('提示', '删除失败');
                            }
                        }
                    });
                
            }
        }
    });    
}

