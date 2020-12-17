﻿var meetChecked = 0;
function getCheckMeetFlag() {
    var checkMeetFlag = $('#checkMeetFlag').is(':checked');
    meetChecked = 0;
    if (true == checkMeetFlag) {
        meetChecked = 1;
    }
}


function checkMe(id) {
    //alert(e);
    var flag = $('#' + id).is(':checked');

    //if ("true" == flag) {
    //    $.post("/DoorCheck/SetMeetIsKeyCriminal", { "FReqCode": id }, function (data, status) {
    //        if ("success" == status) {
    //            alert(data);
    //        }
    //    });
    //}
    var checked = 0;
    if (true == flag) {
        checked = 1;
    }

    getCheckMeetFlag();

    $.post("/DoorCheck/SetMeetIsKeyCriminal", { "FReqCode": id, "Flag": checked, "meetChecked": meetChecked }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] != "OK") {
                alert(data);
            }
        }
    });
}

function selectMe(id) {
    //alert(e);
    var flag = $('#' + id).val();


    $.post("/DoorCheck/SetMeetIsKeyCriminal", { "FReqCode": id, "Flag": flag, "meetChecked": meetChecked }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] != "OK") {
                alert(data);
            }
        }
    });
}

function reSortWin(n) {

    getCheckMeetFlag();

    $.post("/DoorCheck/ReSortWinByPC", { "PC": n, "checkMeetFlag": meetChecked }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                var meets = $.parseJSON(words[1]);
                $("#nav_tabs" + n).empty();
                var div = "";
                div = div + "    <div class='panel panel-default'>"
                    + "        <div class='panel-heading'>第" + meets[0].FPC + "批会见人员名单,共" + meets.length + "人  <button type=\"button\" class=\"btn btn-primary\" onclick=\"reSortWin(" + meets[0].FPC + ")\">重排窗口号</button><a name='exe' href='#' type=\"button\" class=\"btn btn-danger\" onclick=\"GetSpeechFileByPC(" + meets[0].FPC + ")\">语音播报</a>"
                        + "<button type=\"button\" class=\"btn btn-primary\" onclick=\"BeginStartCheckFIdenNo(" + meets[0].FPC + ")\">开始验证</button>"
                        + "    <button type=\"button\" class=\"btn btn-default\" onclick=\"CloseCheckFIdenNo()\">停止验证</button><button type=\"button\" class=\"btn btn-success\" onclick=\"printMeetOrder(" + meets[0].FPC + ")\">打印单据</button> <label id='lbl_ID_" + meets[0].FPC + "'></label></div>"
                    + "        <div class='panel-body'>"
                    + "            <table class='table table-striped'>"
                    + "                <thead>"
                    + "                    <tr>"
                    + "                        <th>编号</th>"
                    + "                        <th>姓名</th>"
                    + "                        <th>队别</th>"
                    + "                        <th>罪名</th>"
                    + "                        <th>是否重点</th>"
                    + "                        <th>批次</th>"
                    + "                        <th>窗口</th>"
                    + "                        <th>会见类型</th>"
                    + "                        <th>会见家属</th>"
                    + "                        <th>操作</th>"
                    + "                    </tr>"
                    + "                </thead>"
                    + "                <tbody>";
                var tr = "";
                for (var j = 0; j < meets.length; j++) {
                    var meet = meets[j];
                    var checked = "";
                    var option = "";
                    if (meet.FKeyFcrimeFlag == 1) {
                        checked = "checked='checked'";
                        option = "<option value=\"0\">普通</option><option value=\"1\" selected=\"selected\">重点</option><option value=\"2\">艾感</option>";
                    } else if (meet.FKeyFcrimeFlag == 2) {
                        option = "<option value=\"0\">普通</option><option value=\"1\">重点</option><option value=\"2\" selected=\"selected\">艾感</option>";
                    } else {
                        option = "<option value=\"0\" selected=\"selected\">普通</option><option value=\"1\">重点</option><option value=\"2\">艾感</option>";
                    }
                    tr = tr + "<tr><td>" + meet.FCCode + "</td><td>" + meet.FCrimeName + "</td><td>" + meet.FAreaCode + "</td><td>" + meet.CrimeTypeName + "</td>"
                                //+ "<td><input type='checkbox' id='" + meet.FReqCode + "' " + checked + " onchange=\"checkMe('" + meet.FReqCode + "')\"></td>"
                                + "<td> <select id='" + meet.FReqCode + "' onchange=\"selectMe('" + meet.FReqCode + "')\">" + option + "</select> </td> "
                                + "<td>" + meet.FPC + "</td><td>" + meet.FMeetSeat + "</td><td>" + meet.FMeetType + "</td><td>" + meet.FamilyNames + "</td>"
                                + "<td><a name='exe' type=\"button\" class=\"btn btn-danger\" onclick=\"speechTTS('请" + meet.FCrimeName + "的家属马上到" + meet.FMeetSeat + "号窗口接见，请" + meet.FCrimeName + "的家属马上到" + meet.FMeetSeat + "号窗口接见')\">叫号</a>"
                                + "<a type=\"button\" class=\"btn btn-primary\" onclick=\"DelMeetOrder('" + meet.FReqCode + "')\">删除</a>"
                                + "<a type=\"button\" class=\"btn btn-default\" onclick=\"ChangePCNo('" + meet.FReqCode + "')\">改批</a></td></tr>";
                }
                div = div + tr + "</tbody></table></div></div>";

                $("#nav_tabs" + n).append(div);
                alert("成功重排窗口号");
            } else {
                alert(data);
            }

        }
    });
}

