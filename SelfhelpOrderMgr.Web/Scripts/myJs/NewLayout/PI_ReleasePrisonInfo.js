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
var selectPrisonVocRow;//所选的狱情文件
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



    var $table = $('#tbvoc').bootstrapTable({
        columns: [
            { field: 'Remark', title: '狱情信息', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'VocName', title: '文件位置', align: 'center', valign: 'middle', width: 150, sortable: true }
        ],
        url: "/PrisonInfo/GetPrisonVocsStr",
        uniqueId: "Remark",
        //toolbar: "#toolbar",
        //search: true,
        striped: true,
        //pagination: true,
        height: 196,
        //pageSize: 5,                       //每页的记录行数（*）
        //pageList: [5, 10, 25, 50, 100],        //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        //showExport: true,                     //是否显示导出
        //exportDataType: "all",              //basic', 'all', 'selected'.
        onClickRow: function (row) {
            //roleRowEdit(row, false);//显示该行相应的数据
            selectPrisonVocRow = row;
        }

    });



    var $table = $('#tb').bootstrapTable({
        columns: [
            { field: 'FCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '名称', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FAreaName', title: '监区', align: 'center', valign: 'middle', width: 150, sortable: true },
            { field: 'readcount', title: '查听次数', align: 'center', titleTooltip: '录音复听回放次数', valign: 'middle', width: 60, sortable: true },
            { field: 'InsertVocFlag', title: '演练标志', titleTooltip: '演练标志', align: 'center', valign: 'middle', width: 60, sortable: true },
            { field: 'InsertVocArea', title: '录音位置', titleTooltip: '插入录音具体位置', align: 'center', valign: 'middle', width: 60, sortable: true },
            { field: 'InsertVocText', title: '演练内容', titleTooltip: '演练录音内容', align: 'center', valign: 'middle', width: 150, sortable: true },
            { field: 'remark', title: '复听情况内容摘要说明', align: 'left', valign: 'middle', width: 300, sortable: true },
            { field: 'FMeetType', title: '会见类型', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FMeetRoom', title: '会见室', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'PNO', title: '批次', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'WNO', title: '窗口', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'SEQ', title: '序号', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FILEPATH', title: '录音文件', align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'STARTTIME', title: '会见时间', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLongTime(value);
                    }
                }
            },
            { field: 'family', title: '会见亲属', align: 'center', valign: 'middle', width: 300, sortable: true }

        ],
        url: "/RecordMgr/GetRecordInfoStr",
        uniqueId: "SEQ",
        toolbar: "#toolbar",
        search: true,
        striped: true,
        pagination: true,
        height: 366,
        pageSize: 5,                       //每页的记录行数（*）
        pageList: [5, 10, 25, 50, 100],        //可供选择的每页的行数（*）
        clickToSelect: true,                //是否启用点击选中行
        showExport: true,                     //是否显示导出
        exportDataType: "all",              //basic', 'all', 'selected'.
        onClickRow: function (row) {
            //alert(row.RoleName);
            //roleRowEdit(row, false);//显示该行相应的数据
            //LoadRecPlus(recName, selected.FilePath, selected.MeetRoom, 150);
            $("#recordFileName").val(row.FILEPATH);
            if ("南区" == row.FMeetRoom) {
                var recordpath = $("#txtRecordPath").val();
            } else {
                var recordpath = $("#txtRecordPathNorth").val();
            }
            var recName = "/" + getShortTime(row.STARTTIME) + "/" + row.WNO + "/" + row.FILEPATH + ".wav";
            $("#recFilePathName").val(recordpath + recName);
            $("#recSeqno").val(row.SEQ);
            selectedRow = row;
        }

    });


    //点击选中行，改变选中行的背景颜色
    $('.table').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });

    $('#myModal').on('show.bs.modal',
    function () {
        CheckIsSelectFlag();
        if (selectedRow.InsertVocFlag == "1") {
            alert("该录音记录已经插入了一个狱情文件，不必重复操作");
            $('#myModal').modal('hide');
            return false;
        }
    })


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


