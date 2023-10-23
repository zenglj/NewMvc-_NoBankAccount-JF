//====================================================


$(function () {
    loadPayGrid();//显示银行转账记录
    loadPayGridDetail();//显示付款明细记录

    loadCrtPayRecDetail();//显示付款明细记录

    $("#paySearchAuditDate_Start").datetimebox('clear');
    $("#paySearchAuditDate_End").datetimebox('clear');
    $("#paySearchPayDate_Start").datetimebox('clear');
    $("#paySearchPayDate_End").datetimebox('clear');

    //创建付款主单的条件
    $("#wselTranDate_Start").datetimebox('clear');
    $("#wselTranDate_End").datetimebox('clear');

    

    /*$("#winChangeList").window('close');*/


    //动态改变行颜色
    $('#tbPay').datagrid({
        rowStyler: function (index, row) {
            if (row.OrderStatus == "2") {
                return 'background-color:coral;';
            } else if (row.OrderStatus == "1") {
                return 'background-color:green;';
            } 
        }
    });

    //关闭明细窗口
    $('#winCustList').window('close');
}); 





function loadPayGrid() {
    $('#tbPay').datagrid({
        //title: '银企直联支付记录',
        //iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.4,
        queryParams: {
            Id: 0
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/BankPayAtmServer/GetBankPayAtmServerList/',
        sortName: 'Id',
        sortOrder: 'asc',
        showFooter: true,
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100, 200, 2000, 20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'Id', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'CrtDate', title: '创建日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CrtDate != null) {
                        if (row.CrtDate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'CrtBy', title: '经办人', sortable: true, width: 100 },
            { field: 'Amount', title: '金额', sortable: true, width: 100 },            
            { field: 'TotalDesc', title: '用途说明', sortable: true, width: 100 },
            { field: 'AuditBy', title: '审核人', sortable: true, width: 100 },
            {
                field: 'AuditDate', title: '审核日期', width: 100, formatter: function (value, row, index) {
                    if (row.AuditDate != null) {
                        if (row.AuditDate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'PayBy', title: '付款经办人', sortable: true, width: 100 },
            {
                field: 'PayDate', title: '付款日期', width: 100, formatter: function (value, row, index) {
                    if (row.PayDate != null) {
                        if (row.PayDate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            {
                field: 'OrderStatus', title: '状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.OrderStatus == "0") {
                        return "未审核";
                    } else if (row.OrderStatus == "1") {
                        return "已审核";
                    } else if (row.OrderStatus == "2") {
                        return "已付款";
                    }else {
                        return "未知";
                    }
                }
            },
            { field: 'Remark', title: '其他备注', sortable: true, width: 300 }
        ]],
        onSelect: function (rowIndex, rowData) {

            $('#tbPayDetail').datagrid('load', {
                atmSrvId: rowData.Id
            });
        },
        onLoadSuccess: function (data) {
            $("#tbPay").datagrid('resize');
            $("#tbPay").datagrid('reloadFooter', [{ CrtBy: '合计', Amount: data.sum }])
        },
        pagination: true,
        rownumbers: true
    });
}

function loadPayGridDetail() {
    $('#tbPayDetail').datagrid({
        //title: '银企直联支付记录',
        //iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.5,
        queryParams: {
            Id: 0
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/BankPayAtmServer/GetPaymentRecordList/',
        sortName: 'Id',
        sortOrder: 'asc',
        showFooter: true,
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100, 200, 2000, 20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'Id', field: 'Id', width: 60, sortable: false },
        ]],
        columns: [[//DataGrid表格数据列
            { field: 'AtmSrvId', title: '单号', sortable: true, width: 80 },
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FCrimeName', title: '姓名', sortable: true, width: 100 },
            {
                field: 'TranType', title: '转账类型', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.TranType == "0") {
                        return "公对私";
                    } else if (row.TranType == "1") {
                        return "公对公";
                    } else if (row.TranType == "2") {
                        return "快捷代发";
                    } else {
                        return "其他";
                    }
                }
            },
            {
                field: 'PayMode', title: '支付方式', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.PayMode == "0") {
                        return "现金";
                    } else if (row.PayMode == "1") {
                        return "ATM取款";
                    } else if (row.PayMode == "2") {
                        return "转账";
                    } else {
                        return "其他";
                    }
                }
            },
            { field: 'BankUserName', title: '转账收款人', sortable: true, width: 100 },
            { field: 'OutBankCard', title: '收款账号', width: 100, sortable: true },
            { field: 'OpeningBank', title: '开户行', width: 100, sortable: true },
            { field: 'OutBankRemark', title: '关系说明', width: 100, sortable: true },
            { field: 'Amount', title: '结算余额', width: 100, sortable: true },
            { field: 'TranMoney', title: '支付金额', width: 100, sortable: true },
            { field: 'PurposeInfo', title: '摘要', width: 100, sortable: true },
            {
                field: 'AuditFlag', title: '审核状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AuditFlag == "0") {
                        return "待审核";
                    } else if (row.AuditFlag == "1") {
                        return "已审核";
                    } else {
                        return value;
                    }
                }
            },
            { field: 'AuditBy', title: '审核人', width: 100, sortable: true },
            {
                field: 'AuditDate', title: '审核日期', width: 100, formatter: function (value, row, index) {
                    if (row.AuditDate != null) {
                        if (row.AuditDate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            {
                field: 'TranDate', title: '转账日期', width: 100, formatter: function (value, row, index) {
                    if (row.TranDate != null) {
                        if (row.TranDate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            {
                field: 'TranStatus', title: '状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.TranStatus == "0") {
                        return "未处理";
                    } else if (row.TranStatus == "1") {
                        return "已提交,待银行处理";
                    } else if (row.TranStatus == "2") {
                        return "支付成功";
                    } else if (row.TranStatus == "3") {
                        return "转账失败";
                    } else if (row.TranStatus == "4") {
                        return "失败已复位";
                    } else if (row.TranStatus == "5") {
                        return "放弃领款";
                    } else {
                        return "其他失败";
                    }
                }
            },
            {
                field: 'Crtdate', title: '创建日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.Crtdate != null) {
                        if (row.Crtdate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            {
                field: 'ReturnTime', title: '到账日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ReturnTime != null) {
                        if (row.ReturnTime != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'BankObssid', title: '银行流水', sortable: true, width: 300 },
            { field: 'BankResultInfo', title: '银行结果描述', sortable: true, width: 300 }
        ]],
        pagination: true,
        rownumbers: true
    });

}


function loadCrtPayRecDetail() {
    $('#invDetails').datagrid({
        //title: '银企直联支付记录',
        //iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.3,
        queryParams: {
            Id: 0
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        //singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/BankPayAtmServer/GetPaymentAtmList/',
        sortName: 'Id',
        sortOrder: 'asc',
        showFooter: true,
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100, 200, 2000, 20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'Id', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FCrimeName', title: '姓名', sortable: true, width: 100 },
            {
                field: 'PayMode', title: '支付方式', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.PayMode == "0") {
                        return "现金";
                    } else if (row.PayMode == "1") {
                        return "ATM取款";
                    } else if (row.PayMode == "2") {
                        return "转账";
                    } else {
                        return "其他";
                    }
                }
            },
            { field: 'Amount', title: '结算余额', width: 100, sortable: true },
            { field: 'TranMoney', title: '支付金额', width: 100, sortable: true },
            
            {
                field: 'AtmSrvPayFlag', title: '对账状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AtmSrvPayFlag == "0") {
                        return "未对账";
                    } else if (row.AtmSrvPayFlag == "1") {
                        return "已对账";
                    } else {
                        return value;
                    }
                }
            },
            {
                field: 'TranStatus', title: '取款状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.TranStatus == "0") {
                        return "未处理";
                    } else if (row.TranStatus == "1") {
                        return "已提交,待银行处理";
                    } else if (row.TranStatus == "2") {
                        return "支付成功";
                    } else if (row.TranStatus == "3") {
                        return "转账失败";
                    } else if (row.TranStatus == "4") {
                        return "失败已复位";
                    } else if (row.TranStatus == "5") {
                        return "放弃领款";
                    } else {
                        return "其他失败";
                    }
                }
            },
            {
                field: 'Crtdate', title: '创建日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.Crtdate != null) {
                        if (row.Crtdate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'PurposeInfo', title: '摘要', width: 100, sortable: true },
            { field: 'BankObssid', title: '银行流水', sortable: true, width: 300 },
            { field: 'BankResultInfo', title: '银行结果描述', sortable: true, width: 300 }
        ]],
        pagination: true,
        rownumbers: true
    });

}

