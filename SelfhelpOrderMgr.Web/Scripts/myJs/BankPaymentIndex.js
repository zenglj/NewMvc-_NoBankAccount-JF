﻿//====================================================
//=====定义模块变量===================================
var selCashTypes = "";//存款类型
var selPayTypes = "";//取款类型
var selAccTypes = "";//账户类型
var selBankFlags = "";//银行处理状态
var selFFlags = "";//有效状态
//===================================================

//获取查询栏里多选框里的值
function GetQueryComboboxValues() {
    var CashTypes = $("#FCashTypes").combobox('getValues');
    selCashTypes = "";
    for (var i = 0; i < CashTypes.length; i++) {
        var CashType = CashTypes[i];
        if (selCashTypes == "") {
            selCashTypes = "'" + CashType + "'";;
        } else {
            selCashTypes = selCashTypes + ",'" + CashType + "'";
        }
    }
    var PayTypes = $("#FPayTypes").combobox('getValues');
    selPayTypes = "";
    for (var i = 0; i < PayTypes.length; i++) {
        var PayType = PayTypes[i];
        if (selPayTypes == "") {
            selPayTypes = "'" + PayType + "'";;
        } else {
            selPayTypes = selPayTypes + ",'" + PayType + "'";
        }
    }

    var AccTypes = $("#FAccTypes").combobox('getValues');
    selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }

    var FFlags = $("#FFlags").combobox('getValues');
    selFFlags = "";
    for (var i = 0; i < FFlags.length; i++) {
        var FFlag = FFlags[i];
        if (selFFlags == "") {
            selFFlags = FFlag;
        } else {
            selFFlags = selFFlags + "," + FFlag + "";
        }
    }
}



$(function () {
    loadDetailTable(); //显示存款明细
    loadPayGrid();//显示银行转账记录
    loadPayGridDetail();//显示付款明细记录
    $("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');

    $("#winChangeList").window('close');
    
    //动态改变行颜色

    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.PayAuditFlag == 0) {

            } else if (row.PayAuditFlag == "1" && row.DAmount > 0) {
                return 'background-color:aqua;';
            } else if (row.PayAuditFlag == "2" && row.DAmount > 0) {
                return 'background-color:brown;';
            } else if (row.PayAuditFlag == "3" && row.DAmount > 0) {
                return 'background-color:green;';
            } 
        }
    });


    $('#tbPay').datagrid({
        rowStyler: function (index, row) {
            if (row.TranStatus == "2") {
                return 'background-color:gray;';
            }else if (row.TranStatus == "3") {
                return 'background-color:red;';
            } else if (row.TranStatus == "4") {
                return 'background-color:brown;';
            } else if (row.TranStatus == "5") {
                return 'background-color:coral;';
            } else if (row.AuditFlag == "1") {
                return 'background-color:green;';
            } 

        }
    });

}); 



//修改消费记录为调账取款
function btnInvListChange() {
    $("#winChangeList").window('open');
}


//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height()*0.8,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: false,
        striped: true,
        collapsible: true,
        url: '/BankPayment/GetVcrdPaymentList/',
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 10,
        pageList: [5,10, 20,50,100,200,2000,20000],
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
            {
                field: 'SendDate', title: '扣款日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.SendDate != null) {
                        if (row.SendDate != "") {
                            if (value  != "/Date(-62135596800000)/") {
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
            { field: 'DType', title: '类型', width: 100, sortable: true },
            { field: 'CrtBy', title: '操作员', width: 100, sortable: true },
            { field: 'FCheckFlag', title: '标志', width: 100,hidden:true, sortable: true },
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
        onSelect: function (rowIndex, rowData) {
            if (rowData.BankFlag == 0) {
                $('#btndel').linkbutton('enable');
            } else {
                $('#btndel').linkbutton('disable');
            }
            if (rowData.OrigId != "") {
                if (rowData.CAmount > 0) {
                    getInvList(rowData.OrigId);
                }
            }
            
        },
        pagination: true,
        rownumbers: true
    });


}

