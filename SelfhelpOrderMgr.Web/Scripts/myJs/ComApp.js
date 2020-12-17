//读取IC卡号
function readCardNo() {
    //$("#lblInfo").html("0000000");
    var cardNo = "";
    Connect();//连接读卡器
    //$("#lblInfo").html("连接成功");
    var xunka = Scard();//寻卡
    if (xunka != "") {
        //$("#lblInfo").html(xunka);
        var rlk = ReaderLoadKey();//装载密码
        if (rlk == "1") {
            //$("#lblInfo").html("装载密码成功");
            var athk = authenticationKey();//验证密码
            if (athk == "1") {
                // $("#lblInfo").html("验证密码成功");
                cardNo = readDataHex();//以16进制的方式读卡
                if (cardNo != "") {
                    //$("#lblInfo").html("读卡完成");
                }
            }
        }
    }
    //$("#lblInfo").html(xunka);
    //alert(cardNo);
    exit();//断开读卡器连接
    return cardNo;
}

//查询用户信息
function btnQueryUserInfo(e, saleSort) {
    var cardNo = readCardNo();
    //alert(cardNo);
    //alert("e3232");
    $("#lblInfo").html(cardNo);
    $.post("/ComApp/QueryUserInfo", { "FCardCode": cardNo, "checkFlag": "1", "FManagerCard": $("#MgrCardNo").val(), "UserName": $("#UserName").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                $("#lblInfo").html(data);
                $("#userNameInfo").html("对不起，您所持的不是有效的IC消费卡，请与管教联系！");
                $("#btnSubmitKK").attr("disabled", "disabled");//禁用扣款提交按钮
                ClearUserDisplayInfo();
            } else if (words[0] == "There") {
                SetAndDisplayUserInfo(words[1]);
                $("#KKMoney").focus();
            }
            else {
                //SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
            }
        }
    });
}


//查询用户信息
function QueryUserInfoByFCrimeCode(e, saleSort) {

    if ($("#userCodeId").val() == "") {
        alert("对不起,用户编号不能为空!");
        return false;
    }

    $.post("/ComApp/QueryUserInfoByFCrimeCode", { "userCodeId": $("#userCodeId").val(), "checkFlag": "1", "FManagerCard": $("#MgrCardNo").val(), "UserName": $("#UserName").val() }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                $("#lblInfo").html(data);
                $("#userNameInfo").html("对不起，您所持的不是有效的IC消费卡，请与管教联系！");
                $("#btnSubmitKK").attr("disabled", "disabled");//禁用扣款提交按钮
                ClearUserDisplayInfo();
            } else if (words[0] == "There") {
                SetAndDisplayUserInfo(words[1]);
                $("#KKMoney").focus();
            }
            else {
                //SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
            }
        }
    });
}


//显示用户相关信息
function SetAndDisplayUserInfo(words) {
    var rts = $.parseJSON(words);
    $("#tbodyOrderList").empty();
    if (rts.UserInfo == null) {
        //$("#lblInfo").html("银行卡号：" + rts.UserInfo.BankCardNo);
        ClearUserDisplayInfo();
    } else {
        var info = "你好, " + rts.UserInfo.FName + "<span class='label label-primary'>" + rts.UserInfo.CyName + "(" + rts.UserInfo.MonthStandard + ")" + "</span>,所在队别：<span class='label label-default'>" + rts.UserInfo.FAreaName + "</span>,您的账户信息如下：";
        $("#userNameInfo").html(info);
        var bankcardno = "";
        if (rts.UserInfo.BankCardNo.length >=19) {
            bankcardno = rts.UserInfo.BankCardNo.substr(0, 4) + " " + rts.UserInfo.BankCardNo.substr(4, 4) + " " + rts.UserInfo.BankCardNo.substr(8, 4) + " " + rts.UserInfo.BankCardNo.substr(12, 4) + " " + rts.UserInfo.BankCardNo.substr(16,10);
        }
        $("#lblInfo").html("银行卡号：" + bankcardno);
        $("#curMonthXFE").html(rts.UserInfo.Xiaofeimoney);
        $("#okUseAllMoney").html(rts.UserInfo.OkUseAllMoney);
        $("#xiaofeiYuE").html(rts.UserInfo.NoXiaofeimoney);
        $("#amountC").html(rts.UserInfo.AmountCmoney);
        $("#amountB").html(rts.UserInfo.AmountBmoney);
        $("#amountA").html(rts.UserInfo.AmountAmoney);
        
        $("#FCrimeCode").val(rts.UserInfo.FCode);
        $("#FIcCardCode").val(rts.UserInfo.CardCode);
        $("#btnSubmitKK").removeAttr("disabled");//启用扣款提交按钮
    }
}