function GetSpeechFileByPC(n) {
    $.post("/DoorCheck/GetSpeechFileByPC", { "PC": n }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                //("#recordFileName").val(words[1]);
                //btnPlayRecord(words[1]);//Web 方式播放
                speechTTS("现在马上要会见进场验证了，" + words[1]);//本地播放
            } else {
                $("#recordFileName").val("");
                alert(data);
            }

        }
    });
}


//开始播放录音
function btnPlayRecord(recordFileName) {
    if (recordFileName == "") {
        alert("没有语音文件无法播报，谢谢！");
        return false;
    }
    var row = selectedRow;
    var recName = "~/" + recordFileName;

    LoadRecPlus(recName, row, 150);
}

//加载MediaPlay插件(录音录像文件,文件名，会见窗口,播放窗口的高度)
function LoadRecPlus(recName, hgt) {
    $("#loadPlay").empty();
    var ff = recName;
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


$(function () {

    loadNotMeetedList();
    initTableSelectMeets();
    initLoad();

});

var selectDataMeets = [{
                id: 1,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 2,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 3,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: ''
            }, {
                id: 4,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 5,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 6,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 7,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 8,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 9,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 10,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 11,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 12,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 13,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 14,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 15,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 16,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 17,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 18,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: ''
            }, {
                id: 19,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 20,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 21,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 22,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 23,
                FReqCode: '',
                FCCode: '',
                FCrimeName: '',
                FAreaCode: '',
                FPC: ""
            }, {
                id: 24,
                FReqCode: '',
                FCCode:'',
                FCrimeName: '',
                FAreaCode: '',
                FPC:""
            }
];


var selectCurrMeetRow;
var removeSelectRow;
//初始加载
function initLoad() {
    $.post("/DoorCheck/GetTodayPCList", {"DT":new Date()}, function (data, status) {
        if ("success" == status) {
            //alert(data);
            var words = data.split("|");
            if ("OK" == words[0]) {
                var pclists = $.parseJSON(words[1]);
                $("#meetList").empty();
                var ul = "<ul class='nav nav-tabs' role='tablist'>";
                //+"<li role='presentation' class='active'><a href='#home' aria-controls='home' role='tab' data-toggle='tab'>Home</a></li>"
                //+"<li role='presentation'><a href='#profile' aria-controls='profile' role='tab' data-toggle='tab'>Profile</a></li>"
                //+"<li role='presentation'><a href='#messages' aria-controls='messages' role='tab data-toggle='tab'>Messages</a></li>";
                var li = "";
                for (var i = 0; i < pclists.length; i++) {
                    //console.log(pclists[i].pc.FPC);
                    if (pclists[i].pc.FPC>0) {
                        if (li == "") {
                            var li = "<li role='presentation' class='active'><a href='#nav_tabs" + pclists[i].pc.FPC + "' aria-controls='nav_tabs" + pclists[i].pc.FPC + "' role='tab' data-toggle='tab'>第" + pclists[i].pc.FPC + "批</a></li>";
                        } else {
                            var li = li + "<li role='presentation'><a href='#nav_tabs" + pclists[i].pc.FPC + "' aria-controls='nav_tabs" + pclists[i].pc.FPC + "' role='tab' data-toggle='tab'>第" + pclists[i].pc.FPC + "批</a></li>";
                        }
                    }                    
                }
                ul = ul + li + "</ul>";
                $("#meetList").append(ul);

                var tab_content = "<div class='tab-content'>";
                var div = "";
                var active = "";
                for (var i = 0; i < pclists.length; i++) {
                    if (pclists[i].pc.FPC > 0) {
                        if (div == "") {
                            active = "active";
                        }
                        div = div + "<div role='tabpanel' class='tab-pane " + active + "' id=\"nav_tabs" + pclists[i].pc.FPC + "\">"
                            + "    <div class='panel panel-default'>"
                            + "        <div class='panel-heading'>第" + pclists[i].pc.FPC + "批会见人员名单,共" + pclists[i].pc.pcCount + "人  <button type=\"button\" class=\"btn btn-primary\" onclick=\"reSortWin(" + pclists[i].pc.FPC + ")\">重排窗口号</button><a name='exe' href='#' type=\"button\" class=\"btn btn-danger\" onclick=\"GetSpeechFileByPC(" + pclists[i].pc.FPC + ")\">语音播报</a>"
                            + "<button type=\"button\" class=\"btn btn-primary\" onclick=\"BeginStartCheckFIdenNo(" + pclists[i].pc.FPC + ")\">开始验证</button>"
                            + "    <button type=\"button\" class=\"btn btn-default\" onclick=\"CloseCheckFIdenNo()\">停止验证</button><button type=\"button\" class=\"btn btn-success\" onclick=\"printMeetOrder(" + pclists[i].pc.FPC + ")\">打印单据</button> <label id='lbl_ID_" + pclists[i].pc.FPC + "'></label></div>"
                            + "        <div class='panel-body'>"
                            + "            <table class='table table-striped'>"
                            + "                <thead>"
                            + "                    <tr>"
                            + "                        <th>编号</th>"
                            + "                        <th>姓名</th>"
                            + "                        <th>队别</th>"
                            + "                        <th>罪名</th>"
                            + "                        <th>是否重点</th>"
                            + "                        <th>批次</th>"
                            + "                        <th>窗口</th>"
                            + "                        <th>会见类型</th>"
                            + "                        <th>会见家属</th>"
                            + "                        <th>操作</th>"
                            + "                    </tr>"
                            + "                </thead>"
                            + "                <tbody>";
                        var tr = "";
                        for (var j = 0; j < pclists[i].meets.length; j++) {
                            var meet = pclists[i].meets[j];
                            var checked = "";
                            var option = "";
                            if (meet.FKeyFcrimeFlag == 1) {
                                checked = "checked='checked'";
                                option = "<option value=\"0\">普通</option><option value=\"1\" selected=\"selected\">重点</option><option value=\"2\">艾感</option>";
                            } else if (meet.FKeyFcrimeFlag == 2) {
                                option = "<option value=\"0\">普通</option><option value=\"1\">重点</option><option value=\"2\" selected=\"selected\">艾感</option>";
                            } else {
                                option = "<option value=\"0\" selected=\"selected\">普通</option><option value=\"1\">重点</option><option value=\"2\">艾感</option>";
                            }
                            tr = tr + "<tr><td>" + meet.FCCode + "</td><td>" + meet.FCrimeName + "</td><td>" + meet.FAreaCode + "</td><td>" + meet.CrimeTypeName + "</td>"
                                       //+ "<td><input type='checkbox' id='" + meet.FReqCode + "' " + checked + " onchange=\"checkMe('" + meet.FReqCode + "')\"></td>"
                                       + "<td> <select id='" + meet.FReqCode + "' onchange=\"selectMe('" + meet.FReqCode + "')\"> " + option + " </select></td> "
                                       + "<td>" + meet.FPC + "</td><td>" + meet.FMeetSeat + "</td><td>" + meet.FMeetType + "</td><td>" + meet.FamilyNames + "</td>"
                                    + "<td><a name='exe' type=\"button\" class=\"btn btn-danger\" onclick=\"speechTTS('请" + meet.FCrimeName + "的家属马上到" + meet.FMeetSeat + "号窗口等候，请" + meet.FCrimeName + "的家属马上到" + meet.FMeetSeat + "号窗口等候')\">叫号</a>"
                                    + "<a type=\"button\" class=\"btn btn-primary\" onclick=\"DelMeetOrder('" + meet.FReqCode + "')\">删除</a>"
                                    + "<a type=\"button\" class=\"btn btn-default\" onclick=\"ChangePCNo('" + meet.FReqCode + "')\">改批</a></td></tr>";
                        }
                        div = div + tr + "</tbody></table></div></div></div>";
                    }
                }
                tab_content = tab_content + div + "</div>";
                $("#meetList").append(tab_content);
            } else {
                $("tab-content").empty();
                $("tab-content").append("<h1>当前暂无会见人员信息</h1>");
            }

        }
    });


    $("#checkMeetFlag").attr("checked", true);//选中验证已经会见过了，不能重排
}

//加载未会见的人员名单
function loadNotMeetedList() {
    var $table = $('#tb').bootstrapTable({
        columns: [
            { field: 'FReqCode', title: '会见单号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FCCode', title: '编号', align: 'center', valign: 'middle', width: 100, sortable: true },
            { field: 'FCrimeName', title: '名称', align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'FAreaCode', title: '队别', align: 'center', valign: 'middle', width: 200, sortable: true
            },
            {
                field: 'FFlag', title: '状态', align: 'center', valign: 'middle', width: 100, sortable: true, formatter: function (value, row, index) {
                    if (value == "1") {
                        return "已会见";
                    } else if (value == "2") {
                        return "已载入";
                    } else {
                        return "未会见";
                    }
                }
            }

        ],
        url: "/DoorCheck/GetTodayNotMeetedList",
        uniqueId: "FReqCode",
        //toolbar: "#toolbar",
        //search: true,
        striped: true,
        pagination: true,
        height: 376,
        pageSize: 24,                       //每页的记录行数（*）
        pageList: [25,50],        //可供选择的每页的行数（*）
        clickToSelect: true                //是否启用点击选中行
        //showExport: true,                     //是否显示导出
        //exportDataType: "all"             //basic', 'all', 'selected'.
        ,onClickRow: function (row) {
            selectCurrMeetRow = row;

        }

    });


    //点击选中行，改变选中行的背景颜色
    $('#tb').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });

}