function loadPayGrid() {
    $('#tbPay').datagrid({
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
        url: '/BankPayment/GetBankPaymentList/',
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
                field: 'TranType', title: '转账类型', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.TranType == "0") {
                        return "公对私";
                    } else if (row.TranType == "1") {
                        return "公对公";
                    } else if (row.TranType == "2") {
                        return "快捷代发";
                    }else {
                        return row.TranType;
                    }
                }
            },
            {
                field: 'PayMode', title: '支付方式', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.PayMode == "0") {
                        return "现金";
                    } else if (row.PayMode == "1") {
                        return "ATM取款";
                    }else if (row.PayMode == "2") {
                        return "转账";
                    } else {
                        return row.PayMode;
                    }
                }
            },
            { field: 'Amount', title: '结算余额', width: 100, sortable: true },
            { field: 'TranMoney', title: '支付金额', width: 100, sortable: true },
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
                        return row.TranStatus;
                    }
                }
            },
            { field: 'BankUserName', title: '转账收款人', sortable: true, width: 100 },
            { field: 'OutBankCard', title: '收款账号', width: 100, sortable: true },
            { field: 'OpeningBank', title: '开户行', width: 100, sortable: true },
            { field: 'OutBankRemark', title: '关系说明', width: 100, sortable: true },
            
            { field: 'PurposeInfo', title: '摘要', width: 100, sortable: true },
            {
                field: 'ToBankId', title: '对公类型', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.TypeFlag == "5") {
                        return "离监取款";
                    } else if (row.TypeFlag == "7") {
                        return "超市消费";
                    } else if (row.TypeFlag == "30") {
                        return "建新消费";
                    } else {
                        return row.TypeFlag;
                    }
                }
            },
            {
                field: 'AuditFlag', title: '审核状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AuditFlag == "0") {
                        return "待审核";
                    } else if (row.TypeFlag == "1") {
                        return "已审核";
                    } else {
                        return value;
                    }
                }
            },
            { field: 'AuditBy', title: '审核人', width: 100, sortable: true },
            {
                field: 'AuditDate', title: '审核日期', width: 100,  formatter: function (value, row, index) {
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
                field: 'TranDate', title: '转账日期', width: 100,  formatter: function (value, row, index) {
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
            { field: 'BankResultInfo', title: '银行结果描述', sortable: true, width: 300 },
            {
                field: 'AtmSrvPayFlag', title: 'ATM对账标志', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AtmSrvPayFlag == "0") {
                        return "否";
                    } else if (row.AtmSrvPayFlag == "1") {
                        return "是";
                    } else {
                        return value;
                    }
                }
            },
            { field: 'AtmSrvId', title: 'ATM对账单号', sortable: true, width: 300 }
        ]],
        onSelect: function (rowIndex, rowData) {
            
            $("#resetRemark").textbox("setValue", "");
            if (rowData.TranStatus == 3) {
                console.log(rowData);
                $("#resetId").textbox("setValue", rowData.Id);
                //$("#resetBtnSave").linkbutton("enable");
                //$("#refundBtnSave").linkbutton("disable");

                var itemReset = $('#mpList').menu('findItem', '确认复位');
                $('#mpList').menu('enableItem', itemReset.target);
                var itemRefund = $('#mpList').menu('findItem', '退款更正复位');
                $('#mpList').menu('disableItem', itemRefund.target);
                //enableItem  disableItem
            } else if (rowData.TranStatus == 2) {
                $("#resetId").textbox("setValue", rowData.Id);
                //$("#resetBtnSave").linkbutton("disable");
                //$("#refundBtnSave").linkbutton("enable");

                var itemReset = $('#mpList').menu('findItem', '确认复位');
                $('#mpList').menu('disableItem', itemReset.target);
                var itemRefund = $('#mpList').menu('findItem', '退款更正复位');
                $('#mpList').menu('enableItem', itemRefund.target);
            }
            else {
                $("#resetId").textbox("setValue", "");
                //$("#resetBtnSave").linkbutton("disable");
                //$("#refundBtnSave").linkbutton("disable");

                var itemReset = $('#mpList').menu('findItem', '确认复位');
                $('#mpList').menu('disableItem', itemReset.target);
                var itemRefund = $('#mpList').menu('findItem', '退款更正复位');
                $('#mpList').menu('disableItem', itemRefund.target);
            }
            $('#tbPayDetail').datagrid('load', {
                mainId: rowData.Id
            });
        },
        onLoadSuccess: function (data) {
            $("#tbPay").datagrid('resize');
            $("#tbPay").datagrid('reloadFooter', [{ Amount: '支付合计', TranMoney: data.sum }])
        },
        pagination: true,
        rownumbers: true
    });
}

