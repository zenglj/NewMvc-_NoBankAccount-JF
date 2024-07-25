﻿
$(function () {


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



var SaleTypes = [{ "value": "1", "text": "超市消费" }, { "value": "2", "text": "医院消费" }, { "value": "3", "text": "加餐消费" }, { "value": "4", "text": "书报消费" }, { "value": "5", "text": "被服消费" }, { "value": "6", "text": "订购水果" }];

//默认是0为开，1为关
var FlagTypes = [{ "value": "0", "text": "开" }, { "value": "1", "text": "关" }];
var JiFenFlagTypes = [{ "value": "0", "text": "开" }, { "value": "1", "text": "关" }];

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

function FlagFormatter(value, rowData, rowIndex) {

    for (var i = 0; i < FlagTypes.length; i++) {
        if (FlagTypes[i].value == value) {
            return FlagTypes[i].text;
        }
    }
}

function JiFenFlagFormatter(value, rowData, rowIndex) {

    for (var i = 0; i < JiFenFlagTypes.length; i++) {
        if (JiFenFlagTypes[i].value == value) {
            return JiFenFlagTypes[i].text;
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
        url: '/BaseInfoMgr/GetAreaMgr',
        sortName: 'FCode',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'FCode',
        pageSize: 10,
        pageList: [10, 20],
        pagination: true,
        columns: [[
            { field: 'ck', checkbox: true },
            { field: 'FCode', width: 50, sortable: true, title: '编号', editor: 'text' },
            { field: 'FName', width: 50, sortable: true, title: '名称', editor: 'text' },
            { field: 'ID', width: 50, sortable: true, title: 'ID', editor: 'text' },
            { field: 'FID', width: 50, sortable: true, title: '父ID', editor: 'text' },
            { field: 'URL', width: 80, title: 'URL', editor: 'text' },
            { field: 'FTZSP_Money', width: 100, title: '特种品购买金额', editor: 'text' },
            {
                field: 'SaleCloseFlag', width: 60, sortable: true, title: '消费开单状态', formatter: FlagFormatter, editor: { type: 'combobox', options: { data: FlagTypes, valueField: "value", textField: "text" } }
            },
            {
                field: 'JiFenCloseFlag', width: 60, sortable: true, title: '积分消费状态', formatter: JiFenFlagFormatter, editor: { type: 'combobox', options: { data: JiFenFlagTypes, valueField: "value", textField: "text" } }
            }]]
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
function BatchSaveDg() {
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

        $.post("/BaseInfoMgr/SaveAreaList", effectRow,
            function (data, status) {
                if ("success" != status) {
                    return false;
                }
                else {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        UpdateDGInfo(words[1]);
                        $dg.datagrid('acceptChanges');
                        $.messager.alert("提示", "保存成功！");
                    }
                    else {
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

function UpdateDGInfo(word) {
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
            FCode: invs[i].FCode,
            FName: invs[i].FName,
            ID: invs[i].ID,
            FID: invs[i].FID,
            URL: invs[i].URL
        });
    }
}
//清空查询条件
function clearSearch() {
    $("#FGoodsName").val('');
    $("#FGoodsGTXM").val('');
    $('#FGoodsType').combobox('setValue', '0');
    $('#FAreaInfo').combobox('setValue', '0');
}

//按条件查询
function FilterSearch() {

    if ($('#FGoodsType').combobox('getValue') == "0") {
        $('#FGoodsType').combobox('setValue', '');
    }
    if ($('#FAreaInfo').combobox('getValue') == "0") {
        $('#FAreaInfo').combobox('setValue', '');
    }
    var te = $('#FGoodsType').combobox('getText');
    $("#typeName").val(te);


    $('#test').datagrid('load', {
        "typeName": $('#FGoodsType').combobox('getText')
        , "FAreaInfo": $('#FAreaInfo').combobox('getValue')
        , "FGoodsName": $('#FGoodsName').val()
        , "FGoodsGTXM": $('#FGoodsGTXM').val()
    });

    //$.post("/Super/GetXEGoodTypeList", {
    //    "typeName": $('#FGoodsType').combobox('getText')
    //    , "FAreaInfo": $('#FAreaInfo').combobox('getValue')
    //    , "FGoodsName": $('#FGoodsName').val()
    //    , "FGoodsGTXM": $('#FGoodsGTXM').val()
    //}
    //    , function (data, status) {
    //        if ("success" != status) {
    //            return false;
    //        } else {
    //            UpdateDGInfo(data);
    //        }
    //    });

}


//添加类别商品信息
function AddTypeGoods() {
    $('#test').datagrid('appendRow', {
        FCode: '',
        FName: "",
        ID: '',
        FID: "",
        URL: "",
        FTZSP_Money: 0
    });
}

//删除类别商品信息
function DeleteTypeInfoById() {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');
            if (row != null) {
                $.post("/BaseInfoMgr/DeleleArea", { "FCode": row.FCode }
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

//改变队别许可消费状态:0表示开，1表示关
function ChangeAallSaleCloseFlag(flag) {
    $.messager.confirm('确认', '您确认进行本操作吗？', function (r) {
        if (r) {
            $.post("/BaseInfoMgr/ChangeAallSaleCloseFlag", { "flag": flag }
                , function (data, status) {
                    if ("success" != status) {
                        return false;
                    } else {
                        if (data == "OK|更新成功") {
                            loadTableList();//载入监区列表数据
                        } else {
                            $.messager.alert('提示', '执行失败');
                        }
                    }
                });
        }
    });
}