function ClearUserDisplayInfo() {
    
    $("#okUseAllMoney").html("0.00");
    $("#xiaofeiYuE").html("0.00");
    $("#amountC").html("0.00");
    $("#amountB").html("0.00");
    $("#amountA").html("0.00");
    $("#tbodyOrderList").empty();
}

function loginCheck(e) {
    var cardNo = readCardNo();
    $("#managerCard").html("管理卡号：" + cardNo);
    //alert(cardNo);
    if (cardNo != "") {
        $.post('/ComApp/LoginCheck', { "managerCardNo": cardNo },
            function (data, status) {
                if ('success' != status) {
                    return false;
                } else {
                    if (data != null) {
                        var ss = data.split("|");
                        if (ss[0] == "OK") {
                            $("#MgrCardNo").val( cardNo);
                            if (e == 1) {
                                $("#selSaveType").empty();
                                var dd = $.parseJSON(ss[1]);
                                $("#selSaveType").append("<option value='9999'>请选择队别</option>");
                                for (var i = 0; i < dd.length; i++) {
                                    var curArea = dd[i];
                                    $("#selSaveType").append("<option value='" + curArea.FCode + "'>" + curArea.FName + "</option>");
                                }
                            }
                            
                        } else {
                            alert("您用的是无效管理卡");
                        }
                    }
                }
            });
    }
}


function changeRmbDX() {
    if ($("#selSaveType").find("option:selected").val() == "") {
        alert("请先选择一个扣款类型");
        return false;
    }
    var kkmoney = $("#KKMoney").val();
    var rmbdx = DX(kkmoney);
    $("#rmbDX").html(rmbdx);
}

//提交扣款
function btnSubmitKK() {

    if ($("#selSaveType").find("option:selected").val() == "") {
        alert("请先选择一个队别");
        return false;
    }

    //if ($("#selSaveType").find("option:selected").val() == "9999") {
    //    alert("请先选择一个队别");
    //    return false;
    //}

    


    $("#btnSubmitKK").attr("disabled", "disabled");//禁用扣款提交按钮
    $.post("/ComApp/btnSubmitKK", {
        "FCrimeCode": $("#FCrimeCode").val()
        , "FIcCardCode": $("#FIcCardCode").val()
        , "FManagerCard": $("#MgrCardNo").val()
        , "DType": $("#selSaveType").find("option:selected").val()
    }
        , function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] == "OK") {                                        
                    alert(words[1]);
                } else {
                    $("#lblInfo").html("调队失败：" + data);
                    alert(data);
                }
            }

        });
}

function btnSubmitChange() {

    if ($("#selSaveType").find("option:selected").val() == "") {
        alert("请先选择一个处遇");
        return false;
    }

    //if ($("#selSaveType").find("option:selected").val() == "9999") {
    //    alert("请先选择一个队别");
    //    return false;
    //}




    $("#btnSubmitKK").attr("disabled", "disabled");//禁用扣款提交按钮
    $.post("/ComApp/btnSubmitChange", {
        "FCrimeCode": $("#FCrimeCode").val()
        , "FIcCardCode": $("#FIcCardCode").val()
        , "FManagerCard": $("#MgrCardNo").val()
        , "DType": $("#selSaveType").find("option:selected").val()
    }
        , function (data, status) {
            if ("success" != status) {
                return false;
            } else {
                var words = data.split("|");
                if (words[0] == "OK") {
                    alert(words[1]);
                } else {
                    $("#lblInfo").html("变更失败：" + data);
                    alert(data);
                }
            }

        });
}
