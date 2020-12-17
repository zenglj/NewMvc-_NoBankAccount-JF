
$(function() {
    $('#test').datagrid({
        title: '即离名单',
        iconCls: 'icon-save',
        //width: 900,
        height: 210,
        queryParams: {
            startDate: $("#crimeSearch input[name=fStartDate]").val(),
            endDate: $("#crimeSearch input[name=fEndDate]").val(),
            FAreaCode: '',
            FCode: '',
            FName: '',
            action: 'GetLeavePrison'
        },
        //fitColumns: true,
        singleSelect:true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Infomgr/GetTempLeavePrisonList',
        sortName: 'foudate',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'fcode',
        pageSize: 50,
        pageList: [50, 100],
        onClickRow: function(rowIndex, rowData) {
            var iRowId = rowIndex;
            $('#test').datagrid('clearSelections'); //清除所有的选择项
            $('#test').datagrid('selectRow', iRowId);
            $('#test').datagrid('checkRow', iRowId); 
        },
        frozenColumns: [[
	    { field: 'ck', checkbox: true },
	    { title: '用户编号', field: 'fcode', width: 100, sortable: true }
        ]],
        columns: [[
		{ field: 'fname', title: '用户姓名', width: 120 },
		{ field: 'foudate', title: '出监日期', width: 100, sortable: true,hidden:true },
        { field: 'strOutDate', title: '出监日期', width: 100, sortable: true },
		{ field: 'fareaname', title: '队别名称', width: 150, sortable: true, //rowspan: 2,
			sorter: function(a, b) {
				return (a > b ? 1 : -1);
			}
		},        
        {
            field: 'AllMoney', title: '总金额', width: 80, sortable: true, formatter: function (value, row, index) {
                return row.AmountA+row.AmountB+row.AmountC;
            }
        },
        {
            field: 'PayMode', title: '结算方式', width: 80, sortable: true
            , formatter: function (value, row, index) {
                if (value == 0) {
                    return '网点取款';
                } else if (value == 1) {
                    return '现金结算';
                } else if (value == 2) {
                    return '转账支付'
                } else {
                    return value
                }

            }},
		{ field: 'OutBankCard', title: '出监收款账号', width: 200, sortable: true },
		{ field: 'BankUserName', title: '收款人姓名', width: 150, sortable: true },
        { field: 'OpeningBank', title: '开户行', width: 200, sortable: true },
		{ field: 'FStatus', title: '办理状态', width: 80, sortable: true },
		{ field: 'BankCardNo', title: '银行卡号', width: 150, sortable: true },
        { field: 'JSMoney', title: '已结金额', width: 80, sortable: true },
        { field: 'AmountA', title: '存款账户', width: 80, sortable: true },
        { field: 'AmountB', title: '报酬账户', width: 80, sortable: true },
        { field: 'AmountC', title: '留存账户', width: 80, sortable: true }
        
        ]],
        pagination: true,
        rownumbers: true,
        onSelect: function (rowIndex, rowData) {
            if (rowData.fcode != $("#selectRowFCode").val()) {
                loadDetailTable(rowData.fcode);
            }
        }
    });


    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function() {
            alert('before refresh');
        }
    });
                    
    //动态改变行颜色
    $('#test').datagrid({
        rowStyler:function(index,row){
            if (row.FStatus=="已结算"){
                return 'background-color:gray;';
            } else if (row.FStatus == "已挂失") {
                return 'background-color:#17A05D;';
            }
        }
    });

    loadDetailTable("00000");//加载明细记录

    //动态改变行颜色
    $('#vcrdList').datagrid({
        rowStyler: function (index, row) {
            if (row.BankFlag<1) {
                return 'background-color:yellow;';
            } else if (row.BankFlag == "1") {
                return 'background-color:red;';
            }
        }
    });

    //清空查询条件
    clearSearch();
});

//打印列表
function PrintList() {
    window.open("/Infomgr/PrintOutPrisonList?startDate=" + $("#crimeSearch input[name=fStartDate]").val() + "&endDate=" + $("#crimeSearch input[name=fEndDate]").val() + "&FAreaCode=" + $("#FAreaCode").combobox('getValue') + "&FCode=" + $("#FCode").val() + "&FName=" + $("#FName").val());
}


