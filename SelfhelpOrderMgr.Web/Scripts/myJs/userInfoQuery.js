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
    QueryUserInfo(cardNo);
}

function QueryUserInfo(cardNo) {
    $("#lblInfo").html(cardNo);
    $.post("/UserQuery/QueryUserInfo", { "FCardCode": cardNo, "checkFlag": "1" }, function (data, status) {
        if ("success" != status) {
            return false;
        } else {
            var words = data.split("|")
            if (words[0] == "Error") {
                $("#lblInfo").html(data);
                $("#userNameInfo").html("对不起，您所持的不是有效的IC消费卡，请与管教联系！");
                ClearUserDisplayInfo();
            } else if (words[0] == "There") {
                SetAndDisplayUserInfo(words[1]);
            }
            else {
                //SetAndDisplayCriminalInfo(words[1], saleSort);//显示抬头栏的账户余额信息
            }
        }
    });

}

//用户登录检查
function UserLoginCheck() {
    //var cardNo = readCardNo();
    var userCode = $("#userCode").val();
    var userPwd = $("#UserPwd").val();
    //$("#managerCard").html("管理卡号：" + userName);
    if (userCode != "") {
        $.post('/UserQuery/GetCardInfo', { "userCode": userCode, "userPwd": userPwd },
            function (data, status) {
                if ('success' != status) {
                    return false;
                } else {
                    if (data != null) {
                        var ss = data.split("|");
                        if (ss[0] == "OK") {
                            QueryUserInfo(ss[1]);
                        } else {
                            //$("#managerCard").html(data);
                            alert(data);
                        }
                    }
                }
            });
    }
}



//显示用户相关信息
function SetAndDisplayUserInfo(words) {
    var rts = $.parseJSON(words);
    $("#tbodyOrderList").empty();
    if (rts.UserInfo == null) {
        //$("#lblInfo").html("银行卡号：" + rts.UserInfo.BankCardNo);
        ClearUserDisplayInfo();
    } else {
        var cardStaus = "";
        if (rts.UserCard.cardflaga == "1") {
            cardStaus = "正常";
        } else if (rts.UserCard.cardflaga == "2") {
            cardStaus = "挂失";
        } else if (rts.UserCard.cardflaga == "3") {
            cardStaus = "作废";
        } else if (rts.UserCard.cardflaga == "4") {
            cardStaus = "停用";
        } else {
            cardStaus = "异常";
        }
        var info = "你好, " + rts.UserInfo.FName + "<span class='label label-primary'>" + rts.UserInfo.CyName + "(" + rts.UserInfo.MonthStandard + ")" + "</span>,<span class='label label-primary'>IC卡状态:" + cardStaus  + "</span>,所在队别：<span class='label label-default'>" + rts.UserInfo.FAreaName + "</span>,您的账户信息如下：";
        $("#userNameInfo").html(info);
        var bankcardno = "";
        //if (rts.UserInfo.BankCardNo.length >=19) {
        //    bankcardno = rts.UserInfo.BankCardNo.substr(0, 4) + " " + rts.UserInfo.BankCardNo.substr(4, 4) + " " + rts.UserInfo.BankCardNo.substr(8, 4) + " " + rts.UserInfo.BankCardNo.substr(12, 4) + " " + rts.UserInfo.BankCardNo.substr(16,10);
        //}
        //$("#lblInfo").html("银行卡号：" + bankcardno);
        //console.log("111111111");
        $("#lblInfo").html("旧烛光卡号：<del>" + BankCardFormat(rts.UserInfo.BankCardNo) + "</del><br/> 中银结算卡号：" + BankCardFormat(rts.UserCard.SecondaryBankCard));
        //console.log("22222222");
        $("#curMonthXFE").html(rts.UserInfo.Xiaofeimoney);
        $("#okUseAllMoney").html(rts.UserInfo.OkUseAllMoney);
        $("#xiaofeiYuE").html(rts.UserInfo.NoXiaofeimoney);
        $("#amountC").html(rts.UserInfo.AmountC);
        $("#amountB").html(rts.UserInfo.AmountB);
        $("#amountA").html(rts.UserInfo.AmountA);
        $("#djMoney").html(rts.UserInfo.dongjieMoney);
        $("#bankTimeMoney").html(rts.bankTimeMoney);
        //alert(rts.bankTimeMoney);
        if (rts.bankTimeMoney != "") {
            $("#trBankMoneyInfo").show();
        } else {
            $("#trBankMoneyInfo").hide();
        }
        
        var details = rts.vcrds;
        if (details.length > 0) {
            for (var i = 0; i < details.length; i++) {
                var detail = details[i];
                var fmoney = (detail.DAmount - detail.CAmount);
                var option = "<tr><td>" + getLocalTime(detail.CrtDate) + "</td><td>" + detail.DType + "</td><td>" + fmoney + "</td><td>" + detail.Remark + "</td></tr>";
                $("#tbodyOrderList").append(option);
            }
        }    
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



////数字键盘，用户信息查询，触屏方式输入
//function btnEntUserQuery(e, g) {
//    if (e == "确认") {
//        var usercode = $("#userCode").val();
//        if (usercode.length >= 10) {
//            $('#UserPwd').focus();
//            if ($("#UserPwd").val().length >= 0) {//密码长度默认为0位
//                UserLoginCheck();
//            }
//        } else {
//            $('#userCode').focus();
//        }
//    } else if (e == "清空") {
//        if ($('#UserPwd').val() != "") {
//            $('#UserPwd').val("");
//            $('#UserPwd').focus();
//        } else {
//            $('#userCode').val("");
//            $('#userCode').focus();
//        }
//    } else if (e == "删除") {
//        if ($("#UserPwd").val() != "") {
//            var vLength = $("#UserPwd").val().length;
//            $("#UserPwd").val($("#UserPwd").val().substr(0, vLength - 1));
//        } else {
//            if ($("#userCode").val() != "") {
//                var vLength = $("#userCode").val().length;
//                $("#userCode").val($("#userCode").val().substr(0, vLength - 1));
//            }
//        }
//    } else if (e == "/") {
//        //空，不处理
//    } else {
//        var usercode = $("#userCode").val();
//        var userpwd = $("#UserPwd").val();
//        if (usercode.length < 10) {
//            $("#userCode").val(usercode + e);
//            $("#userCode").focus();
//        } else {
//            $("#UserPwd").val(userpwd + e);
//            $("#UserPwd").focus();
//        }
//    }


//}





$(document).ready(function () {

    $("#div_UserQueryKeyBoard div span button").click(function () {
        btnInuptBoxEnt($.trim($(this).html()));
    });

});