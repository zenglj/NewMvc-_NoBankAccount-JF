$(function () {
    //jquery js 当文本框获得焦点时，自动选中里面的文字
    $(function () {
        $(":text").focus(function () {
            this.select();
        });
    });

    //加载并显示劳报明细列表
    $('#test').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 850,
        height: 280,
        queryParams: {
            fName: '',
            fCode: '00000',
            FAreaName: '',
            action: 'NewSystem'
        },
        fitColumns: true,
        nowrap: true,
        singleSelect: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/LingyongJin/getLaobaoDetailByBid',
        sortName: 'seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'seqno',
        pageSize: 10,
        pageList: [10,20,50, 100],
        onSelect: function (rowIndex, rowData) {
            var iRowId = rowIndex;
            //$('#test').datagrid('clearSelections'); //清除所有的选择项
            //$('#test').datagrid('selectRow', iRowId);
            //$('#test').datagrid('checkRow', iRowId);                     
        },
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '用户编号', field: 'FCrimeCode', width: 80, sortable: true }
        ]],
        columns: [[
        { field: 'seqno', title: 'seqno', width: 120, hidden: true },
        { field: 'FCriminal', title: '姓名', width: 120 },
        { field: 'FSex', title: '性别', width: 60 },
        {
            field: 'FAreaName', title: '队别名称', width: 220, sortable: true, //rowspan: 2,
            sorter: function (a, b) {
                return (a > b ? 1 : -1);
            }
        },
        //{ field: 'FBankAccNo', title: '银行卡号', width: 150 },
        //{ field: 'FAmountA', title: '存款账户', width: 150 },
        //{ field: 'FAmountB', title: '报酬账户', width: 150 },
        //{ field: 'FAmountC', title: '留存金额', width: 150 },
        { field: 'FAmount', title: '金额', width: 150, editor: 'numberbox'},        
        { field: 'Remark', title: '备注', width: 150 }
        ]],
        pagination: true,
        rownumbers: true,
        toolbar: [{
            id: 'btnDetailDel',
            text: '单条删除',
            iconCls: 'icon-cancel',
            handler: function () {
                var delRow = $("#test").datagrid('getSelected');
                var delIdx = $("#test").datagrid('getRowIndex', delRow);
                DelDetailList(delRow.seqno, delIdx);//删除一条明细记
            }
        }]
    });

    //获取DataGrid Table页数据
    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function () {
            alert('before refresh');
        }
    });

    //加载显示队别列表
    //$('#cc').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});


    //加载显示队别列表
    //$('#mainAreaName').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});


    //加载主单列表
    loadMainOrderTable();

});
function resize() {
    $('#test').datagrid('resize', {
        width: 700,
        height: 400
    });
}

function getSelected() {
    var selected = $('#test').datagrid('getSelected');
    if (selected) {
        alert(selected.code + ":" + selected.name + ":" + selected.addr + ":" + selected.col4);
    }
}

function getSelections() {
    var ids = [];
    var rows = $('#test').datagrid('getSelections');
    for (var i = 0; i < rows.length; i++) {
        ids.push(rows[i].code);
    }
    alert(ids.join(':'));
}
function clearSelections() {
    $('#test').datagrid('clearSelections');
}
function selectRow() {
    $('#test').datagrid('selectRow', 2);
}
function selectRecord() {
    $('#test').datagrid('selectRecord', '002');
}
function unselectRow() {
    $('#test').datagrid('unselectRow', 2);
}
function mergeCells() {
    $('#test').datagrid('mergeCells', {
        index: 2,
        field: 'addr',
        rowspan: 2,
        colspan: 2
    });
}
//清空查询条件
function clearSearch() {
    $("#crimeSearch input").val("");
}
//过滤查询
function FilterSearch() {
    $('#mainOrderTable').datagrid('load', {
        fCrimeName: $("#crimeSearch input[name=fCrimeName]").val(),
        FCrimeCode: $("#crimeSearch input[name=FCrimeCode]").val(),
        fRemark: $("#crimeSearch input[name=fRemark]").val(),
        FAreaName: $('#cc').combobox('getText'),
        startDate: $("#crimeSearch input[name=startDate]").val(),
        endDate: $("#crimeSearch input[name=endDate]").val(),
        action: 'GetSearchMainOrder'
    });
}

