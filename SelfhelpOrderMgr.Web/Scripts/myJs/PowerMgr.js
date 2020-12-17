
$(function () {
    $('#tt5').tree({
        url: "/Power/GetTreeArea",
        lines: true,
        checkbox: true
    });

    var node = $('#tt5').tree('find', 1);
    $("#FCode").attr('disabled', true);
    $("#FName").attr('disabled', true);
    $("#FPwd").attr('disabled', true);
    $("#FManagerCard").attr('disabled', true);
    $("#FUserChinaName").attr('disabled', true);
    $('#btnSave').linkbutton('disable');



    //用户列表清单
    $('#test').datagrid({
        title: '用户列表',
        iconCls: 'icon-save',
        //width: 700,
        height: $(window).height() * 0.5,
        fitColumns: true,
        nowrap: true,
        autoRowHeight: false,
        striped: true,
        collapsible: true,
        sortName: 'FCode',
        sortOrder: 'asc',
        remoteSort: false,
        singleSelect: true,
        queryParams: {
            action: 'GetUserList'
        },
        idField: 'FCode',
        pageSize: 25,
        pageList: [25, 50],
        frozenColumns: [[
	                { field: 'ck', checkbox: true },
	                { title: '用户编号', field: 'FCode', width: 80, sortable: true }
        ]],
        columns: [[
					{ field: 'FName', title: '登录名', width: 120 },
					{ field: 'FUserChinaName', title: '用户姓名', width: 120 },
					{
					    field: 'FPwd', title: '密  码', width: 100, type: 'password',
					    formatter: function (value, row, index) {
					        if (row.FPwd) {
					            return "**********";
					        } else {
					            return value;
					        }
					    }
					},
					{
					    field: 'FUserArea', title: '队别名称', width: 200, sortable: true, //rowspan: 2,
					    sorter: function (a, b) {
					        return (a > b ? 1 : -1);
					    }
					},
					{ field: 'RoleName', title: '角色', width: 100 },
                    { field: 'FRole', title: '角色号', width: 100 , hidden:true},
					{ field: 'FPrivate', title: '管理员', width: 60 },
                    { field: 'FManagerCard', title: '管理卡号', width: 60 }
        ]],
        onClickRow: function (rowIndex, rowData) {
            $("#FCode").val(rowData.FCode);
            $("#FName").val(rowData.FName);
            $("#FPwd").val(rowData.FPwd);
            $("#FUserChinaName").val(rowData.FUserChinaName);
            var s = $('#cc').combobox('getData'); //获取控件中所有的值
            for (var i = 0; i < s.length; i++)
            {
                console.log(s[i]);
                var a=s[i];
                if(a.text==rowData.FUserArea){
                    $("#cc").combobox('setValue', a.value);
                    break;
                }
            }
            //$("#cc").combobox('setText', rowData.FUserArea);
            $("#Role").combobox('setValue', rowData.FRole);
            $("#FManagerCard").val(rowData.FManagerCard);
            if (rowData.FPrivate == '是') {
                $('input:radio[name=selectRadio]')[0].checked = true;
            } else {
                $('input:radio[name=selectRadio]')[1].checked = true;
            }

            $.post("/Power/GetAreaPower", { "FUserID": $("#FCode").val() }, function (data, status) {
                if ("success" != status) {
                    return false;
                } else {
                    var nodes = $("#tt5").tree('getRoots');
                    for (var i = 0; i < nodes.length; i++) {
                        var node = nodes[i];
                        $("#tt5").tree('uncheck', node.target);
                    }
                    if ("" != data) {
                        var selRoles = data.split("|");
                        var selRole;
                        for (var i = 0; i < selRoles.length; i++) {
                            selRole = selRoles[i];
                            var mynode = $("#tt5").tree('find', selRole);
                            //console.info(mynode);
                            if (mynode != null) {
                                var chk = $('#tt5').tree('isLeaf', mynode.target);
                                if (chk) {
                                    $("#tt5").tree('check', mynode.target);
                                }
                            }
                            
                        }
                        $("#FCode").attr('disabled', true);
                        $("#FName").attr('disabled', true);
                        $("#FPwd").attr('disabled', true);
                        $("#FUserChinaName").attr('disabled', true);
                        $('#btnSave').linkbutton('disable');
                    }
                }
            });
        }
    });
});

