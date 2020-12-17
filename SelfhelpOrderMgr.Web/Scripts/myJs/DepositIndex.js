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
        url: '/DepositList/GetSearchList/',
        sortName: 'Id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5,10, 20,50,100,200,2000,20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: 'Id', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            {
                field: 'InVcrdFlag', title: 'Vcrd标志', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.InVcrdFlag == "-1") {
                        return "确认失败";
                    }
                    else if (row.InVcrdFlag == "0" || row.InVcrdFlag == null || row.InVcrdFlag == "") {
                        return "待审核";
                    }
                    else if (row.InVcrdFlag == "1") {
                        return "已入账";
                    }
                    else if (row.InVcrdFlag == "2") {
                        return "已退账";
                    }
                    else{
                        return "其他";
                    }
                    
                }
            },
            { field: 'obssid', title: '流水号', sortable: true, width: 100 },
            { field: 'FromActAccount', title: '付款账号', sortable: true, width: 100 },
            { field: 'FromActName', title: '付款人', width: 100, sortable: true },
            { field: 'ToName', title: '收款人', width: 100, sortable: true },
            { field: 'TranAmount', title: '汇款金额', sortable: true, width: 100 },
            { field: 'TranCurrency', title: '币种', sortable: true, width: 100 },
            {
                field: 'TranSuccDate', title: '汇款日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.TranSuccDate != null) {
                        if (row.TranSuccDate != "") {
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
            { field: 'TranStatus', title: '汇款状态', width: 100, sortable: true },

            { field: 'PurposeInfo', title: '用途', width: 100, sortable: true },
            {
                field: 'AccountingTime', title: '到账时间', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AccountingTime != null) {
                        if (row.AccountingTime != "") {
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
            { field: 'Memo', title: '汇款备注', sortable: true, width: 100 },
            { field: 'AuditRemark', title: '入账审核说明', sortable: true, width: 100 },
            {
                field: 'CrtDate', title: '导入日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CrtDate != null) {
                        if (row.CrtDate != "") {
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
            }
        ]],
        onSelect: function (rowIndex, rowData) {
            console.log(rowData);
            if (rowData.InVcrdFlag ==1) {
                var item = $('#mmOpt').menu('findItem', '入账审核');
                $('#mmOpt').menu('disableItem', item.target);
                var item = $('#mmOpt').menu('findItem', '退账审核');
                $('#mmOpt').menu('enableItem', item.target);

            } else if (rowData.InVcrdFlag == 0) {
                var item = $('#mmOpt').menu('findItem', '入账审核');
                $('#mmOpt').menu('enableItem', item.target);
                var item = $('#mmOpt').menu('findItem', '退账审核');
                $('#mmOpt').menu('disableItem', item.target);
            }
            SetAuditFormInfo(rowData);
        },
        pagination: true,
        rownumbers: true
    });
}

//设定审核表单
function SetAuditFormInfo(rowData) {
    //console.log($("#ffAudit div input[name='TranAmount']"));
    $("#formObssid").textbox('setValue', rowData.obssid);
    $("#formTranAmount").numberbox('setValue', rowData.TranAmount);
    $("#formFCrimecode").textbox('setValue', rowData.FCrimecode);
    $("#formRemark").textbox('setValue', rowData.Remark);
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

//确认消费退货——消费记录为一般扣款（不影响消费限额）
function btnSaveChangeType() {
    $.messager.confirm('确认', '您确认要退货调账吗？', function (r) {
        if (r) {
            var saveType = $("#comChangeType").combobox('getValue');
            if (saveType != 0) {
                var saleId = $("#tTypeFlag").val();
                $.post("/Report/GetSaleType/", { "saleId": saleId }, function (data, status) {
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
    var searchInfo = {
        FromActAccount: 1221211,
        TranAmount: 200,
        CrtDate_Start: '2020-05-01',
        CrtDate_End: '2020-05-21'
    };

    var inpunts = $("#crimeSearch:input");
    var json = {};
    var message = [];
    var strJson = "";
        $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
            //var username = $(this).find("input[name='username']").val();
            //var email = $(this).find("input[name='email']").val();
            //json = {
            //    "username":username,
            //    "email":email
            //};
            //message.push(json);
            //if ($(this).val() != 'undefined' && $(this).attr("name") != "undefined") {
            //    //console.log(this);
            //    console.log($(this).attr("name") +":" +$(this).val());
                
            //}
            
            if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
                console.log($(this).attr("name") + ":" + $(this).val());
                if (strJson == "") {
                    strJson = "\""+$(this).attr("name") + "\":\"" + $(this).val()+ "\"";
                } else {
                    strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
                }                
            }
        });
        strJson = "{" + strJson + "}";
    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });
}

//打印报表，id 是的不同报表的参数
function printMenuBtn(id) {
    var strWhere = getSearchCondition();
    window.open("/Report/PrintCriminalSumOrder/" + id + "?" + strWhere);
}

function OutExcelSumOrder(id) {
    //$.messager.alert("提示",id);
    var objWhere = getSearchObjCondition();

    $.post("/Report/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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
    $.post("/Report/ExcelCriminalSumOrder/" + id, objWhere, function (data, status) {
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
        FFlags: selFFlags
    };    
    return objWhere;
}

//清空条件
function btnClear() {
    $("#SearchId").textbox('clear');
    $("#SearchObssid").textbox('clear');
    $("#SearchFromActAccount").textbox('clear');
    $("#SearchFromActName").textbox('clear');
    $("#SearchToActAccount").textbox('clear');
    $("#SearchToActName").textbox('clear');
    $("#SearchTranAmount").textbox('clear');
    $("#SearchTranSuccDate_Start").datetimebox('clear');
    $("#SearchTranSuccDate_End").datetimebox('clear');
    $("#SearchTranStatus").textbox('clear');
    $("#SearchPurposeInfo").textbox('clear');
    $("#SearchAccountingTime_Start").datetimebox('clear');
    $("#SearchAccountingTime_End").datetimebox('clear');
    $("#SearchInVcrdFlag").combobox('clear');
    $("#SearchCrtDate_Start").datetimebox('clear');
    $("#SearchCrtDate_End").datetimebox('clear');
    $("#SearchAuditRemark").textbox('clear');
}

//A4打印消费单
//printType 0表示消费清单，1表示签字确认单
function printMulXiaofeiDan(printType) {
    CreateXiaofeiDanReport('mul', printType);
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