//====================================================


$(function () {
    loadPayGrid();//显示银行转账记录

    $("#paySearchModifyDate_Start").datetimebox('clear');
    $("#paySearchModifyDate_End").datetimebox('clear');

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


    //注册editWinFCrimeCode文本框回车事件
    $("#editWinFCrimeCode").textbox("textbox").bind("keypress", function (e) {
        if (e.keyCode == 13) {
            // 回车事件
            GetCriminalInfo();
        }
    });

    //注册增加按钮的事件
    $("#btnAdd").click(function() {
        $('#createPayOrder').form('clear');
        $('#winCustList').window('open');
    });

    onclick = ""
}); 





function loadPayGrid() {
    $('#tbPay').datagrid({
        //title: '银企直联支付记录',
        //iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.95,
        queryParams: {
            Id: 0
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/RetentionAmount/GetMainDataGridList/',
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
            { field: 'FCrimeCode', title: '资金类型', sortable: true, width: 100 },
            { field: 'FName', title: '资金类型', sortable: true, width: 100 },
            { field: 'FAreaCode', title: '队别编号', sortable: true,hidden:true, width: 100 },
            { field: 'FAreaName', title: '队别名称', sortable: true, width: 100 },
            { field: 'TypeName', title: '资金类型', sortable: true, width: 100 },
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
            { field: 'CauseDesc', title: '滞留原因', sortable: true, width: 100 },
            { field: 'ModifyBy', title: '修改人', sortable: true, width: 100 },
            {
                field: 'ModifyDate', title: '修改日期', width: 100, formatter: function (value, row, index) {
                    if (row.ModifyDate != null) {
                        if (row.ModifyDate != "") {
                            return getLocalTime(value);
                        } else {
                            return '';
                        }
                    } else {
                        return '';
                    }
                }
            },
            { field: 'ResultDesc', title: '处理结果', sortable: true, width: 100 },
            {
                field: 'OrderStatus', title: '状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.OrderStatus == "0") {
                        return "待处理";
                    } else if (row.OrderStatus == "1") {
                        return "已入账";
                    } else if (row.OrderStatus == "2") {
                        return "已退回";
                    }else {
                        return "未知";
                    }
                }
            },
            { field: 'Remark', title: '其他备注', sortable: true, width: 300 }
        ]],
        onSelect: function (rowIndex, rowData) {

            //$('#tbPayDetail').datagrid('load', {
            //    atmSrvId: rowData.Id
            //});
        },
        onLoadSuccess: function (data) {
            $("#tbPay").datagrid('resize');
            $("#tbPay").datagrid('reloadFooter', [{ CrtBy: '合计金额', Amount: data.sum }])
        },
        pagination: true,
        rownumbers: true
    });
}

function GetCriminalInfo() {
    if ($("#editWinFCrimeCode").textbox('getValue') != "") {
        $.post("/RetentionAmount/GetCriminalInfo", { "fcode": $("#editWinFCrimeCode").textbox('getValue') }, function (data, status) {
            if ("success" == status) {
                if (data.Flag == true) {
                    $("#editWinFName").textbox('setValue', data.DataInfo)
                } else {
                    $.messager.alert("提示", data.ReMsg);
                }
            }
        });
    }
    
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


    $("#editWinId").textbox('setValue', row.Id);
    $("#editWinFCrimeCode").textbox('setValue', row.FCrimeCode);
    $("#editWinFName").textbox('setValue', row.FName);
    $("#editWinTypeName").combobox('setValue', row.TypeName);
    $("#editWinAmount").numberbox('setValue', row.Amount);
    $("#editWinCauseDesc").textbox('setValue', row.CauseDesc);
    $("#editWinHistoryOrderNO").textbox('setValue', row.HistoryOrderNO);
    $("#editWinOrderStatus").combobox('setValue', row.OrderStatus);
    $("#editWinResultDesc").textbox('setValue', row.ResultDesc);
    $("#editWinRemark").textbox('setValue', row.Remark);

    
    $('#winCustList').window('open');

    
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
            $.post("/RetentionAmount/DeleteMainRec", { "id": row.Id }, function (data, status) {
                if ("success" == status) {
                    if (data.Flag) {
                        //成功
                        $('#tbPay').datagrid('deleteRow', rowIdx);
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

    

    funcSearch("createPayOrder", "invDetails");
    funcSearchByPost("createPayOrder", '/BankPayAtmServer/GetPaymentRecord/');
}

//创建对账主单===============================================
function wLoadCrtPayMainList() {

    if ($("#editWinId").textbox('getValue') != "") {
        if ($("#editWinOrderStatus").combobox('getValue') == "0") {
            $.messager.alert("提示", "请选择一种处理方式");
            return false;
        }
        if ($("#editWinResultDesc").textbox('getValue') == "") {
            $.messager.alert("提示", "处理结果描述不能为空");
            return false;
        }
    }

    if ($("#editWinTypeName").combobox('getValue') == "") {
        $.messager.alert("提示", "请输入资金类型");
        return false;
    }
    if ($("#editWinAmount").numberbox('getValue') == "" || $("#editWinAmount").numberbox('getValue') <= 0) {
        $.messager.alert("提示", "请输入金额");
        return false;
    }

    if ($("#editWinCauseDesc").textbox('getValue') == "" ) {
        $.messager.alert("提示", "请输入滞留原因");
        return false;
    }
    $.messager.confirm('提示', '确认开始保存滞留主单吗?', function (r) {
        if (r) {
            var strJsonWhere = GetSearchJson('createPayOrder');
            $.post("/RetentionAmount/SaveMainRec", { "strJsonWhere": strJsonWhere }, function (data, status) {
                if ("success" == status) {
                    if (data.Flag == true) {
                        if ($("#editWinId").textbox('getValue') != "") {
                            UpdateDataGridSelectRow('tbPay', data.DataInfo);
                        }
                        else {
                            $('#tbPay').datagrid('appendRow', data.DataInfo);//插入一行
                        }

                        ClearFormSearch('createPayOrder');
                        $('#winCustList').window('close');//关闭窗口
                        $.messager.alert("提示", "保存成功");
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
function btnPrintDetailReport(mode) {


    var strJson = GetSearchJson('formPaySearch');

    window.open("/RetentionAmount/PrintDetailReport/?strJsonWhere=" + strJson);

}