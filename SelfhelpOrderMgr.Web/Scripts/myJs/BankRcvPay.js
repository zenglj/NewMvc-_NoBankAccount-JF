
$(function () {
    loadDetailTable(); //显示存款明细    
    $("#FBankFlags").combobox('clear');
    
}); 



//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.9,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/BankRcvPay/GetSearchLists/' + $("#bankTypeId").val(),
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 10,
        pageList: [5,10, 20,50,100,1000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'seqno', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            {
                field: 'SuccFlag', title: '交易结果', width: 120, sortable: true, formatter: function (value, row, index) {
                    if (row.Flag == "1") {
                        return "成功";
                    } else if (row.Flag == "-1") {
                        return "失败";
                    } else if (row.Flag == "0") {
                        return "未处理";
                    } else {
                        return value;
                    }
                }
            },
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FName', title: '姓名', sortable: true, width: 100 },            
            { field: 'Amount', title: '金额', width: 100, sortable: true },
            { field: 'Flag', title: '状态', width: 100, sortable: true },
            {
                field: 'RcvDate', title: '收款日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.RcvDate != null) {
                        if (row.RcvDate != "") {
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
                field: 'PayDate', title: '付款日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.PayDate != null) {
                        if (row.PayDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },            
            { field: 'BankAccNo', title: '银行卡号', sortable: true, width: 100 },
            {
                field: 'BankRcvDate', title: '银行返时间', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.BankRcvDate != null) {
                        if (row.BankRcvDate != "") {
                            return getLocalTime(value);
                        } else {
                            return value;
                        }
                    } else {
                        return value;
                    }
                }
            },
            { field: 'Remark', title: '备注', sortable: true, width: 150 }
        ]],
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
        FCode: $("#FCode").numberbox('getValue'),
        FName :$("#FName").textbox('getText'),
        startTime:$("#StartDate").datetimebox('getValue'),
        endTime:$("#EndDate").datetimebox('getValue'),
        BankFlags:selBankFlags
    });


}

//打印报表，id 是的不同报表的参数
function printMenuBtn(id) {
    id = $("#bankTypeId").val();
    var strWhere = getSearchCondition();
    window.open("/BankRcvPay/PrintCriminalSumOrder/" + id + "?" + strWhere);

}

function OutExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    id = $("#bankTypeId").val();
    var objWhere = getSearchObjCondition();

    $.post("/BankRcvPay/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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
    //window.open("/Report/ExcelCriminalSumOrder/" + id + "?" + strWhere);

    $.post("/BankRcvPay/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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

    strSearchWhere = "FCode=" + $("#FCode").numberbox('getValue');
    strSearchWhere = strSearchWhere + "&FName=" + $("#FName").textbox('getText');
    strSearchWhere = strSearchWhere + "&startTime=" + $("#StartDate").datetimebox('getValue');
    strSearchWhere = strSearchWhere + "&endTime=" + $("#EndDate").datetimebox('getValue');

    strSearchWhere = strSearchWhere + "&BankFlags=" + selBankFlags;
    return strSearchWhere;
}

//获取Obj查询条件,用于Post方式
function getSearchObjCondition() {
    
    

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
        FCode: $("#FCode").numberbox('getValue'),
        FName: $("#FName").textbox('getText'),
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        BankFlags: selBankFlags
    };
    
    return objWhere;
}

//清空条件
function btnClear() {
    $("#FBankFlags").combobox('clear');
    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');

    $("#FCode").numberbox('clear');
    $("#FName").textbox('clear');
    
 
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
    //                + "<tr><td>单号：" + inv.invoice.InvoiceNo + "</td><td>编号：" + inv.invoice.FCrimeCode + "</td><td>姓名：" + inv.invoice.FCriminal + "</td><td >队别：" + inv.invoice.FAreaName + "</td><td>日期：" + getLocalTime(inv.invoice.OrderDate) + "</td></tr>"

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

