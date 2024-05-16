//====================================================


$(function () {
    loadPayGrid();//显示银行转账记录

    $("#paySearchModDate_Start").datetimebox('clear');
    $("#paySearchModDate_End").datetimebox('clear');

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
        url: '/Family/GetMainDataGridList/',
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
            { field: 'FCrimeCode', title: '狱政编号', sortable: true, width: 100 },
            { field: 'FName', title: '罪犯姓名', sortable: true, width: 100 },
            { field: 'FAreaCode', title: '队别编号', sortable: true,hidden:true, width: 100 },
            { field: 'FAreaName', title: '所在队别', sortable: true, width: 100 },
            { field: 'FamilyName', title: '家属姓名', sortable: true, width: 100 },
            { field: 'FSex', title: '性别', sortable: true, width: 50 },
            { field: 'FIdenNo', title: '身份证号', sortable: true, width: 100 },
            { field: 'FamilyName', title: '家属姓名', sortable: true, width: 100 },            
            { field: 'Relation', title: '家属关系', sortable: true, width: 100 },
            { field: 'PhoneNum', title: '手机号码', sortable: true, width: 100 },
            { field: 'FAddress', title: '地址', sortable: true, width: 100 },
            { field: 'UserAuthCode', title: '授权码', sortable: true, width: 100 },
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
            { field: 'BankCard', title: '银行卡', sortable: true, width: 100 },            
            { field: 'OpeningBank', title: '开户行', sortable: true, width: 100 },
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
        $.post("/Family/GetCriminalInfo", { "fcode": $("#editWinFCrimeCode").textbox('getValue') }, function (data, status) {
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


    $("#editWinId").textbox('setValue', row.Id);
    $("#editWinFCrimeCode").textbox('setValue', row.FCrimeCode);
    $("#editWinFName").textbox('setValue', row.FName);
    $("#editWinFamilyName").textbox('setValue', row.FamilyName);
    $("#editWinFIdenNo").textbox('setValue', row.FIdenNo);
    $("#editWinFSex").combobox('setValue', row.FSex);
    $("#editWinRelation").combobox('setValue', row.Relation);
    $("#editWinPhoneNum").textbox('setValue', row.PhoneNum);
    $("#editWinUserAuthCode").textbox('setValue', row.UserAuthCode);
    $("#editWinFAddress").textbox('setValue', row.FAddress);
    $("#editWinBankCard").textbox('setValue', row.BankCard);
    $("#editWinOpeningBank").textbox('setValue', row.OpeningBank);
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
            $.post("/Family/DeleteMainRec", { "id": row.Id }, function (data, status) {
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


//Excel导入
function ExcelInport() {
    $('#ff').form({
        url:"/Family/ExcelInport",
        onSubmit: function () {
            // do some check   
            // return false to prevent submit;   
        },
        success: function (data) {
            var rs = $.parseJSON(data);
            if (rs.Flag == true) {
                alert(rs.ReMsg)
            } else {
                alert(rs.ReMsg);
                window.open("/Upload/" + rs.DataInfo);
            }
            
        }
    });
    // submit the form   
    $('#ff').submit();
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

    //if ($("#editWinId").textbox('getValue') != "") {
    //    if ($("#editWinOrderStatus").combobox('getValue') == "0") {
    //        $.messager.alert("提示", "请选择一种处理方式");
    //        return false;
    //    }
    //    if ($("#editWinResultDesc").textbox('getValue') == "") {
    //        $.messager.alert("提示", "处理结果描述不能为空");
    //        return false;
    //    }
    //}

    if (!($("#editWinFCrimeCode").textbox('getValue').length != 11 || $("#editWinFCrimeCode").textbox('getValue').length != 10)) {
        $.messager.alert("提示", "请输入有效的狱政编号");
        return false;
    }

    if ($("#editWinFamilyName").textbox('getValue') =="" ) {
        $.messager.alert("提示", "家属姓名不能为空");
        return false;
    }

    if ($("#editWinFSex").numberbox('getValue') == "") {
        $.messager.alert("提示", "请选择家属性别");
        return false;
    }


    var idenNo = $("#editWinFIdenNo").textbox('getValue');
    if (!(idenNo.length == 18 || idenNo.length >= 8 && idenNo.length <= 12)) {
        $.messager.alert("提示", "证件长度不正确");
        return false;
    }

    if (idenNo.length == 18 && !isValidDateFormat(idenNo.substr(6, 8))) {
        console.log(idenNo.substr(6, 8));
        $.messager.alert("提示", "请输入有效的身份证号");
        return false;
    }
        

    if ($("#editWinRelation").numberbox('getValue') == "") {
        $.messager.alert("提示", "请选择家属关系");
        return false;
    }
    //if ($("#editWinAmount").numberbox('getValue') == "" || $("#editWinAmount").numberbox('getValue') <= 0) {
    //    $.messager.alert("提示", "请输入金额");
    //    return false;
    //}

    //if ($("#editWinCauseDesc").textbox('getValue') == "" ) {
    //    $.messager.alert("提示", "请输入滞留原因");
    //    return false;
    //}


    $.post("/Family/CheckIdennoExists", { "fidenno": idenNo }, function (data, status) {
        if ("success" == status) {
            if (data.Flag == true) {
                //判断是否继续保存
                $.messager.confirm('提示', '该身份证号已经存在，是否继续?', function (r) {
                    if (r) {
                        SaveFamilyFunction();
                    }
                });
            }
            else {
                SaveFamilyFunction();
            }
        }
    });


    //保存家属信息
    function SaveFamilyFunction() {
        $.messager.confirm('提示', '确认开始保存家属信息吗?', function(r) {
            if (r) {
                var strJsonWhere = GetSearchJson('createPayOrder');
                $.post("/Family/SaveMainRec", { "strJsonWhere": strJsonWhere }, function(data, status) {
                    if ("success" == status) {
                        if (data.Flag == true) {
                            if ($("#editWinId").textbox('getValue') != "") {
                                UpdateDataGridSelectRow('tbPay', data.DataInfo);
                            }
                            else {
                                $('#tbPay').datagrid('appendRow', data.DataInfo); //插入一行
                            }

                            ClearFormSearch('createPayOrder');
                            $('#winCustList').window('close'); //关闭窗口
                            $.messager.alert("提示", "保存成功");
                        } else {
                            $.messager.alert("提示", data.ReMsg);
                        }
                    }
                });
            }
        });
    }
}


//生产授权码
function CreateAuthCode() {
    var idenNo = $("#editWinFIdenNo").textbox('getValue');
    if (!(idenNo.length == 18 || idenNo.length >= 8 && idenNo.length <= 12)) {
        $.messager.alert("提示", "证件长度不正确");
        return false;
    }

    var code = Math.round((Math.random() * 10000 - 1 + 1) + 1);
    //var subCode = idenNo.substr(12, 6);
    var subCode = idenNo.substr(idenNo.length - 5, 6);
    $("#editWinUserAuthCode").textbox('setValue', code + "" + subCode);
}

//============打印报表====================================

//机对账报表
function btnPrintDetailReport(mode) {
    var strJson = GetSearchJson('formPaySearch');
    window.open("/Family/PrintDetailReport/?strJsonWhere=" + strJson);
}

//身份证重复记录的报表
function btnPrintRepeatDetailReport(mode) {
    var strJson = GetSearchJson('formPaySearch');
    window.open("/Family/PrintRepeatDetailReport/?strJsonWhere=" + strJson);
}

function btnExcelDetailReport(mode) {
    var strJson = GetSearchJson('formPaySearch');

    $.post("/Family/ExcelDetailReport", { "strJsonWhere": strJson }, function (data, status) {
        if (data.Flag == true) {
            window.open("/Upload/" + data.DataInfo);
        }
    }
    );
}



function validateDate(date) {
    // 定义日期的正则表达式模式
    var pattern = /^\d{4}-\d{2}-\d{2}$/;

    if (pattern.test(date)) {
        return true;
    } else {
        return false;
    }
}


function formatDate(str) {
    var year = str.substr(0, 4);
    var month = str.substr(4, 2);
    var day = str.substr(6, 2);
    return year + "-" + month + "-" + day;
}

function isValidDateFormat(dateString) {
    var regExp = /^\d{8}$/; // 只接受8位数字的字符串作为有效的日期格式
    console.log(dateString);
    if (!regExp.test(dateString)) {
        return false; // 如果不符合要求的格式，则返回false
    } else {
        var year = dateString.substring(0, 4);
        console.log(year);
        var month = dateString.substring(4, 6); 
        console.log(month);
        var day = dateString.substring(6, 8);
        console.log(day);

        var newStrDate = year + "-" + month + "-" + day;
        return isValidDateFormatSub(newStrDate);
    }
}


function isValidDateFormatSub(dateString) {
    var regExp = /^\d{4}-\d{2}-\d{2}$/; // 定义日期格式的正则表达式

    if (regExp.test(dateString)) {
        var dateObj = new Date(dateString);

        return !isNaN(dateObj.getTime()); // 判断日期对象是否有效
    } else {
        return false; // 如果不符合指定的日期格式，返回false
    }
}