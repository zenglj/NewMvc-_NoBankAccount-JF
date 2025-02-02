﻿

//UI/TreeMenuUI.ashx?action=GetAllTree
//../Answer/TreeMenu.ashx?action=GetAllTree
$(function () {
    $('#tt3').tree({
        url: "/Power/GetAllTree",
        state: "open",
        lines: true,
        checkbox: true

    });

    $('#btnSave').linkbutton('disable');
    //$('#btnSave').attr('disabled',"disabled");

    ////加载授权级别
    //$('#Level').combobox({
    //    url: "/Power/GetLevel",
    //    valueField: 'FValue',
    //    textField: 'FName'
    //});

    //用户列表清单
    $('#test').datagrid({
        title: '用户列表',
        iconCls: 'icon-save',
        //width: 700,
        height: $(window).height() * 0.6,
        //fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        url: '/Power/GetRoleList',
        sortName: 'RoleID',
        sortOrder: 'asc',
        //queryParams: {
        //    action: 'GetRoleList'
        //},
        remoteSort: false,
        singleSelect: true,
        idField: 'RoleID',
        pageSize: 25,
        pageList: [25, 50],
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: 'ID号', field: 'RoleID', width: 80, sortable: true }
        ]],
        columns: [[
					{ field: 'RoleName', title: '角色名', width: 120 }
                    //{ field: 'LevelName', title: '审批级别', width: 120 },
                    //{ field: 'LevelId', title: '级别Id', width: 120, hidden: true }

        ]],
        onClickRow: function (rowIndex, rowData) {
            //alert("eere");
            $("#FCode").val(rowData.RoleID);
            $("#FName").val(rowData.RoleName);
            //$("#Level").combobox('setValue', rowData.LevelId);
            $.post("/Power/GetRolePower", { "RoleID": $("#FCode").val() }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    //$.messager.alert('提示', data);
                    if ("" != data) {
                        var selRoles = data.split("|");
                        var selRole;
                        var node = $("#tt3").tree('find', 0);
                        $("#tt3").tree('uncheck', node.target);
                        //console.info(selRoles);
                        for (var i = 0; i < selRoles.length; i++) {
                            selRole = selRoles[i];
                            var node1 = $("#tt3").tree('find', selRole);
                            $("#tt3").tree('check', node1.target);
                        }
                        $("#FCode").attr('disabled', true);
                        $("#FName").attr('disabled', true);
                        $('#btnSave').linkbutton('disable');
                    } else {
                        var node = $("#tt3").tree('find', 0);
                        $("#tt3").tree('uncheck', node.target);
                    }
                }
            });
        }

    });

});

//保存修改内容
function saveContent() {
    var t = $('#tt3').tree('getChecked');	// get the tree object        
    var selectTree = "";
    for (var i = 0; i < t.length; i++) {
        if (selectTree == "") {
            selectTree = t[i].text;
        } else {
            selectTree = selectTree + "|" + t[i].text;
        }
    }
    $.post("/Power/SaveRoleTree", { "RoleID": $("#FCode").val(), "RoleName": $("#FName").val(),"selectTree": selectTree }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            $.messager.alert('提示', data);
            if (data == "OK.Insert") {
                $('#test').datagrid('appendRow', {
                    RoleID: $("#FCode").val(),
                    RoleName: $("#FName").val()
                    //LevelId: $("#Level").combobox('getValue'),
                    //LevelName: $("#Level").combobox('getText')
                });
            } else if (data == "OK.Update") {
                var row = $('#test').datagrid('getSelected');
                var idx = $('#test').datagrid('getRowIndex', row);
                $('#test').datagrid('updateRow', {
                    index: idx,
                    row: {
                        //RoleID: $("#FCode").val(),
                        RoleName: $("#FName").val(),
                        //LevelId: $("#Level").combobox('getValue'),
                        //LevelName: $("#Level").combobox('getText')
                    }
                });
            }
        }
    });
    $("#FCode").attr('disabled', true);
    $("#FName").attr('disabled', true);
    $('#btnSave').linkbutton('disable');
    $('#Level').combobox('disable');

}

//新增角色
function AddRoleContent() {
    $("#FCode").val('');//   
    $("#FName").val('');//  
    $("input").removeAttr("disabled");// 
    $('#btnSave').linkbutton('enable');
    //$("#Level").combobox("enable");// 
}

//修改角色
function AlterRoleContent() {
    $("#FCode").attr("disabled", true);//   
    $("#FName").removeAttr("disabled");// 
    //$("#Level").combobox("enable");// 
    $('#btnSave').linkbutton('enable');
}

//删除角色
function DelRoleContent() {
    $.messager.confirm('Confirm', '您确认要删除该角色吗?', function (r) {
        if (r) {
            if ("" != $("#FCode").val() && "" != $("#FName").val()) {
                $.post("/Power/DelRoleTree", { "RoleID": $("#FCode").val(), "RoleName": $("#FName").val() }, function (data, status) {
                    if ("success" != status) {
                        return false;
                    } else {
                        var row = $('#test').datagrid('getSelected');
                        var idx = $('#test').datagrid('getRowIndex', row);
                        if ("删除成功" == data) {
                            $('#test').datagrid('deleteRow', idx);
                        }
                        $.messager.alert('提示', data);
                    }
                });
            }
        }
    });


}