function initTableSelectMeets() {

    var $table = $('#tbSelectMeets').bootstrapTable({
        columns: [
            { field: 'id', title: '窗口号', align: 'center', valign: 'middle', width: 80, sortable: true },
            { field: 'FReqCode', title: '会见单号', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FCrimeName', title: '姓名', align: 'center', valign: 'middle', width: 120, sortable: true },
            { field: 'FCCode', title: '编号', visible: false, align: 'center', valign: 'middle', width: 200, sortable: true },
            {
                field: 'FAreaCode', title: '队别', align: 'center', valign: 'middle', width: 200, sortable: true
            }
            , { field: 'FPC', title: '批次', visible: false, align: 'center', valign: 'middle', width: 120, sortable: true }
        ],
        data: selectDataMeets,
        uniqueId: "id",
        //toolbar: "#toolbar",
        //search: true,
        striped: true,
        pagination: true,
        height: 396,
        pageSize: 8,                       //每页的记录行数（*）
        pageList: [8, 16, 24],        //可供选择的每页的行数（*）
        clickToSelect: true                //是否启用点击选中行
        //showExport: true,                     //是否显示导出
        //exportDataType: "all"             //basic', 'all', 'selected'.
        , onClickRow: function (row) {
            //alert(row.RoleName);
            //roleRowEdit(row, false);//显示该行相应的数据
            removeSelectRow = row;
        }

    });
    //点击选中行，改变选中行的背景颜色
    $('#tbSelectMeets').on('click-row.bs.table', function (e, row, element) {
        $('.success').removeClass('success');//去除之前选中的行的，选中样式
        $(element).addClass('success');//添加当前选中的 success样式用于区别
        var index = $('#formTempDetailTable_new').find('tr.success').data('index');//获得选中的行的id
    });
}

//加载会见重排窗口
function loadMeetsReOrder() {
    
    $.post("DoorCheck/GetTodayNotMeetedList", {}, function (data, status) {
        if ("success" == status) {
            $('#tb').bootstrapTable('load', $.parseJSON(data));
        }
        
    });
    clearTableAllMeets();
}
//删除会见单号
function DelMeetOrder(orderNo) {
    if (confirm("确定删除该会见单吗?")) {
        $.post("/DoorCheck/DelMeetOrder", { "FReqCode": orderNo }, function (data, status) {
            if (status == "success") {
                var words = data.split("|");
                if (words[0] == "OK") {
                    initLoad();
                }
                alert(data);
            }
        });
    }
}

//修改会见单的批次
function ChangePCNo(orderNo) {
    if (confirm("您确认要进行批次调整吗?")) {

        getCheckMeetFlag();

        var pc = prompt("请输入批次号", "");//将输入的内容赋给变量 name ，

        if (isIntNumber(pc) == false) {
            alert("请输入数字批次号");
            return false;
        }
        $.post("/DoorCheck/ChangePCNo", { "FReqCode": orderNo, "FPC": pc }, function (data, status) {
            if (status == "success") {
                var words = data.split("|");
                if (words[0] == "OK") {
                    initLoad();
                }
                alert(data);
            }
        });
    }

}

//点击开始验证，开始身份证读卡
function BeginStartCheckFIdenNo(pc) {
    myopen_onclick();
    if (document.getElementsByName("tResult")[0].value == "openport成功") {
        beginread_onclick();//开始读卡
        $("#mySelectPC").val(pc);
    }
}


//点击结束验证，停止身份证读卡
function CloseCheckFIdenNo() {
    endread_onclick();//停止读卡
    myclose_onclick();//关闭机具
}

//读身份卡信息
function readCard(cardNo, userName) {
    //alert(userName);
    //cardNo = '350122195702156410';
    if (cardNo != "") {
        if (cardNo != $("#cardNo").val()) {
            $.post("/DoorCheck/FIdenNoCheckWin", { "cardNo": cardNo, "PC": $("#mySelectPC").val() }, function (data, status) {
                if ("success" == status) {
                    var words = data.split("|");
                    if (words[0] == "OK") {
                        //alert(words[1]);
                        if (words[1] != "-1") {
                            $("#lbl_ID_" + $("#mySelectPC").val()).html("----------" + userName + "，会见单，第" + $("#mySelectPC").val() + "批，" + words[1] + "号窗口");
                        } else {
                            $("#lbl_ID_" + $("#mySelectPC").val()).html("----------" + words[2]);

                        }
                        speechTTS(words[2]);//本地播放
                    } else {
                        //alert(data);                        
                    }
                }
            });
            $("#cardNo").val(cardNo);
        }
    }

}



//会见批次排单管理
function joinMeetPC() {

    if ($("#selectpc").val() == "") {
        alert("请选择相应的批次");
        return false;
    }
 
    if (selectCurrMeetRow == null) {
        alert("请选择一条记录");
        return false;
    }
    var currRow = selectCurrMeetRow;
    var allWindsData = $("#tbSelectMeets").bootstrapTable('getData');
    if (removeSelectRow != null) {
        $('#tbSelectMeets').bootstrapTable('updateRow', {
            index: removeSelectRow.id-1,
            row: {
                FReqCode: currRow.FReqCode,
                FCCode: currRow.FCCode,
                FCrimeName: currRow.FCrimeName,
                FAreaCode: currRow.FAreaCode,
                FPC: $("#selectpc").val()
            }
        });
    } else {
        for (var i = 0; i < allWindsData.length; i++) {
            console.info(allWindsData[i].id);
            if (allWindsData[i].FReqCode == '') {
                $('#tbSelectMeets').bootstrapTable('updateRow', {
                    index: i,
                    row: {
                        FReqCode: currRow.FReqCode,
                        FCCode: currRow.FCCode,
                        FCrimeName: currRow.FCrimeName,
                        FAreaCode: currRow.FAreaCode,
                        FPC: $("#selectpc").val()
                    }
                });
                break;
            }
        }
    }
    
    //删除该会见行
    $('#tb').bootstrapTable('removeByUniqueId', selectCurrMeetRow.FReqCode);
    selectCurrMeetRow = null;
    removeSelectRow = null;
}

//清除所有记录
function clearTableAllMeets() {
    var allWindsData = $("#tbSelectMeets").bootstrapTable('getData');
    for (var i = 0; i < allWindsData.length; i++) {
        $('#tbSelectMeets').bootstrapTable('updateRow', {
            index: i,
            row: {
                FReqCode: "",
                FCCode: "",
                FCrimeName: "",
                FAreaCode: "",
                FPC: ""
            }
        });
    }
}

//移去会见批次
function leaveMeetPC() {
    if (removeSelectRow == null) {
        alert("请选择一条已安排的会见记录");
        return false;
    }
    
    $.post("DoorCheck/leaveMeetPcByFReqCode", { "FReqCode": removeSelectRow.FReqCode }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                var allWindsData = $("#tbSelectMeets").bootstrapTable('getData');

                $('#tb').bootstrapTable('append', [{
                    "FReqCode": removeSelectRow.FReqCode,
                    "FCCode": removeSelectRow.FCCode,
                    "FCrimeName": removeSelectRow.FCrimeName,
                    "FAreaCode": removeSelectRow.FAreaCode,
                    "FFlag": "0"
                }]);
                for (var i = 0; i < allWindsData.length; i++) {
                    if (i+1 == removeSelectRow.id) {
                        $('#tbSelectMeets').bootstrapTable('updateRow', {
                            index: i,
                            row: {
                                FReqCode: "",
                                FCCode: "",
                                FCrimeName: "",
                                FAreaCode: "",
                                FPC: ""
                            }
                        });
                    }
                }
                
                removeSelectRow = null;
            } else {
                alert(data);
            }
        }
    });
}

