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

var cyTypes;
var crimes;
var areas;
var areasStr;
var myExp;
var mediaObj;
var selectedRow;
$(document).ready(function () {

    $.post("/BaseInfo/GetCyTypeInfo", { "DT": new Date() }, function (data, status) {
        cyTypes = data;
        //装载角色的Select控件
        for (var i = 0; i < cyTypes.length; i++) {
            $("#FCYCode").append("<option value='" + cyTypes[i].FCode + "'>" + cyTypes[i].FName + "</option>")
        }
    });
    $.post("/BaseInfo/GetAreaInfo", { "DT": new Date() }, function (data, status) {
        areas = data;
        //装载角色的Select控件
        for (var i = 0; i < areas.length; i++) {
            $("#FAreaCode").append("<option value='" + areas[i].FCode + "'>" + areas[i].FName + "</option>")
        }
    });
    $.post("/BaseInfo/GetCrimeInfo", { "DT": new Date() }, function (data, status) {
        crimes = data;
        //装载角色的Select控件
        for (var i = 0; i < crimes.length; i++) {
            $("#FCrimeCode").append("<option value='" + crimes[i].FCode + "'>" + crimes[i].FName + "</option>")
        }
    });

    //日期控件
    $('.some_class').datetimepicker();
    myExp = myexplorer();
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


//获取IE myexplorer()  已经改定义到commonJs里了


function btnSearch() {
    $.post("/RecordMgr/GetRecordInfo", {
        "FName": $("#FName").val()
        , "FCode": $("#FCode").val()
        , "FAreaCode": $("#FAreaCode").val()
        , "startTime": $("#startTime").val()
        , "endTime": $("#endTime").val()
    }, function (data, status) {
        if ("success" == status) {
            if (data.length != 0) {
                $('#tb').bootstrapTable('load', data);
            } else {
                $('#tb').bootstrapTable('removeAll');
                alert("对不起，没有相应条件的录音数据");
            }

        }
    })
}