function loadPayGridDetail() {
    $('#tbPayDetail').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.35,
        queryParams: {
            mainId: '0'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/BankPayment/GetVcrdByMain/',
        //sortName: 'seqno',
        //sortOrder: 'asc',
        remoteSort: false,
        //idField: 'seqno',
        pageSize: 10,
        pageList: [5, 10, 20, 50],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列

            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FCriminal', title: '姓名', sortable: true, width: 100 },
            { field: 'DAmount', title: '收(元)', width: 100, sortable: true },
            { field: 'CAmount', title: '支(元)', width: 100, sortable: true },
            { field: 'DType', title: '类型', width: 100, sortable: true },
            { field: 'CrtBy', title: '操作员', width: 100, sortable: true },
            { field: 'FCheckFlag', title: '标志', width: 100, hidden: true, sortable: true },
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

        pagination: true,
        rownumbers: true
    });


}


function getInvList(invno) {
    $.post("/Report/getInvList/", { "invNo": invno }, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                inv = $.parseJSON(words[1]);
                $("#tInvno").html(inv.InvoiceNo);
                $("#tFCrimeCode").html(inv.FCrimeCode);
                $("#tFCriminal").html(inv.FCriminal);
                $("#tPType").html(inv.PType);
                $("#tOrderDate").html(getLocalTime(inv.OrderDate));
                $("#tAmount").html(inv.Amount);
                $("#tTypeFlag").val(inv.TypeFlag);
            }
        }
    });
}


//失败重发复位设定
function ResetCheckSend() {
    var row = $("#tbPay").datagrid("getSelected");
    if (row == null || row.TranStatus != 3) {
        $.messager.alert("提示", "只有失败的记录才能复位重发");
        return false;
    }
    if ($("#resetId").textbox('getValue') == "") {
        $.messager.alert("提示", "失败的记录Id不能为空");
        return false;
    }
    if ($("#resetRemark").textbox('getValue') == "") {
        $.messager.alert("提示", "复位原因不能为空");
        return false;
    }

    $.messager.confirm('确认框', '请确您已经修正了收款人的账户信息，否则复位账户还是错误的，是否继续?', function (r) {
        if (r) {
            $.messager.confirm('确认框', '请确认记录,只有失败没有转账成功才可以复位重发，是否继续?', function (r) {
                if (r) {
                    $.post("/BankPayment/ResetCheckSend", { "id": $("#resetId").textbox('getValue'), "remark": $("#resetRemark").textbox('getValue') }, function (data, status) {
                        if ("success" == status) {
                            alert(data);
                        }
                    });
                }
            });
        }
    });

    
    
}

//退款更正复位
function ResetRefund() {
    var row = $("#tbPay").datagrid("getSelected");
    if (row == null || row.TranStatus != 2) {
        $.messager.alert("提示", "只有退款成功的记录才能复位重发");
        return false;
    }
    if ($("#resetId").textbox('getValue') == "") {
        $.messager.alert("提示", "失败的记录Id不能为空");
        return false;
    }
    if ($("#resetRemark").textbox('getValue') == "") {
        $.messager.alert("提示", "必须输入退款的银行流水单号才能复位");
        return false;
    }

    $.messager.confirm('确认框', '请确您已经修正了收款人的账户信息，否则复位账户还是错误的，是否继续?', function (r) {
        if (r) {
            $.messager.confirm('确认框', '请确认记录，只有退款成功才可以复位重发，否则产生的后果由您承担，是否继续?', function (r) {
                if (r) {
                    $.post("/BankPayment/ResetRefund", { "id": $("#resetId").textbox('getValue'), "remark": $("#resetRemark").textbox('getValue') }, function (data, status) {
                        if ("success" == status) {
                            alert(data);
                        }
                    });
                }
            });
        }
    });
}




function btnSearch() {
    if ($("#vcrdSearchDType").combobox("getValue")=="") {
        alert("请先选择一个取款类型");
        return false;
    }
    funcSearch("formVcrdSearch", "test");
    funcSearchByPost("formVcrdSearch", '/BankPayment/GetVcrdPaymentSumMoney/');
    
}