//挂失打印证明
function PrintLossReport() {
    $.messager.confirm('确认框', '您是否真的给该犯人办理挂失结算手续?', function (r) {
        if (r) {
            var selected = $('#test').datagrid('getSelected');
            if (selected) {
                $.post("/InfoMgr/GetCheckLeaveUserMoney", { "FCode": selected.fcode }, function (data, status) {
                    if ("success" == status) {                        
                        if ("Err|用户编号不能为空" == data) {
                            return false;
                        } else {
                            var results = data.split("|");
                            if (results[0] == "OK") {
                                $.messager.confirm('确认框', results[1] + ",是否继续结算?", function (r) {
                                    if (r) {
                                        $.post("/Infomgr/NoBankCardLeavePrisonList", { "payMode": 3, "FCode": selected.fcode, "FName": selected.fname, "FOuDate": selected.strOutDate }, function (data, status) {
                                            if (status == "success") {
                                                var words = data.split("|");
                                                if (words[0] != "OK") {
                                                    //$.messager.alert('提示', '打印查询失败!');
                                                    $.messager.alert('提示', data);
                                                    return false;
                                                } else {
                                                    $('#btnprint_bank').linkbutton('enable');
                                                    window.open("/Infomgr/PrintOutPrisonReport?FCode=" + selected.fcode);
                                                }
                                            }
                                            else {
                                                alert("通信失败");
                                            }
                                        });
                                    }
                                });
                            }
                        }
                    }
                });
            }
        }
    });
}
//Excel导出名单列表
function ExcelExport () {              
    $.post("/Infomgr/ExcelCriminalList", {
        "startDate": $("#fStartDate").datetimebox('getValue'),
        "endDate": $("#fEndDate").datetimebox('getValue'),
        "FAreaCode:": $("#FAreaCode").combobox('getValue'),
        "FCode":$("#FCode").val(),
        "FName": $("#FName").val()
    }, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
}
//恢复在押
function RecoveryInPrison() {
    var selected = $('#test').datagrid('getSelected');
    if (selected == null) {
        $.messager.alert("提示", "请选择一条记录，谢谢");
        return false;
    }
    var paymode = 0;
    if (selected.PayMode != null && selected.PayMode!="") {
        paymode = selected.PayMode;
    }

    $.post("/Infomgr/ResotreCriminalInPrison", {
        "FCode": selected.fcode,
        "FName": selected.fname,
        "payMode": paymode
    }, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            $.messager.alert("提示", data.ReMsg);
        }
    });
}

//出监结算
function OutPrisonSettle() {
    var payMode = $('#ccPayMode').combobox("getValue");

    if (payMode == "") {
        $.messager.alert("提示", "请选择一个结算模式！");
        return false;
    } else if (payMode == 0) {//网点支取
        NoBankCard_JieSuanMode('现金结算', 0);
    } else if (payMode == 1) {//现金领取
        NoBankCard_JieSuanMode('现金结算', 1);
    } else if (payMode == 2) {//转账支付
        NoBankCard_JieSuanMode('银行转账', 2)
    }
}

//网点支取结算
function OutletsWithdraw() {
    $.messager.confirm('确认框', '您是否真的给该犯人办理离监结算手续?', function (r) {
        if (r) {
            var selected = $('#test').datagrid('getSelected');
            if (selected) {
                $.post("/InfoMgr/GetCheckLeaveUserMoney", { "FCode": selected.fcode }, function (data, status) {
                    if ("success" == status) {
                        if ("Err|用户编号不能为空" == data) {
                            return false;
                        } else {
                            var results = data.split("|");
                            if (results[0] != "OK") {
                                $.messager.confirm('确认框', results[1] + ",是否继续结算?", function (r) {
                                    if (r) {
                                        $.post("/Infomgr/LeavePrisonList", { "action": "BankProve", "FCode": selected.fcode, "FName": selected.fname, "FOuDate": selected.strOutDate }, function (data, status) {
                                            if (status == "success") {
                                                var words = data.split("|");
                                                if (words[0] != "OK") {
                                                    $.messager.alert('提示', data);
                                                    return false;
                                                } else {
                                                    $('#btnprint_bank').linkbutton('enable');
                                                    window.open("/Infomgr/PrintOutPrisonReport?FCode=" + selected.fcode);
                                                }
                                            }
                                            else {
                                                alert("通信失败");
                                            }
                                        });
                                    }
                                });
                            } else {
                                $.post("/Infomgr/LeavePrisonList", { "action": "BankProve", "FCode": selected.fcode, "FName": selected.fname, "FOuDate": selected.strOutDate }, function (data, status) {
                                    if (status == "success") {
                                        var words = data.split("|");
                                        if (words[0] != "OK") {
                                            $.messager.alert('提示', data);
                                            return false;
                                        } else {
                                            $('#btnprint_bank').linkbutton('enable');
                                            window.open("/Infomgr/PrintOutPrisonReport?FCode=" + selected.fcode);
                                        }
                                    }
                                    else {
                                        alert("通信失败");
                                    }
                                });
                            }
                        }
                    }
                });

            }
        }
    });
}

//银行转账或现金结算
function NoBankCard_JieSuanMode(modeText,payMode) {
    $.messager.confirm('确认框', '您是否真的给该犯人办理【' + modeText + '】结算手续?', function (r) {
        if (r) {
            var selected = $('#test').datagrid('getSelected');
            if (selected) {
                if (selected.FStatus != "已结算") {
                    $.post("/Infomgr/NoBankCardLeavePrisonList", { "payMode": payMode, "FCode": selected.fcode, "FName": selected.fname, "FOuDate": selected.strOutDate }, function (data, status) {
                        if (status == "success") {
                            var words = data.split("|");
                            if (words[0] != "OK") {
                                $.messager.alert('提示', data);
                                return false;
                            } else {
                                $('#btnprint_bank').linkbutton('enable');
                                if (payMode > 0) {
                                    window.open("/Infomgr/NewPrintOutPrisonReport?FCode=" + selected.fcode);
                                }
                                else {
                                    window.open("/Infomgr/NewPrintOutPrisonReport?FCode=" + selected.fcode);
                                } 
                            }
                        }
                        else {
                            alert("通信失败");
                        }
                    });
                } else {
                    $('#btnprint_bank').linkbutton('enable');
                    if (payMode > 0) {
                        window.open("/Infomgr/NewPrintOutPrisonReport?FCode=" + selected.fcode);
                    }
                    else {
                        window.open("/Infomgr/NewPrintOutPrisonReport?FCode=" + selected.fcode);
                    }
                }
            }
        }
    });
}


