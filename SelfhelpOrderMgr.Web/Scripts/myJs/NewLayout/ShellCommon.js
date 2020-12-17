

function closeSystem() {

    if (confirm("你是否要关闭电脑?")) {
        //点击确定后操作
        Run('shutdown -s -t 1');
    }
}


function speechTTS(text) {
    //if (confirm("你是否要播放语音?")) {
    //    //点击确定后操作 
    //}
    Run("D:/ttsDemo/ttsDemo.exe speech \"" + text + "\"");
}


function closePC() {
    var wscript = new ActiveXObject("WScript.Shell");
    if (wscript != null) {
        wscript.SendKeys("{shutdown -s}");
    }
    //wshshell = new ActiveXObject("wscript.shell");
    //wshshell.sendkeys("{Shutdown -s -t}");
    //wshshell.sendkeys("{u}");

}

function addfavorite() {
    if (window.sidebar && "object" == typeof (window.sidebar) && "function" == typeof (window.sidebar.addPanel)) {
        window.sidebar.addPanel('互联友吧的网易博客', 'http://www.u8.blog.163.com', '互联友吧的网易博客');
    }
    else if (document.all && "object" == typeof (window.external)) {
        window.external.addFavorite('http://www.u8.blog.163.com', '互联友吧的网易博客');
    }
}

window.onbeforeunload = function () {//运行Shell命令
    var n = window.event.screenX - window.screenLeft;
    var b = n > document.documentElement.scrollWidth - 20;
    if (b && window.event.clientY < 0 || window.event.altKey) {
        if (confirm("如果你觉得互联友吧的空间有用.就请收藏本空间下次好再次访问!")) {
            addfavorite();
        }
    }
}
function Run(strPath) {
    exe.value = strPath;
    try {
        var objShell = new ActiveXObject("wscript.shell");
        objShell.Run(strPath);
        objShell = null;
    }
    catch (e) {
        alert('你的系统中找不到执行文件"' + strPath + '"(或在子文件夹)')
    }
}