//获取IE
function myexplorer() {
    var explorer = window.navigator.userAgent;
    if (explorer.indexOf("QQBrowser") >= 0 || explorer.indexOf("QQ") >= 0) {
        return myexplorer = "腾讯QQ";
    } else if (explorer.indexOf("Safari") >= 0 && explorer.indexOf("MetaSr") >= 0) {
        return myexplorer = "搜狗";
    } else if (!!window.ActiveXObject || "ActiveXObject" in window) {//IE
        if (!window.XMLHttpRequest) {
            return myexplorer = "IE6";
        } else if (window.XMLHttpRequest && !document.documentMode) {
            return myexplorer = "IE7";
        } else if (!-[1, ] && document.documentMode && !("msDoNotTrack" in window.navigator)) {
            return myexplorer = "IE8";
        } else {//IE9 10 11
            var hasStrictMode = (function () { "use strict"; return this === undefined; }());
            if (hasStrictMode) {
                if (!!window.attachEvent) { return myexplorer = "IE10"; } else { return myexplorer = "IE11"; }
            } else {
                return myexplorer = "IE9";
            }
        }
    } else {//非IE
        if (explorer.indexOf("LBBROWSER") >= 0) {
            return myexplorer = "猎豹";
        } else if (explorer.indexOf("360ee") >= 0) {
            return myexplorer = "360极速浏览器";
        } else if (explorer.indexOf("360se") >= 0) {
            return myexplorer = "360安全浏览器";
        } else if (explorer.indexOf("se") >= 0) {
            return myexplorer = "搜狗浏览器";
        } else if (explorer.indexOf("aoyou") >= 0) {
            return myexplorer = "遨游浏览器";
        } else if (explorer.indexOf("qqbrowser") >= 0) {
            return myexplorer = "QQ浏览器";
        } else if (explorer.indexOf("baidu") >= 0) {
            return myexplorer = "百度浏览器";
        } else if (explorer.indexOf("Firefox") >= 0) {
            return myexplorer = "火狐";
        } else if (explorer.indexOf("Maxthon") >= 0) {
            return myexplorer = "遨游";
        } else if (explorer.indexOf("Chrome") >= 0) {
            return myexplorer = "谷歌（或360伪装）";
        } else if (explorer.indexOf("Opera") >= 0) {
            return myexplorer = "欧朋";
        } else if (explorer.indexOf("TheWorld") >= 0) {
            return myexplorer = "世界之窗";
        } else if (explorer.indexOf("Safari") >= 0) {
            return myexplorer = "苹果";

        } else {
            return myexplorer = "其他";
        }
    }

}

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

function SaveInsertVoc() {
    CheckIsSelectFlag();
    if (selectPrisonVocRow == null) {
        alert("请选择一狱情文件").val();
        return false;
    }
    $.post("/PrisonInfo/SaveInsertVoc", {
        //"FileName": $("#recFilePathName").val()
        "FileName": "http://localhost:8066/rec/20171106170740陈昌星.wav"
        , "recSeqno": $("#recSeqno").val()
        , "VocText": selectPrisonVocRow.Remark
        , "VocName": selectPrisonVocRow.VocName
    }
        , function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                var allTableData = $("#tb").bootstrapTable('getData');
                var recinfo = $.parseJSON(words[2]);
                for (var i = 0; i < allTableData.length; i++) {
                    if (allTableData[i].SEQ == recinfo.SEQ) {
                        $('#tb').bootstrapTable('updateRow', {
                            index: i,
                            row: {
                                InsertVocFlag: recinfo.InsertVocFlag,
                                InsertVocArea: recinfo.InsertVocArea,
                                InsertVocText: recinfo.InsertVocText
                            }
                        });
                    }
                }
                $('#myModal').modal('hide');
                alert(words[1]);
            } else {
                alert(data);
            }
        }
    });
}

//判断是否选择好了录音文件
function CheckIsSelectFlag() {
    if ($("#recSeqno").val() == "") {
        alert("请选择一录音文件").val();
        return false;
    }
    if ($("#recSeqno").val() != selectedRow.SEQ) {
        alert("请重新选择一个录音文件").val();
        return false;
    }
    if ($("#recFilePathName").val() == "") {
        alert("无法查看录文件，请重新选择").val();
        return false;
    }
}