//选择全部记录
function wSelectAll() {
    $("#invDetails").datagrid('selectAll');
}

//提交主单
function SubmitMain() {
    var row = $("#tbPay").datagrid('getSelected');
    if (row==null) {
        $.messager.alert("提示", "请选择一条记录");
        return false;
    }
    if (row.OrderStatus >= 1) {
        $.messager.alert("提示", "已审核无重复审核");
        return false;
    }
    $.messager.confirm('Confirm', '您确认要审核这个主单吗?', function (r) {
        if (r) {
            var rowIdx = $("#tbPay").datagrid('getRowIndex', row);
            $.post("/BankPayAtmServer/AuditSubmitMain", { "Id": row.Id }, function (data, status) {
                if ("success" == status) {
                    if (data.Flag) {
                        //成功
                        $('#dg').datagrid('updateRow', {
                            index: rowIdx,
                            row: data.DataInfo
                        });
                        $.messager.alert("提示", "审核成功");
                    } else {
                        //失败
                        $.messager.alert("提示", data.ReMsg);
                    }
                }
            });
        }
    });
    
}


//提交主单
function SubmitMain() {
    var row = $("#tbPay").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一条记录");
        return false;
    }
    if (row.OrderStatus >= 1) {
        $.messager.alert("提示", "已审核无重复审核");
        return false;
    }
    $.messager.confirm('Confirm', '您真的要删除此记录吗?', function (r) {
        if (r) {            
            $.post("/BankPayAtmServer/AuditSubmitMain", { "Id": row.Id }, function (data, status) {
                if ("success" == status) {
                    if (data.Flag) {
                        //成功                        
                        UpdateDataGridSelectRow('tbPay',data.DataInfo);//更新行记录信息
                        $.messager.alert("提示", "审核成功");
                    } else {
                        //失败
                        $.messager.alert("提示", data.ReMsg);
                    }
                }
            });
        }
    });
    
}

