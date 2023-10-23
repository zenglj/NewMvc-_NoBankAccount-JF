//====================================================
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
    $("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');
    $("#FCheckFlag").combobox('clear');

    $("#winChangeList").window('close');
    
    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.Flag == "1") {
                return 'background-color:brown;';
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
        height: $(window).height()*0.9,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/JfReport/GetSearchVcrds/',
        sortName: 'Id',
        sortOrder: 'asc',
        showFooter:true,
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5,10, 20,50,100,200,2000,20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'Id', width: 60, sortable: true }
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
            { field: 'DAmount', title: '收(积分)', width: 100, sortable: true },
            { field: 'CAmount', title: '支(积分)', width: 100, sortable: true },
            { field: 'DType', title: '类型', width: 100, sortable: true },
            { field: 'CrtBy', title: '操作员', width: 100, sortable: true },
            { field: 'FCheckFlag', title: '审核标志', width: 100, sortable: true, formatter: function (value, row, index) {
                if (row.DAmount != 0) {
                    if (row.FCheckFlag == 0) {
                        return "未审";
                    } else {
                        return "已审";
                    }
                } else {
                    if (row.FCheckFlag == -1) {
                        return "未审";
                    } else {
                        return "已审";
                    }
                }
            } },
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
        onLoadSuccess: function (data) {
            $("#test").datagrid('resize');
            $("#test").datagrid('reloadFooter', [{ FCriminal: '合计', DAmount :data.sum}])
        },
        pagination: true,
        rownumbers: true
    });
}


function getInvList(invno) {
    $.post("/JfReport/getInvList/", { "invNo": invno }, function (data, status) {
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

//确认消费退货——消费记录为一般扣款（不影响消费限额）
function btnSaveChangeType() {
    $.messager.confirm('确认', '您确认要退货调账吗？', function (r) {
        if (r) {
            var saveType = $("#comChangeType").combobox('getValue');
            if (saveType != 0) {
                var saleId = $("#tTypeFlag").val();
                $.post("/JfReport/GetSaleType/", { "saleId": saleId }, function (data, status) {
                    if (status != "success") {
                        return false;
                    } else {
                        var words = data.split("|");
                        if (words[0] == "OK") {
                            $.post("/Report/SaveChangeType/", { "invNo": $("#tInvno").html(), "saveType": saveType }, function (data, status) {
                                if (status != "success") {
                                    return false;
                                } else {
                                    var words = data.split("|");
                                    if (words[0] == "OK") {
                                        $.messager.alert("提示", words[1]);
                                    } else {
                                        $.messager.alert("提示", data);
                                    }
                                }
                            });
                        } else {
                            $.messager.alert("提示", data);
                        }

                    }
                });
            } else {
                $.messager.alert("提示", "请选择您要修改的类型");
            }
        }
    });
    
    
}



function btnSearch() {
    var rows = $("#test").datagrid('getRows');
    for (var i = rows.length-1; i >=0 ; i--) {
        $("#test").datagrid('deleteRow', 0);
    }
    //alert($("#FCashTypes").combobox('getValues'));

    GetQueryComboboxValues();//获取查询栏里多选框里的值
    //alert(selFFlags);

    $('#test').datagrid('load', {
        FCode: $("#FCode").numberbox('getValue'),
        FName :$("#FName").textbox('getText'),
        FRemark :$("#FRemark").textbox('getText'),
        cyName:$("#FCyName").combobox('getValue'),
        startTime:$("#StartDate").datetimebox('getValue'),
        endTime:$("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy:$("#FCrtBy").combobox('getValue'),
        CriminalFlag:$("#FCriminalFlag").combobox('getValue'),
        CashTypes:selCashTypes,
        PayTypes:selPayTypes,
        AccTypes:selAccTypes,
        BankFlags: selBankFlags,
        FFlags: selFFlags,
        CheckFlag:$("#FCheckFlag").combobox('getValue'),
        CardTypeFlag: $("#CardTypeFlag").combobox('getValue')
    });


}

//打印报表，id 是的不同报表的参数
function printMenuBtn(id) {

    var strWhere = getSearchCondition();
    window.open("/JfReport/PrintCriminalSumOrder/" + id + "?" + strWhere);

}

function OutExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    var objWhere = getSearchObjCondition();

    $.post("/JfReport/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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

    var objWhere = getSearchObjCondition();
    //window.open("/Report/ExcelCriminalSumOrder/" + id + "?" + strWhere);

    $.post("/JfReport/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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


    GetQueryComboboxValues();//获取查询栏里多选框里的值

    strSearchWhere = "FCode=" + $("#FCode").numberbox('getValue');
    strSearchWhere = strSearchWhere + "&FName=" + $("#FName").textbox('getText');
    strSearchWhere = strSearchWhere + "&FRemark=" + $("#FRemark").textbox('getText');
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
    strSearchWhere = strSearchWhere + "&FFlags=" + selFFlags;
    strSearchWhere = strSearchWhere + "&CardTypeFlag=" + $("#CardTypeFlag").combobox('getValue');

    return strSearchWhere;
}

//获取Obj查询条件,用于Post方式
function getSearchObjCondition() {



    GetQueryComboboxValues();//获取查询栏里多选框里的值

    var objWhere = {
        FCode: $("#FCode").numberbox('getValue'),
        FName: $("#FName").textbox('getText'),
        FRemark: $("#FRemark").textbox('getText'),
        cyName: $("#FCyName").combobox('getValue'),
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        areaName: $("#FAreaName").combobox('getValue'),
        CrtBy: $("#FCrtBy").combobox('getValue'),
        CriminalFlag: $("#FCriminalFlag").combobox('getValue'),
        CashTypes: selCashTypes,
        PayTypes: selPayTypes,
        AccTypes: selAccTypes,
        BankFlags: selBankFlags,
        FFlags: selFFlags,
        CardTypeFlag:$("#CardTypeFlag").combobox('getValue')
    };
    
    return objWhere;
}

//清空条件
function btnClear() {
    $("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');

    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');

    $("#FCode").numberbox('clear');
    $("#FName").textbox('clear');
    $("#FRemark").textbox('clear');
    $("#FCyName").combobox('clear');
    $("#FAreaName").combobox('clear');
    $("#FCrtBy").combobox('clear');
    $("#FCriminalFlag").combobox('clear');
    $("#CardTypeFlag").combobox('clear');    
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
    window.open("/JfConsumePrint/PrintXiaofeiDan?invoices=" + selectRows + "&printType=" + printType);

}


function DeleteMenuBtn() {

    var row = $("#test").datagrid("getSelected");
    if (row != null) {
        if (row.BankFlag == "-1") {
           
            $.messager.confirm('确认对话框', '您想要删除该记录吗？', function (r) {
                if (r) {                    
                    $.post("/JfReport/DeleteBankflagFailVcrd/", { "Seqno": row.seqno }, function (data, status) {
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