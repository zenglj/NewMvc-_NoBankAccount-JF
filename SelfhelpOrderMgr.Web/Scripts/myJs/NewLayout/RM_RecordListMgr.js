
$(document).ready(function () {


    var $table=$('#tb').bootstrapTable({
        columns: [
            { field: 'FCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FName', title: '名称', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'CrimeTypeName', title: '犯罪名称', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FAreaName', title: '监区', align: 'center', valign: 'middle', width: 150, sortable: true },
            { field: 'FMeetType', title: '会见类型', align: 'center', valign: 'middle', width: 100, sortable: true },
            
            { field: 'FMeetRoom', title: '会见室', align: 'center', valign: 'middle', width: 100, sortable: true},
            { field: 'PNO', title: '批次', align: 'center', valign: 'middle', width: 80, sortable: true}, 
            { field: 'WNO', title: '窗口', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FILEPATH', title: '录音文件', align: 'center', valign: 'middle', width: 200, sortable: true},
            {
                field: 'STARTTIME', title: '会见时间', align: 'center', valign: 'middle', width: 150, sortable: true, formatter: function (value, row, index) {
                    if (value !== "" || value != null) {
                        return getLongTime(value);
                    }
                }
            },
            { field: 'family', title: '会见亲属', align: 'center', valign: 'middle', width: 300, sortable: true},
            { field: 'readcount', title: '查听次数', align: 'center', valign: 'middle', width: 100, sortable: true }
            ,{ field: 'remark', title: '备注', align: 'center', valign: 'middle', width: 300, sortable: true }

        ],
        url: "/RecordMgr/GetRecordInfoStr",
        uniqueId: "FCode",
        toolbar:"#toolbar",
        search:true,
        striped: true,
        pagination: true,
        height: 556,
        pageSize: 10,                       //每页的记录行数（*）
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
            $("#btnDown").attr("href", recordpath + recName);
            $("#strFCrimeCode").val(row.FCode);
            $("#readCountId").val("");
            selectedRow = row;            
        }

    });


    //点击选中行，改变选中行的背景颜色
    $('#tb').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });

    $('#myModal').on('hide.bs.modal',
    function () {
        ClosePlayWin();//关闭模态窗口
    })

});



//开始播放录音
function btnPlayRecord() {
    if (selectedRow == null) {
        alert("请先选择一条录音，谢谢！");
        return false;
    }
    var row = selectedRow;
    var recName = "/" + getShortTime(row.STARTTIME) + "/" + row.WNO + "/" + row.FILEPATH + ".wav";

    LoadRecPlus(recName, row, 150);
    SaveRecordList('save');
}


//关闭录音窗口
function ClosePlayWin() {
    //mediaObj = null;
    SaveRecordList('close');
    if (myExp.substr(0, 2) == "IE") {
        var WMP = this.document.getElementById("mp");
        WMP.controls.stop;
    }    
    $("#loadPlay").empty();
    $("#readCountId").val("");
    //$('#myModal').modal('hide');

}

//保存录音记录
function SaveRecordList(action) {
    if ($("#recordFileName").val() == "") {
        alert("录音文件名不能为空");
        return false;
    }
    $.post("/RecordMgr/InsertPlayRecList", {
        "FCode": $("#strFCrimeCode").val(),
        "FilePath": $("#recordFileName").val(),
        "readTime": $("#strReadDate").val(),
        "remarkText": $("#remarkText").val(),
        "readCountId": $("#readCountId").val()
    }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] != "OK") {
                alert(data);
            } else {
                $("#readCountId").val(words[2]);
            }
        }
    });
}

function SetEditMode() {
    $("#FCode").removeAttr("disabled");
    $("#FName").removeAttr("disabled");
    $("#FSex").removeAttr("disabled");
    $("#FCYCode").removeAttr("disabled");
    $("#FAreaCode").removeAttr("disabled");
    $("#FCrimeCode").removeAttr("disabled");
    $("#FTerm").removeAttr("disabled");
    $("#FIdenNo").removeAttr("disabled");
    $("#FAddr").removeAttr("disabled");
    $("#fflag").removeAttr("disabled");
    $("#FDesc").removeAttr("disabled");
    $("#FInDate").removeAttr("disabled");
    $("#FOuDate").removeAttr("disabled");
    $("#btnSave").removeAttr("disabled");;
}

function CloseEditMode() {
    $("#FCode").attr("disabled", "disabled");
    $("#FName").attr("disabled", "disabled");
    $("#FSex").attr("disabled", "disabled");
    $("#FCYCode").attr("disabled", "disabled");
    $("#FAreaCode").attr("disabled", "disabled");
    $("#FCrimeCode").attr("disabled", "disabled");
    $("#FTerm").attr("disabled", "disabled");
    $("#FIdenNo").attr("disabled", "disabled");
    $("#FAddr").attr("disabled", "disabled");
    $("#fflag").attr("disabled", "disabled");
    $("#FDesc").attr("disabled", "disabled");
    $("#FInDate").attr("disabled", "disabled");
    $("#FOuDate").attr("disabled", "disabled");
    $("#btnSave").attr("disabled", "disabled");
}