//向上移动窗口
function moveUpMeetPC() {
    if (removeSelectRow.id <=1) {
        alert("已经是最小窗口，不能移动了");
        return false;
    }
    $('#tbSelectMeets').bootstrapTable('updateRow', {
        index: removeSelectRow.id-2,
        row: {
            FReqCode: removeSelectRow.FReqCode,
            FCCode: removeSelectRow.FCCode,
            FCrimeName: removeSelectRow.FCrimeName,
            FAreaCode: removeSelectRow.FAreaCode,
            FPC: removeSelectRow.FPC
        }
    });

    $('#tbSelectMeets').bootstrapTable('updateRow', {
        index: removeSelectRow.id - 1,
        row: {
            FReqCode: "",
            FCCode: "",
            FCrimeName: "",
            FAreaCode: "",
            FPC: ""
        }
    });
    removeSelectRow = null;
}

//向下移动窗口
function moveNextMeetPC() {
    if( removeSelectRow.id>=24){
        alert("已经是最大窗口，不能移动了");
        return false;
    }
    $('#tbSelectMeets').bootstrapTable('updateRow', {
        index: removeSelectRow.id,
        row: {
            FReqCode: removeSelectRow.FReqCode,
            FCCode: removeSelectRow.FCCode,
            FCrimeName: removeSelectRow.FCrimeName,
            FAreaCode: removeSelectRow.FAreaCode,
            FPC: removeSelectRow.FPC
        }
    });

    $('#tbSelectMeets').bootstrapTable('updateRow', {
        index: removeSelectRow.id-1,
        row: {
            FReqCode: "",
            FCCode: "",
            FCrimeName: "",
            FAreaCode: "",
            FPC: ""
        }
    });
    removeSelectRow = null;
}
//保存会见批次安排结果
function savePcMeets() {

    var allWindsData = $("#tbSelectMeets").bootstrapTable('getData');
    var pc = $("#selectpc").val();
    var meets = "";
    for (var i = 0; i < allWindsData.length; i++) {
        if (allWindsData[i].FReqCode != "") {
            if (meets == "") {
                meets = allWindsData[i].FReqCode+"|" +allWindsData[i].id;
            } else {
                meets = meets + "," + allWindsData[i].FReqCode + "|" + allWindsData[i].id;
            }
        }
        
    }

    $.post("/DoorCheck/SavePcMeets", { "PC": pc, "Meets": meets }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if ("OK" == words[0]) {
                alert(data);
                initLoad();
            } else {
                alert(data);
            }
        }
    });
}

