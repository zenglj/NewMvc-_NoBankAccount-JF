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

var zNodes = [
    { id: 1, pId: 0, name: "父节点1", open: true },
    { id: 11, pId: 1, name: "父节点11" },
    { id: 111, pId: 11, name: "叶子节点111" },
    { id: 112, pId: 11, name: "叶子节点112" },
    { id: 113, pId: 11, name: "叶子节点113" },
    { id: 114, pId: 11, name: "叶子节点114" },
    { id: 12, pId: 1, name: "父节点12" },
    { id: 121, pId: 12, name: "叶子节点121" },
    { id: 122, pId: 12, name: "叶子节点122" },
    { id: 123, pId: 12, name: "叶子节点123" },
    { id: 124, pId: 12, name: "叶子节点124" },
    { id: 13, pId: 1, name: "父节点13", isParent: true },
    { id: 2, pId: 0, name: "父节点2" },
    { id: 21, pId: 2, name: "父节点21", open: true },
    { id: 211, pId: 21, name: "叶子节点211" },
    { id: 212, pId: 21, name: "叶子节点212" },
    { id: 213, pId: 21, name: "叶子节点213" },
    { id: 214, pId: 21, name: "叶子节点214" },
    { id: 22, pId: 2, name: "父节点22" },
    { id: 221, pId: 22, name: "叶子节点221" },
    { id: 222, pId: 22, name: "叶子节点222" },
    { id: 223, pId: 22, name: "叶子节点223" },
    { id: 224, pId: 22, name: "叶子节点224" },
    { id: 23, pId: 2, name: "父节点23" },
    { id: 231, pId: 23, name: "叶子节点231" },
    { id: 232, pId: 23, name: "叶子节点232" },
    { id: 233, pId: 23, name: "叶子节点233" },
    { id: 234, pId: 23, name: "叶子节点234" },
    { id: 3, pId: 0, name: "父节点3", isParent: true }
];

//$(document).ready(function () {
//    $.fn.zTree.init($("#treeDemo"), setting, zNodes);
//});

$(document).ready(function () {
    $.post("/SystemSet/GetTreeMenuInfo", { "DT": new Date() }, function (data, status) {
        //alert(data);
        //var zNewNodes=$.parseJSON(data)
        $.fn.zTree.init($("#treeDemo"), setting, data);
    });
    
    //GetTreeRoleInfo();

    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'RoleID', title: 'ID号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'RoleName', title: '角色名称', align: 'center', valign: 'middle', width: 200, sortable: true }
        ],
        url: '/SystemSet/GetTreeRoleInfoStr',
        uniqueId: "RoleID",
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
            roleRowEdit(row.RoleID, row.RoleName, false);//显示该行相应的数据
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
    $("#roleId").val("");
    $("#roleName").val("");
    //默认取消所有权限功能
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.checkAllNodes(false);

    $("#roleId").removeAttr("disabled");
    $("#roleName").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}


function SetEditMode() {
    $("#roleId").removeAttr("disabled");
    $("#roleName").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}

function CloseEditMode() {
    $("#roleId").attr("disabled", "disabled");
    $("#roleName").attr("disabled", "disabled");
    $("#btnSave").attr("disabled", "disabled");
}


function btnSaveRole() {
    if ($("#roleId").val() == "") {
        alert("对不起，ID号不能为空");
        return false;
    }
    if ($("#roleName").val() == "") {
        alert("对不起，名称不能为空");
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
        $.post("/SystemSet/SaveRoleInfo", { "RoleID": $("#roleId").val(), "RoleName": $("#roleName").val(), "RoleMenuIds": selectRoleMenuIds }, function (data, status) {
            if ("success" == status) {
                var words = data.split("|");
                if (words[2] == "Add") {
                    $('#tb').bootstrapTable('append', [
                        {
                            RoleID: $("#roleId").val(),
                            RoleName: $("#roleName").val()
                        }
                    ]);
                } else {
                    var allTableData = $("#tb").bootstrapTable('getData');
                    for(var i=0;i<allTableData.length;i++){
                        if (allTableData[i].RoleID == $("#roleId").val()) {
                            $('#tb').bootstrapTable('updateRow', {
                                index: i,
                                row: {
                                    RoleID: $("#roleId").val(),
                                    RoleName: $("#roleName").val()
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

function roleRowEdit(roleId,roleName,flag) {
    $("#roleId").val(roleId);
    $("#roleName").val(roleName);
    if (flag == true) {
        $("#roleId").removeAttr("disabled");
        $("#roleName").removeAttr("disabled");
        $("#btnSave").removeAttr("disabled");
    } else {
        $("#roleId").attr("disabled", "disabled");
        $("#roleName").attr("disabled", "disabled");
        $("#btnSave").attr("disabled", "disabled");
    }
    var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    zTree.checkAllNodes(false);
    nodes = zTree.getCheckedNodes(false);

    
    $.post("/SystemSet/GetRoleMenu", { "RoleID": roleId }, function (data,status) {
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
    $

    if ($("#roleId").val()=="") {
        alert("要删除的ID不能为空");
        return false;
    }
    if ($("#roleName").val() == "") {
        alert("要删除的角色名称不能为空");
        return false;
    }
    if (confirm("确定删除该角色吗?")) {
        //点击确定后操作
        $.post("/SystemSet/DeleteRoleInfo", { "RoleID": $("#roleId").val() }, function (data, status) {
            if ("success" == status) {                
                var words = data.split("|");
                if ("OK" == words[0]) {
                    $('#tb').bootstrapTable('removeByUniqueId', $("#roleId").val());
                }
                alert(data);
            }
        });
    }
    
}
