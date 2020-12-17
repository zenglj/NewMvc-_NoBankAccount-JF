
$(function () {
    loadDetailTable(); //显示存款明细
    //$("#FPayTypes").combobox('clear');
    //$("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');
    $("#rjStartDate").datetimebox('clear');
    $("#rjEndDate").datetimebox('clear');
    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');
    $('#txtFCode').textbox('readonly', true);

    $("#win").window('close');
    $("#winSearch").window('close');
    $("#winBank").window('close');
    $('#winTP_YingYanyCan').window('close');  // open a window  

    $('#selChangeType').combobox({
        data:[{    
            "id":"",    
            "text": "请选择",
            "selected": true
        },{    
            "id":"1",    
            "text":"处遇"   
        },{    
            "id":"2",    
            "text": "队别" 
        }] ,
        valueField: 'id',
        textField: 'text',
        onSelect: function (rowIndex, rowData) {
            selectChangeTypeInfo()
        }
    });

    loadUserDetail();
    loadChangeList();
    
}); 



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
        fitColumns: false,
        nowrap: true,
        autoRowHeight: false,
        //singleSelect: true,
        striped: true,
        collapsible: true,
        url: '/Criminal/GetChangeList',
        sortName: 'Seqno',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'Seqno',
        pageSize: 10,
        pageList: [5,10, 20,50,100],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '序号', field: 'Seqno', width: 50, sortable: true },
            {
                field: 'AuditFlag', title: '审核状态', sortable: true, width: 80, formatter: function (value, row, index) {
                    if (row.AuditFlag == "9") {
                        return "已同意";
                    } else if (row.AuditFlag == "10") {
                        return "已拒绝";
                    } else {
                        return "待审核";
                    }
                }
            },
            { title: '编号', field: 'FCode', width: 80, sortable: true },
            { field: 'FName', title: '姓名', sortable: true, width: 80 }
        ]],
        columns: [[//DataGrid表格数据列
            
            { field: 'ChangeType', title: 'ChangeType', width: 100, sortable: true,hidden:true },
            { field: 'ChangeTypeName', title: '变更类型', width: 80, sortable: true},
            { field: 'OldCode', title: 'OldCode', width: 120, sortable: true, hidden: true },
            { field: 'OldName', title: '变更前', width: 90, sortable: true },
            { field: 'NewCode', title: 'NewCode', width: 100, sortable: true, hidden: true },
            { field: 'NewName', title: '变更后', width: 90, sortable: true },
            { field: 'ChangeInfo', title: '变更原因', width: 250, sortable: true },
            { field: 'CrtBy', title: '申请人', width: 100, sortable: true },
            {
                field: 'CrtDate', title: '申请日期', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (row.CrtDate != null) {
                        if (row.CrtDate != "") {
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
            { field: 'AuditArea', title: '审核部门', sortable: true, width: 100 },
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
            { field: 'AuditInfo', title: '审核意见', sortable: true, width: 100 },
            
            { field: 'Remark', title: '备注', sortable: true, width: 100 }
        ]],
        onSelect: function (rowIndex, rowData) {
            //loadTP_YYCList(rowData);//营养餐记录
            //setDataRowInfo(rowData);

            
        },
        pagination: true,
        rownumbers: true
    });
}



//设置行信息
function setDataRowInfo(rowData) {
    $("#txtFCode").textbox('setValue', rowData.FCode);
    $("#txtFName").textbox('setValue', rowData.FName);
    $("#txtFSex").combobox('setValue', rowData.FSex);
    $("#txtFCyCode").combobox('setValue', rowData.FCYCode);
    $("#txtFIdenNo").textbox('setValue', rowData.FIdenNo);
    $("#txtFAddr").textbox('setValue', rowData.FAddr);
    $("#txtFAreaCode").combobox('setValue', rowData.FAreaCode);
    $("#txtFCrimeCode").combobox('setValue', rowData.FCrimeCode);
    $("#txtFTerm").textbox('setValue', rowData.FTerm);    
    if (rowData.fflag == 1) {
        $("#txtFFlag").textbox('setValue', "离监");
    } else {
        $("#txtFFlag").textbox('setValue', "在押");
    }
    if (rowData.flimitflag == 1) {
        $("#txtFlimitFlag").attr('checked', 'checked');
    } else {
        $("#txtFlimitFlag").removeAttr('checked');
    }
    $("#txtFlimitAmt").textbox('setValue', rowData.flimitamt);
    $("#txtFDesc").textbox("setValue", rowData.FDesc);
    $("#txtFInDate").val(getShortTime(rowData.FInDate));
    $("#txtFOuDate").val(getShortTime(rowData.FOuDate));
    $("#TP_YingYangCan_Money").textbox("setValue", rowData.TP_YingYangCan_Money);
}

