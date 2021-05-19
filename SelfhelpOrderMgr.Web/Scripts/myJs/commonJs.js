//制保留2位小数，如：2，会在2后面补上00.即2.00
function toDecimal2(x) {
    var f = parseFloat(x);
    if (isNaN(f)) {
        return false;
    }
    var f = Math.round(x * 100) / 100;
    var s = f.toString();
    var rs = s.indexOf('.');
    if (rs < 0) {
        rs = s.length;
        s += '.';
    }
    while (s.length <= rs + 2) {
        s += '0';
    }
    return s;
}

//js把时间戳转为普通格式的方法。
function getLocalTime(nS) {
    var snS = nS.substr(6);
    snS = snS.substr(0, snS.length - 2);
    return new Date(parseInt(snS)).toLocaleString().substr(0, 17)
}


function getShortTime(nS) {
    var snS = nS.substr(6);
    snS = snS.substr(0, snS.length - 2);
    var shortDt = new Date(parseInt(snS));
    return shortDt.getFullYear() + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate()
}

function getLongTime(nS) {
    var snS = nS.substr(6);
    snS = snS.substr(0, snS.length - 2);
    var shortDt = new Date(parseInt(snS));
    return shortDt.getFullYear() + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate() + " " + shortDt.getHours() + ":" + shortDt.getMinutes() + " " + shortDt.getSeconds()
}

//获取两个时间差
function getTimeDiff(nSStart, nSEnd) {
    var snS = nSStart.substr(6);
    snS = snS.substr(0, snS.length - 2);
    var snS2 = nSEnd.substr(6);
    snS2 = snS2.substr(0, snS2.length - 2);

    snDiff = parseInt(snS2) - parseInt(snS);

    var leave2 = snDiff % (3600 * 1000);         //计算小时数后剩余的毫秒数
    var minutes = Math.floor(leave2 / (60 * 1000));
    //计算相差秒数
    var leave3 = leave2 % (60 * 1000);       //计算分钟数后剩余的毫秒数
    var seconds = Math.round(leave3 / 1000);

    if (minutes > 0) {
        return minutes + "分" + seconds + "秒";
    } else {
        return seconds + "秒";
    }
}


function getDateLongTime(shortDt) {
    return shortDt.getFullYear() + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate() + " " + shortDt.getHours() + ":" + shortDt.getMinutes() + ":" + shortDt.getSeconds()
}

function getTime(/** timestamp=0 **/) {
    var ts = arguments[0] || 0;
    var t, y, m, d, h, i, s;
    t = ts ? new Date(ts * 1000) : new Date();
    y = t.getFullYear();
    m = t.getMonth() + 1;
    d = t.getDate();
    h = t.getHours();
    i = t.getMinutes();
    s = t.getSeconds();
    // 可根据需要在这里定义时间格式  
    return y + '-' + (m < 10 ? '0' + m : m) + '-' + (d < 10 ? '0' + d : d) + ' ' + (h < 10 ? '0' + h : h) + ':' + (i < 10 ? '0' + i : i) + ':' + (s < 10 ? '0' + s : s);
}

//判断字符串是否为数字
function checkRate(input) {
    var re = /^[0-9]+.?[0-9]*$/;   //判断字符串是否为数字     //判断正整数 /^[1-9]+[0-9]*]*$/  
    var nubmer = document.getElementById(input).value;

    if (!re.test(nubmer)) {
        alert("请输入数字");
        document.getElementById(input).value = "";
        return false;
    }
}

//判断是否整数
function isIntNumber(input) {
    if (parseInt(input) == input) {
        return true;
    } else {
        return false;
    }
    
}

//数字转成大写
function DX(n) {

    if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(n))

        return "数据非法";

    var unit = "千百拾亿千百拾万仟佰拾元角分", str = "";

    n += "00";

    var p = n.indexOf('.');

    if (p >= 0)

        n = n.substring(0, p) + n.substr(p + 1, 2);

    unit = unit.substr(unit.length - n.length);

    for (var i = 0; i < n.length; i++)

        str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);

    return str.replace(/零(千|百|拾|角)/g, "零").replace(/(零)+/g, "零").replace(/零(万|亿|元)/g, "$1").replace(/(亿)万|壹(拾)/g, "$1$2").replace(/^元零?|零分/g, "").replace(/元$/g, "元整");

}


