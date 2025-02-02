﻿
$(function () {
    loadDetailTable(); //显示存款明细
    $("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    //$("#FBankFlags").combobox('clear');

    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.BankFlag >=1) {
                return 'background-color:red;';
            } else if (row.CheckFlag == "1" && row.DAmount>0) {
                return 'background-color:green;';
            } else if (row.CheckFlag == "0" && row.CAmount > 0) {
                return 'background-color:green;';
            }
        }
    });
    
}); 



//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: 400,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: false,
        striped: true,
        collapsible: true,
        url: '/PayAudit/GetSearchVcrds/',
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 10,
        pageList: [5,10, 20,50,100],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'CheckFlag', title: '审核状态', width: 120, sortable: true, formatter: function (value, row, index) {
                    if (row.DAmount > 0) {
                        if (row.CheckFlag == "1") {
                            return "已审核";
                        } else if (row.CheckFlag == "0") {
                            return "未审核";
                        } else {
                            return value;
                        }
                    } else {
                        if (row.CheckFlag == "0") {
                            return "已审核";
                        } else if (row.CheckFlag == "-1") {
                            return "未审核";
                        } else {
                            return value;
                        }
                    }
                    
                }
            },
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FCriminal', title: '姓名', sortable: true, width: 100 },            
            { field: 'DAmount', title: '收(元)', width: 100, sortable: true },
            { field: 'CAmount', title: '支(元)', width: 100, sortable: true },
            { field: 'DType', title: '类型', width: 100, sortable: true },
            {
                field: 'BankFlag', title: '交易状态', width: 120, sortable: true, formatter: function (value, row, index) {
                    if (row.BankFlag == "3") {
                        return "手动成功";
                    } else if (row.BankFlag == "2") {
                        return "已成功";
                    } else if (row.BankFlag == "1") {
                        return "已发送";
                    } else {
                        return "待发送";
                    }
                }
            },
            { field: 'CrtBy', title: '操作员', width: 100, sortable: true },
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
            { field: 'Remark', title: '备注', sortable: true, width: 150 }
        ]],
        onSelect: function (rowIndex, rowData) {
            if (rowData.BankFlag == 0) {
                $('#btndel').linkbutton('enable');
            } else {
                $('#btndel').linkbutton('disable');
            }
        },
        pagination: true,
        rownumbers: true
    });
}



function btnSearch() {
    var rows = $("#test").datagrid('getRows');
    for (var i = rows.length-1; i >=0 ; i--) {
        $("#test").datagrid('deleteRow', 0);
    }
    //alert($("#FCashTypes").combobox('getValues'));

    var CashTypes = $("#FCashTypes").combobox('getValues');
    var selCashTypes = "";
    for (var i = 0; i < CashTypes.length; i++){
        var CashType = CashTypes[i];
        if (selCashTypes == "") {
            selCashTypes = "'" + CashType + "'";;
        } else {
            selCashTypes = selCashTypes+",'"+ CashType +"'";
        }
    }
    

    var PayTypes = $("#FPayTypes").combobox('getValues');
    var selPayTypes = "";
    for (var i = 0; i < CashTypes.length; i++) {
        var PayType = PayTypes[i];
        if (selPayTypes == "") {
            selPayTypes = "'" + PayType + "'";;
        } else {
            selPayTypes = selPayTypes + ",'" + PayType + "'";
        }
    }

    var AccTypes = $("#FAccTypes").combobox('getValues');
    var selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType ;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    var selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }


    $('#test').datagrid('load', {
        idOperationType:$("#idOperationType").val(),
        FCode: $("#FCode").numberbox('getValue'),
        FName :$("#FName").textbox('getText'),
        cyName:$("#FCyName").combobox('getValue'),
        startTime:$("#StartDate").datetimebox('getValue'),
        endTime:$("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy:$("#FCrtBy").combobox('getValue'),
        CriminalFlag:$("#FCriminalFlag").combobox('getValue'),
        CashTypes:selCashTypes,
        PayTypes:selPayTypes,
        AccTypes:selAccTypes,
        BankFlags:selBankFlags

    });

    var objWhere = getSearchObjCondition('');
    $.post("/PayAudit/GetVcrdsMoney", objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            var words = data.split("|");
            if (words[0] == "OK") {
                $("#VcrdSumMoney").html(words[1]);
            }
            //$.messager.alert("提示", data);
        }
    });

}

//执行审核，param 是的不同的参数 all表示全部，mul表示个别选中
function auditMenuBtn(param) {
    selSeqno = "";
    if (param == 'mul') {
        var rows = $("#test").datagrid("getSelections");
        if (rows.length >= 1) {
            for (var i = 0; i < rows.length; i++) {
                if (selSeqno == "") {
                    selSeqno = rows[i].seqno;
                } else {
                    selSeqno =selSeqno+ ","+rows[i].seqno;
                }
            }
            param = selSeqno;
        } else {
            $.messager.alert("提示", "请至少选择一行记录");
            return false;
        }
    }

    var objWhere = getSearchObjCondition(param);

    $.post("/PayAudit/auditMenuBtn/" + $("#idOperationType").val(), objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            $.messager.alert("提示",data);
        }
    });


}

