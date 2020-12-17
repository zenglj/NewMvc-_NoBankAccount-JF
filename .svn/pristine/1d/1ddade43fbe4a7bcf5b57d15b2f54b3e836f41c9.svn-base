$(function () {
    $('#myAlert').on('closed.bs.alert', function () {
        // do something…
    })

    
});
function loginCheck() {
    var cardNo = readCardNo();
    $("#managerCard").html("管理卡号：" + cardNo);
    if (cardNo != "") {
        $.post('/Home/LoginCheck', { "managerCardNo": cardNo},
            function (data, status) {
                if ('success' != status) {
                    return false;
                } else {
                    if (data != null) {
                        var ss = data.split("|");
                        if (ss[0] == "OK") {
                            window.location.href = "/Home/Index";
                        } else {
                            //$("#managerCard").html(data);
                        }
                    }
                }
            });
    }
}

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

//用户登录检查
function UserLoginCheck() {
    //var cardNo = readCardNo();
    var userName = $("#UserName1").val();
    var userPwd = $("#UserPwd1").val();
    $("#managerCard").html("管理卡号：" + userName);
    if (userName != "") {
        $.post('/Home/UserSignInCheck', { "userName": userName, "userPwd": userPwd },
            function (data, status) {
                if ('success' != status) {
                    return false;
                } else {
                    if (data != null) {
                        var ss = data.split("|");
                        if (ss[0] == "OK") {
                            window.location.href = "/Home/Index";
                        } else {
                            //$("#managerCard").html(data);
                            alert(data);
                        }
                    }
                }
            });
    }
}


