
$(function () {


    var consumeAccount;//= [{ "value": "7", "text": "超市消费" }, { "value": "16", "text": "医院消费" }, { "value": "18", "text": "加餐消费" }, { "value": "22", "text": "书报消费" }, { "value": "23", "text": "被服消费" }, { "value": "24", "text": "水果消费" }];

    

    //function consumeformatter(value, rowData, rowIndex) {
    //    //if (value == 0) {
    //    //    return;
    //    //}
    //    for (var i = 0; i < consumeAccount.length; i++) {
    //        if (consumeAccount[i].value == value) {
    //            return consumeAccount[i].text;
    //        }
    //    }
    //}

    //function consumeTypeIdformatter(value, rowData, rowIndex) {
    //    //if (value == 0) {
    //    //    return;
    //    //}
    //    for (var i = 0; i < consumeAccount.length; i++) {
    //        if (consumeAccount[i].value == value) {
    //            return consumeAccount[i].value;
    //        }
    //    }
    //}

    var paymentAccount;
    //var paymentAccount = [{ "value": "0", "text": "存款账户" }, { "value": "1", "text": "报酬账户" }];

    //function paymentformatter(value, rowData, rowIndex) {
    //    //if (value == 0) {
    //    //    return;
    //    //}
    //    for (var i = 0; i < paymentAccount.length; i++) {
    //        if (paymentAccount[i].value == value) {
    //            return paymentAccount[i].text;
    //        }
    //    }
    //}





    var shoppingFlag = [{ "value": "0", "text": "关闭" }, { "value": "1", "text": "开启" }];

    var fifoFlag = [{ "value": "-1", "text": "扣款" }, { "value": "1", "text": "存款" }];


    function shoppingFormatter(value, rowData, rowIndex) {
        //if (value == 0) {
        //    return;
        //}
        for (var i = 0; i < shoppingFlag.length; i++) {
            if (shoppingFlag[i].value == value) {
                return shoppingFlag[i].text;
            }
        }
    }

    function fifoflagFormatter(value, rowData, rowIndex) {
        //if (value == 0) {
        //    return;
        //}
        for (var i = 0; i < fifoFlag.length; i++) {
            if (fifoFlag[i].value == value) {
                return fifoFlag[i].text;
            }
        }
    }

    $('#dg').datagrid({
        url: 'datagrid_data.json',
        columns: [[
            { field: 'code', title: 'Code', width: 100 },
            { field: 'name', title: 'Name', width: 100 },
            { field: 'price', title: 'Price', width: 100, align: 'right' }
        ]]
    });


    $.post("/BaseInfoMgr/GetSystemSaleType", { "action": "" }, function (data, status) {
        if ("success" == status) {
            consumeAccount = $.parseJSON(data);
            
            
            $.post("/BaseInfoMgr/GetAreaListInfo", { "action": "" }, function (areadata, areastatus) {
                if ("success" == areastatus) {
                    paymentAccount = $.parseJSON(areadata);

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
                        url: '/BaseInfoMgr/GetSaleDayListMgr',
                        sortName: 'Seqno',
                        sortOrder: 'asc',
                        remoteSort: false,
                        singleSelect: true,
                        idField: 'Seqno',
                        pageSize: 10,
                        pageList: [10, 20],
                        pagination: true,
                        columns: [[
                                    { field: 'ck', checkbox: true },
                                    { field: 'Seqno', width: 50, sortable: true, title: '编号', editor: 'numberbox' },
                                    //{
                                    //    field: 'SaleTypeId', width: 60, title: '消费类型', formatter: consumeformatter, editor: { type: 'combobox', options: { data: consumeAccount, valueField: "value", textField: "text" } }
                                    //},
                                    {
                                        field: 'PType', width: 60, title: '消费类型', formatter: function (value, rowData, rowIndex) {
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
                                    {
                                        field: 'SaleTypeId', width: 60, title: '消费科目值', formatter: function (value, rowData, rowIndex) {
                                            for (var i = 0; i < consumeAccount.length; i++) {
                                                if (consumeAccount[i].typeFlag == value) {
                                                    return consumeAccount[i].text;
                                                }
                                            }
                                        }, editor: { type: 'combobox', options: { data: consumeAccount, valueField: "typeFlag", textField: "text" } }
                                    },                                    
                                    {
                                        field: 'FAreaCode', width: 60, title: '应用队别', formatter: function(value, rowData, rowIndex) {
                                                for (var i = 0; i < paymentAccount.length; i++) {
                                                    if (paymentAccount[i].value == value) {
                                                        return paymentAccount[i].text;
                                                    }
                                                }
                                            }, editor: { type: 'combobox', options: { data: paymentAccount, valueField: "value", textField: "text" } }
                                    },
                                    { field: 'StartDay', width: 50, sortable: true, title: '起日', editor: 'numberbox' },
                                    { field: 'EndDay', width: 50, sortable: true, title: '止日', editor: 'numberbox' },
                                    {
                                        field: 'Flag', width: 60, title: '开关', formatter: shoppingFormatter, editor: { type: 'combobox', options: { data: shoppingFlag, valueField: "value", textField: "text" } }
                                    },
                                    { field: 'LevelId', width: 80, title: '优先级', editor: 'text' }
                                    ,{ field: 'Remark', width: 80, title: '备注', editor: 'text' }]]
                    });

                }
            });
            
        }
    });




    



});

//初始化数据
function intGridList() {

    

 
    

    //$('#editWindows').window({
    //    modal: true,
    //    closed: true
    //});

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
 
        $.post("/BaseInfoMgr/Save_SaleDayList", effectRow,
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
            Seqno: invs[i].Seqno,
            PType: invs[i].PType,
            SaveTypeId: invs[i].SaveTypeId,
            StartDay: invs[i].StartDay,
            EndDay: invs[i].EndDay,
            Flag: invs[i].Flag,
            FAreaCode: invs[i].FAreaCode,
            LevelId: invs[i].LevelId,
            Remark: invs[i].Remark
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
        ID: '0',
        PType: "",
        //TypeFlagId: "7",
        //CanconsumeAccount: "1",
        //FirstPaymentAccount: "0",
        LevelId: 0,
        Flag: "1",
        Remark: "",
        Fifoflag: "-1"
    });
}

//删除类别商品信息
function DeleteTypeInfoById() {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');            
            if (row != null) {
                $.post("/BaseInfoMgr/DeleleSaleDayList", { "Seqno": row.Seqno }
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