//保存修改内容
function saveContent() {
    //检测输入是否完整
    if ($('#FCode').val() == '') {
        $.messager.alert('提示', '用户编号不能为空');
        return false;
    }
    if ($('#FName').val() == '') {
        $.messager.alert('提示', '用户登录名不能为空');
        return false;
    }
    if ($('#FPwd').val() == '') {
        $.messager.alert('提示', '用户密码不能为空');
        return false;
    }
    if ($('#FUserChinaName').val() == '') {
        $.messager.alert('提示', '用户姓名不能为空');
        return false;
    }
    if ($('#cc').combobox('getValue') == '0') {
        $.messager.alert('提示', '请选择一个监区');
        return false;
    }
    if ($('#Role').combobox('getValue') == '-1') {
        $.messager.alert('提示', '请选择一种角色');
        return false;
    }
        var t = $('#tt5').tree('getChecked');	// get the tree object
        var selectTree = "";
        if (t.length <= 0) {
            $.messager.alert('提示', '请选择至少选择一个监区');
            return false;
        }
        for (var i = 0; i < t.length; i++) {
            if (selectTree == "") {
                selectTree = t[i].text;
            } else {
                selectTree = selectTree + "|" + t[i].text;
            }
        }
        //console.info(selectTree);
        $('#selectTree').val(selectTree);
        $('#FCode').removeAttr('disabled');
        $('#ff').form({
            url: "/Power/SaveTree",
            onSubmit: function () {
                // do some check    
                // return false to prevent submit;    
            },
            success: function (data) {
                $.messager.alert('提示', data);
                if (data == "OK.Insert") {
                    InsertRowToGrid();//插入一条记录DataGrid中
                    closeEdit();
                } else if (data == "OK.Update") {
                    UpdateRowToGrid();//更新DataGrid中的当前选择的记录                
                    closeEdit();
                }
            }
        });
        // submit the form    
        $('#ff').submit();
    

}

//插入一条记录DataGrid中
function InsertRowToGrid() {
    $('#test').datagrid('appendRow', {
        FCode: $("#FCode").val(),
        FName: $("#FName").val(),
        FPwd: $("#FPwd").val(),
        FUserChinaName: $("#FUserChinaName").val(),
        FUserArea: $("#cc").combobox('getText'),
        RoleName: $("#Role").combobox('getText'),
        FManagerCard: $("#FManagerCard").val()
    });
}
//更新DataGrid中的当前选择的记录
function UpdateRowToGrid() {
    var row = $('#test').datagrid('getSelected');
    var idx = $('#test').datagrid('getRowIndex', row);
    $('#test').datagrid('updateRow', {
        index: idx,
        row: {
            FCode: $("#FCode").val(),
            FName: $("#FName").val(),
            FPwd: $("#FPwd").val(),
            FUserChinaName: $("#FUserChinaName").val(),
            FUserArea: $("#cc").combobox('getText'),
            RoleName: $("#Role").combobox('getText'),
            FManagerCard: $("#FManagerCard").val()
        }
    });
}
//检测输入是否完整
function checkInputInfo() {
    if ($('#FCode').val() == '') {
        $.messager.alert('提示', '用户编号不能为空');
        return false;
    }
    if ($('#FName').val() == '') {
        $.messager.alert('提示', '用户登录名不能为空');
        return false;
    }
    if ($('#FPwd').val() == '') {
        $.messager.alert('提示', '用户密码不能为空');
        return false;
    }
    if ($('#FUserChinaName').val() == '') {
        $.messager.alert('提示', '用户姓名不能为空');
        return false;
    }
    if ($('#cc').combobox('getValue') == '0') {
        $.messager.alert('提示', '请选择一个监区');
        return false;
    }
    if ($('#Role').combobox('getValue') == '-1') {
        $.messager.alert('提示', '请选择一种角色');
        return false;
    }

}
function closeEdit() {
    $("#FCode").attr('disabled', true);
    $("#FName").attr('disabled', true);
    $("#FPwd").attr('disabled', true);
    $("#FUserChinaName").attr('disabled', true);
    $('#btnSave').linkbutton('disable');
}

//新增用户
function AddRoleContent() {
    $("#FCode").val('');//   
    $("#FName").val('');//
    $("#FPwd").val('');//  
    $("#FUserChinaName").va; ('');
    $("input").removeAttr("disabled");// 
    $('#btnSave').linkbutton('enable');
}

//修改用户
function AlterRoleContent() {
    $("#FCode").attr("disabled", true);//   
    $("#FName").removeAttr("disabled");//
    $("#FPwd").removeAttr("disabled");//
    $("#FManagerCard").removeAttr("disabled");// 
    $("#FUserChinaName").removeAttr('disabled');
    $('#btnSave').linkbutton('enable');
}

//删除用户
function DelRoleContent() {
    $.messager.confirm('Confirm', '您确认要删除该用户吗?', function (r) {
        if (r) {
            if ("" != $("#FCode").val() && "" != $("#FName").val()) {
                $.post("/Power/DelTreeUser", { "action": "DelTreeUser", "FUserID": $("#FCode").val(), "FUserName": $("#FName").val() }, function (data, status) {
                    if ("success" != status) {
                        return false;
                    } else {
                        if ("删除成功" == data) {
                            var row = $('#test').datagrid('getSelected');
                            var idx = $('#test').datagrid('getRowIndex', row);
                            $('#test').datagrid('deleteRow', idx);

                        }
                        $.messager.alert('提示', data);
                    }
                });
            }
        }
    });
}