//显示所选择批次的会见记录
function getAndDisplayPCList() {
    if ($("#selectpc").val() != "") {
        var pc = $("#selectpc").val();
        clearTableAllMeets();
        $.post("DoorCheck/GetTodayMeetsByPC", { "PC": pc }, function (data,status) {
            if ("success" == status) {
                var words = data.split("|");
                if (words[0] == "OK") {
                    var meets = $.parseJSON(words[1]);
                    var allWindsData = $("#tbSelectMeets").bootstrapTable('getData');
                    for (var j = 0; j < meets.length; j++) {
                        for (var i = 0; i < allWindsData.length; i++) {
                            if (allWindsData[i].id == meets[j].FMeetSeat) {
                                $('#tbSelectMeets').bootstrapTable('updateRow', {
                                    index: i,
                                    row: {
                                        FReqCode: meets[j].FReqCode,
                                        FCCode: meets[j].FCCode,
                                        FCrimeName: meets[j].FCrimeName,
                                        FAreaCode: meets[j].FAreaCode,
                                        FPC: meets[j].FPC
                                    }
                                });
                                break;
                            }
                        }
                    }
                    
                } else {
                    alert(data);
                }
            }
        });
    }
}


//打印单据
function printMeetOrder(pc) {
    if (pc == null) {
        alert("请输入批次号");
        return false;
    }
    window.open("/DoorCheck/printMeetOrderByPC?PC=" +pc +"");
}