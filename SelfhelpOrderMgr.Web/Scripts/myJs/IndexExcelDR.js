
var oldSelectpkId = '';
$(function () {

    $('#btn').linkbutton('disable');


    $('#tbTrade').datagrid({
        width: 850,
        height: 110,
        singleSelect: true,
        url: '/CashPay/GetTrades/' + $("#saveTypeId").val(),
        columns: [[
            { field: 'Bid', title: '批号', width: 100 },
            { field: 'FCourseType', title: '类型', width: 100 },            
            {
                field: 'FAmount', title: '金额', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.FAmount == "0") {
                        return "数据检测";
                    } else {
                        return value;
                    }
                }
            },
            { field: 'ApplyBy', title: '操作员', width: 100 },
            { field: 'Remark', title: '备注', width: 100, align: 'right' },
            {
                field: 'Opration', title: '操作', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.Remark != "WEB批量导入") {
                        return "<input type='button' value='清空检测数据' onclick='plDeleteByPKId(\"" + row.Bid + "\")'/>";
                    } else {
                        return "<input type='button' value='批量撤消入账' onclick='plDeleteByPKId(\"" + row.Bid + "\")'/>";
                    }
                }
            }
        ]]
        , onSelect: function (rowIndex, rowData) {
            $("#strFBid").val(rowData.Bid);
            if (oldSelectpkId != rowData.Bid) {//如果选择的是不同的行才刷新
                oldSelectpkId = rowData.Bid;

                $('#tbSuccessList').datagrid('load', {
                    strFBid: rowData.Bid
                });

                $('#tbErrList').datagrid('load', {
                    strFBid: rowData.Bid
                });
            }
            
        }
    });

    $('#tbSuccessList').datagrid({
        url: '/CashPay/GetBatchTradeDtls/' + $("#saveTypeId").val(),
        columns: [[
            { field: 'Bid', title: '批号', width: 100 },
            { field: 'FCrimeCode', title: '编号', width: 100 },
            { field: 'FCriminal', title: '姓名', width: 100 },
            { field: 'FAmount', title: '金额', width: 100 },
            { field: 'Remark', title: '备注', width: 100, align: 'right' }
        ]]
    });

    $('#tbErrList').datagrid({
        url: '/CashPay/GetBatchTradeErrs/' + $("#saveTypeId").val(),
        columns: [[
            { field: 'pc', title: '批号', width: 100 },
            { field: 'fcrimecode', title: '编号', width: 100 },
            { field: 'fname', title: '姓名', width: 100 },
            { field: 'Amount', title: '金额', width: 100 },
            { field: 'Remark', title: '备注', width: 100, align: 'right' },
            { field: 'notes', title: '错误原因', width: 100, align: 'right' }
        ]]
    });


    $('#selSaveType').combobox({
        onChange: function (row) {
            //console.log($("#"+$("#txtType").combobox('getValue')).data("fushuflag"));
            if ($("#" + $("#selSaveType").combobox('getValue')).data("fushuflag") == "1") {
                alert("注意：该类型是可以支持透支扣款的");
            }
        }
    });

});


//startDataCheck 先进行验证数据  开始Excel导入
function startDaoru(onlyCheckFlag) {
    if ($("#selSaveType").combobox('getValue') == "") {
        $.messager.alert("提示", "请选择一个存款类型");
        return false;
    }
    $("#selSaveName").val($("#selSaveType").combobox('getText'));
    if (onlyCheckFlag == '0') {
        var tipText = "您确认想要校验Excel记录吗？";
    } else {
        var tipText = "您确认想要导入Excel记录吗？";
    }

    $.messager.confirm('确认', '您确认想要校验导入Excel记录吗？', function (r) {
        if (r) {
            $.messager.progress({
                title: '导入数可能需要几分钟，请耐心等待',
                msg: '数据正在导入中...'
            });
            $('#ff').form({
                url: "/CashPay/ExcelInport/" + $("#saveTypeId").val() + "?onlyCheckFlag=" + onlyCheckFlag,
                onSubmit: function () {
                    // do some check    
                    // return false to prevent submit;    
                },
                success: function (data) {
                    $.messager.progress('close');
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        var rtn = $.parseJSON(words[2]);
                        $("#strFBid").val(rtn.trade.Bid);
                        $('#tbSuccessList').datagrid('load', {
                            strFBid: rtn.trade.Bid
                        });

                        $('#tbErrList').datagrid('load', {
                            strFBid: rtn.trade.Bid
                        });

                        $("#lblCheckResult").html("结果:" + words[1]);
                        if (onlyCheckFlag == '1') {
                            $('#btn').linkbutton('enable');
                        } else {
                            $('#btn').linkbutton('disable');
                        }

                        $.messager.alert("提示", words[1]);
                    } else {
                        $("#lblCheckResult").html("结果:失败");
                        $.messager.alert("提示", "导入失败");
                    }

                }
            });
            // submit the form    
            $('#ff').submit();
        }
    });

}


//按主单号批量删除
function plDeleteByPKId(pkId) {
    $.messager.confirm('确认', '您确认要删除该主单的记录吗？', function (r) {
        if (r) {
            $.post("/CashPay/plDeleteByPKId/" + $("#saveTypeId").val(), { "pkId": pkId }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    var words = data.split("|");
                    if (words[0] == "OK") {

                        var rows = $("#tbSuccessList").datagrid('getRows');
                        for (var i = rows.length - 1; i >= 0; i--) {
                            $("#tbSuccessList").datagrid('deleteRow', i);
                        }
                        var errRows = $("#tbErrList").datagrid('getRows');
                        for (var i = rows.length - 1; i >= 0; i--) {
                            $("#tbErrList").datagrid('deleteRow', i);
                        }

                        var index=$("#tbTrade").datagrid('getRowIndex');
                        $("#tbTrade").datagrid('deleteRow', index);
                        

                        $.messager.alert("提示", "批量删除成功，请重新查询以校验数据");
                    } else {
                        $.messager.alert("提示", data);
                    }
                }
            });
        }
    });

}

//开始查询
function startSearch() {
    $("#searchTypeName").val($("#searchType").combobox('getText'));
    $('#tbTrade').datagrid('load', {
        searchType: $("#searchType").combobox('getValue'),
        searchTypeName: $("#searchType").combobox('getText'),
        searchPCH: $("#searchPCH").val(),
        searchStartDate: $("#searchStartDate").datetimebox('getValue'),
        searchEndDate: $("#searchEndDate").datetimebox('getValue')
    });
}

//导出成功记录
function outportSucList() {
    if($("#strFBid").val()=="")
    {
        $.messager.alert("提示","主单批次号不能为空");
        return false;
    }
    $.post("/CashPay/ExcelOutSucList/" + $("#saveTypeId").val(), { "strFBid": $("#strFBid").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            } else {
                $.messager.alert("提示", data);
            }
        }
    });
}

//导出失败记录
function outportErrList() {
    if ($("#strFBid").val() == "") {
        $.messager.alert("提示", "主单批次号不能为空");
        return false;
    }
    $.post("/CashPay/ExcelOutErrList/" + $("#saveTypeId").val(), { "strFBid": $("#strFBid").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
            else {
                $.messager.alert("提示", data);
            }
        }
    });
}