//格式化银行卡号
function BankCardFormat(bankCardNo) {
    if (bankCardNo >= 19) {
        var cardno = bankCardNo.substr(0, 4) + " " + bankCardNo.substr(4, 4) + " " + bankCardNo.substr(8, 4) + " " + bankCardNo.substr(12, 4) + " " + bankCardNo.substr(16, 10);
        return cardno;
    } else {
        return bankCardNo
    }
}



//获取查询表单条件的Json
function GetFormSearchJson(formId) {
    var strJson = "";
    $("#" + formId +" tr td span input").each(function (index, element) {   //element-当前的元素,也可使用this选择器
        if (typeof $(this).attr("name") != "undefined" && $(this).val().replace(/^\s*|\s*$/g, "") != "" && typeof $(this).val() != "undefined") {
            console.log($(this).attr("name") + ":" + $(this).val());
            if (strJson == "") {
                strJson = "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            } else {
                strJson = strJson + "," + "\"" + $(this).attr("name") + "\":\"" + $(this).val() + "\"";
            }
        }
    });
    strJson = "{" + strJson + "}";
    return strJson;
}

var Commath = {
    /* #region  精确加法 */
    accAdd: function (arg1, arg2) {
        var r1 = 0,
            r2 = 0,
            m;

        if (arg1.toString().indexOf(".") > -1) {
            r1 = arg1.toString().split(".")[1].length;
        }

        if (arg2.toString().indexOf(".") > -1) {
            r2 = arg2.toString().split(".")[1].length;
        }

        m = Math.pow(10, Math.max(r1, r2));

        if (r2 > r1) {
            arg1 = Number(arg1.toString().replace(".", "")) * Math.pow(10, r2 - r1);
        }

        if (r1 > r2) {
            arg2 = Number(arg2.toString().replace(".", "")) * Math.pow(10, r1 - r2);
        }

        return (Number(arg1.toString().replace(".", "")) + Number(arg2.toString().replace(".", ""))) / m;
    },
    /* #endregion */

    /* #region  精确减法 */
    accMinus: function (arg1, arg2) {
        var r1 = 0,
            r2 = 0,
            m;

        if (arg1.toString().indexOf(".") > -1) {
            r1 = arg1.toString().split(".")[1].length;
        }

        if (arg2.toString().indexOf(".") > -1) {
            r2 = arg2.toString().split(".")[1].length;
        }

        if (r2 > r1) {
            arg1 = Number(arg1.toString().replace(".", "")) * Math.pow(10, r2 - r1);
        }

        if (r1 > r2) {
            arg2 = Number(arg2.toString().replace(".", "")) * Math.pow(10, r1 - r2);
        }

        m = Math.pow(10, Math.max(r1, r2));
        return (Number(arg1.toString().replace(".", "")) - Number(arg2.toString().replace(".", ""))) / m;
    },
    /* #endregion */

    /* #region  精确乘法 */
    //精确乘法
    accMul: function (arg1, arg2) {
        var m = 0,
            s1 = arg1.toString(),
            s2 = arg2.toString();

        if (s1.toString().indexOf('.') > -1) {
            m += s1.split(".")[1].length;
        }

        if (s2.toString().indexOf('.') > -1) {
            m += s2.split(".")[1].length;
        }

        return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m);
    },
    /* #endregion */
    /* #region 精确除法  */
    //精确除法
    accDiv: function (arg1, arg2) {
        var t1 = 0,
            t2 = 0,
            r1, r2;

        if (arg1.toString().indexOf('.') > -1) {
            t1 = arg1.toString().split(".")[1].length;
        }

        if (arg2.toString().indexOf('.') > -1) {
            t2 = arg2.toString().split(".")[1].length;
        }
        with (Math) {
            r1 = Number(arg1.toString().replace(".", ""));
            r2 = Number(arg2.toString().replace(".", ""));
            return (r1 / r2) * pow(10, t2 - t1);
        }
    }
}
/* #endregion */