//删除一条明细记
function DelDetailList(seqno, idx) {
    $.post("/LingyongJin/DelOrderDetail", {  "seqno": seqno, "sbid": $("#sbid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split(".");
            if (words[0] == "OK") {
                //更新主单数量及金额
                var infos = words[1].split("|");
                UpdateMainOrderMoneyCount(infos[1], infos[2]);
                $("#test").datagrid('deleteRow', idx);
                $.messager.alert('提示', "删除成功");
                return false;
            } else {
                $.messager.alert('提示', data);
            }
        } else {
            alert("通信失败");
        }
    });
}


//确定提交主单
function SubmitMainOrder(bid) {
    $.post("/LingyongJin/PostMainOrder", { "sbid": bid }, function (data, status) {
        if (status == "success") {
            var words = data.split(".");
            if (words[0] == "OK") {
                //更新确认状态                                                
                UpdateMainOrderStatus("1");
                $.messager.alert('提示', "提交成功");
                return false;
            } else {
                $.messager.alert('提示', data);
            }
        } else {
            alert("通信失败");
        }
    });
}

//过账
function DbPostMainOrder(bid) {
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });
    $.post("/LingyongJin/DbInPostMainOrder", { "sbid": bid }, function (data, status) {
        if (status == "success") {
            $.messager.progress('close');
            var words = data.split(".");
            if (words[0] == "OK") {
                //更新确认状态                                                
                UpdateMainOrderStatus("1");
                $.messager.alert('提示', "提交成功");
                return false;
            } else {
                $.messager.alert('提示', data);
            }
        } else {
            $.messager.progress('close');
            alert("通信失败");
        }
    });
}