//加载MediaPlay插件(录音录像文件,文件名，会见窗口,播放窗口的高度)
function LoadRecPlus(recName, row, hgt) {
    $("#loadPlay").empty();
    $("#recordFileName").val(row.FILEPATH);
    if ("南区" == row.FMeetRoom) {
        var recordpath = $("#txtRecordPath").val();
    } else {
        var recordpath = $("#txtRecordPathNorth").val();
    }
    var now = new Date();
    //var nowStr = now.format("yyyy-MM-dd hh:mm:ss");
    var nowStr = getDateLongTime(now);
    $("#strReadDate").val(nowStr);
    //GetRemarkText()//获取备注信息
    $("#remarkText").val(row.remark);
    
    //alert(myExp.substr(0, 2));

    var ff = recordpath + recName;
    //ff = "/rec/Spoonful.mp3";
    //ff = "http://localhost:8066/rec/Spoonful.mp3";
    //ff = "http://localhost:8066/rec/20130425093503欧圣炎.wav";
    //embed  object
    mediaObj = "<object classid='CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6' height='" + hgt + "' width='100%' id='mp'>"
            + "<param name='AudioStream' value='-1'>"
            + "<param name='AutoSize' value='-1'>"
            + "<param name='AutoStart' value='-1'>"//<!–是否自动调整播放大小–>
            + "<param name='AnimationAtStart' value='-1'>"//<!–是否自动播放–>
            + "<param name='AllowScan' value='-1'>"//新加的
            + "<param name='AllowChangeDisplaySize' value='-1'>"//新加的
            + "<param NAME='Balance' VALUE='0'>"             //<!--调整左右声道平衡,同上面旧播放器代码-->
            + "<param name='enabled' value='-1'>"            //<!--播放器是否可人为控制-->
            + "<param NAME='EnableContextMenu' VALUE='-1'>"  //<!--是否启用上下文菜单--> 
            + "<param NAME='url' VALUE='" + ff + "'>"          //<!--播放的文件地址-->
            + "<param NAME='PlayCount' VALUE='1'>"           //<!--播放次数控制,为整数-->
            + "<param name='Rate' value='1'>"                //<!--播放速率控制,1为正常,允许小数,1.0-2.0-->
            + "<param name='CurrentPosition' value='0'>"     //<!--控件设置:当前位置-->
            + "<param name='currentMarker' value='0'>"       //<!--控件设置:当前标记-->
            + "<param name='defaultFrame' value=''>"         //<!--显示默认框架-->
            + "<param name='invokeURLs' value='-1'>"         //<!--脚本命令设置:是否调用URL-->
            + "<param name='baseURL' value=''>"              //<!--脚本命令设置:被调用的URL-->
            + "<param name='stretchToFit' value='0'>"        //<!--是否按比例伸展--> 
            + "<param name='volume' value='80'>"             //<!--默认声音大小0%-100%,50则为50%-->
            + "<param name='mute' value='0'>"                //<!--是否静音-->
            + "<param name='uiMode' value='full'>"           //<!--播放器显示模式:Full显示全部;mini最简化;None不显示播放控制,只显示视频窗口;invisible全部不显示-->
            + "<param name='windowlessVideo' value='0'>"     //<!--如果是0可以允许全屏,否则只能在窗口中查看--> 
            + "<param name='fullScreen' value='0'>"          //<!--开始播放是否自动全屏-->
            + "<param name='enableErrorDialogs' value='-1'>" //<!--是否启用错误提示报告--> 
            + "<param name='SAMIStyle' value>"               //<!--SAMI样式-->
            + "<param name='SAMILang' value>"                //<!--SAMI语言-->
            + "<param name='SAMIFilename' value>"            //<!--字幕ID--> 
            + "</object>";
    
    //media = $(strobject);

    if (myExp.substr(0, 2) == "IE") {
        $("#loadPlay").append(mediaObj);
    } else {
        window.open(ff);
    }
}


//获取录音文件备注
function GetRemarkText() {
    var fname = $("#recordFileName").val();
    //alert(fname);
    $.post("/RecordMgr/GetRemarkText", {"recName": fname }, function (data, status) {
        //alert("ddd");
        if ("success" == status) {
            if ("" != data) {
                $("#remarkText").val(data);
            } else {
                $("#remarkText").val("");
            }
        } else {
            $("#remarkText").val("");
        }
    });
}

