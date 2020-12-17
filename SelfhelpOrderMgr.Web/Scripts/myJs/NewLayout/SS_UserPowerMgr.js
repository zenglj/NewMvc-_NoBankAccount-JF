var setting = {
    view: {
        addHoverDom: addHoverDom,
        removeHoverDom: removeHoverDom,
        selectedMulti: false
    },
    check: {
        enable: true
    },
    data: {
        simpleData: {
            enable: true
        }
    },
    edit: {
        enable: false
    }
};


//$(document).ready(function () {
//    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
//});

var roles;
var areas;

$(document).ready(function () {
    $.post("/SystemSet/GetTreeAreaInfo", { "DT": new Date() }, function (data, status) {
        //alert(data);
        //var zNewNodes=$.parseJSON(data)
        $.fn.zTree.init($("#treeDemo"), setting, data);
    });
    
    //GetTreeRoleInfo();

    $.post("/SystemSet/GetTreeRoleInfo", { "DT": new Date() }, function (data, status) {
        roles = data;
        //装载角色的Select控件
        for (var i = 0; i < roles.length; i++) {
            $("#FRole").append("<option value='" + roles[i].RoleID + "'>" + roles[i].RoleName + "</option>")
        }
    });

    $.post("/SystemSet/GetAreaInfo", { "DT": new Date() }, function (data, status) {
        areas = data;
        //装载角色的Select控件
        for (var i = 0; i < areas.length; i++) {
            $("#FUserArea").append("<option value='" + areas[i].ID + "'>" + areas[i].FName + "</option>")
        }
    });


    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'FCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '登录名', align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'FPwd', title: '密码', align: 'center', valign: 'middle', width: 200, sortable: true, formatter: function (value, row, index) {
                    return "********";
                }
            },
            {
                field: 'FPRIVATE', title: '管理员', align: 'center', valign: 'middle', width: 200, sortable: true, formatter: function (value, row, index) {
                    if (value == 1) {
                        return '是';
                    }
                    if (value == 0) {
                        return '否';
                    }
                    return value;
                }
            },
            { field: 'FUserArea', title: '所在单位', align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'FRole', title: '角色', align: 'center', valign: 'middle', width: 200, sortable: true, formatter: function (value, row, index) {
                    for (var i = 0; i < roles.length; i++) {
                        if (value == roles[i].RoleID) {
                            return roles[i].RoleName;
                        }
                    }                    
                }
            }

        ],
        url: '/SystemSet/GetCzyInfoStr',
        uniqueId: "FCode",
        toolbar:"#toolbar",
        search:true,
        striped: true,
        pagination: true,
        height: 396,
        pageSize: 10,                       //每页的记录行数（*）
        pageList: [5, 10, 25, 50, 100],        //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        showExport: true,                     //是否显示导出
        exportDataType: "all",              //basic', 'all', 'selected'.
        onClickRow: function (row) {
            //alert(row.RoleName);
            roleRowEdit(row, false);//显示该行相应的数据
        }

    });


    //点击选中行，改变选中行的背景颜色
    $('#tb').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });
});

function addHoverDom(treeId, treeNode) {
    var sObj = $("#" + treeNode.tId + "_span");
    if (treeNode.editNameFlag || $("#addBtn_" + treeNode.tId).length > 0) return;
    var addStr = "<span class='button add' id='addBtn_" + treeNode.tId
        + "' title='add node' onfocus='this.blur();'></span>";
    sObj.after(addStr);
    var btn = $("#addBtn_" + treeNode.tId);
    if (btn) btn.bind("click", function () {
        var zTree = $.fn.zTree.getZTreeObj("treeDemo");
        zTree.addNodes(treeNode, { id: (100 + newCount), pId: treeNode.id, name: "new node" + (newCount++) });
        return false;
    });
};
function removeHoverDom(treeId, treeNode) {
    $("#addBtn_" + treeNode.tId).unbind().remove();
};


//获到ZTree选中的节点数
function GetZTreeSelectNodes() {
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    nodes = zTree.getCheckedNodes(true);
    v = "";
    var parentid = "";
    var parentlevel = "";
    for (var i = 0, l = nodes.length; i < l; i++) {
        v += nodes[i].name + ",";
        parentid += nodes[i].id + ",";
        parentlevel += nodes[i].menu_level + ",";
    }
    alert(v);
}


function GetTreeRoleInfo() {
    $.post("/SystemSet/GetTreeRoleInfo", { "DT": new Date() }, function (data, status) {
        if ("success" == status) {
            $("#roleTbBody").empty();
            for (var i = 0; i < data.length; i++) {
                var role = data[i];
                var tr = "<tr>"
                    +"<td>"+ role.RoleID +"</td>"
                    +"<td>"+ role.RoleName +"</td>"
                    +"<td>"
                    +"    <a class=\"btn btn-primary btn-sm\" href=\"#\" role=\"button\" onclick=\"roleRowEdit("+ role.RoleID +",'"+ role.RoleName +"')\"><i class=\"fa fa-edit\"> 编辑</i></a>"
                    + "    <a class=\"btn btn-danger btn-sm\" href=\"#\" role=\"button\" onclick=\"roleRowDelete(" + role.RoleID + ",'" + role.RoleName + "')\"><i class=\"fa fa-minus-circle\"> 删除</i></a>"
                    + "</td></tr>";
                $("#roleTbBody").append(tr);
            }
        }
    });
}


function btnAddRole() {
    $("#FCode").val("");
    $("#FName").val("");
    $("#FPwd").val("");
    $("#FRole").val(-1);
    $("#FUserArea").val(-1);
    //默认取消所有权限功能
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.checkAllNodes(false);

    $("#FCode").removeAttr("disabled");
    $("#FName").removeAttr("disabled");
    $("#FPwd").removeAttr("disabled");
    $("#FRole").removeAttr("disabled");
    $("#FUserArea").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}


