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

var meetTypes;
var areas;
var areasStr;

$(document).ready(function () {

    $.post("/BaseInfo/GetMeetTypeInfo", { "DT": new Date() }, function (data, status) {
        meetTypes = data;
        //装载角色的Select控件
        for (var i = 0; i < meetTypes.length; i++) {
            $("#fmtype").append("<option value='" + meetTypes[i].FCode + "'>" + meetTypes[i].FName + "</option>")
        }
    });


    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'FCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '名称', align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'flag', title: '默认', align: 'center', valign: 'middle', width: 200, sortable: true
            },
            { field: 'Mdays', title: '会见间隔天数', align: 'center', valign: 'middle', width: 100, sortable: true },
            {
                field: 'fmtype', title: '会见类型', align: 'center', valign: 'middle', width: 200, sortable: true, formatter: function (value, row, index) {
                    for (var i = 0; i < meetTypes.length; i++) {
                        if (value == meetTypes[i].FCode) {
                            return meetTypes[i].FName;
                        }
                    }
                }
            }
            //,{ field: 'URL', title: 'URL地址', align: 'center', valign: 'middle', width: 200, sortable: true, visible: true }

        ],
        url: "/BaseInfo/GetCyTypeInfoStr",
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



function btnAddRole() {
    $("#FCode").val("");
    $("#FName").val("");
    $("#flag").val("");
    $("#Mdays").val("");
    $("#fmtype").val(-1);
    //默认取消所有权限功能
    //var zTree = $.fn.zTree.getZTreeObj("treeDemo");
    //zTree.checkAllNodes(false);

    $("#FCode").removeAttr("disabled");
    $("#FName").removeAttr("disabled");
    $("#flag").removeAttr("disabled");
    $("#Mdays").removeAttr("disabled");
    $("#fmtype").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}


function SetEditMode() {
    $("#FCode").removeAttr("disabled");
    $("#FName").removeAttr("disabled");
    $("#flag").removeAttr("disabled");
    $("#Mdays").removeAttr("disabled");
    $("#fmtype").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");
}

function CloseEditMode() {
    $("#FCode").attr("disabled", "disabled");
    $("#FName").attr("disabled", "disabled");
    $("#flag").attr("disabled", "disabled");
    $("#fmtype").attr("disabled", "disabled");
    $("#Mdays").attr("disabled", "disabled");
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
    if ($("#Mdays").val() == "") {
        alert("对不起，会见间隔天数不能为空");
        return false;
    }

    $.post("/BaseInfo/SaveCyTypeInfo", {
        "FCode": $("#FCode").val()
            , "FName": $("#FName").val()
            , "flag": $("#flag").val()
            , "Mdays": $("#Mdays").val()
            , "fmtype": $("#fmtype").val()
        //, "UserMgrAreas": selectRoleMenuIds
    }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[2] == "Add") {
                $('#tb').bootstrapTable('append', [
                    {
                        FCode: $("#FCode").val(),
                        FName: $("#FName").val(),
                        flag: $("#flag").val(),
                        Mdays: $("#Mdays").val(),
                        fmtype: $("#fmtype").val()
                    }
                ]);
            } else {
                var allTableData = $("#tb").bootstrapTable('getData');
                for (var i = 0; i < allTableData.length; i++) {
                    if (allTableData[i].FCode == $("#FCode").val()) {
                        $('#tb').bootstrapTable('updateRow', {
                            index: i,
                            row: {
                                FCode: $("#FCode").val(),
                                FName: $("#FName").val(),
                                flag: $("#flag").val(),
                                Mdays: $("#Mdays").val(),
                                fmtype: $("#fmtype").val()
                            }
                        });
                    }
                }

            }
            CloseEditMode();
            alert(words[1]);
        }
    });
}

function roleRowEdit(row,flag) {
    $("#FCode").val(row.FCode);
    $("#FName").val(row.FName);
    $("#flag").val(row.flag);
    $("#Mdays").val(row.Mdays);
    //通过文本方式设定选中项;
    //$("#FID").find("option:contains('" + row.FID + "')").attr("selected", true);
    //通过数值方式设定选中项;
    $("#fmtype").val(row.fmtype);

    if (flag == true) {
        $("#FCode").removeAttr("disabled");
        $("#FName").removeAttr("disabled");
        $("#flag").removeAttr("disabled");
        $("#Mdays").removeAttr("disabled");
        $("#fmtype").removeAttr("disabled");
        $("#btnSave").removeAttr("disabled");
    } else {
        $("#FCode").attr("disabled", "disabled");
        $("#FName").attr("disabled", "disabled");
        $("#flag").attr("disabled", "disabled");
        $("#Mdays").attr("disabled", "disabled");
        $("#fmtype").attr("disabled", "disabled");
        $("#btnSave").attr("disabled", "disabled");
    }
    
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
        $.post("/BaseInfo/DeleteCyTypeInfo", { "FCode": $("#FCode").val(), "FName": $("#FName").val() }, function (data, status) {
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
