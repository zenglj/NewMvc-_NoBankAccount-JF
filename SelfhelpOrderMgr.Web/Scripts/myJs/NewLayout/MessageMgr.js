
$(function () {
    //setActiveMenu("topMenu_01");
    $("#topMenu_01").addClass("active");
    //$("#topImage").hide();
    $(".navbar").hide();
    GetPCMeets(1);
    
});

//表格样式的
//function GetPCMeets(pc) {
//    getPcList(pc);//显示批次菜单
//    var flag = 0;//要查询会见单的带犯状态,默认0显示所有人
//    $.post("/BringCriminal/GetMeetList", { "PC": pc, "BringFlag": flag }, function (data, status) {

//        if ("success" == status) {
//            var words = data.split("|");
//            if (words[0] == "OK") {
//                var meets = $.parseJSON(words[1]);
//                $("#bodyMeetList").empty();
//                for (var i = 0; i < meets.length; i++) {
//                    var meet = meets[i];
//                    var fcrimename = meet.FCrimeName;
//                    if (fcrimename.length == 2) {
//                        fcrimename = fcrimename + "　";
//                    }
//                    var bringText = "未带出";
//                    if (meet.BringFlag == 1) {
//                        bringText = "已带出";
//                    } else if (meet.BringFlag == 2) {
//                        bringText = "已带到";
//                    } else if (meet.BringFlag == 2) {
//                        bringText = "已带回";
//                    }
//                    var seqno = i + 1;
//                    var tr = "<tr><td>" + seqno + "</td><td>第 " + meet.FPC + " 批</td><td>" + meet.FAreaCode + "</td><td>" + meet.FCCode + "</td><td>" + meet.FCrimeName + "</td><td>" + meet.FamilyNames + "</td><td>" + bringText + "</td></tr>";

//                    $("#bodyMeetList").append(tr);
//                }
//            } else {
//                $("#bodyMeetList").empty();
//                $("#bodyMeetList").append("<h1>" + words[1] + "</h1>");
//                //alert(data);
//            }
//        }
//    });

    
//}


//名片样式的

function GetPCMeets(pc) {
    getPcList(pc);//显示批次菜单
    
    //alert($("#currPcNo").val());


}

function DisplayPCMeets(pc, flag) {
    $.post("/BringCriminal/GetMeetList", { "PC": pc, "BringFlag": flag }, function (data, status) {

        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                var meets = $.parseJSON(words[1]);
                $("#bodyMeetList").empty();
                $("#msgTitle").html(" ——第 " + chineseNum(meets[0].FPC) + " 批会见人员名单");

                for (var i = 0; i < meets.length; i++) {
                    var meet = meets[i];

                    var fcrimename = meet.FCrimeName;
                    if (fcrimename.length == 2) {
                        fcrimename = fcrimename + "　";
                    }
                    var bringText = "未带出";
                    if (meet.BringFlag == 1) {
                        bringText = "已带出";
                    } else if (meet.BringFlag == 2) {
                        bringText = "已带到";
                    } else if (meet.BringFlag == 2) {
                        bringText = "已带回";
                    }
                    var seqno = i + 1;
                    //var tr = "<tr><td>" + seqno + "</td><td>第 " + meet.FPC + " 批</td><td>" + meet.FAreaCode + "</td><td>" + meet.FCCode + "</td><td>" + meet.FCrimeName + "</td><td>" + meet.FamilyNames + "</td><td>" + bringText + "</td></tr>";
                    var div = "<div class=\"col-sm-2\">"
                        + "<div class=\"thumbnail text-center\">"
                        + "    <div class=\"alert-info\">"
                        + "        <div class=\"caption\" style=\" height:140px;\">"
                        + "            <h3 style=\"color: #2C699E\">" + meet.FCrimeName + "<span class=\"badge\">" + meet.FMeetSeat + "</span></h3>"
                        + "            <h4>" + meet.FamilyNames + "</h4>"
                        + "        </div>"
                        + "    </div>"
                        + "</div>"
                        + "</div>";

                    $("#bodyMeetList").append(div);
                }
            } else {
                $("#bodyMeetList").empty();
                $("#bodyMeetList").append("<h1>" + words[1] + "</h1>");
                //alert(data);
            }
        }
    });

}

function chineseNum(n) {
    if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n))
        return "数据非法";
    var str = "";
    if (n == 1) {
        str = "一";
    } else if (n == 2) {
        str = "二";
    } else if (n == 3) {
        str = "三";
    } else if (n == 4) {
        str = "四";
    } else if (n == 5) {
        str = "五";
    } else if (n == 6) {
        str = "六";
    } else if (n == 7) {
        str = "七";
    } else if (n == 8) {
        str = "八";
    } else if (n == 9) {
        str = "九";
    } else if (n ==10) {
        str = "十";
    }
    
    return str;
}

//获取批次列表
function getPcList(p) {
    $.post("/BringCriminal/GetTodayPCList", {"Flag":1}, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if ("OK" == words[0]) {
                var li = "";
                var pcText = "一";
                var pcs = $.parseJSON(words[1]);
                $("#currPcNo").val(pcs[0].FPC);//设定最新未会见的批次号
                for (var i = 0; i < pcs.length; i++) {
                    pc = pcs[i];
                    switch (pc.FPC) {
                        case 0:
                            pcText = "零";
                        case 1:
                            pcText = "一";
                            break;
                        case 2:
                            pcText = "二";
                            break;
                        case 3:
                            pcText = "三";
                            break;
                        case 4:
                            pcText = "四";
                            break;
                        case 5:
                            pcText = "五";
                            break;
                        case 6:
                            pcText = "六";
                        case 2:
                            pcText = "二";
                            break;
                        case 7:
                            pcText = "七";
                            break;
                        case 8:
                            pcText = "八";
                            break;
                        case 9:
                            pcText = "九";
                            break;
                        case 10:
                            pcText = "十";
                            break;
                    }
                    if (pc.FPC == p) {
                        li = li + "<a href='#' class='list-group-item active' onclick='GetPCMeets(" + pc.FPC + ")'><span class='badge'>" + pc.pcCount + "</span>第" + pcText + "批次<small>(接见人员)</small></a>"
                    } else {
                        li = li + "<a href='#' class='list-group-item' onclick='GetPCMeets(" + pc.FPC + ")'><span class='badge'>" + pc.pcCount + "</span>第" + pcText + "批次<small>(接见人员)</small></a>"
                    }
                }
                $("#piciList").empty();
                $("#piciList").append(li);

                var flag = 0;//要查询会见单的带犯状态,默认0显示所有人
                var pc = $("#currPcNo").val();

                DisplayPCMeets(pc, flag)

            } else {
                //$("#meetBringOut").append("<h1>当前暂无会见人员信息</h1>");
            }

        }
    });
}


