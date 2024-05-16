
$(function () {

    var consumeAccount = [{ "value": "0", "text": "全部账户" }, { "value": "1", "text": "存款账户" }, { "value": "2", "text": "报酬账户" }];

    function consumeformatter(value, rowData, rowIndex) {
        //if (value == 0) {
        //    return;
        //}
        for (var i = 0; i < consumeAccount.length; i++) {
            if (consumeAccount[i].value == value) {
                return consumeAccount[i].text;
            }
        }
    }

    var paymentAccount = [{ "value": "0", "text": "存款账户" }, { "value": "1", "text": "报酬账户" }];

    function paymentformatter(value, rowData, rowIndex) {
        //if (value == 0) {
        //    return;
        //}
        for (var i = 0; i < paymentAccount.length; i++) {
            if (paymentAccount[i].value == value) {
                return paymentAccount[i].text;
            }
        }
    }

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

    //用户列表清单
    $('#test').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        //width: 880,
        height: $(window).height() * 0.8,
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/InfoMgr/GetCYTypesMgr',
        sortName: 'FCode',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        idField: 'FCode',
        pageSize: 10,
        pageList: [10, 20],
        pagination: true,
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { field: 'FCode', width: 50, sortable: true, title: '编号', editor: 'numberbox' },
            { field: 'FName', width: 100, sortable: true, title: '名称', editor: 'text' }
        ]],
        columns: [[
					
                    {
                        field: 'flag', width: 60, title: '启动标志', formatter: shoppingFormatter, editor: { type: 'combobox', options: { data: shoppingFlag, valueField: "value", textField: "text" } }
                    },
                    { field: 'ftotamtmonth', width: 100, sortable: true, title: '总消费限额', editor: 'numberbox' },
                    { field: 'famtmonth', width: 100, sortable: true, title: '存款账户限额', editor: 'numberbox' },
                    { field: 'FBamtMonth', width: 100, sortable: true, title: '报酬账户限额', editor: 'numberbox' },
                    {
                        field: 'Fbonusflag', width: 60, title: '劳酬留存', formatter: shoppingFormatter, editor: { type: 'combobox', options: { data: shoppingFlag, valueField: "value", textField: "text" } }
                    },
                    { field: 'cpct', width: 60, sortable: true, title: '留存比率', editor: 'numberbox' },

                    {
                        field: 'FPower', width: 100, title: '用户可管理标志', formatter: shoppingFormatter, editor: { type: 'combobox', options: { data: shoppingFlag, valueField: "value", textField: "text" } }
                    },
                    { field: 'FamtLimit', width: 80, sortable: true, title: '存款限额', editor: 'numberbox' },
                    { field: 'fcamtlimit', width: 80, sortable: true, title: '取款限额', editor: 'numberbox' },
                    {
                        field: 'payaccount', width: 80, title: '优先账户', formatter: paymentformatter, editor: { type: 'combobox', options: { data: paymentAccount, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'FDinnerAFlag', width: 100, title: 'A账户加餐可用', formatter: shoppingFormatter, editor: { type: 'combobox', options: { data: shoppingFlag, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'FDinnerBFlag', width: 100, title: 'B账户加餐可用', formatter: shoppingFormatter, editor: { type: 'combobox', options: { data: shoppingFlag, valueField: "value", textField: "text" } }
                    },
                    {
                        field: 'totpct', width: 100, title: '加餐最高限额', sortable: true, editor: 'numberbox'
                    },
                    {
                        field: 'FTZSP_Money', width: 100, title: '平常食品(特种)可用非劳金额', sortable: true, editor: 'numberbox'
                    },
                    {
                        field: 'JaRi_Cy_Money', width: 100, title: '节日增加金额', sortable: true, editor: 'numberbox'
                    },
                    {
                        field: 'JaRi_Cy_FTZSP_Money', width: 100, title: '节日食品(特种)非劳额度', sortable: true, editor: 'numberbox'
                    },
                    {
                        field: 'FTZSP_Zero_Flag', width: 150, sortable: true, title: '食品(特种)归零标志', editor: {
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
            { field: 'FTZSP_Zero_MaxMoney', width: 200, sortable: true, title: '置零后最高食品限额', editor: 'text' },
            { field: 'FDesc', width: 200, sortable: true, title: '备注描述', editor: 'text' }
        ]]
    });

    //$('#editWindows').window({
    //    modal: true,
    //    closed: true
    //});


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

        $.post("/InfoMgr/SaveCYTypeList", effectRow,
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
            ftotamtmonth: invs[i].ftotamtmonth,
            famtmonth: invs[i].famtmonth,
            FBamtMonth: invs[i].FBamtMonth,
            Fbonusflag: invs[i].Fbonusflag,
            cpct: invs[i].cpct,
            totpct: invs[i].totpct,//加餐最高限额
            flag: invs[i].flag,
            FPower: invs[i].FPower,
            FamtLimit: invs[i].FamtLimit,
            fcamtlimit: invs[i].fcamtlimit,
            payaccount: invs[i].payaccount,
            FDesc: invs[i].FDesc,
            FDinnerAFlag: invs[i].FDinnerAFlag,
            FDinnerBFlag: invs[i].FDinnerBFlag
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
        FCode: "",
        FName: "",
        ftotamtmonth: 0,
        famtmonth: 0,
        FBamtMonth: 0,
        Fbonusflag: 1,
        cpct: 30,

        flag: 1,
        FPower: 0,
        FamtLimit: 2000000,
        fcamtlimit: 100000,
        payaccount: 0,
        FDesc: "",
        FDinnerAFlag: 0,
        FDinnerBFlag: 1
    });
}

//删除类别商品信息
function DeleteTypeInfoById() {
    $.messager.confirm('确认', '您确认想要删除记录吗？', function (r) {
        if (r) {
            var row = $('#test').datagrid('getSelected');
            if (row != null) {
                $.post("/InfoMgr/DeleleCYType", { "FCode": row.FCode }
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