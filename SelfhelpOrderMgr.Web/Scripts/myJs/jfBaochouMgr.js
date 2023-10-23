
$(function () {
    loadMainTable(); //显示主单明细
    loadDetailTable("JFN00000")
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
function loadMainTable() {
    $('#test').datagrid({
        //title: '存款记录',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height()*0.4,
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
        url: '/JFBaochou/GetSearchList/',
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
            { field: 'OrderId', title: '主单号', sortable: true, width: 80 },
            { field: 'TypeFlag', title: '类型标志', sortable: true, width: 100 },
            { field: 'YearMonth', title: '年月', sortable: true, width: 100 },
            { field: 'FAreaCode', title: '队别编号', sortable: true,hidden:true, width: 100 },
            { field: 'FAreaName', title: '队别名称', sortable: true, width: 100 },
            { field: 'CompleteMoney', title: '生产完成金额', sortable: true, width: 100 },
            { field: 'WorkDay', title: '出勤天数', sortable: true, width: 100 },
            { field: 'ExaminePersonNum', title: '考核人数', sortable: true, width: 100 },
            { field: 'Ranking', title: '排名', sortable: true, width: 100 },
            { field: 'BaseAmount', title: '基本报酬', sortable: true, width: 100 },
            { field: 'ExtAmount', title: '专项报酬', sortable: true, width: 100 },
            { field: 'Amount', title: '实发金额', sortable: true, width: 100 },
            { field: 'Points', title: '实发积分', sortable: true, width: 100 },
            {
                field: 'OrderStatus', title: '单据状态', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ImportFlag != null) {
                        if (row.ImportFlag == 1) {
                            return '已入账';
                        } if (row.ImportFlag == 3) {
                            return '已退回';
                        } if (row.ImportFlag == 2) {
                            return '公账';
                        } else {
                            return '否';
                        }
                    } else {
                        return '否';
                    }
                }
            },
            { field: 'WorkChangeRatio', title: '工分兑换系数', sortable: true, width: 100 },
            {
                field: 'CreateDate', title: '建档日期', width: 100, sortable: true, formatter: function (value, row, index) {
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
            },
            { field: 'CreateBy', title: '创建人', sortable: true, width: 100 },
            {
                field: 'AuditDate', title: '审核日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.AuditDate != null) {
                        if (row.AuditDate != "") {
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
            { field: 'AuditBy', title: '审核人', sortable: true, width: 100 },
            { field: 'Remark', title: '备注', sortable: true, width: 100 },         
            {
                field: 'Opration', title: '操作', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.ImportFlag == 0) {
                        return "<input type='button' value='设为公账' onclick='SetImportFlag(2," + row.Id + ")'/>"
                    } else if (row.ImportFlag == 2){
                        return "<input type='button' value='设为个人账' onclick='SetImportFlag(0," + row.Id + ")'/>";
                    }else {
                    return "";
                }
                }
            }
        ]],
        onSelect: function (rowIndex, rowData) {
            $("#detailOrderId").val(rowData.OrderId);
            loadDetailTable(rowData.OrderId);
        },
        pagination: true,
        rownumbers: true
    });
}




//显示存款明细
function loadDetailTable(orderid) {
    $('#detail').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.4,
        queryParams: {
            orderId: orderid
        },
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: false,
        striped: true,
        collapsible: true,
        url: '/JfBaochou/GetDetailList/',
        sortName: 'Id',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Id',
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100, 200, 2000, 20000],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'Id', width: 60, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
            { field: 'OrderId', title: '主单号', sortable: true, width: 80 },
            { field: 'FCrimeCode', title: '狱号', sortable: true, width: 100 },
            { field: 'FCrimeName', title: '姓名', width: 100, sortable: true },
            { field: 'WorkPostCode', title: '工作岗位', width: 100, sortable: true },
            { field: 'WorkLevel', title: '工种等级', width: 100, sortable: true },
            { field: 'CompleteRatio', title: '完成率', width: 100, sortable: true },
            { field: 'WorkCommentCode', title: '工作评议', width: 100,  sortable: true },
            { field: 'Ranking', title: '排名', width: 100,  sortable: true },
            { field: 'WorkDays', title: '出勤天数', width: 100,  sortable: true },
            { field: 'WorkRatio', title: '工分系数', width: 100,  sortable: true },
            { field: 'WorkMark', title: '出勤工分', width: 100,  sortable: true },
            { field: 'PerformanceMark', title: '绩效工分', width: 100,  sortable: true },
            { field: 'DutyDays', title: '值星天数', width: 100,  sortable: true },
            { field: 'DutyCommentsCode', title: '值星评议', width: 100,  sortable: true },
            { field: 'DutyRatio', title: '值星系数', width: 100,  sortable: true },
            { field: 'DutyMark', title: '值星工分', width: 100,  sortable: true },
            { field: 'BaseAmount', title: '基本劳酬', width: 100,  sortable: true },
            { field: 'ExtAmount', title: '专项劳酬', width: 100,  sortable: true },
            { field: 'DeductAmount', title: '扣罚金额', width: 100,  sortable: true },
            { field: 'SumAmount', title: '合计金额', width: 100,  sortable: true },
            { field: 'OverrunAmount', title: '超限金额', width: 100,  sortable: true },
            { field: 'Amount', title: '实发金额', width: 100,  sortable: true },
            { field: 'MerberPoints', title: '奖励积分', width: 100,  sortable: true },
            { field: 'DeductPoints', title: '扣罚积分', width: 100,  sortable: true },
            { field: 'SumPoints', title: '合计积分', width: 100,  sortable: true },
            { field: 'OverrunPoints', title: '超限积分', width: 100,  sortable: true },
            { field: 'Points', title: '实发积分', width: 100,  sortable: true },
            { field: 'OrderStatus', title: '状态', width: 100,  sortable: true },
            { field: 'Remark', title: '备注', width: 100,  sortable: true }
        ]],
        pagination: true,
        rownumbers: true
    });


}