//删除主单
function DeleteMain() {
    var row = $("#tbPay").datagrid('getSelected');
    if (row == null) {
        $.messager.alert("提示", "请选择一条记录");
        return false;
    }
    if (row.OrderStatus >= 1) {
        $.messager.alert("提示", "已审核不能删除");
        return false;
    }
    $.messager.confirm('Confirm', '您真的要删除此记录吗?', function (r) {
        if (r) {
            var rowIdx = $("#tbPay").datagrid('getRowIndex', row);
            $.post("/BankPayAtmServer/DeletePayServerMain", { "Id": row.Id }, function (data, status) {
                if ("success" == status) {
                    if (data.Flag) {
                        //成功
                        $('#tbPay').datagrid('deleteRow', rowIdx);
                        ClearDataGridAllRow("tbPayDetail");//清楚Grid所有行
                        $.messager.alert("提示", "删除成功");
                    } else {
                        //失败
                        $.messager.alert("提示", data.ReMsg);
                    }
                }
            });
        }
    });
    
}




function btnPaySearch() {
    var searchInfo = {
        TranType: 1,
        Amount: 200,
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };


    funcSearch("formPaySearch", "tbPay");
}

//清空条件
function btnPayClear() {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    $("#formPaySearch").form('clear');
}



//打印报表，id 是的不同报表的参数
function printMenuBtn(id) {
    var strWhere = getSearchCondition();
    window.open("/Report/PrintCriminalSumOrder/" + id + "?" + strWhere);

}



//查找未对账的ATM付款记录
function wselSelectInvs() {
    $("#invDetails").datagrid("clearSelections");
    funcSearch("createPayOrder", "invDetails");
    funcSearchByPost("createPayOrder", '/BankPayAtmServer/GetPaymentRecord/');
}

//创建对账主单===============================================
function wLoadCrtPayMainList() {
    $.messager.confirm('提示', '确认开始创建对账主单?', function (r) {
        if (r) {
            var totalDesc = $("#paymainTotalDesc").val();
            console.log(totalDesc);
            var selIds = "";
            var rows = $("#invDetails").datagrid("getSelections");
            if (rows == null) {
                $.messager.alert("提示", "请选择一条记录");
                return false;
            }
            for (var i = 0; i < rows.length; i++) {
                if (selIds == "") {
                    selIds = selIds + rows[i].Id;
                } else {
                    selIds = selIds +','+ rows[i].Id;
                }
            }
            $.post("/BankPayAtmServer/DoCreatePayMain", { "TotalDesc": totalDesc, "SelectIds": selIds }, function (data, status) {
                if ("success" == status) {
                    if (data.Flag == true) {
                        $('#tbPay').datagrid('appendRow', data.DataInfo);//插入一行
                        ClearDataGridAllRow("invDetails");//清空明细行记录
                        $('#winCustList').window('close');//关闭窗口
                        $.messager.alert("提示", "创建成功");
                    } else {
                        $.messager.alert("提示", data.ReMsg);
                    }
                    
                }
            });
        }
    });
}


//============打印报表====================================

//机对账报表
function btnPrintATMReport(mode) {

    var row = $("#tbPay").datagrid("getSelected");
    if (row ==null) {
        $.messager.alert("提示","请选择一条主单记录");
    }

    window.open("/BankPayAtmServer/PrintPayMentAtmReport/?atmSrvId=" + row.Id);

}