//Excel导出数据
function btnExcelSave(id) {
    var strJson = GetSearchJson("formPaySearch");
    $.post("/BankPayment/ExcelDetailList/", { "strJsonWhere": strJson }, function (data, status) {
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


//Excel导出数据明细
function btnExcelSaveDetail() {
    var row = $("#tbPay").datagrid("getSelected");
    var strJson = GetSearchJson("formPaySearch");
    $.post("/BankPayment/ExcelVcrdDetailList/", { "mainId": row.Id }, function (data, status) {
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



//密码此次归零
function pwdCountReset() {
    console.log("sdfdsfdsfsdsdfd");
    var row = $('#tbPay').datagrid('getSelected');
    if (row == null) {
        alert("请选择一条记录");
    }
    $.messager.confirm('确认框', '您是否真的进行密码输错次数归零设定?', function (r) {
        if (r) {
            $.post("/BankPayment/PwdCountReset/", { "Id": row.Id }, function (data, status) {
                if (status != "success") {
                    return false;
                } else {
                    alert(data);
                }
            });
        }
    });
    
}


function btnPaySubmit() {
    var purposeInfo=$("#payPurposeInfo").textbox("getValue");
    if (purposeInfo == "") {
        alert("请输入付款摘要");
        return false;
    }
    var selectIds = "";
    var rows = $('#test').datagrid('getSelections');
    rows.forEach(function (element, index) {
        if (selectIds == "") {
            selectIds = element.seqno;
        } else {
            selectIds = selectIds + "," + element.seqno;
        }
    });;
    var strJson = GetSearchJson("formVcrdSearch");
    console.log(selectIds);
    console.log(strJson);
    $.post("/BankPayment/PayListCreate"
        , { "strJsonWhere": strJson, "selectMulIds": selectIds, "purposeInfo": purposeInfo,"tranType":$("#ccPaySelect").combobox("getValue") }
        , function (data, status) {
            if ("success" == status) {
                $.messager.alert("提示", data.ReMsg);
            }
        });
}

//清空条件
function btnClear() {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    $("#formVcrdSearch").form('clear');
}

function btnPaySearch() {
    var searchInfo = {
        TranType: 1,
        Amount: 200,
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };

    funcSearch('formPaySearch','tbPay');//查询tbPay的DataGrid的记录
}

//清空条件
function btnPayClear() {
    //通过form表单的.form('clear')功能,可以直接清除所有的数据项
    $("#formPaySearch").form('clear');
}



//审核
function auditMenuBtn(mode) {
    auditRequest(mode, 'tbPay', 'formPaySearch', '/BankPayment/AuditPayList');
}


//撤销审核
function unAuditMenuBtn(mode) {
    auditRequest(mode, 'tbPay', 'formPaySearch', '/BankPayment/UnAuditPayList');
}


//放弃领款
function abandonMoney() {
    var rows = $("#tbPay").datagrid('getSelections');
    if (rows.length != 1) {
        $.messager.alert('提示', "每次只能选择一条放弃领款的记录");
        return false;
    }
    var remark = $("#resetRemark").textbox('getValue');
    if (remark=="") {
        $.messager.alert('提示', "请输入放弃原因备注");
        return false;
    }


    $.messager.confirm('确认对话框', '您确认要放弃该记录吗？', function (r) {
        if (r) {
            //auditRequest("mul", 'tbPay', 'formPaySearch', '/BankPayment/AbandonMoney');
            var selectIds = "";
            selectIds = GetGridSelectIds('tbPay');
            console.log(selectIds);
            var strJson = GetSearchJson('formPaySearch');
            $.post('/BankPayment/AbandonMoney', { "strJsonWhere": strJson, "selectMulIds": selectIds, "remark": remark}, function (data,status) {
                if ("success" == status) {
                    alert(data);
                }
            });
            $("#tbPay").datagrid("unselectAll");
            
        }
    });

}


//审核与撤销审核的请求提交
function auditRequest(mode,gridId,formId,urlAddress) {
    var selectIds = "";
    if (mode == "all") {
        $.messager.confirm('确认对话框', '一般是单条审核，您确认要全部审付款记录吗？', function (r) {
            if (r) {
                $.messager.confirm('再次确认', '请再次确认，是否要全部审？', function (r) {
                    if (r) {
                        $.messager.prompt({
                            title: '验证',
                            msg: '请输入审核密码:',
                            fn: function (r) {
                                if (r) {
                                    $.post("/BankPayment/CheckUserInfo", {
                                        pwd: r,
                                    }, function (rdata, status) {
                                        if ("success" == status) {
                                            if (rdata.Flag == true) {

                                                console.log(selectIds);
                                                var strJson = GetSearchJson(formId);
                                                $.post(urlAddress, {
                                                    strJsonWhere: strJson,
                                                    selectMulIds: selectIds
                                                }, function (data, status) {
                                                    if ("success" == status) {
                                                        alert(data);
                                                    }
                                                });
                                                $("#" + gridId).datagrid("unselectAll");
                                            }
                                            else {

                                                $.messager.alert('提示', '审核密码不正确!', 'info');
                                            }
                                        }
                                    });
                                }
                                
                            }
                        });

                        
                    }
                });

            }
        });
    }
    else if (mode == "mul") {
        var rows = $('#'+gridId).datagrid('getSelections');
        if (rows != null) {
            if (rows[0].PayMode != 0) {
                $.messager.confirm('确认对话框', '审核后将立即进行支付，确认无误才能审核，是否继续？', function (r) {
                    if (r) {

                        console.log(selectIds);
                        var strJson = GetSearchJson(formId);
                        $.post(urlAddress, {
                            strJsonWhere: strJson,
                            selectMulIds: selectIds
                        }, function (data, status) {
                            if ("success" == status) {
                                alert(data);
                            }
                        });
                        $("#" + gridId).datagrid("unselectAll");
                    }
                });
                //获取选中的Id记录
                selectIds = GetGridSelectIds(gridId);
            }
            else {
                alert("本模式不支持现金支付审核");
            }
        }

        
    }
    else if (mode == "xianjin") {
        rows = $('#'+gridId).datagrid('getSelections');
        if (rows != null) {
            if (rows[0].PayMode == 0) {
                $.messager.confirm('确认对话框', '本记录是现金支付，是否正确？', function (r) {
                    if (r) {

                        $.messager.confirm('再次确认', '现金审核不能恢复，是否继续？', function (r) {
                            if (r) {
                                console.log(selectIds);
                                var strJson = GetSearchJson(formId);
                                $.post("/BankPayment/AuditXianJinPayList", {
                                    strJsonWhere: strJson,
                                    selectMulIds: selectIds,
                                    modeXianJin: 0  //现金模式
                                }, function (data, status) {
                                    if ("success" == status) {
                                        alert(data);
                                    }
                                });
                                $("#" + gridId).datagrid("unselectAll");
                            }
                        });

                    }
                });
                //获取选中的Id记录
                selectIds = GetGridSelectIds(gridId);
            }
            else {
                alert("本模式不支持【转账】支付审核");
            }
        }
        
    }
    else {
        alert("您传入的参数不对");
        return false;
    }
    
}




function DeleteMenuBtn() {

    var row = $("#test").datagrid("getSelected");
    if (row != null) {
        if (row.BankFlag == "-1") {
           
            $.messager.confirm('确认对话框', '您想要删除该记录吗？', function (r) {
                if (r) {                    
                    $.post("/Report/DeleteBankflagFailVcrd/", { "Seqno": row.seqno }, function (data, status) {
                        if (status != "success") {
                            return false;
                        } else {
                            var words = data.split("|");
                            if (words[0] == "OK") {
                                $.messager.alert("提示", "删除成功");
                            } else {
                                $.messager.alert("提示", data);
                            }
                        }
                    });
                }
            });


        } else {
            $.messager.alert("提示", "必须是银行扣款失败的记录才能删除");
        }          
    } else {
        $.messager.alert("提示", "请选择一条记录");
    }



}



function btnPrintPayMentReport(mode) {

    var searchInfo = {
        TranType: 1,
        Amount: 200,
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };

    var inpunts = $("#formPaySearch:input");
    var json = {};
    var message = [];
    var strJson = "";
    $("#formPaySearch table tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    console.log(strJson);

    if (mode == 1) {
        window.open("/BankPayment/PrintPayMentMonthReport/?strJsonWhere=" + strJson);
    }
    else {
        window.open("/BankPayment/PrintPayMentReport/?strJsonWhere=" + strJson);
    }
}


//机对账报表
function btnPrintATMReport(mode) {

    var searchInfo = {
        TranType: 1,
        Amount: 200,
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };

    var inpunts = $("#formPaySearch:input");
    var json = {};
    var message = [];
    var strJson = "";
    $("#formPaySearch table tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    console.log(strJson);

    if (mode == 1) {
        window.open("/BankPayment/PrintPayMentAtmReport/?strJsonWhere=" + strJson);
    }
    else {
        window.open("/BankPayment/PrintPayMentAtmReport/?strJsonWhere=" + strJson);
    }
}