function btnSearch() {
    var rows = $("#test").datagrid('getRows');
    for (var i = rows.length-1; i >=0 ; i--) {
        $("#test").datagrid('deleteRow', 0);
    }

    $('#test').datagrid('load', {
        FCode: $("#FCode").numberbox('getValue'),
        endFCode: $("#endFCode").numberbox('getValue'),
        FName: $("#FName").textbox('getText'),
        changeType: $("#schChangeType").combobox('getValue'),
        startTime: $("#StartDate").datetimebox('getValue'),
        endTime: $("#EndDate").datetimebox('getValue'),
        areaCode: $("#FAreaName").combobox('getValue'),
        crtby: $("#FCrtBy").combobox('getValue'),
        CriminalFlag: $("#FCriminalFlag").combobox('getValue'),
        auditFlag: $("#schAuditFlag").combobox('getValue')

    });


}

//查找需要变更的用户
function userSearch() {

    //$('#ffSearch').form({    
    //    url: "/Criminal/GetUserList",
    //    onSubmit: function(){    
    //        // do some check    
    //        // return false to prevent submit;    
    //    },    
    //    success:function(data){    
    //        //loadUserDetail($.parseJSON(data));
    //        $('#tbUserList').datagrid({                
    //            data: $.parseJSON(data)                
    //        });
    //    }    
    //});    
    //// submit the form    
    //$('#ffSearch').submit();

    var rows = $("#tbUserList").datagrid('getRows');
    for (var i = rows.length - 1; i >= 0 ; i--) {
        $("#tbUserList").datagrid('deleteRow', 0);
    }

    $('#tbUserList').datagrid('load', {
        addSchFCode: $("#addSchFCode").textbox('getValue'),
        addSchFName: $("#addSchFName").textbox('getValue'),
        addSchFArea: $("#addSchFArea").combobox('getValue'),
        addSchFCy: $("#addSchFCy").combobox('getValue'),
        addSchFFlag: $("#addSchFFlag").combobox('getValue')
    });
}

//加载变更前表格
function loadUserDetail() {
        $('#tbUserList').datagrid({
            //title: '账户余额列表',
            iconCls: 'icon-save',
            //width: 900,
            height:300,
            queryParams: {
                fName: '',
                FCode: '00000',
                FAreaName: '',
                action: 'LoginIn'
            },
            fitColumns: false,
            nowrap: true,
            autoRowHeight: false,
            //singleSelect: true,
            striped: true,
            collapsible: true,
            url: '/Criminal/GetUserList',
            //data:data,
            sortName: 'FCode',
            sortOrder: 'asc',
            remoteSort: false,
            idField: 'FCode',
            pageSize: 10,
            pageList: [5,10, 20,50,100],
            frozenColumns: [[//DataGrid表格排序列
                { field: 'ck', checkbox: true },
                { title: '编号', field: 'FCode', width: 80, sortable: true },
                { field: 'FName', title: '姓名', sortable: true, width: 80 }
            ]],
            columns: [[//DataGrid表格数据列
            { field: 'FCYCode', title: 'FCYCode', sortable: true, width: 60 ,hidden:true},
                { field: 'CyName', title: '处遇', sortable: true, width: 60 },
                { field: 'FAreaCode', title: 'FAreaCode', sortable: true, width: 60, hidden: true },
                { field: 'FAreaName', title: '队别', width: 100, sortable: true }
            ]],
            
            pagination: true,
            rownumbers: true
        });
}


