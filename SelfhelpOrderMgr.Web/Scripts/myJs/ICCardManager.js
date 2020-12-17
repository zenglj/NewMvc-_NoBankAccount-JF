
$(function () {

    $('#btnSave').linkbutton('disable');
    //$('#btnSave').attr('disabled',"disabled");

    $('#FCYCode').combobox({
        url: '../UI/Commons.ashx?action=GetCYList',
        valueField: 'FCode',
        textField: 'FName'
    });

    //$('#FAreaCode').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});
    //$('#cc').combobox({
    //    url: '../UI/Commons.ashx?action=GetAreaList',
    //    valueField: 'FCode',
    //    textField: 'FName'
    //});

    //用户列表清单
    $('#test').datagrid({
        //title: '用户列表',
        iconCls: 'icon-save',
        width: 780,
        height: 250,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Infomgr/GetICCardInfo',
        sortName: 'FCode',
        sortOrder: 'asc',
        queryParams: {
            action: 'GetSearchList',
            FCode: "",
            FName: "",
            FAreaCode: ""
        },
        remoteSort: false,
        singleSelect: true,
        idField: 'FCode',
        pageSize: 25,
        pageList: [25, 50],
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: 'ID号', field: 'FCode', width: 120, sortable: true },
	                { field: 'FName', title: '姓名', width: 120, sortable: true },
					{ field: 'FAreaCode', title: '队别', width: 200, sortable: true }
        ]],
        columns: [[

					{ field: 'CardCode', title: 'IC卡号', width: 120 },
                    {
                        field: 'FCardStatus', title: '状态', width: 60, formatter: function (value, row, index) {
                            if (row.FCardStatus == "1") {
                                return "正常";
                            } else if (row.FCardStatus == "2") {
                                return "挂失";
                            } else if (row.FCardStatus == "3") {
                                return "作废";
                            } else{
                                return "停用";
                            }
                        }
                    },
					{ field: 'FAddr', title: '地址', width: 300 }

        ]],
        toolbar: [{
            text: 'IC卡挂失',
            iconCls: 'icon-cancel',
            handler: function () {
                SetICCardStatus("2")
            }
        }, '-', {
            text: '解挂IC卡',
            iconCls: 'icon-ok',
            handler: function () {
                SetICCardStatus("1")
            }
        }
        //, '-', {
        //    text: 'IC卡作废',
        //    iconCls: 'icon-no',
        //    handler: function () {
        //        SetICCardStatus("3")
        //    }
        //}, '-', {
        //    text: '初始化IC卡',
        //    iconCls: 'icon-reload',
        //    handler: function () {
        //        if (initIcCardInfo('') == 1) {
        //            $.messager.alert('提示', '初始化数据成功');
        //        } else {
        //            $.messager.alert('提示', '初始化数据失败');
        //        }
        //    }
        //}
        ],
        onClickRow: function (rowIndex, rowData) {
            $('#ICCardList').datagrid('load', {
                action: 'GetCardList',
                CardCode: rowData.CardCode,
                FCrimeCode: rowData.FCode
            });
        }
    });


    $('#ICCardList').datagrid({
        //title: 'IC卡列表',
        iconCls: 'icon-save',
        width: 800,
        height: 100,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Infomgr/GetCardList',
        sortName: 'CardCode',
        sortOrder: 'asc',
        queryParams: {
            action: 'GetCardList',
            CardCode: "",
            FCrimeCode: ""
        },
        remoteSort: false,
        singleSelect: true,
        idField: 'CardCode',
        pageSize: 25,
        pageList: [25, 50],
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: 'IC卡号', field: 'CardCode', width: 120, sortable: true }
        ]],
        columns: [[
					{ field: 'FCrimeCode', title: '编号', width: 120 },
					{ field: 'FRCZY', title: '制卡员', width: 60 },
                    {
                        field: 'FRDate', title: '制卡日期', width: 100, formatter: function (value, row, index) {
                            if (row.FRDate != null) {
                                if (row.FRDate != "") {
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
                        field: 'FFlag', title: '状态', width: 60, formatter: function (value, row, index) {
                            if (row.FFlag == "1") {
                        return "正常";
                            } else if (row.FFlag == "2") {
                        return "挂失";
                            } else if (row.FFlag == "3") {
                        return "作废";
                    } else{
                        return "停用";
                    }
    }
},
        ]]

    });

    //动态改变行颜色
    $('#test').datagrid({
        rowStyler: function (index, row) {
            if (row.FCardStatus != "1") {
                return 'background-color:gray;';
            }
        }
    });

    //动态改变行颜色
    $('#ICCardList').datagrid({
        rowStyler: function (index, row) {
            if (row.FFlag != "1") {
                return 'background-color:gray;';
            }
        }
    });

});

