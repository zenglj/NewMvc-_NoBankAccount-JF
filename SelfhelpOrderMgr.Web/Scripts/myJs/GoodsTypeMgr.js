
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


    //var SaleTypes = [{ "value": "1", "text": "超市消费" }, { "value": "2", "text": "医院消费" }, { "value": "3", "text": "加餐消费" }, { "value": "4", "text": "书报消费" }, { "value": "5", "text": "被服消费" }, { "value": "6", "text": "订购水果" }, { "value": "7", "text": "订购茶叶" }];

    //function unitformatter(value, rowData, rowIndex) {
    //    if (value == 0) {
    //        return;
    //    }

    //    for (var i = 0; i < SaleTypes.length; i++) {
    //        if (SaleTypes[i].value == value) {
    //            return SaleTypes[i].text;
    //        }
    //    }
    //}

    $.post("/BaseInfoMgr/GetSystemSaleType", { "action": "" }, function (data, status) {
        if ("success" == status) {
            consumeAccount = $.parseJSON(data);

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
                url: '/Super/GetGoodsTypeMgr',
                sortName: 'Fcode',
                sortOrder: 'asc',
                remoteSort: false,
                singleSelect: true,
                idField: 'Fcode',
                pageSize: 10,
                pageList: [10, 20],
                pagination: true,
                columns: [[
                            { field: 'ck', checkbox: true },
                            { field: 'Fcode', width: 50, sortable: true, title: '编号' },
                            { field: 'Fname', width: 50, sortable: true, title: '名称', editor: 'text' },
                            {
                                field: 'flag', width: 80, sortable: true, title: '医药类', editor: {
                                    type: 'checkbox',
                                    options: {
                                        on: 1,
                                        off: 0
                                    }
                                },
                                formatter: function (value, row, index) {
                                    if (value == "0") {
                                        return "否";
                                    } else {
                                        return "是";
                                    }
                                }
                            },
                            //{
                            //    field: 'SaleTypeId', width: 60, title: '销售类别', formatter: unitformatter, editor: { type: 'combobox', options: { data: SaleTypes, valueField: "value", textField: "text" } }
                            //},
                            {
                                field: 'SaleTypeId', width: 60, title: '销售类别', formatter: function (value, rowData, rowIndex) {
                                    //alert(value);
                                    for (var i = 0; i < consumeAccount.length; i++) {
                                        //console.log(value + "," + consumeAccount[i].value);
                                        if (consumeAccount[i].value == value) {
                                            return consumeAccount[i].text;
                                            //console.log("value："+ value);
                                        }
                                    }
                                }, editor: { type: 'combobox', options: { data: consumeAccount, valueField: "value", textField: "text" } }
                            },
                            { field: 'Remark', width: 80, title: '备注', editor: 'text' },
                            {
                                field: 'FTZSP_TypeFlag', width: 150, sortable: true, title: '劳酬特购商品', editor: {
                                    type: 'checkbox',
                                    options: {
                                        on: 1,
                                        off: 0
                                    }
                                },
                                formatter: function (value, row, index) {
                                    if (value == "0") {
                                        return "否";
                                    } else {
                                        return "是";
                                    }
                                }
                            },
                            { field: 'MaxBuyCount', width: 80, title: '限购数量', editor: 'numberbox' },
                            {
                                field: 'CtrlMode', width: 150, sortable: true, title: '限购模式', editor: {
                                    type: 'checkbox',
                                    options: {
                                        on: 1,
                                        off: 0
                                    }
                                },
                                formatter: function (value, row, index) {
                                    if (value == "0") {
                                        return "合计限购";
                                    } else {
                                        return "单品限购";
                                    }
                                }
                            }
                ]]
            });

        }
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
 
        $.post("/Super/SaveGoodsTypeList", effectRow,
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
            Fcode: invs[i].Fcode,
            Fname: invs[i].Fname,
            flag: invs[i].flag,
            Remark: invs[i].Remark,
            SaleTypeId: invs[i].SaleTypeId
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
        Fcode: '0',
        FName: "",
        flag: 0,
        Remark: "",
        SaleTypeId: "1",
        FTZSP_TypeFlag:0
    });
}

//删除类别商品信息
function DeleteTypeInfoById() {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');            
            if (row != null) {
                $.post("/Super/DeleleGoodsType", { "Fcode": row.Fcode }
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