//删除主单
function DeleteMainOrder(bid, idx) {
    $.post("/LingyongJin/DelMainOrder", {"sbid": bid }, function (data, status) {
        if (status == "success") {
            //alert(data);
            var words = data.split(".");
            if (words[0] == "OK") {
                var rows = $("#test").datagrid('getRows');
                for (var i = rows.length - 1; i >= 0; i--) {
                    $("#test").datagrid('deleteRow', i);
                }
                $.messager.alert('提示', "删除成功");
                $("#mainOrderTable").datagrid('deleteRow', idx);

            } else {
                $.messager.alert('提示', "删除失败");
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}


//获取指定单的明细记录并显示在Test列表中
function GetOrderDetailToTable(bid) {
    $('#test').datagrid('load', {
        FBid: bid,
        action: 'GetOrderDetail'
    });
}

//设定明细框的编辑状态
function SetDetailTextBox(Flag) {
    //alert(Flag);
    if (Flag == "1") {
        $('#iCode').attr("disabled", "disabled");
        $('#iName').attr("disabled", "disabled");
        $('#iMoney').attr("disabled", "disabled");
        $('#btnDetailDel').linkbutton('disable');
        $('#btnsave').linkbutton('disable');
        //$('#btndel').linkbutton('disable'); 
        $('#btnsubmit').linkbutton('disable');
        $('#btnExcelImport').linkbutton('disable');
        $('#CreateAreaList').linkbutton('disable');
        $('#CreateAllList').linkbutton('disable');
        $('#SaveList').linkbutton('disable');

    } else {
        $('#iCode').removeAttr("disabled");
        $('#iName').removeAttr("disabled");
        $('#iMoney').removeAttr("disabled");
        $('#btnDetailDel').linkbutton('enable');
        $('#btnsave').linkbutton('enable');
        //$('#btndel').linkbutton('enable'); 
        $('#btnsubmit').linkbutton('enable');
        $('#btnExcelImport').linkbutton('enable');
        $('#CreateAreaList').linkbutton('enable');
        $('#CreateAllList').linkbutton('enable');
        $('#SaveList').linkbutton('enable');

    }
}

//主单审核的工具按钮状态
function SetMainToolBox(Flag) {
    //alert(Flag);
    if (Flag == "1") {
        $('#btndel').linkbutton('disable');
    } else {
        $('#btndel').linkbutton('enable');
    }
}
//加载主单列表函数
function loadMainOrderTable() {
    $('#mainOrderTable').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: 150,
        queryParams: {
            fName: '',
            fCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/LingyongJin/getBonus',
        sortName: 'PId',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'PId',
        //                pageSize: 50,
        //                pageList: [50, 100],
        onSelect: function (rowIndex, rowData) {
            //var iRowId = rowIndex;
            //$('#test').datagrid('clearSelections'); //清除所有的选择项
            //$('#mainOrderTable').datagrid('selectRow', rowIndex);
            $("#sbid").val(rowData.PId);
            $("#sOrderStatus").val(rowData.FCheckFlag);
            SetDetailTextBox(rowData.FCheckFlag);//设定明细框的编辑状态                    
            SetMainToolBox(rowData.AuditFlag);//主单审核的工具按钮状态
            GetOrderDetailToTable(rowData.PId);//获取指定单的明细记录并显示在Test列表中
            $("#excelFileName").val("");//清空所选文件名
        },
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'PId', width: 100, sortable: true }
        ]],
        columns: [[//DataGrid表格数据列
        {
            field: 'PDate', title: '月份', width: 100, sortable: true, formatter: function (value, row, index) {
                if (row.PDate != null) {
                    if (row.PDate != "") {
                        nowyear = getShortTime(value);
                        var years = nowyear.split('-');
                        return years[0] + "年" + years[1] + "月";
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }
        },
        {
            field: 'ApplyBy', title: '申请人', width: 100, sortable: true, //rowspan: 2,
            sorter: function (a, b) {
                return (a > b ? 1 : -1);
            }
        },
        {
            field: 'PFlag', title: '确认', width: 60, formatter: function (value, row, index) {
                if (row.PFlag == "1") {
                    return "√";
                } else {
                    return "";
                }
            }
        },
        { field: 'CheckBy', title: '过账人', width: 100 },
        {
            field: 'Flag', title: '过账', width: 60, formatter: function (value, row, index) {
                if (row.Flag == "1") {
                    return "√";
                } else {
                    return "";
                }
            }
        },

        { field: 'FAreaName', title: '所属监区', width: 150, sortable: true },
        { field: 'FManNb', title: '发放数量', width: 80 },
        { field: 'FWomNb', title: '女性数量', width: 80 ,hidden:true},
        { field: 'FManAmount', title: '发放标准', width: 80 },
        { field: 'FWomAmount', title: '女性标准', width: 80, hidden: true },
        { field: 'FAmount', title: '总余额', width: 100 },
        { field: 'CrtBy', title: '创建人', width: 100, sortable: true },
        {
            field: 'CrtDt', title: '创建日期', width: 100, sortable: true, formatter: function (value, row, index) {
        if (row.CrtDt != null) {
            if (row.CrtDt != "") {
                return getLocalTime(value);
            } else {
                return value;
                }
            } else {
                return value;
            }
        }

        },
        { field: 'Remark', title: '备注', width: 150 }
        ]],
        //pagination: true,
        //rownumbers: true,
        toolbar: [{
            id: 'btnadd',
            text: '增加主单',
            iconCls: 'icon-add',
            handler: function () {
                $("#mainAreaName").combobox('setValue', "");
                var dt = new Date();
                $("#syear").numberspinner('setValue', dt.getFullYear());
                $("#smonth").numberspinner('setValue', dt.getMonth() + 1);
                $("#MainDoType").val("Add");
                $('#btnsave').linkbutton('enable');
            }
        }, '-', {
            id: 'btnsave',
            text: '保存主单',
            iconCls: 'icon-save',
            handler: function () {
                if ($("#sapplyby").val() == "") {
                    $.messager.alert('提示', '申请人不能为空');
                    return false;
                }
                if ($("#MainDoType").val() == "Add") {
                    $("#MainAction").val("AddMainOrder");
                    $("#sAreaName").val($("#mainAreaName").combobox('getText'));
                    $("#sAreaCode").val($("#mainAreaName").combobox('getValue'));
                    $('#ffMainOrder').form({
                        url: "/LingyongJin/AddMainOrder",
                        onSubmit: function () {
                        },
                        success: function (data) {
                            var words = data.split('|');
                            if (words[0] == "OK") {
                                var infos = $.parseJSON(words[1]);
                                $('#sbid').val(infos.PId);
                                $('#mainOrderTable').datagrid('insertRow', {
                                    index: 0,
                                    row: {
                                        PId: infos.PId,
                                        PDate: infos.PDate,
                                        ApplyBy: infos.ApplyBy,
                                        PFlag: "0",
                                        CheckBy: "",
                                        Flag: "0",
                                        FAreaName: $("#mainAreaName").datetimebox('getText'),
                                        FManNb: "0",
                                        FWomNb: "0",
                                        FManAmount: "0",
                                        FWomAmount: "0",
                                        FAmount: "0",
                                        CrtBy: infos.CrtBy,
                                        CrtDt: infos.CrtDt,
                                        Reamrk: $("#sRemark").val()
                                    }
                                });
                                
                                //$('#mainOrderTable').datagrid('checkRow', 0);
                                $('#mainOrderTable').datagrid('selectRow', 0);
                                $("#sapplyby").val('');
                                $("#MainDoType").val('');
                                $('#btnsave').linkbutton('disable');
                                $.messager.alert('提示', '保存成功');
                            } else {
                                $('#sbid').val("");
                                $.messager.alert('提示', data);
                            }
                        }
                    });
                    $('#MainDoType').submit();
                } else {
                    $.messager.alert('提示', '请点增加，才填写主单');
                }
            }
        }, '-', {
            id: 'btnsubmit',
            text: '确认提交',
            iconCls: 'icon-ok',
            handler: function () {
                var okRow = $("#mainOrderTable").datagrid('getSelected');
                SubmitMainOrder(okRow.PId);//确认提交主单
            }
        }, '-', {
            id: 'btndbpost',
            text: '过账',
            iconCls: 'icon-ok',
            handler: function () {
                var okRow = $("#mainOrderTable").datagrid('getSelected');
                DbPostMainOrder(okRow.PId);//确认提交主单
            }
        }, '-', {
            id: 'btndel',
            text: '删除主单',
            iconCls: 'icon-cancel',
            handler: function () {
                $.messager.confirm('提示', '您真的删除该主单吗?', function (r) {
                    if (r) {
                        var delRow = $("#mainOrderTable").datagrid('getSelected');
                        if (delRow.auditflag == 1) {
                            $.messager.alert('提示', '主单已经审核不能删除!');
                            return false;
                        } else {
                            var idx = $("#mainOrderTable").datagrid('getRowIndex', delRow);
                            DeleteMainOrder(delRow.PId, idx);//删除主单
                        }
                    }
                });
            }
        }, '-', {
            id: 'btnprint',
            text: '打印列表',
            iconCls: 'icon-print',
            handler: function () {
                //$('#btnprint').linkbutton('enable');
                var row = $("#mainOrderTable").datagrid("getSelected");
                window.open("/LingyongJin/PrintReportList?bid=" + row.PId);
            }
        }, '-', {
            id: 'btnprint_bank',
            text: '导出Excel',
            iconCls: 'icon-redo',
            handler: function () {
                var row = $("#mainOrderTable").datagrid("getSelected");
                $("#action").val("ExcelOut");
                $("#FBidExcel").val(row.PId);
                $('#ffExcel').form({
                    url: "/LingyongJin/ExcelOut",
                    onSubmit: function () {
                        
                    },
                    success: function (data) {
                        var words = data.split("|");
                        if (words[0] == "OK") {
                            window.open("/Upload/" + words[1]);
                        }
                    }
                });
                $('#ffExcel').submit();
            }
        }, '-', {
            id: 'btnUndo_bank',
            text: '财务退账',
            iconCls: 'icon-undo',
            handler: function () {
                $.messager.confirm('提示', '您真要对该主单进行退账操作吗?', function (r) {
                    if (r) {
                        var delRow = $("#mainOrderTable").datagrid('getSelected');
                        $.post("/LingyongJin/CancalOrderById", {
                            "PId": delRow.PId
                        }, function (data, status) {
                            if ("success" != status) {
                                return false;
                            } else {
                                //$.messager.alert('提示', data); 
                                var words = data.split(".");
                                if (words[0] == "OK") {
                                    $.messager.alert("提示", "已成功退账");
                                    
                                } else {
                                    $.messager.alert("提示", data);
                                }
                            }
                        });
                    }
                });
            }
        }]
    });
}

//狱号框内按回车
function noNumbers() {
    var code = $("#iCode").val();
    //var name=$("#iName").val();
    if ("" == code) {
        //console.info($("#InCY").val());
        //$.messager.alert('提示', '请输入相应的编号!'); 
        $("#lblInfo").text('请输入相应的编号!');
        $("#iCode").focus();
    } else {
        $.post("/LingyongJin/QueryCrimeCode", {
            "FCode": code
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split(".");
                if (words[0] == "OK") {
                    var flieds = words[1].split("|");
                    if (flieds[2] == 1) {
                        //$.messager.alert('提示', "该犯人已经离监结算了，不能再存!");
                        $("#lblInfo").text('该犯人已经离监结算了，不能再存!');
                        $("#iCode").focus();
                    } else {
                        if (flieds[3] == 1) {
                            $("#iName").val(flieds[1]);
                            $("#iCpct").val(flieds[4]);
                            $("#lblInfo").text('');
                            $("#iMoney").focus();
                        } else {
                            $.messager.alert('提示', flieds[4]);
                        }
                    }
                } else {
                    //$.messager.alert('提示', "对不起,您输入的狱号不存在!");
                    $("#lblInfo").text('对不起,您输入的狱号不存在!');
                    $("#iCode").focus();
                }
            }
        });
    }
}
//金额框内按回车，可以直接发放钱
function noMoneyEnter() {
    
    if ($("#sOrderStatus").val() == "1") {
        $.messager.alert('提示', '主单已经确认,不可以再编辑!');
        return false;
    }
    if ($("#iCode").val() == "") {
        //$.messager.alert('提示', '请输入相应的编号!');

        //$("#lblInfo").text('请输入相应的编号!');
        $("#iCode").focus();
        return false;
    }
    if (isNaN($("#iMoney").val())) {
        $("#lblInfo").text('金额必须是数值的!');
        $("#iMoney").focus();
        return false;
    } else {
    }
    if ($("#iName").val() == "") {
        //$.messager.alert('提示', '请输入相应的姓名!');
        $("#lblInfo").text('请输入相应的姓名!');
        $("#iName").focus();
        return false;
    }
    if ($("#iMoney").val() == "") {
        //$.messager.alert('提示', '请输入相应的金额!');
        $("#lblInfo").text('请输入相应的金额!');
        $("#iMoney").focus();
        return false;
    }
    if ($("#iCpct").val() == "") {
        //$.messager.alert('提示', '处遇信息不正确!');
        $("#lblInfo").text('处遇信息不正确!');
        $("#iCode").focus();
        return false;
    }
    var code = $("#iCode").val();
    //var name=$("#iName").val();

    if ("" == code) {
        //console.info($("#InCY").val());
        //$.messager.alert('提示', '请输入相应的编号!');
        $("#lblInfo").text('请输入相应的编号!');
        $("#iCode").focus();
    } else {
        $("#btnDetailSave").linkbutton('disable');
        $("#iMoney").val(toDecimal2($("#iMoney").val()));
        $.post("/LingyongJin/AddOrderDetail", {
            "FCode": code, "FName": $("#iName").val(), "FMoney": $("#iMoney").val(), "FBid": $("#sbid").val()
        }, function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                //$.messager.alert('提示', data); 
                var words = data.split(".");
                if (words[0] == "OK") {
                    words[1] = data.substring(3);
                    var infos = words[1].split("|");
                    //增加一条记录
                    $('#test').datagrid('appendRow', {
                        FCrimeCode: infos[0],
                        FCriminal: infos[1],
                        FAreaName: infos[2],
                        FAmount: infos[3],
                        FSex: infos[4],
                        Remark: infos[5]
                    });

                    //$('#test').datagrid('selectRow',0);
                    //更新主单数量及金额
                    UpdateMainOrderMoneyCount(infos[6],0, infos[7]);
                    $("#iCode").val("");
                    $("#iName").val("");
                    //$("iMoney").val("");
                    $("#iCode").focus();
                    $("#iCode").val("");
                } else {
                    //$.messager.alert('提示', words[1]);
                    $("#lblInfo").text(words[1]);
                    $("#iCode").focus();
                }
                $("#btnDetailSave").linkbutton('enable');
            }
        });

        
    }
}

