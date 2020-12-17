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
    if (snS == "-62135596800000") {
        return 'Invalid Date'
    } else {
        return new Date(parseInt(snS)).toLocaleString().substr(0, 17);
    }
   
}

function getShortTime(nS) {
    var snS = nS.substr(6);
    snS = snS.substr(0, snS.length - 2);
    var shortDt = new Date(parseInt(snS));
    var myMonth=  "0"+ (parseInt(shortDt.getMonth()) + 1);
    myMonth=myMonth.substr(myMonth.length - 2,2);
	
    var myDay=  "0"+ (parseInt(shortDt.getDate()));
    myDay=myDay.substr(myDay.length - 2,2);
    return shortDt.getFullYear() + "-" + myMonth + "-" + myDay;
}

function getLongTime(nS) {
    var snS = nS.substr(6);
    snS = snS.substr(0, snS.length - 2);
    var shortDt = new Date(parseInt(snS));
    return shortDt.getFullYear() + "-" + (parseInt(shortDt.getMonth()) + 1) + "-" + shortDt.getDate() + " " + shortDt.getHours() + ":" + shortDt.getMinutes() + ":" + shortDt.getSeconds()
}

//获取两个时间差
function getTimeDiff(nSStart,nSEnd) {
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

function getTimeMin(/** timestamp=0 **/) {
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
    return y + '-' + (m < 10 ? '0' + m : m) + '-' + (d < 10 ? '0' + d : d) + ' ' + (h < 10 ? '0' + h : h) + ':' + (i < 10 ? '0' + i : i);
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

    var unit = "千百拾亿千百拾万千百拾元角分", str = "";

    n += "00";

    var p = n.indexOf('.');

    if (p >= 0)

        n = n.substring(0, p) + n.substr(p + 1, 2);

    unit = unit.substr(unit.length - n.length);

    for (var i = 0; i < n.length; i++)

        str += '零壹贰叁肆伍陆柒捌玖'.charAt(n.charAt(i)) + unit.charAt(i);

    return str.replace(/零(千|百|拾|角)/g, "零").replace(/(零)+/g, "零").replace(/零(万|亿|元)/g, "$1").replace(/(亿)万|壹(拾)/g, "$1$2").replace(/^元零?|零分/g, "").replace(/元$/g, "元整");

}




//获取IE
function myexplorer() {
    var explorer = window.navigator.userAgent;
    if (explorer.indexOf("QQBrowser") >= 0 || explorer.indexOf("QQ") >= 0) {
        return myexplorer = "腾讯QQ";
    } else if (explorer.indexOf("Safari") >= 0 && explorer.indexOf("MetaSr") >= 0) {
        return myexplorer = "搜狗";
    } else if (!!window.ActiveXObject || "ActiveXObject" in window) {//IE
        if (!window.XMLHttpRequest) {
            return myexplorer = "IE6";
        } else if (window.XMLHttpRequest && !document.documentMode) {
            return myexplorer = "IE7";
        } else if (!-[1, ] && document.documentMode && !("msDoNotTrack" in window.navigator)) {
            return myexplorer = "IE8";
        } else {//IE9 10 11
            var hasStrictMode = (function () { "use strict"; return this === undefined; }());
            if (hasStrictMode) {
                if (!!window.attachEvent) { return myexplorer = "IE10"; } else { return myexplorer = "IE11"; }
            } else {
                return myexplorer = "IE9";
            }
        }
    } else {//非IE
        if (explorer.indexOf("LBBROWSER") >= 0) {
            return myexplorer = "猎豹";
        } else if (explorer.indexOf("360ee") >= 0) {
            return myexplorer = "360极速浏览器";
        } else if (explorer.indexOf("360se") >= 0) {
            return myexplorer = "360安全浏览器";
        } else if (explorer.indexOf("se") >= 0) {
            return myexplorer = "搜狗浏览器";
        } else if (explorer.indexOf("aoyou") >= 0) {
            return myexplorer = "遨游浏览器";
        } else if (explorer.indexOf("qqbrowser") >= 0) {
            return myexplorer = "QQ浏览器";
        } else if (explorer.indexOf("baidu") >= 0) {
            return myexplorer = "百度浏览器";
        } else if (explorer.indexOf("Firefox") >= 0) {
            return myexplorer = "火狐";
        } else if (explorer.indexOf("Maxthon") >= 0) {
            return myexplorer = "遨游";
        } else if (explorer.indexOf("Chrome") >= 0) {
            return myexplorer = "谷歌（或360伪装）";
        } else if (explorer.indexOf("Opera") >= 0) {
            return myexplorer = "欧朋";
        } else if (explorer.indexOf("TheWorld") >= 0) {
            return myexplorer = "世界之窗";
        } else if (explorer.indexOf("Safari") >= 0) {
            return myexplorer = "苹果";

        } else {
            return myexplorer = "其他";
        }
    }

}