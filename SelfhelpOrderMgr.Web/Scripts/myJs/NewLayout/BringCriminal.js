function GetPCMeets(pc, bringFlag) {
    var flag = bringFlag - 1;//要查询会见单的带犯状态
    $.post("/BringCriminal/GetMeetList", { "PC": pc,"BringFlag":flag }, function (data, status) {
        
        $("#selPcNo").val(pc + "|" + bringFlag);

        if ("success" == status) {            
            var words = data.split("|");
            
            if (words[0] == "OK") {
                var meets = $.parseJSON(words[1]);
                $("#meetBringOut").empty();
                for (var i = 0; i < meets.length; i++) {
                    var meet = meets[i];
                    var fcrimename = meet.FCrimeName;
                    if (fcrimename.length == 2) {
                        fcrimename = fcrimename + "　";
                    }
                    var div = "<div class='col-sm-2' id='" + meet.FReqCode + "' onclick=\"ChangBringFlag('" + meet.FReqCode + "'," + bringFlag + ")\">"
                            + "<p>";
                    if (bringFlag == 2) {//带到管理
                        if (meet.BringFlag == 1) {
                            div = div + "<button type='button' class='btn btn-danger'>" + fcrimename + "<small>(已带出)</small></button>";
                        } else if (meet.BringFlag == 0) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(未带出)</small></button>";
                        } else if (meet.BringFlag == 2) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带到)</small></button>";
                        } else if (meet.BringFlag == 3) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带回)</small></button>";
                        }
                    } else if (bringFlag == 3) {//带回管理
                        if (meet.BringFlag == 2) {
                            div = div + "<button type='button' class='btn btn-success'>" + fcrimename + "<small>(已带到)</small></button>";
                        } else if (meet.BringFlag == 1) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带出)</small></button>";
                        } else if (meet.BringFlag == 0) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(未带出)</small></button>";
                        } else if (meet.BringFlag == 3) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带回)</small></button>";
                        }
                    } else {//小于2，就默认为1 是带出犯人
                        if (meet.BringFlag == 0) {
                            div = div + "<button type='button' class='btn btn-info'>" + fcrimename + "<small>(未带出)</small></button>";
                        } else if (meet.BringFlag == 1) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带出)</small></button>";
                        } else if (meet.BringFlag == 2) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带到)</small></button>";
                        } else if (meet.BringFlag == 3) {
                            div = div + "<button type='button' class='btn btn-default'>" + fcrimename + "<small>(已带回)</small></button>";
                        }
                    }
                    
                    div = div + "</p>"
                            + "</div>";

                    $("#meetBringOut").append(div);
                }
            } else {
                $("#meetBringOut").empty();
                $("#meetBringOut").append("<h1>" + words[1] + "</h1>");
                //alert(data);
            }
        }
    });
}


//更新Meet状态为带出
function ChangBringFlag(FreqCode,flag) {
    $.post("/BringCriminal/ChangBringFlag", { "FReqCode": FreqCode, "Flag": flag }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                var div = "<p>";
                //if (flag == 0) {
                //    div = div + "<button type='button' class='btn btn-info'>" + words[1] + "<small>(未带出)</small></button>";
                //} else if (flag == 1) {
                //    div = div + "<button type='button' class='btn btn-default'>" + words[1] + "<small>(已带出)</small></button>";
                //} else if (flag == 2) {
                //    div = div + "<button type='button' class='btn btn-default'>" + words[1] + "<small>(已带到)</small></button>";
                //} else if (flag == 3) {
                //    div = div + "<button type='button' class='btn btn-default'>" + words[1] + "<small>(已带回)</small></button>";
                //}
                var bringText = "未带出";
                if (flag == 1) {
                    bringText = "已带出";
                } else if (flag == 2) {
                    bringText = "已带到";
                } else if (flag == 3) {
                    bringText = "已带回";
                }
                div = div + "<button type='button' class='btn btn-default'>" + words[1] + "<small>(" + bringText + ")</small></button>";

                div = div + "</p>";
                $("#" + FreqCode).empty();
                $("#" + FreqCode).append(div);
            } else {
                alert(data);
            }
        }
    });
}




//更新本批次全部Meet状态为带出
function changeStatusALL(flag) {
    if ($("#selPcNo").val() != "") {
        flag = $("#selPcNo").val();
    }    
    var flags = flag.split("|");
    $.post("/BringCriminal/changeStatusALL", { "FPC": flags[0], "Flag": flags[1] }, function (data, status) {
        if ("success" == status) {
            var words = data.split("|");
            if (words[0] == "OK") {
                GetPCMeets(flags[0], flags[1]);
            } else {
                alert(data);
            }
        }
    });
}