//Excel文件导入
function ExcelFileInport() {

    if ($("#excelFileName").val() == "") {
        $.messager.alert('提示', "请选择一个Excel文件!");
        return false;
    } else {
        $.messager.progress({
            title: '导入数可能需要几分钟，请耐心等待',
            msg: '数据正在导入中...'
        });
        var row = $('#mainOrderTable').datagrid("getSelected");
        if (row) {
            $('#excelBid').val(row.PId);//设定主单号Bid
            $('#excelAction').val("ExcelInport");//设定action动作

            $('#ffDetailEditBoxExcel').form({
                url: "/LingyongJin/ExcelInport",
                onSubmit: function () {
                    // do some check    
                    // return false to prevent submit;    
                },
                success: function (data) {
                    $.messager.progress('close');
                    //alert(data);
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        var rtn = $.parseJSON(words[2]);
                        //更新主单的金额及数量
                        var mainRow = $('#mainOrderTable').datagrid('getSelected');
                        var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
                        $('#mainOrderTable').datagrid('updateRow', {
                            index: idx,
                            row: {
                                FManNb: rtn.provide.FManNb,
                                FManAmount: rtn.provide.FManAmount,
                                FWomNb: rtn.provide.FWomNb,
                                FWomAmount: rtn.provide.FWomAmount,
                                FAmount: rtn.provide.FAmount
                            }
                        });
                        //插入明细记录
                        for (var i = 0; i < rtn.dtls.length; i++) {
                            var dtl = rtn.dtls[i];
                            $('#test').datagrid('appendRow', {
                                FCrimeCode: rtn.dtls[i].FCrimeCode,
                                seqno: rtn.dtls[i].seqno,
                                FCriminal: rtn.dtls[i].FCriminal,
                                FAreaName: rtn.dtls[i].FAreaName,
                                FAmount: rtn.dtls[i].FAmount,
                                FSex: rtn.dtls[i].FSex
                                //AmountC: rtn.dtls[i].AmountC,
                                //Remark: rtn.dtls[i].remark
                            });


                        }
                        $.messager.alert('提示', words[1]);
                    }
                }
            });
            $('#ffDetailEditBoxExcel').submit();
        } else {
            $.messager.alert('提示', "请选择一个主单!");
        }
    }
}

