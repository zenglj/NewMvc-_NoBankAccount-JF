﻿
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

    
    //$('#FGoodsStatus').combobox({
    //    url: '../UI/Commons.ashx?action=GetCommonTypeTab&FType=GoodsZT',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});

    //$('#FGoodsIsXianE').combobox({
    //    url: '../UI/Commons.ashx?action=GetCommonTypeTab&FType=XianE',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});


    var SaleTypes = [{ "value": "1", "text": "超市消费" }, { "value": "2", "text": "医院消费" }, { "value": "3", "text": "加餐消费" }, { "value": "4", "text": "书报消费" }, { "value": "5", "text": "被服消费" }, { "value": "6", "text": "订购水果" }];

    var SaveTypes = [{ "value": "0", "text": "存款" }, { "value": "1", "text": "取款" }];

    var FlagTypes = [{ "value": "0", "text": "否" }, { "value": "1", "text": "是" }];

    var useTypes = [{ "value": "0", "text": "购物系统" }, { "value": "1", "text": "积分系统" }];


    //消费类型
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

    //存取类型
    function SaveFormatter(value, rowData, rowIndex) {

        for (var i = 0; i < SaveTypes.length; i++) {
            if (SaveTypes[i].value == value) {
                return SaveTypes[i].text;
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


    function useTypesFormatter(value, rowData, rowIndex) {

        for (var i = 0; i < useTypes.length; i++) {
            if (useTypes[i].value == value) {
                return useTypes[i].text;
            }
        }
    }

    var selectAccountName = [{ "value": "0", "text": "存款账户" }, { "value": "1", "text": "报酬账户" }, { "value": "2", "text": "留存账户" }];

    function selectAccountNameformatter(value, rowData, rowIndex) {
        //if (value == 0) {
        //    return;
        //}
        for (var i = 0; i < selectAccountName.length; i++) {
            if (selectAccountName[i].value == value) {
                return selectAccountName[i].text;
            }
        }
    }

    var fushuFlag = [{ "value": "0", "text": "禁止" }, { "value": "1", "text": "许可" }];

    function fushuFlagFormatter(value, rowData, rowIndex) {
        //if (value == 0) {
        //    return;
        //}
        for (var i = 0; i < fushuFlag.length; i++) {
            if (fushuFlag[i].value == value) {
                return fushuFlag[i].text;
            }
        }
    }

    //用户列表清单
    $('#test').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        //width: 750,
        height: 350,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/BaseInfoMgr/GetSaveTypeMgr/' + $("#useTypeId").val(),
        sortName: 'typeflag',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'fname',
        pageSize: 10,
        pageList: [10, 20],
        pagination: true,
        columns: [[
					{ field: 'ck', checkbox: true },
					{ field: 'fcode', width: 50, sortable: true, title: '编号', editor: 'text' },
                    { field: 'fname', width: 50, sortable: true, title: '名称', editor: 'text' },
                    {
                        field: 'typeflag', width: 60, sortable: true, title: '存取类型', formatter: SaveFormatter, editor: { type: 'combobox', options: { data: SaveTypes, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'PLXE_Flag', width: 60, sortable: true, title: '批量限额', formatter: FlagFormatter, editor: { type: 'combobox', options: { data: FlagTypes, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'ZZKK_Flag', width: 60, sortable: true, title: '自助扣款', formatter: FlagFormatter, editor: { type: 'combobox', options: { data: FlagTypes, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'FuShuFlag', width: 60, title: '负数透支', formatter: fushuFlagFormatter, editor: { type: 'combobox', options: { data: fushuFlag, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'AccType', width: 60, sortable: true, title: '资金账户', formatter: selectAccountNameformatter, editor: { type: 'combobox', options: { data: selectAccountName, valueField: "value", textField: "text" } }
            },
            {
                field: 'UseType', width: 60, sortable: true, title: '使用类型', formatter: useTypesFormatter, editor: { type: 'combobox', options: { data: useTypes, valueField: "value", textField: "text" } }
            }]]
    });
    
    $('#editWindows').window({
        modal: true,
        closed: true
    });


});



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
 
        $.post("/BaseInfoMgr/SaveSubSaveTypeList", effectRow,
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
            fcode: invs[i].fcode,
            fname: invs[i].fname,
            typeflag: invs[i].typeflag,
            PLXE_Flag: invs[i].PLXE_Flag,
            ZZKK_Flag: invs[i].ZZKK_Flag
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
function AddTypeGoods()
{
    $('#test').datagrid('appendRow', {
        fcode: '',
        fname: "",
        typeflag: '0',
        PLXE_Flag: "0",
        ZZKK_Flag:"0"
    });
}

//删除类别商品信息
function DeleteTypeInfoById() {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');            
            if (row != null) {
                $.post("/BaseInfoMgr/DeleleArea", { "fcode": row.fcode, "typeflag": row.typeflag }
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