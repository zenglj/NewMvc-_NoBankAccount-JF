
$(function () {
    loadDetailTable(); //显示存款明细

    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.Flag == "1") {
                return 'background-color:brown;';
            }
        }
    });

    $('#addFCrimeName').textbox({
        inputEvents: $.extend({}, $.fn.textbox.defaults.inputEvents, {
            keypress: function test() {
                //alert(event.keyCode);
                if (event.keyCode == 13) {
                    findFCrimeCode();
                }
            }
        })
    });
}); 




//显示存款明细
function loadDetailTable() {
    $('#test').datagrid({
        //title: '存款记录',
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
        url: '/BankRcv/GetSearchList/',
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
            { field: 'CardNo', title: '结算卡号', sortable: true, width: 180 },
            { field: 'VchNum', title: 'VchNum', sortable: true, width: 100 },
            { field: 'RcvAmount', title: '收款金额', sortable: true, width: 100 },
            {
                field: 'transtype', title: '类型', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.transtype == "01") {
                        return "国内汇款";
                    }
                    else if (row.transtype == "02") {
                        return "国外汇款";
                    }
                    else if (row.transtype == "03") {
                        return "人行大额";
                    }
                    else if (row.transtype == "04") {
                        return "人行小额";
                    }

                    else if (row.transtype == "05") {
                        return "现金存款";
                    }
                    else if (row.transtype == "06") {
                        return "转帐收入";
                    }
                    else if (row.transtype == "07") {
                        return "汇票";
                    }
                    else if (row.transtype == "08") {
                        return "本票";
                    }
                    else if (row.transtype == "09") {
                        return "支票";
                    }
                    else if (row.transtype == "10") {
                        return "冲账";
                    }
                    else if (row.transtype == "11") {
                        return "冲正";
                    }
                    else if (row.transtype == "12") {
                        return "承兑汇票";
                    }
                    else if (row.transtype == "13") {
                        return "托收承付";
                    }

                    else if (row.transtype == "14") {
                        return "保证金";
                    }
                    else if (row.transtype == "15") {
                        return "现金取款";
                    }
                    else if (row.transtype == "16") {
                        return "转帐支出";
                    }
                    else if (row.transtype == "17") {
                        return "贷款放款";
                    }
                    else if (row.transtype == "18") {
                        return "贷款还款";
                    }
                    else if (row.transtype == "21") {
                        return "实时汇划";
                    }
                    else if (row.transtype == "22") {
                        return "退汇";
                    }
                    else if (row.transtype == "31") {
                        return "结息";
                    }
                    else if (row.transtype == "32") {
                        return "批量收费";
                    }
                    else if (row.transtype == "41") {
                        return "收费";
                    }
                    else if (row.transtype == "99") {
                        return "其他";
                    }
                    else{
                        return "其他";
                    }
                }
            },
            { field: 'tnxdate', title: '流水日期', sortable: true, width: 100 },
            { field: 'fractName', title: '汇款人', sortable: true, width: 100 },
            { field: 'OffsetVchNum', title: '对冲流水', sortable: true, width: 100 },
            { field: 'Remark', title: '备注', width: 100, sortable: true },
            { field: 'FCrimeCode', title: '狱号', width: 100, sortable: true },
            { field: 'FName', title: '罪犯姓名', sortable: true, width: 100 },
            { field: 'Error', title: '错误信息', sortable: true, width: 100 },
            {
                field: 'ImportFlag', title: '导入标志', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ImportFlag != null) {
                        if (row.ImportFlag == 1) {
                            return '是';
                        } else {
                            return '否';
                        }
                    } else {
                        return '否';
                    }
                }
            },
            {
                field: 'CreateDate', title: '导入日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CreateDate != null) {
                        if (row.CreateDate != "") {
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
            }
        ]],
        onSelect: function (rowIndex, rowData) {
            SetAuditDiv(rowData);
        },
        pagination: true,
        rownumbers: true
    });
}

function SetAuditDiv(row) {
    if (row.ImportFlag == 1) {
        $("#divAuditRec").hide();
    } else {
        $("#addVchnum").val(row.VchNum);
        $("#divAuditRec").show();
        $('#addFCrimeName').textbox('setValue','');
        $('#addFCrimeCode').numberbox('setValue','');
    }
}