//加载变更后表格
function loadChangeList() {
    $('#tbChangeList').datagrid({
        //title: '账户余额列表',
        iconCls: 'icon-save',
        //width: 900,
        height: 300,
        queryParams: {
            fName: '',
            FCode: '00000',
            FAreaName: '',
            action: 'LoginIn'
        },
        fitColumns: false,
        nowrap: true,
        autoRowHeight: false,
        //singleSelect: true,
        striped: true,
        collapsible: true,
        //url: '/Criminal/GetSearchUsers',
        //data:data,
        sortName: 'FCode',
        sortOrder: 'asc',
        remoteSort: false,
        idField: 'FCode',
        pageSize: 10,
        pageList: [5, 10, 20, 50, 100],
        frozenColumns: [[//DataGrid表格排序列
            { field: 'ck', checkbox: true },
            { title: '编号', field: 'FCode', width: 80, sortable: true },
            { field: 'FName', title: '姓名', sortable: true, width: 80 }
        ]],
        columns: [[//DataGrid表格数据列

            { field: 'ChangeType', title: 'ChangeType', sortable: true, width: 60 ,hidden:true},
            { field: 'ChangeTypeName', title: '变更类型', sortable: true, width: 60 },
            { field: 'NewCode', title: 'NewCode', sortable: true, width: 60, hidden: true },
            { field: 'NewName', title: '变更后', width: 100, sortable: true },
            { field: 'OldCode', title: 'OldCode', sortable: true, width: 60, hidden: true },
            { field: 'OldName', title: 'OldName', width: 100, sortable: true, hidden: true },
            { field: 'ChangeInfo', title: 'ChangeInfo', width: 100, sortable: true, hidden: true }
        ]],
        
        pagination: true,
        rownumbers: true
    });
}

//选择变更的类型
function selectChangeTypeInfo() {
    
    var selectValue = $("#selChangeType").combobox('getValue');
    //alert(selectValue);
    if ("1" == selectValue) {
        $("#cyChangeDiv").show();
        $("#areaChangeDiv").hide();
        $("#optButtonDiv").show();
        
    } else if ("2" == selectValue) {
        $("#cyChangeDiv").hide();
        $("#areaChangeDiv").show();
        $("#optButtonDiv").show();
    } else {
        $("#cyChangeDiv").hide();
        $("#areaChangeDiv").hide();
        $("#optButtonDiv").hide();
    }

}


//加入变列
function addToList() {
    var changeType = $('#selChangeType').combobox("getValue");
    var changeTypeName = $('#selChangeType').combobox("getText");
    var newCode = "";
    var newName = "";

    
    if (changeType == "1") {        
        newCode = $('#selCyChangeType').combobox("getValue");
        newName = $('#selCyChangeType').combobox("getText");
    } else if (changeType == "2") {        
        newCode = $('#selAreaChangeType').combobox("getValue");
        newName = $('#selAreaChangeType').combobox("getText");
    }
    
    var rows = $('#tbUserList').datagrid("getSelections");
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择一条数据，谢谢");
        return false;
    }
    
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var oldCode = "";
        var oldName = "";
        var changeInfo = "";
        if (changeType == "1") {
            oldCode = row.FCYCode;
            oldName = row.CyName;
            //changeInfo = "申请[处遇],从【" + oldName + "】变更到【"+ newName +"】";
        } else if (changeType == "2") {
            oldCode =row.FAreaCode;
            oldName = row.FAreaName;
            //changeInfo = "申请[处遇],从【" + oldName + "】变更到【" + newName + "】";
        }
        $('#tbChangeList').datagrid('appendRow', {
            FCode: row.FCode,
            FName: row.FName,
            ChangeInfo: $("#changeInfo").val(),
            ChangeType: changeType,
            ChangeTypeName: changeTypeName,
            NewCode: newCode,
            NewName: newName,
            OldCode: oldCode,
            OldName: oldName
        });        
    }
    $('#tbUserList').datagrid("unselectAll");
}

//移除列表
function removeToList() {
    var rows = $('#tbChangeList').datagrid("getSelections");
    if (rows.length == 0) {
        $.messager.alert("提示", "请选择一条数据，谢谢");
        return false;
    }

    for (var i = rows.length-1; i >=0; i--) {
        var idx = $('#tbChangeList').datagrid('getRowIndex', rows[i]); 
        $('#tbChangeList').datagrid('deleteRow', idx);
    }
}

