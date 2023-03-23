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

    var unit = "仟佰拾亿仟佰拾万仟佰拾元角分", str = "";

    n += "00";

    var p = n.indexOf('.');

    if (p >= 0)

        n = n.substring(0, p) + n.substr(p + 1, 2);

    unit = unit.substr(unit.length - n.length);

    for (var i = 0; i < n.length; i++)
        str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);

    return str.replace(/零(仟|佰|拾|角)/g, "零").replace(/(零)+/g, "零").replace(/零(万|亿|元)/g, "$1")
        //.replace(/(亿)万|壹(拾)/g, "$1$2")
        .replace(/^元零?|零分/g, "").replace(/元$/g, "元整");


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







//base64 加密解密============================

var base64encodechars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
var base64decodechars = new Array(
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, -1, -1, 63,
    52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,
    -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
    15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, -1,
    -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1);

function base64encode(str) {
    var out, i, len;
    var c1, c2, c3;
    len = str.length;
    i = 0;
    out = "";
    while (i < len) {
        c1 = str.charCodeAt(i++) & 0xff;
        if (i == len) {
            out += base64encodechars.charAt(c1 >> 2);
            out += base64encodechars.charAt((c1 & 0x3) << 4);
            out += "==";
            break;
        }
        c2 = str.charCodeAt(i++);
        if (i == len) {
            out += base64encodechars.charAt(c1 >> 2);
            out += base64encodechars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xf0) >> 4));
            out += base64encodechars.charAt((c2 & 0xf) << 2);
            out += "=";
            break;
        }
        c3 = str.charCodeAt(i++);
        out += base64encodechars.charAt(c1 >> 2);
        out += base64encodechars.charAt(((c1 & 0x3) << 4) | ((c2 & 0xf0) >> 4));
        out += base64encodechars.charAt(((c2 & 0xf) << 2) | ((c3 & 0xc0) >> 6));
        out += base64encodechars.charAt(c3 & 0x3f);
    }
    return out;
}

function base64decode(str) {
    var c1, c2, c3, c4;
    var i, len, out;

    len = str.length;

    i = 0;
    out = "";
    while (i < len) {

        do {
            c1 = base64decodechars[str.charCodeAt(i++) & 0xff];
        } while (i < len && c1 == -1);
        if (c1 == -1)
            break;

        do {
            c2 = base64decodechars[str.charCodeAt(i++) & 0xff];
        } while (i < len && c2 == -1);
        if (c2 == -1)
            break;

        out += String.fromCharCode((c1 << 2) | ((c2 & 0x30) >> 4));

        do {
            c3 = str.charCodeAt(i++) & 0xff;
            if (c3 == 61)
                return out;
            c3 = base64decodechars[c3];
        } while (i < len && c3 == -1);
        if (c3 == -1)
            break;

        out += String.fromCharCode(((c2 & 0xf) << 4) | ((c3 & 0x3c) >> 2));

        do {
            c4 = str.charCodeAt(i++) & 0xff;
            if (c4 == 61)
                return out;
            c4 = base64decodechars[c4];
        } while (i < len && c4 == -1);
        if (c4 == -1)
            break;
        out += String.fromCharCode(((c3 & 0x03) << 6) | c4);
    }
    return out;
}