//显示存款明细
function loadDetailTable(fcode) {
    $("#selectRowFCode").val(fcode);//保存选中行的值
    $('#vcrdList').datagrid({
        title: '=====个人账单明细记录================',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.5,
        queryParams: {
            fName: '',
            FCode: fcode,
            FAreaName: '',
            action: 'LoginIn'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Report/GetSearchVcrds/',
        sortName: 'CrtDate',
        sortOrder: 'desc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 20,
        pageList: [5, 10, 20, 50, 100, 200, 2000, 20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'BankFlag', title: '交易状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.BankFlag == "3") {
                        return "手动成功";
                    } else if (row.BankFlag == "2") {
                        return "已成功";
                    } else if (row.BankFlag == "1") {
                        return "已发送";
                    } else if (row.BankFlag == "-1") {
                        return "失败";
                    } else if (row.BankFlag == "-2") {
                        return "失败停发";
                    } else {
                        return "待发送";
                    }
                }
            },
            { field: 'DType', title: '类型', width: 100, sortable: true },
            {
                field: 'CrtDate', title: '创建日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CrtDate != null) {
                        if (row.CrtDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },
            {
                field: 'SendDate', title: '扣款日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.SendDate != null) {
                        if (row.SendDate != "") {
                            if (value != "/Date(-62135596800000)/") {
                                var dt = getLocalTime(value);
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
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FCriminal', title: '姓名', sortable: true, width: 100 },
            { field: 'DAmount', title: '收(元)', width: 100, sortable: true },
            { field: 'CAmount', title: '支(元)', width: 100, sortable: true },
            
            { field: 'CrtBy', title: '操作员', width: 100, sortable: true },
            { field: 'FCheckFlag', title: '标志', width: 100, hidden: true, sortable: true },
            

            { field: 'FAreaName', title: '队别', sortable: true, width: 100 },
            { field: 'OrigId', title: '消费单号', sortable: true, width: 100 },
            {
                field: 'AccType', title: '账户类型', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AccType == "0") {
                        return "存款账户";
                    } else if (row.AccType == "1") {
                        return "报酬账户";
                    } else if (row.AccType == "2") {
                        return "留存账户";
                    } else {
                        return "存款账户";
                    }
                }
            },
            { field: 'Remark', title: '备注', sortable: true, width: 300 }
        ]],
        //onSelect: function (rowIndex, rowData) {
        //    if (rowData.BankFlag == 0) {
        //        $('#btndel').linkbutton('enable');
        //    } else {
        //        $('#btndel').linkbutton('disable');
        //    }
        //    if (rowData.OrigId != "") {
        //        if (rowData.CAmount > 0) {
        //            getInvList(rowData.OrigId);
        //        }
        //    }

        //},
        pagination: true,
        rownumbers: true
    });
}


function resize() {
    $('#test').datagrid('resize', {
        width: 700,
        height: 400
    });
}
function getSelected() {
    var selected = $('#test').datagrid('getSelected');
    if (selected) {
        alert(selected.code + ":" + selected.name + ":" + selected.addr + ":" + selected.col4);
    }
}
function getSelections() {
    var ids = [];
    var rows = $('#test').datagrid('getSelections');
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].code);
    }
    alert(ids.join(':'));
}
function clearSelections() {
    $('#test').datagrid('clearSelections');
}
function selectRow() {
    $('#test').datagrid('selectRow', 2);
}
function selectRecord() {
    $('#test').datagrid('selectRecord', '002');
}
function unselectRow() {
    $('#test').datagrid('unselectRow', 2);
}
function mergeCells() {
    $('#test').datagrid('mergeCells', {
        index: 2,
        field: 'addr',
        rowspan: 2,
        colspan: 2
    });
}

function clearSearch() {
    //$("#crimeSearch input").val("");
    $("#formSearch").form('clear');
}

function FilterSearch() {
    if ("" == $("#crimeSearch input[name=fCrimeName]").val() && "" == $("#crimeSearch input[name=fCrimeCode]").val() && "000" == $("#cc").val()) {
        $.messager.alert('提示', '请输入或是选择相应的查询条件!');   

    } else {
        $('#test').datagrid('load', {
            startDate: $("#crimeSearch input[name=fStartDate]").val(),
            endDate: $("#crimeSearch input[name=fEndDate]").val(),
            FAreaCode: $("#FAreaCode").combobox('getValue'),
            FCode: $("#FCode").val(),
            FName: $("#FName").val(),
            action: 'GetLeavePrison'
        });
    }

}