//修改IC卡的状态:挂失\恢复
function SetICCardStatus(fflag) {
    var row = $('#test').datagrid('getSelected');
    $.post("/Infomgr/SetICCardStatus", {
        "action": "SetICCardStatus",
        "CardCode": row.CardCode,
        "FCrimeCode": row.FCode,
        "FFlag": fflag
    }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            $.messager.alert('提示', data);

            if (data != "您无权解挂IC卡,请与管理科联系,谢谢!") {
                var fstatus;
                if (fflag == "2") {
                    fstatus = "挂失";
                }
                if (fflag == "1") {
                    fstatus = "正常";
                }
                var i = $('#test').datagrid('getRowIndex', row);

                $('#test').datagrid('updateRow', {
                    index: i,
                    row: {
                        FCardStatus: fflag
                    }
                });
            }
        }
    });





    $('#ICCardList').datagrid('load', {
        action: 'GetCardList',
        CardCode: row.FICCard,
        FCrimeCode: row.FCode
    });
}

//保存修改内容
function saveContent() {

    ////console.info(selectTree);        
    $.post("../UI/T_CriminalUI.ashx", {
        "action": "SaveCrimeList",
        "FCode": $("#FCode").val(),
        "FName": $("#FName").val(),
        "FSex": $("#FSex").val(),
        "FCYCode": $("#FCYCode").combobox('getText'),
        "FAreaCode": $("#FAreaCode").combobox('getText'),
        "FFlag": $("#FFlag").val(),
        "FlimitFlag": $("#FlimitFlag").val(),
        "FlimitAmt": $("#FlimitAmt").val(),
        "FAddr": $("#FAddr").val()
    }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            $.messager.alert('提示', data);
        }
    });
    $("#FCode").attr('disabled', true);
    $("#FName").attr('disabled', true);
    $("#FSex").attr('disabled', true);

    $("#FFlag").attr('disabled', true);
    $("#FlimitFlag").attr('disabled', true);
    $("#FlimitAmt").attr('disabled', true);
    $("#FAddr").attr('disabled', true);
    $('#btnSave').linkbutton('disable');

    $('#test').datagrid('load', {
        action: 'GetAllList'
    });

}

//新增角色
function AddRoleContent() {
    $("#FCode").val('');//   
    $("#FName").val('');// 
    $("#FSex").val('');//  
    $("input").removeAttr("disabled");// 
    $('#btnSave').linkbutton('enable');
}

//修改角色
function AlterRoleContent() {
    $("#FCode").attr("disabled", true);//   
    $("#FName").removeAttr("disabled");//         
    $("#FSex").removeAttr("disabled");// 

    $("#FAreaCode").removeAttr("disabled");// 
    $("#FFlag").removeAttr("disabled");// 
    $("#FlimitFlag").removeAttr("disabled");// 
    $("#FlimitAmt").removeAttr("disabled");// 
    $("#FAddr").removeAttr("disabled");//  

    $('#btnSave').linkbutton('enable');
}

//删除角色
function DelRoleContent() {
    $.messager.confirm('Confirm', '您确认要删除该处遇吗?', function (r) {
        if (r) {
            if ("" != $("#FCode").val() && "" != $("#FName").val()) {
                $.post("../UI/T_CriminalUI.ashx", { "action": "DelRoleTree", "FCode": $("#FCode").val(), "FName": $("#FName").val() }, function (data, status) {
                    if ("success" != status) {
                        return false;
                    } else {
                        if ("删除成功" == data) {
                            $('#test').datagrid('load', {
                                action: 'GetRoleList'
                            });
                        }
                        $.messager.alert('提示', data);
                    }
                });
            }
        }
    });
}

//清空查询条件
function clearSearch() {
    $("#fCrimeCode").val('');
    $("#fCrimeName").val('');
    $('#cc').combobox('setValue', "");
}

//按条件查询
function FilterSearch() {
    $('#test').datagrid('load', {
        action: 'GetSearchList'+Date(),
        FCode: $("#fCrimeCode").val(),
        FName: $("#fCrimeName").val(),
        FAreaCode: $('#cc').combobox('getValue')
    });
}