//没有银行的记录审核入账
function btnAuditBankRec() {
    if ($('#addFCrimeCode').val() == "") {
        $.message.alert("罪犯编号不能为空");
        return false;
    }
    if ($('#addFCrimeName').val() == "") {
        $.message.alert("罪犯姓名不能为空");
        return false;
    }
    $('#ffBankRecAudit').form({
        url:"/BankRcv/SetBankArtificialAddNotImportRec",
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            var rs = $.parseJSON(data);

            if (rs.Flag == true) {
                var mainRow = $('#test').datagrid('getSelected');
                var idx = $('#test').datagrid('getRowIndex', mainRow);
                var bouns = rs.DataInfo;
                $('#test').datagrid('updateRow', {
                    index: idx,
                    row: {
                        FCrimeCode: bouns.FCrimeCode,
                        FName: bouns.FName,
                        ImportFlag: bouns.ImportFlag
                    }
                });
            }

            alert(rs.ReMsg)
        }
    });
    // submit the form    
    $('#ffBankRecAudit').submit();  
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

    $.post("/BankRcv/GetListSumAmount", { "strJsonWhere": strJson }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                $("#schSumMoney").html("查询结果总额:<u>" + data.DataInfo + " 元</u>");
            }
        }
    });
    
}

//打印汇总单
function btnPrintSum() {
    var strJson = "";
    $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
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

    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });
    window.open("/BankRcv/PrintSumList/?strJsonWhere=" +   strJson);
}


//打印明细清单
function btnPrintList() {
    var strJson = "";
    $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
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

    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });
    window.open("/BankRcv/PrintDetailList/?strJsonWhere=" + strJson);
}


function btnExcelSave(id) {


    var strJson = "";
    $("#crimeSearch tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
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

    $('#test').datagrid('load', {
        //FCode: $("#FCode").numberbox('getValue'),
        //FName :$("#FName").textbox('getText'),
        strJsonWhere: strJson
    });


    $.post("/BankRcv/ExcelDetailList/", { "strJsonWhere": strJson}, function (data, status) {
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


//月统计报表 期初 期间  期末
function btnExcelReportSave() {

    $.post("/BankRcv/ExcelReportDetailList/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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


function btnExcelBankDtlSch() {

    //$("#lblInfo").html("系统正在导入请稍候......");
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });

    $('#frmBankExcel').form({
        url: '/BankRcv/ExcelInport',
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            $.messager.progress('close');
            //$("#winSearch").window('close');
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            } else {
                alert(data);
            }
        }
    });
    // submit the form    
    $('#frmBankExcel').submit();

}



//清空条件
function btnClear() {
    $("#formSearch").form('clear');
}

//导出未识别的存款记录单
function btnExcelNoFindCashReport() {
    var startTime = $("#SearchCreateDate_Start").datetimebox('getValue');
    var endTime = $("#SearchCreateDate_End").datetimebox('getValue');
    if (startTime == '') {
        alert("请选择一个时间段,谢谢");
        return false;
    }

    $.post("/BankRcv/ExcelNoFindCashReport/", { "startTime": startTime, "endTime": endTime }, function (data, status) {
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

//手动识别的流水号
function btnCheckVchnum() {
    $.messager.confirm('确认框', '记录不能是退款记录，不能是看守所转款，是否真的要识别该记录?', function (r) {
        if (r) {
            var vchnum = $("#search_vchnum").textbox('getValue');
            if (vchnum == '') {
                alert("请输入流水号,谢谢");
                return false;
            }

            $.post("/BankRcv/btnCheckVchnum/", { "vchnum": vchnum }, function (data, status) {
                if (status != "success") {
                    return false;
                } else {
                    var words = data.split("|");
                    alert(data);
                }
            });
        }
    });
    
}


function findFCrimeCode() {
    console.log("dsfdsfsdfsafserew");
    var fname = $("#addFCrimeName").textbox('getValue');
    $.post("/BankRcv/findFCrimeCode", { "fname": fname }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                $("#addFCrimeCode").textbox('setValue', data.DataInfo);
            } else {
                alert(data.ReMsg);
            }
        }
    });
}