function SetEditMode() {
    $("#FCode").removeAttr("disabled");
    $("#FName").removeAttr("disabled");
    $("#FPwd").removeAttr("disabled");
    $("#FRole").removeAttr("disabled");
    $("#FUserArea").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}

function CloseEditMode() {
    $("#FCode").attr("disabled", "disabled");
    $("#FName").attr("disabled", "disabled");
    $("#FPwd").attr("disabled", "disabled");
    $("#FRole").attr("disabled", "disabled");
    $("#FUserArea").attr("disabled", "disabled");
    $("#btnSave").attr("disabled", "disabled");
}


function btnSaveRole() {
    if ($("#FCode").val() == "") {
        alert("对不起，ID号不能为空");
        return false;
    }
    if ($("#FName").val() == "") {
        alert("对不起，名称不能为空");
        return false;
    }
    if ($("#FPwd").val() == "") {
        alert("对不起，密码不能为空");
        return false;
    }

    if ($("#FRole").val() == -1) {
        alert("对不起，请选择一个角色");
        return false;
    }
    if ($("#FUserArea").val() == -1) {
        alert("对不起，请选择一个所属单位");
        return false;
    }
    
    //默认取消所有权限功能
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    var nodes = zTree.getCheckedNodes(true);
    var selectRoleMenuIds = "";
    if (nodes.length > 0) {
        for (var i = 0; i < nodes.length; i++) {
            if (nodes[i].isParent == false) {
                if (selectRoleMenuIds == "") {
                    selectRoleMenuIds = selectRoleMenuIds + nodes[i].id;
                } else {
                    selectRoleMenuIds = selectRoleMenuIds +"|"+ nodes[i].id;
                }
            }
        }
        $.post("/SystemSet/SaveCZYInfo", {
            "FCode": $("#FCode").val()
            , "FName": $("#FName").val()
            , "FPwd": $("#FPwd").val()
            , "FRole": $("#FRole").val()
            , "FUserArea": $("#FUserArea option:selected").text()
            , "UserMgrAreas": selectRoleMenuIds
        }, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if (words[2] == "Add") {
                    $('#tb').bootstrapTable('append', [
                        {
                            FCode: $("#FCode").val(),
                            FName: $("#FName").val(),
                            FPwd: $("#FPwd").val(),
                            FRole: $("#FRole").val(),
                            FPRIVATE: 0,
                            FUserArea: $("#FUserArea option:selected").text()
                        }
                    ]);
                } else {
                    var allTableData = $("#tb").bootstrapTable('getData');
                    for(var i=0;i<allTableData.length;i++){
                        if (allTableData[i].FCode == $("#FCode").val()) {
                            $('#tb').bootstrapTable('updateRow', {
                                index: i,
                                row: {
                                    FCode: $("#FCode").val(),
                                    FName: $("#FName").val(),
                                    FPwd: $("#FPwd").val(),
                                    FRole: $("#FRole").val(),
                                    FPRIVATE: 0,
                                    FUserArea: $("#FUserArea option:selected").text()
                                }
                            });
                        }
                    }
                    
                }
                CloseEditMode();
                alert(words[1]);
            }
        })
    } else {
        alert("对不起，请最少选择一个功能菜单，谢谢");
        return false;
    }
    

    $("#roleId").removeAttr("disabled");
    $("#roleName").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}

function roleRowEdit(row,flag) {
    $("#FCode").val(row.FCode);
    $("#FName").val(row.FName);
    $("#FPwd").val(row.FPwd);
    $("#FRole").val(row.FRole);
    //通过文本方式设定选中项;
    $("#FUserArea").find("option:contains('" + row.FUserArea + "')").attr("selected", true);

    if (flag == true) {
        $("#FCode").removeAttr("disabled");
        $("#FName").removeAttr("disabled");
        $("#FPwd").removeAttr("disabled");
        $("#FRole").removeAttr("disabled");
        $("#FUserArea").removeAttr("disabled");
        $("#btnSave").removeAttr("disabled");
    } else {
        $("#FCode").attr("disabled", "disabled");
        $("#FName").attr("disabled", "disabled");
        $("#FPwd").attr("disabled", "disabled");
        $("#FRole").attr("disabled", "disabled");
        $("#FUserArea").attr("disabled", "disabled");
        $("#btnSave").attr("disabled", "disabled");
    }
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.checkAllNodes(false);
    nodes = zTree.getCheckedNodes(false);

    
    $.post("/SystemSet/GetUserMgrAreas", { "FCode": row.FCode }, function (data, status) {
        if ("success" == status) {
            for (var i = 0; i < data.length; i++) {                
                for (var j = 0; j < nodes.length; j++) {
                    if (nodes[j].id == data[i]) {
                        zTree.checkNode(nodes[j], true, true);
                    }
                }
            }
        }
    });
}



function btnDelRole() {

    if ($("#FCode").val()=="") {
        alert("要删除的ID不能为空");
        return false;
    }
    if ($("#FName").val() == "") {
        alert("要删除的用户名称不能为空");
        return false;
    }
    if (confirm("确定删除该用户吗?")) {
        //点击确定后操作
        $.post("/SystemSet/DeleteCzyInfo", { "FCode": $("#FCode").val(), "FName": $("#FName").val() }, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if ("OK" == words[0]) {
                    $('#tb').bootstrapTable('removeByUniqueId', $("#FCode").val());
                }
                alert(data);
            }
        });
    }
    
}