//撤消审核，param 是的不同的参数 all表示全部，mul表示个别选中
function unAuditMenuBtn(param) {
    selSeqno = "";
    if (param == 'mul') {
        var rows = $("#test").datagrid("getSelections");
        if (rows.length >= 1) {
            for (var i = 0; i < rows.length; i++) {
                if (selSeqno == "") {
                    selSeqno = rows[i].seqno;
                } else {
                    selSeqno = selSeqno + "," + rows[i].seqno;
                }
            }
            param = selSeqno;
        } else {
            $.messager.alert("提示", "请至少选择一行记录");
            return false;
        }
    }

    var objWhere = getSearchObjCondition(param);

    $.post("/PayAudit/unAuditMenuBtn/" + $("#idOperationType").val(), objWhere, function (data, status) {
        if (status != "success") {
            return false;
        } else {
            $.messager.alert("提示", data);
        }
    });


}

function OutExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    var objWhere = getSearchObjCondition();

    $.post("/PayAudit/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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

//导出Excel数据，id 是的不同报表的参数
function ExcelMain(id) {
    alert(id);

    var objWhere = getSearchObjCondition();
    //window.open("/PayAudit/ExcelCriminalSumOrder/" + id + "?" + strWhere);

    $.post("/PayAudit/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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

//获取查询条件，用于直接开链接方式
function getSearchCondition() {
    var CashTypes = $("#FCashTypes").combobox('getValues');
    var selCashTypes = "";
    for (var i = 0; i < CashTypes.length; i++) {
        var CashType = CashTypes[i];
        if (selCashTypes == "") {
            selCashTypes = "'" + CashType + "'";;
        } else {
            selCashTypes = selCashTypes + ",'" + CashType + "'";
        }
    }
    

    var PayTypes = $("#FPayTypes").combobox('getValues');
    var selPayTypes = "";
    for (var i = 0; i < PayTypes.length; i++) {
        var PayType = PayTypes[i];
        if (selPayTypes == "") {
            selPayTypes = "'" + PayType + "'";;
        } else {
            selPayTypes = selPayTypes + ",'" + PayType + "'";
        }
    }

    var AccTypes = $("#FAccTypes").combobox('getValues');
    var selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    var selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }
    
    strSearchWhere = "idOperationType=" + $("#idOperationType").val();
    strSearchWhere = strSearchWhere + "&FCode=" + $("#FCode").numberbox('getValue');
    strSearchWhere = strSearchWhere + "&FName=" + $("#FName").textbox('getText');
    strSearchWhere = strSearchWhere + "&cyName=" + $("#FCyName").combobox('getValue');
    strSearchWhere = strSearchWhere + "&startTime=" + $("#StartDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&endTime=" + $("#EndDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&areaName=" + $("#FAreaName").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CrtBy=" + $("#FCrtBy").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CriminalFlag=" + $("#FCriminalFlag").combobox('getValue');
    strSearchWhere = strSearchWhere + "&CashTypes=" + selCashTypes;
    strSearchWhere = strSearchWhere + "&PayTypes=" + selPayTypes;
    strSearchWhere = strSearchWhere + "&AccTypes=" + selAccTypes;
    strSearchWhere = strSearchWhere + "&BankFlags=" + selBankFlags;
    return strSearchWhere;
}

//获取Obj查询条件,用于Post方式
function getSearchObjCondition(param) {
    var CashTypes = $("#FCashTypes").combobox('getValues');
    var selCashTypes = "";
    for (var i = 0; i < CashTypes.length; i++) {
        var CashType = CashTypes[i];
        if (selCashTypes == "") {
            selCashTypes = "'" + CashType + "'";;
        } else {
            selCashTypes = selCashTypes + ",'" + CashType + "'";
        }
    }
    

    var PayTypes = $("#FPayTypes").combobox('getValues');
    var selPayTypes = "";
    for (var i = 0; i < PayTypes.length; i++) {
        var PayType = PayTypes[i];
        if (selPayTypes == "") {
            selPayTypes = "'" + PayType + "'";;
        } else {
            selPayTypes = selPayTypes + ",'" + PayType + "'";
        }
    }

    var AccTypes = $("#FAccTypes").combobox('getValues');
    var selAccTypes = "";
    for (var i = 0; i < AccTypes.length; i++) {
        var AccType = AccTypes[i];
        if (selAccTypes == "") {
            selAccTypes = AccType;
        } else {
            selAccTypes = selAccTypes + "," + AccType + "";
        }
    }

    var BankFlags = $("#FBankFlags").combobox('getValues');
    var selBankFlags = "";
    for (var i = 0; i < BankFlags.length; i++) {
        var BankFlag = BankFlags[i];
        if (selBankFlags == "") {
            selBankFlags = BankFlag;
        } else {
            selBankFlags = selBankFlags + "," + BankFlag + "";
        }
    }

    var objWhere = {
        AuditMode: param,
        idOperationType: $("#idOperationType").val(),
        FCode: $("#FCode").numberbox('getValue'),
        FName: $("#FName").textbox('getText'),
        cyName: $("#FCyName").combobox('getValue'),
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy: $("#FCrtBy").combobox('getValue'),
        CriminalFlag: $("#FCriminalFlag").combobox('getValue'),
        CashTypes: selCashTypes,
        PayTypes: selPayTypes,
        AccTypes: selAccTypes,
        BankFlags: selBankFlags
    };
    
    return objWhere;
}

//清空条件
function btnClear() {
    //$("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');

    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');

    $("#FCode").numberbox('clear');
    $("#FName").textbox('clear');
    $("#FCyName").combobox('clear');
    $("#FAreaName").combobox('clear');
    $("#FCrtBy").combobox('clear');
    $("#FCriminalFlag").combobox('clear');
 
}

//A4打印消费单
//printType 0表示消费清单，1表示签字确认单
function printMulXiaofeiDan(printType) {

    CreateXiaofeiDanReport('mul', printType)

    //var rows = $("#test").datagrid('getSelections');
    //for (var i = 0; i < rows.length; i++) {
    //    var tid = i;
    //    myTestHtml("#temp" + tid);
    //}
}

//生成消费单
//printType 0表示消费清单，1表示签字确认单
function CreateXiaofeiDanReport(e,printType) {
    if (e == "mul") {
        var rows = $("#test").datagrid("getSelections");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = selectRows + "|" + rows[i].InvoiceNo;
                }
            }
        }
    } else if (e == "one") {
        var row = $("#test").datagrid("getSelected");
        var selectRows = row.InvoiceNo;
    } else if (e == "all") {
        $("#test").datagrid("selectAll");
        var rows = $("#test").datagrid("getRows");
        var selectRows = "";
        if (rows.length > 0) {
            for (var i = 0; i < rows.length; i++) {
                if (selectRows == "") {
                    selectRows = rows[i].InvoiceNo;
                } else {
                    selectRows = rows[i].InvoiceNo + "|" + selectRows;
                }
            }
        }
    } else {
        return false;
    }
    window.open("/Super/PrintXiaofeiDan?invoices=" + selectRows + "&printType=" + printType);
    //$.post("/Home/GetInvoices", {
    //    "invoices": selectRows
    //}, function (data, status) {
    //    if ("success" != status) {
    //        return false;
    //    } else {
    //        //$.messager.alert('提示', data);
    //        var invs = $.parseJSON(data);
    //        $("#template").empty();//清空
    //        if (invs.length > 0) {
    //            for (var i = 0; i < invs.length; i++) {
    //                var content = invs[i];
    //                var inv = content;

    //                var xiaopiao = "<div id='temp" + i + "'><div> <div style='text-align:center'><h2>监狱一卡通系统</h2></div>"
    //                + "<table >"
    //                + "<tbody>"
    //                + "<tr><td>单号：" + inv.invoice.InvoiceNo + "</td><td>编号：" + inv.invoice.FCrimeCode + "</td><td>姓名：" + inv.invoice.FCriminal + "</td><td >监区：" + inv.invoice.FAreaName + "</td><td>日期：" + getLocalTime(inv.invoice.OrderDate) + "</td></tr>"

    //                + "</tbody>"
    //                + "</table>"
    //                + "<hr />"
    //                + "<table style='font-size:11px;'>"
    //                + "<thead>"
    //                + "<tr><th>货号</th><th>品名</th><th style='width:80px;'>型号</th><th style='width:50px;'>单价</th><th style='width:50px;'>数量</th><th>金额</th></tr>"
    //                + "</thead>"
    //                + "<tbody>";

    //                for (var j = 0; j < inv.details.length; j++) {
    //                    xiaopiao = xiaopiao + "<tr><td>" + inv.details[j].SPShortCode + "</td><td  style=' border-bottom:dashed;border-bottom-width:1px;'>" + inv.details[j].GNAME + "</td><td>" + inv.details[j].Remark + "</td><td align='center'>" + inv.details[j].GDJ + "</td><td align='center'>" + inv.details[j].QTY + "</td><td align='right'>" + inv.details[j].AMOUNT + "</td></tr>"
    //                    + ""

    //                }

    //                xiaopiao = xiaopiao + "</tbody>"
    //                + "</table>"
    //                + "<hr />"
    //                + "<h3><span>消费合计：" + inv.invoice.Amount + "元</span></h3><hr/>"
                    
    //                + "</div><br/></div>";

    //                $("#template").append(xiaopiao);
    //            }
    //        }

    //    }
    //});
}


//生成代付数据发送到银行
function sendBankDataAll() {
    var fcrimecode = $("#sendFCrimeCode").val();
    $.messager.confirm('确认','是否真的立即发送到银行？',function(r){    
        if (r){    
            $.post("/PayAudit/SendBankDataAll", {"FCrimeCode":fcrimecode}, function (data, status) {
                if (status != "success") {
                    return false;
                } else {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        $.messager.alert("提示","发送成功");
                    } else {
                        $.messager.alert("提示", "发送失败");
                    }

                }
            });
        }    
    });  
}