//设置对公对私
function SetImportFlag(flag, id) {
    
    if (id !== '') {
        $.post("/JFBaochou/SetImportFlag", { "flag": flag, "id": id }, function (data, status) {
            if ("success" == status) {
                console.log(data);
                if (data.fflag == true) {
                    var idx = $('#test').datagrid('getRowIndex', id);
                    $('#test').datagrid('updateRow', {
                        index: idx,
                        row: data.DataInfo
                    });
                    alert(data.ReMsg);
                } else {
                    alert(data.ReMsg);
                }
                
            }
        });
    }
}

function SetAuditDiv(row) {
    if (row.ImportFlag == 1) {
        $("#divAuditRec").hide();
    } else {
        if ($("#userTypeId").val() == "1") {
            $("#addVchnum").val(row.VchNum);
            $("#divAuditRec").show();
            $('#addFCrimeName').textbox('setValue', '');
            $('#addFCrimeCode').numberbox('setValue', '');
        } else {
            $("#divAuditRec").hide();
        }
        
    }
}

//没有银行的记录审核入账
function btnAuditBankRec() {
    if ($('#addFCrimeCode').val() == "") {
        $.messager.alert("提示","罪犯编号不能为空");
        return false;
    }
    if ($('#addFCrimeName').val() == "") {
        $.messager.alert("提示","罪犯姓名不能为空");
        return false;
    }
    $('#ffBankRecAudit').form({
        url:"/JFBaochou/SetBankArtificialAddNotImportRec",
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


//网银退回个人账户
function btnReturnPersonalAccount() {
    if ($('#addFCrimeCode').val() == "") {
        $.messager.alert("提示","罪犯编号不能为空");
        return false;
    }
    if ($('#addFCrimeName').val() == "") {
        $.messager.alert("提示","罪犯姓名不能为空");
        return false;
    }
    if ($('#addFRemark').val() == "") {
        $.messager.alert("提示","备注信息不能为空");
        return false;
    }
    $('#ffBankRecAudit').form({
        url: "/BankRcv/SetListForReturnPersonalAccount",//设置为退回个人账户
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


//原路退回
function btnReturnToBack() {

    if ($('#addFRemark').val() == "") {
        $.messager.alert("提示","备注信息不能为空");
        return false;
    }
    $('#ffBankRecAudit').form({
        url: "/JFBaochou/SetListForReturnToBack",//设置为退回个人账户
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
                        Remark: bouns.Remark,
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

    $.post("/JFBaochou/GetListSumAmount", { "strJsonWhere": strJson }, function (data, status) {
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
    window.open("/JFBaochou/PrintSumList/?strJsonWhere=" +   strJson);
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
    window.open("/JFBaochou/PrintDetailList/?strJsonWhere=" + strJson);
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


    $.post("/JFBaochou/ExcelDetailList/", { "strJsonWhere": strJson}, function (data, status) {
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


//导入明细记录
function btnImportDetailOrder() {
    $('#ffDetail').form({
        url: "/JfBaochou/ImportExcelDetail",
        onSubmit: function () {
            // do some check
            // return false to prevent submit;
        },
        success: function (data) {
            alert(data.ReMsg)
        }
    });
    // submit the form
    $('#ffDetail').submit();

}


//月统计报表 期初 期间  期末
function btnExcelReportSave() {

    $.post("/JFBaochou/ExcelReportDetailList/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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


function btnPrintBankDateReport() {

    //$.post("/BankRcv/BankDateReport/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
    //    if (status != "success") {
    //        return false;
    //    } else {
    //        var words = data.split("|");
    //        if (words[0] == "OK") {
    //            window.open("/Upload/" + words[1]);
    //        }
    //    }
    //});

    window.open("/JFBaochou/BankDateReport/?startDate=" + $("#SearchCreateDate_Start").datetimebox('getValue'));
}


function btnPrintBankMonthReport() {
    window.open("/JFBaochou/BankMonthReport/?startDate=" + $("#SearchCreateDate_Start").datetimebox('getValue') + "&endDate="+ $("#SearchCreateDate_End").datetimebox('getValue'));
}

function btnPrintBankYearReport() {
    window.open("/JFBaochou/BankYearReport/?startDate=" + $("#SearchCreateDate_Start").datetimebox('getValue') + "&endDate=" + $("#SearchCreateDate_End").datetimebox('getValue'));
}


function btnExcelOutBankTransList() {
    $.post("/JFBaochou/ExcelOutBankTransList/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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

function btnExcelOutBankDuizhang() {
    $.post("/JFBaochou/ExcelOutBankDuizhang/", { "startTime": $("#SearchCreateDate_Start").datetimebox('getValue'), "endTime": $("#SearchCreateDate_End").datetimebox('getValue') }, function (data, status) {
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
        url: '/JFBaochou/ExcelInport',
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

    $.post("/JFBaochou/ExcelNoFindCashReport/", { "startTime": startTime, "endTime": endTime }, function (data, status) {
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

            $.post("/JFBaochou/btnCheckVchnum/", { "vchnum": vchnum }, function (data, status) {
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
    $.post("/JFBaochou/findFCrimeCode", { "fname": fname }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                $("#addFCrimeCode").textbox('setValue', data.DataInfo);
            } else {
                $("#addFCrimeCode").textbox('setValue', '');
                alert(data.ReMsg);
            }
        }
    });
}