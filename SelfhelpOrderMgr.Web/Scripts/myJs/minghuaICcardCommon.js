
function Connect() {
    var version = MWRFATL.openReader();
    //msg.value = version + "\n";
    MWRFATL.ReaderBeep(30);
}

//寻卡
function Scard() {
    var CardNumber = MWRFATL.openCard(1);//参数为0表示：寻完卡必须要拿开才能再寻卡，参数为1表示：可以多次寻卡
    //msg.value = msg.value + CardNumber + "\n";
    return CardNumber;
}

//加载密码
function ReaderLoadKey() {
    //var key="ffffffffffff";
    var key = "192105011236";

    var iRet = MWRFATL.Loadkey(0, 1, key);
    if (iRet) {
        //msg.value = msg.value + "读写器装载密码失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "读写器装载密码成功！" + "\n";
        return "1";
    }
}

//验证密码
function authenticationKey() {
    var iRet = MWRFATL.mifare_authentication(0, 1);
    if (iRet) {
        //msg.value = msg.value + "验证密码失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "验证密码成功！" + "\n";
        return "1";
    }
}

//写数据
function writeData() {
    var data = "深圳明华澳汉科技";
    var iRet = MWRFATL.mifare_write(4, data);
    if (iRet) {
        //msg.value = msg.value + "写数据失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "写数据成功！" + "\n";
        return "1";
    }
}

//读数据
function readData() {
    var iRet = MWRFATL.mifare_read(6);
    //msg.value = msg.value + iRet + "\n";
}

//以16进制写数据
function writeDataHex() {
    //var data1="12345678901234561234567890123456"; 
    var data1 = "30141200600000000000000000000000";
    var iRet = MWRFATL.mifare_writeHex(5, data1);
    if (iRet) {
        //msg.value = msg.value + "以16进制写数据失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "以16进制写数据成功！" + "\n";
        return "1";
    }
}
//以16进制读数据
function readDataHex() {
    var databuff = MWRFATL.mifare_readHex(6);
    //msg.value = msg.value + databuff + "\n";
    if (databuff != "") {
        return databuff.substring(0, 10);
    } else {
        return "";
    }    
}

//改写密码
function ChangeCardKey() {
    var iRet = MWRFATL.ChangeKey(1, "ffffffffffff", 0, 0, 0, 1, "ffffffffffff");
    if (iRet) {
        //msg.value = msg.value + "改写密码失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "改写密码成功！" + "\n";
        return "1";
    }
}

//关闭卡片
function closeCard() {
    var iRet = MWRFATL.CloseCard();
    MWRFATL.ReaderBeep(30);
    if (iRet) {
        //msg.value = msg.value + "关闭卡片失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "关闭卡片成功！" + "\n";
        return "1";
    }
}

//断开设备
function exit() {
    var iRet = MWRFATL.CloseReader();
    if (iRet) {
        //msg.value = msg.value + "断开设备失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "断开设备成功！" + "\n";
        return "-1";
    }
}

//初始化IC卡信息
function initIcCardInfo(mode) {
    //连接读卡器
    var version = MWRFATL.openReader();
    MWRFATL.ReaderBeep(30);
    //寻卡
    var CardNumber = MWRFATL.openCard(1);//参数为0表示：寻完卡必须要拿开才能再寻卡，参数为1表示：可以多次寻卡
    //加载密码
    if (mode == "new") {
        var key = "ffffffffffff";
    } else {
        var key = "192105011236";
    }
    var iRet = MWRFATL.Loadkey(0, 1, key);
    if (iRet) {
        //msg.value = msg.value + "读写器装载密码失败！" + "\n";
        return "-1";
    }    
    //验证密码
    var iRet = MWRFATL.mifare_authentication(0, 1);
    if (iRet) {
        //msg.value = msg.value + "验证密码失败！" + "\n";
        return "-1";
    }
    //修改密码
    if (mode == "new") {
        var iRet = MWRFATL.ChangeKey(1, "192105011236", 0, 0, 0, 1, "192105011236");
        if (iRet) {            
            return "-1";//msg.value = msg.value + "改写密码失败！" + "\n";
        }        
    }

    //写数据：初始化IC为32个0
    var data1 = "00000000000000000000000000000000";
    var iRet = MWRFATL.mifare_writeHex(6, data1);
    if (iRet) {
        //msg.value = msg.value + "以16进制写数据失败！" + "\n";
        return "-1";
    }
    //断开读卡器
    var iRet = MWRFATL.CloseReader();
    if (iRet) {
        //msg.value = msg.value + "断开设备失败！" + "\n";
        return "-1";
    }
    else {
        //msg.value = msg.value + "断开设备成功！" + "\n";
        return "-1";
    }
}