//批量生成全监零用金记录
function CreateAllList() {
    $.post("/LingyongJin/CreateAllList", { "sbid": $("#sbid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //$.messager.alert('提示', "删除成功");
                var lists = $.parseJSON(words[1]);
                if (lists.length > 0) {
                    for (var i = 0; i < lists.length; i++) {
                        var list = lists[i];
                        //增加一条记录
                        $('#test').datagrid('appendRow', {
                            FCrimeCode: list.FCrimeCode,
                            FCriminal: list.FCriminal,
                            FAreaName: list.FAreaName,
                            FAmount: list.FAmount,
                            FSex: list.FSex,
                            Remark: list.Remark
                        });
                    }
                    var provide = $.parseJSON(words[2]);
                    var row = $("#mainOrderTable").datagrid('getSelected');
                    var idx = $("#mainOrderTable").datagrid('getRowIndex', row);
                    $('#mainOrderTable').datagrid('updateRow', {
                        index: idx,
                        row: {
                            FManNb: provide.FManNb,
                            FWomNb: provide.FWomNb,
                            FAmount: provide.FAmount
                        }
                    });

                }
                //$("#mainOrderTable").datagrid('deleteRow', idx);

            } else {
                $.messager.alert('提示', words[1]);
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}

//批量生成劳报记录
function CreateAreaList() {
    $.post("/LingyongJin/CreateAreaList", { "sbid": $("#sbid").val() }, function (data, status) {
        if (status == "success") {
            var words = data.split("|");
            if (words[0] == "OK") {
                //$.messager.alert('提示', "删除成功");
                var lists = $.parseJSON(words[1]);
                if (lists.length > 0) {
                    for (var i = 0; i < lists.length; i++) {
                        var list = lists[i];
                        //增加一条记录
                        $('#test').datagrid('appendRow', {
                            FCrimeCode: list.FCrimeCode,
                            FCriminal: list.FCriminal,
                            FAreaName: list.FAreaName,
                            FAmount: list.FAmount,
                            FSex: list.FSex,
                            Remark: list.Remark
                        });
                    }
                    var provide = $.parseJSON(words[2]);
                    var row = $("#mainOrderTable").datagrid('getSelected');
                    var idx = $("#mainOrderTable").datagrid('getRowIndex', row);
                    $('#mainOrderTable').datagrid('updateRow', {
                        index: idx,
                        row: {
                            FManNb: provide.FManNb,
                            FWomNb: provide.FWomNb,
                            FAmount: provide.FAmount
                        }
                    });

                }
                //$("#mainOrderTable").datagrid('deleteRow', idx);

            } else {
                $.messager.alert('提示', words[1]);
            }
        } else {
            $.messager.alert('提示', "通信失败");
        }
    });
}

var $dg = $("#test");

//批量保存数据
function BatchSaveDg() {
    endEdit();
    if ($dg.datagrid('getChanges').length) {
        var inserted = $dg.datagrid('getChanges', "inserted");
        var deleted = $dg.datagrid('getChanges', "deleted");
        var updated = $dg.datagrid('getChanges', "updated");

        var effectRow = new Object();
        if (inserted.length) {
            effectRow["inserted"] = JSON.stringify(inserted);
        }
        if (deleted.length) {
            effectRow["deleted"] = JSON.stringify(deleted);
        }
        if (updated.length) {
            effectRow["updated"] = JSON.stringify(updated);
        }

        $.post("/LingyongJin/BatchSaveDtlList", effectRow,
            function (data, status) {
                if ("success" != status) {
                    return false;
                }
                else {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        var model = $.parseJSON(words[1]);
                        var row = $("#mainOrderTable").datagrid('getSelected');
                        var idx = $("#mainOrderTable").datagrid('getRowIndex', row);
                        $dg.datagrid('acceptChanges');
                        $('#mainOrderTable').datagrid('updateRow', {
                            index: idx,
                            row: {
                                FManNb: model.FManNb,
                                FWomNb: model.FWomNb,
                                FAmount: model.FAmount
                            }
                        });

                        
                        
                        $.messager.alert("提示", "保存成功！");
                    }
                    else {
                        $dg.datagrid('rejectChanges');
                        $.messager.alert("提示", data);
                    }
                }
            });
    }
}

//结束编辑
function endEdit() {
    var rows = $dg.datagrid('getRows');
    for (var i = 0; i < rows.length; i++) {
        $dg.datagrid('endEdit', i);
    }
}

//导出Excel导入的失败记录
function ErrorListExcelOut() {
    var row = $('#mainOrderTable').datagrid("getSelected");
    if (row) {
        $('#excelBid').val(row.PId);//设定主单号Bid
        $('#excelAction').val("ErrorListOutport");//设定action动作

        $('#ffDetailEditBoxExcel').form({
            url: "/LingyongJin/ErrorListOutport",
            onSubmit: function () {
                
            },
            success: function (data) {
                //alert(data)
                var words = data.split("|");
                if (words[0] == "OK") {
                    window.open("/Upload/" + words[1]);
                }
            }
        });
        $('#ffDetailEditBoxExcel').submit();
    } else {
        $.messager.alert('提示', "请选择一个主单!");
    }
}

//更新主单数量及金额
function UpdateMainOrderMoneyCount(FManNb, FWomNb, FAmount) {
    var mainRow = $('#mainOrderTable').datagrid('getSelected');
    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);

    $('#mainOrderTable').datagrid('updateRow', {
        index: idx,
        row: {
            FManNb: FManNb,
            FWomNb:FWomNb,
            FAmount: FAmount
        }
    });
}

//更新主单确认状态
function UpdateMainOrderStatus(PFlag) {
    var mainRow = $('#mainOrderTable').datagrid('getSelected');
    var idx = $('#mainOrderTable').datagrid('getRowIndex', mainRow);
    $('#mainOrderTable').datagrid('updateRow', {
        index: idx,
        row: {
            PFlag: PFlag
        }
    });
}

//扩展Editer
$.extend($.fn.datagrid.methods, {
    editCell: function (jq, param) {
        return jq.each(function () {
            var opts = $(this).datagrid('options');
            var fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields'));
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor1 = col.editor;
                if (fields[i] != param.field) {
                    col.editor = null;
                }
            }
            $(this).datagrid('beginEdit', param.index);
            var ed = $(this).datagrid('getEditor', param);
            if (ed) {
                if ($(ed.target).hasClass('textbox-f')) {
                    $(ed.target).textbox('textbox').focus();
                } else {
                    $(ed.target).focus();
                }
            }
            for (var i = 0; i < fields.length; i++) {
                var col = $(this).datagrid('getColumnOption', fields[i]);
                col.editor = col.editor1;
            }
        });
    },
    enableCellEditing: function (jq) {
        return jq.each(function () {
            var dg = $(this);
            var opts = dg.datagrid('options');
            opts.oldOnClickCell = opts.onClickCell;
            opts.onClickCell = function (index, field) {
                if (opts.editIndex != undefined) {
                    if (dg.datagrid('validateRow', opts.editIndex)) {
                        dg.datagrid('endEdit', opts.editIndex);
                        opts.editIndex = undefined;
                    } else {
                        return;
                    }
                }
                dg.datagrid('selectRow', index).datagrid('editCell', {
                    index: index,
                    field: field
                });
                opts.editIndex = index;
                opts.oldOnClickCell.call(this, index, field);
            }
        });
    }
});

$(function () {
    $('#test').datagrid().datagrid('enableCellEditing');
})