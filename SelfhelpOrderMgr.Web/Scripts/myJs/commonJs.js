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
    return (1900+shortDt.getYear()) + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate()
}

function getLongTime(nS) {
    var snS = nS.substr(6);
    snS = snS.substr(0, snS.length - 2);
    var shortDt = new Date(parseInt(snS));
    return (1900+shortDt.getYear()) + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate() + " " + shortDt.getHours() + ":" + shortDt.getMinutes() + " " + shortDt.getSeconds()
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
    return (1900+shortDt.getYear()) + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate() + " " + shortDt.getHours() + ":" + shortDt.getMinutes() + ":" + shortDt.getSeconds()
}

function getTime(/** timestamp=0 **/) {
    var ts = arguments[0] || 0;
    var t, y, m, d, h, i, s;
    t = ts ? new Date(ts * 1000) : new Date();
    y = (1900+t.getYear());
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

//==================================================
//=====MD5 加密（与C# MD5ProcessHelper.GetMD5一致）==
// 1) 输入按 UTF-16LE 取字节（对应 C# Encoding.Unicode.GetBytes）
// 2) 输出 ToString("x") 格式（单字节十六进制，不补前导零）
//==================================================
function md5Bytes(bytes) {
    function RotateLeft(lValue, iShiftBits) {
        return (lValue << iShiftBits) | (lValue >>> (32 - iShiftBits));
    }
    function AddUnsigned(lX, lY) {
        var lX4, lY4, lX8, lY8, lResult;
        lX8 = (lX & 0x80000000);
        lY8 = (lY & 0x80000000);
        lX4 = (lX & 0x40000000);
        lY4 = (lY & 0x40000000);
        lResult = (lX & 0x3FFFFFFF) + (lY & 0x3FFFFFFF);
        if (lX4 & lY4) {
            return (lResult ^ 0x80000000 ^ lX8 ^ lY8);
        }
        if (lX4 | lY4) {
            if (lResult & 0x40000000) {
                return (lResult ^ 0xC0000000 ^ lX8 ^ lY8);
            } else {
                return (lResult ^ 0x40000000 ^ lX8 ^ lY8);
            }
        } else {
            return (lResult ^ lX8 ^ lY8);
        }
    }
    function F(x, y, z) { return (x & y) | ((~x) & z); }
    function G(x, y, z) { return (x & z) | (y & (~z)); }
    function H(x, y, z) { return (x ^ y ^ z); }
    function I(x, y, z) { return (y ^ (x | (~z))); }
    function FF(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(F(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    }
    function GG(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(G(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    }
    function HH(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(H(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    }
    function II(a, b, c, d, x, s, ac) {
        a = AddUnsigned(a, AddUnsigned(AddUnsigned(I(b, c, d), x), ac));
        return AddUnsigned(RotateLeft(a, s), b);
    }

    // 将字节数组转为32位字数组，附加MD5填充
    function bytesToWords(data) {
        var nBytes = data.length;
        var nBlocks = Math.floor((nBytes + 8) / 64) + 1;
        var words = new Array(nBlocks * 16);
        for (var i = 0; i < words.length; i++) words[i] = 0;
        for (var i = 0; i < nBytes; i++) {
            words[i >> 2] |= data[i] << ((i % 4) * 8);
        }
        words[nBytes >> 2] |= 0x80 << ((nBytes % 4) * 8);
        words[nBlocks * 16 - 2] = (nBytes * 8) & 0xFFFFFFFF;
        words[nBlocks * 16 - 1] = Math.floor(nBytes * 8 / 0x100000000);
        return words;
    }
    // C# ToString("x")：单字节十六进制，不补前导零
    function wordToHexNoPad(lValue) {
        var s = "";
        for (var lCount = 0; lCount <= 3; lCount++) {
            var lByte = (lValue >>> (lCount * 8)) & 255;
            s += lByte.toString(16);
        }
        return s;
    }

    var x = bytesToWords(bytes);
    var a = 0x67452301, b = 0xEFCDAB89, c = 0x98BADCFE, d = 0x10325476;
    var S11 = 7, S12 = 12, S13 = 17, S14 = 22;
    var S21 = 5, S22 = 9, S23 = 14, S24 = 20;
    var S31 = 4, S32 = 11, S33 = 16, S34 = 23;
    var S41 = 6, S42 = 10, S43 = 15, S44 = 21;
    for (var k = 0; k < x.length; k += 16) {
        var AA = a, BB = b, CC = c, DD = d;
        a = FF(a, b, c, d, x[k + 0], S11, 0xD76AA478);
        d = FF(d, a, b, c, x[k + 1], S12, 0xE8C7B756);
        c = FF(c, d, a, b, x[k + 2], S13, 0x242070DB);
        b = FF(b, c, d, a, x[k + 3], S14, 0xC1BDCEEE);
        a = FF(a, b, c, d, x[k + 4], S11, 0xF57C0FAF);
        d = FF(d, a, b, c, x[k + 5], S12, 0x4787C62A);
        c = FF(c, d, a, b, x[k + 6], S13, 0xA8304613);
        b = FF(b, c, d, a, x[k + 7], S14, 0xFD469501);
        a = FF(a, b, c, d, x[k + 8], S11, 0x698098D8);
        d = FF(d, a, b, c, x[k + 9], S12, 0x8B44F7AF);
        c = FF(c, d, a, b, x[k + 10], S13, 0xFFFF5BB1);
        b = FF(b, c, d, a, x[k + 11], S14, 0x895CD7BE);
        a = FF(a, b, c, d, x[k + 12], S11, 0x6B901122);
        d = FF(d, a, b, c, x[k + 13], S12, 0xFD987193);
        c = FF(c, d, a, b, x[k + 14], S13, 0xA679438E);
        b = FF(b, c, d, a, x[k + 15], S14, 0x49B40821);
        a = GG(a, b, c, d, x[k + 1], S21, 0xF61E2562);
        d = GG(d, a, b, c, x[k + 6], S22, 0xC040B340);
        c = GG(c, d, a, b, x[k + 11], S23, 0x265E5A51);
        b = GG(b, c, d, a, x[k + 0], S24, 0xE9B6C7AA);
        a = GG(a, b, c, d, x[k + 5], S21, 0xD62F105D);
        d = GG(d, a, b, c, x[k + 10], S22, 0x02441453);
        c = GG(c, d, a, b, x[k + 15], S23, 0xD8A1E681);
        b = GG(b, c, d, a, x[k + 4], S24, 0xE7D3FBC8);
        a = GG(a, b, c, d, x[k + 9], S21, 0x21E1CDE6);
        d = GG(d, a, b, c, x[k + 14], S22, 0xC33707D6);
        c = GG(c, d, a, b, x[k + 3], S23, 0xF4D50D87);
        b = GG(b, c, d, a, x[k + 8], S24, 0x0455A14ED);
        a = GG(a, b, c, d, x[k + 13], S21, 0xA9E3E905);
        d = GG(d, a, b, c, x[k + 2], S22, 0xFCEFA3F8);
        c = GG(c, d, a, b, x[k + 7], S23, 0x676F02D9);
        b = GG(b, c, d, a, x[k + 12], S24, 0x8D2A4C8A);
        a = HH(a, b, c, d, x[k + 5], S31, 0xFFFA3942);
        d = HH(d, a, b, c, x[k + 8], S32, 0x8771F681);
        c = HH(c, d, a, b, x[k + 11], S33, 0x6D9D6122);
        b = HH(b, c, d, a, x[k + 14], S34, 0xFDE5380C);
        a = HH(a, b, c, d, x[k + 1], S31, 0xA4BEEA44);
        d = HH(d, a, b, c, x[k + 4], S32, 0x4BDECFA9);
        c = HH(c, d, a, b, x[k + 7], S33, 0xF6BB4B60);
        b = HH(b, c, d, a, x[k + 10], S34, 0xBEBFBC70);
        a = HH(a, b, c, d, x[k + 13], S31, 0x289B7EC6);
        d = HH(d, a, b, c, x[k + 0], S32, 0xEAA127FA);
        c = HH(c, d, a, b, x[k + 3], S33, 0xD4EF3085);
        b = HH(b, c, d, a, x[k + 6], S34, 0x04881D05);
        a = HH(a, b, c, d, x[k + 9], S31, 0xD9D4D039);
        d = HH(d, a, b, c, x[k + 12], S32, 0xE6DB99E5);
        c = HH(c, d, a, b, x[k + 15], S33, 0x1FA27CF8);
        b = HH(b, c, d, a, x[k + 2], S34, 0xC4AC5665);
        a = II(a, b, c, d, x[k + 0], S41, 0xF4292244);
        d = II(d, a, b, c, x[k + 7], S42, 0x432AFF97);
        c = II(c, d, a, b, x[k + 14], S43, 0xAB9423A7);
        b = II(b, c, d, a, x[k + 5], S44, 0xFC93A039);
        a = II(a, b, c, d, x[k + 12], S41, 0x655B59C3);
        d = II(d, a, b, c, x[k + 3], S42, 0x8F0CCC92);
        c = II(c, d, a, b, x[k + 10], S43, 0xFFEFF47D);
        b = II(b, c, d, a, x[k + 1], S44, 0x85845DD1);
        a = II(a, b, c, d, x[k + 8], S41, 0x6FA87E4F);
        d = II(d, a, b, c, x[k + 15], S42, 0xFE2CE6E0);
        c = II(c, d, a, b, x[k + 6], S43, 0xA3014314);
        b = II(b, c, d, a, x[k + 13], S44, 0x4E0811A1);
        a = II(a, b, c, d, x[k + 4], S41, 0xF7537E82);
        d = II(d, a, b, c, x[k + 11], S42, 0xBD3AF235);
        c = II(c, d, a, b, x[k + 2], S43, 0x2AD7D2BB);
        b = II(b, c, d, a, x[k + 9], S44, 0xEB86D391);
        a = AddUnsigned(a, AA);
        b = AddUnsigned(b, BB);
        c = AddUnsigned(c, CC);
        d = AddUnsigned(d, DD);
    }
    return wordToHexNoPad(a) + wordToHexNoPad(b) + wordToHexNoPad(c) + wordToHexNoPad(d);
}

// JS字符串转UTF-16LE字节（BMP字符），等价于C# Encoding.Unicode.GetBytes
function strToUtf16leBytes(str) {
    var bytes = [];
    for (var i = 0; i < str.length; i++) {
        var code = str.charCodeAt(i);
        bytes.push(code & 0xFF);
        bytes.push((code >>> 8) & 0xFF);
    }
    return bytes;
}

// 等价于 C# MD5ProcessHelper.GetMD5
function getMd5Unicode(str) {
    return md5Bytes(strToUtf16leBytes(str));
}

// 双重MD5：等价于 C# MD5ProcessHelper.GetMd5Password，登录密码加密传输使用
function md5Twice(str) {
    return getMd5Unicode(getMd5Unicode(str));
}


//表单数据序列化为Json对象
// 扩展 jQuery 原型
$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name]) {
            // 如果属性已存在，说明是同名元素（如 checkbox），转为数组处理
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            // 单个值直接赋值
            o[this.name] = this.value || '';
        }
    });
    return o;
};