//提交变更记录
function submitToDb() {

    BatchSaveDg();
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
    $('#tbChangeList').datagrid().datagrid('enableCellEditing');
})

var $dg = $("#tbChangeList");

//批量保存数据
function BatchSaveDg() {
    
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
        console.info(effectRow);
        $.post("/Criminal/Save_RequestChangeList", effectRow,
            function (data, status) {
                if ("success" != status) {
                    return false;
                }
                else {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        var rows = $('#tbChangeList').datagrid("getRows");
                        for (var i = rows.length - 1; i >= 0; i--) {
                            var idx = $('#tbChangeList').datagrid('getRowIndex', rows[i]);
                            $('#tbChangeList').datagrid('deleteRow', idx);
                        }
                        $.messager.alert("提示", "保存成功！");
                    }
                    else {
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


//提交审核
function btnSubmitAduit() {
    var remark = $('#txtRemark').textbox("getText");
    var selSHYJ = $("#selSHYJ").combobox('getValue');


    var rows = $('#test').datagrid("getSelections");
    console.info(rows);
    if (rows.length == 0) {
        $.messager.alert("提示","请选择一条数据，谢谢");
        return false;
    }
    var seqnos = "";
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        if (rows[i].AuditFlag >= 9) {
            $.messager.alert("提示", "序号为"+row.Seqno+"的记录已经审核过了，无法重复审核");
            return false;
        }
        if (seqnos == "") {
            seqnos = seqnos + rows[i].Seqno;
        } else {
            seqnos = seqnos + ","+rows[i].Seqno;
        }
    }
    $.post("/Criminal/btnSubmitAduit", { "seqnos": seqnos, "remark": remark, "auditFlag": selSHYJ }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                $('#test').datagrid({
                    data: $.parseJSON(words[1])
                });
                $.messager.alert("提示","审核完成");
            } else {
                $.messager.alert("提示", data);
            }
            
        }
    });
}


//创建申请单
function AddChangeRequest() {
    $("#win").window('open');
}

function OutExcelSumOrder(id) {
    $('#mainSearch').form({
        url: "/Criminal/ExcelChangeList",
        onSubmit: function () {
            // do some check    
            // return false to prevent submit;    
        },
        success: function (data) {
            var words = data.split("|");
            if (words[0] == "OK") {
                window.open("/Upload/" + words[1]);
            }
        }
    });
    // submit the form    
    $('#mainSearch').submit();
}

//Excel信息导入
function InExcelCriminalInfo() {
    //$("#lblInfo").html("系统正在导入请稍候......");
    $.messager.progress({
        title: '导入数可能需要几分钟，请耐心等待',
        msg: '数据正在导入中...'
    });

    $('#ffExcel').form({
        url: '/Criminal/ExcelInport',
        onSubmit: function(){    
            // do some check    
            // return false to prevent submit;    
        },    
        success: function (data) {
            $.messager.progress('close');
            $("#win").window('close');
            alert(data)    
        }    
    });    
    // submit the form    
    $('#ffExcel').submit();
}




//导出Excel数据，id 是的不同报表的参数
function ExcelMain(id) {
    alert(id);

    var objWhere = getSearchObjCondition();
    //window.open("/Report/ExcelCriminalSumOrder/" + id + "?" + strWhere);

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


//清空条件
function btnClear() {
    $("#FPayTypes").combobox('clear');
    $("#FCashTypes").combobox('clear');
    $("#FAccTypes").combobox('clear');
    $("#FBankFlags").combobox('clear');

    $("#StartDate").datetimebox('clear');
    $("#EndDate").datetimebox('clear');

    $("#FCode").numberbox('clear');
    $("#endFCode").numberbox('clear');
    $("#FName").textbox('clear');
    $("#FCyName").combobox('clear');
    $("#FAreaName").combobox('clear');
    $("#FCrtBy").combobox('clear');
    $("#FCriminalFlag").combobox('clear');
 
}



