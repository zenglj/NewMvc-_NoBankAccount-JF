
$(function() {
        $('#test').datagrid({
            title: '收付列表',
            iconCls: 'icon-save',
            //width: 900,
            height: $(window).height() * 0.8,
            queryParams: {
                startDate: $("#crimeSearch input[name=fStartDate]").val(),
                endDate: $("#crimeSearch input[name=fEndDate]").val(),
                action: 'GetList',
                rcvpay: 'all'
            },
            fitColumns: true,
            nowrap: true,
            autoRowHeight: false,
            striped: true,
            singleSelect:true,
            collapsible: true,
            url: '/BankRpt/GetEdiBankSumList',
            remoteSort: false,
            idField: 'MainSeqno',
            pageSize: 50,
            pageList: [50, 100],
            onClickRow: function(rowIndex, rowData) {
                var iRowId = rowIndex;
                $('#test').datagrid('clearSelections'); //清除所有的选择项
                $('#test').datagrid('selectRow', iRowId);
                $('#test').datagrid('checkRow', iRowId);
                if ($("#selMainSeqno").val() != rowData.MainSeqno) {
                    $("#selMainSeqno").val(rowData.MainSeqno);
                    DisplayOrderList(rowData.MainSeqno);
                }

            },
            toolbar: [{
                iconCls: 'icon-print',
                text:'Excel导出',
                handler: function(){
                    //FilterSearch('ExcelOutport');
                    $('#ff').submit();  
                }
            }, {
                iconCls: 'icon-print',
                text: '打印报表',
                handler: function () {
                    //FilterSearch('ExcelOutport');
                    PrintReport("");
                }
            }
            ],
            frozenColumns: [[
	        { field: 'ck', checkbox: true },
	        { title: '主单号', field: 'MainSeqno', width: 80, sortable: true }

            ]],
            columns: [[
            { title: '说明', field: 'Remark', width: 200, sortable: true },
            {
                title: '日期', field: 'CrtDate', width: 80, sortable: true
            , formatter: function (value, row, index) {
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
	        {
	            title: '上传时间', field: 'UpLoadDate', width: 80, sortable: true, formatter: function (value, row, index) {
	                if (row.UpLoadDate != null) {
	                    if (row.UpLoadDate != "") {
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
                title: '下载时间', field: 'DetailDownLoadDate', width: 80, sortable: true
            , formatter: function (value, row, index) {
                if (row.DetailDownLoadDate != null) {
                    if (row.DetailDownLoadDate != "") {
                        return getLocalTime(value);
                    } else {
                        return value;
                    }
                } else {
                    return value;
                }
            }
            },
	        { title: '状态', field: 'SuccFlag', width: 80, sortable: true },
            { title: '代发总金额', field: 'DfMoney', width: 80, sortable: true },
            { title: '代发成功', field: 'DfSuccMoney', width: 80, sortable: true },
            { title: '失败应退', field: 'DfFailMoney', width: 80, sortable: true },
            { title: '代收消费款', field: 'DsSuccMoney', width: 80, sortable: true },
            { title: '未处理金额', field: 'NodoMoney', width: 80, sortable: true },
	        { title: '复位标志', field: 'ResetFlag', width: 80, sortable: true }
            ]],
            pagination: true,
            rownumbers: true                    
        });
        var p = $('#test').datagrid('getPager');
        $(p).pagination({
            onBeforeRefresh: function() {
                alert('before refresh');
            }
        });





    
});


//显示明细记录

function DisplayOrderList(mainSeqno) {
    //列明细列表
    $('#orderList').datagrid({
        title: '订单列表',
        iconCls: 'icon-save',
        //width: 900,
        height: $(window).height() * 0.8,
        //queryParams: {
        //    startDate: $("#crimeSearch input[name=fStartDate]").val(),
        //    endDate: $("#crimeSearch input[name=fEndDate]").val(),
        //    action: 'GetList',
        //    rcvpay: 'all'
        //},
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        singleSelect: true,
        collapsible: true,
        url: '/BankRpt/GetListByMainSeqno?mainSeqno=' + mainSeqno,
        remoteSort: false,
        idField: 'ID',
        pageSize: 50,
        pageList: [50, 100],
        //onClickRow: function (rowIndex, rowData) {
        //    var iRowId = rowIndex;
        //    $('#test').datagrid('clearSelections'); //清除所有的选择项
        //    $('#test').datagrid('selectRow', iRowId);
        //    $('#test').datagrid('checkRow', iRowId);
        //},
        toolbar: [{
            iconCls: 'icon-print',
            text: 'Excel导出',
            handler: function () {
                //FilterSearch('ExcelOutport');
                var row = $('#test').datagrid('getSelected');
                if (row == null) {
                    return false;
                }
                $.post("/BankRpt/GetMainList", { "mainSeqno": row.MainSeqno }, function (data, status) {
                    if (status == "success") {
                        var words = data.split("|");
                        if (words[0] == "OK") {
                            window.open("/Upload/" + words[1]);
                        }
                    }
                });                
            }
        }],
        frozenColumns: [[
        { field: 'ck', checkbox: true },
        { title: '主单号', field: 'MainSeqno', hidden: true, width: 80, sortable: true }

        ]],
        columns: [[
        { title: '姓名', field: 'FCriminal', width: 80, sortable: true },
        { title: '编号', field: 'FCRIMECODE', width: 80, sortable: true },
        { title: '类型', field: 'Dtype', width: 80, sortable: true },
        { title: '收', field: 'DAMOUNT', width: 80, sortable: true },
        { title: '支', field: 'CAMOUNT', width: 80, sortable: true },
        {
            field: 'CrtDate', title: '日期', width: 100, sortable: true, formatter: function (value, row, index) {
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
        {
            field: 'SuccFlag', title: '状态', width: 120, sortable: true, formatter: function (value, row, index) {
                if (row.SuccFlag == "1") {
                    return "成功";
                } else if (row.SuccFlag == "-1") {
                    return "失败";
                } else {
                    return "已发送";
                }
            }
        },
        { title: '备注', field: 'remark', width: 80, sortable: true }
        ]],
        pagination: true,
        rownumbers: true
    });
    var p = $('#test').datagrid('getPager');
    $(p).pagination({
        onBeforeRefresh: function () {
            alert('before refresh');
        }
    });

}

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

function clearSearch() {
    $("#crimeSearch input").val("");
}

function FilterSearch(doAction) {
    if ("" == $("#crimeSearch input[name=fCrimeName]").val() && "" == $("#crimeSearch input[name=fCrimeCode]").val() && "000" == $("#cc").val()) {
        $.messager.alert('提示', '请输入或是选择相应的查询条件!'); 
    } else {
        var rp;
        if ($("#chkDS").prop("checked") == true) {
            if ($("#chkDF").prop("checked") == true) {
                rp="all";                                 
            }else{
                rp="rcv";                                 
            }
        } else if ($("#chkDF").prop("checked") == true) {
            if ($("#chkDS").prop("checked") == true) {
                rp="all";                                 
            }else{
                rp="pay";                                 
            }
        }else{            
            rp="all";            
        }
        var rows = $('#test').datagrid('getRows');
        for (var i = rows.length - 1; i >= 0; i--) {
            var index = $('#test').datagrid('getRowIndex', rows[i]);
            console.log("i=" + i + "&index=" + index);
            $('#test').datagrid('deleteRow', index);
        }
        $('#test').datagrid('load', {
            startDate: $("#crimeSearch input[name=startDate]").val(),
            endDate: $("#crimeSearch input[name=endDate]").val(),
            succflag: $("#succflag").combobox("getValue"),
            remark: $("#schRemark").textbox("getValue"),
            action: doAction,
            rcvpay: rp
        });
    }                    
    //表单
    $('#ff').form({
        url: '/BankRpt/GetEdiBankSumList?action=ExcelOutport&rcvpay=' + rp,

        onSubmit: function () {
            // do some check   
            // return false to prevent submit;   
        },
        success: function (data) {
            //alert(data)   
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
    // submit the form
}

//打印报表
function PrintReport(doAction) {
    if ("" == $("#crimeSearch input[name=fCrimeName]").val() && "" == $("#crimeSearch input[name=fCrimeCode]").val() && "000" == $("#cc").val()) {
        $.messager.alert('提示', '请输入或是选择相应的查询条件!');
    } else {
        var rp;
        if ($("#chkDS").prop("checked") == true) {
            if ($("#chkDF").prop("checked") == true) {
                rp = "all";
            } else {
                rp = "rcv";
            }
        } else if ($("#chkDF").prop("checked") == true) {
            if ($("#chkDS").prop("checked") == true) {
                rp = "all";
            } else {
                rp = "pay";
            }
        } else {
            rp = "all";
        }
        window.open("/BankRpt/PrintBankDataReport?"
            +"startDate=" + $("#crimeSearch input[name=startDate]").val()
        +"&endDate=" +$("#crimeSearch input[name=endDate]").val()
        +"&succflag=" +$("#succflag").combobox("getValue")
        +"&remark=" + $("#schRemark").textbox("getValue")
        +"&action="+ doAction
        +"&rcvpay="+ rp);
    }

}
                
//选单复位
function resetFlag() {
                    
    var rows=$('#test').datagrid('getSelected');
    if (null==rows){
        $.messager.alert('提示','请选择一条需要复位的记录');
    }else{
        $("#MainSeqno").val(rows.MainSeqno);
        $("#UploadDate").val(rows.UploadDate);
        if(rows.ResetFlag=="已复位"){
            $.messager.alert('提示','该记录已经复位了，无需再复位！');
            return false;
        }                        
        if(rows.SuccFlag=="已发送"){                        
            var startdate=$('#startDate').datetimebox('getValue');
            $.post("/BankRpt/GetBankDataInfo", { "action": "resetFlag", "MainSeqno": rows.MainSeqno, "UploadDate": rows.UploadDate, "startDate": startdate }, function (data, status) {
                if ("success" != status) {
                    return false;
                }else{                            
                    $.messager.alert('提示', data);
                    //$('#txtSelect').combobox('select',data);
                                    
                }                                                          
            }); 
        }else{
            $.messager.alert('提示', "只有处于【已发送】状态才可以复位！");
        }
    }
}
                    

///执行发送数据到银行扣款
function DoSendBankData() {
    $.post("/BankRpt/DoSendBankData", { "action": "DoSendBankData" }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            $.messager.alert('提示', data);
            //$('#txtSelect').combobox('select',data);

        }
    });
}
