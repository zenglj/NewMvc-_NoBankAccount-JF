var vid = document.getElementById('videoel'),
    overlay = document.getElementById('overlay'),
    context = overlay.getContext('2d');
// tracking启用摄像头,这里我选择调用原生的摄像头
var commonObj = {
    vid_width: vid.width,
    vid_height: vid.height,
    overlay: document.getElementById('overlay'),
    overlayCC: overlay.getContext('2d'),
    callTheCamera: function () {
        var _this = this;
        // navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
        window.URL = window.URL || window.webkitURL || window.msURL || window.mozURL;
        navigator.mediaDevices = navigator.mediaDevices || ((navigator.mozGetUserMedia || navigator.webkitGetUserMedia) ? {
            getUserMedia: function (c) {
                return new Promise(function (y, n) {
                    (navigator.mozGetUserMedia ||
                        navigator.webkitGetUserMedia).call(navigator, c, y, n);
                });
            }
        } : null);
        if (!navigator.mediaDevices) {
            throw new Error("getUserMedia() not supported.");
        }
        if (navigator.mediaDevices) {
            navigator.mediaDevices.getUserMedia({ video: true }).then(_this.gumSuccess).catch(_this.gumFail);
        } else if (navigator.getUserMedia) {
            navigator.getUserMedia({ video: true }, _this.gumSuccess, _this.gumFail);
        } else {
            _this.insertAltVideo(vid);
            alert("当前设备不支持打开摄像头或无此权限！");
        }
    },
    // 调用摄像头成功回调
    gumSuccess: function (stream) {
        var that = this;
        if ("srcObject" in vid) {
            vid.srcObject = stream;
        } else {
            vid.src = (window.URL && window.URL.createObjectURL(stream));
        }
        vid.onloadedmetadata = function () {
            // userFace.adjustVideoProportions();
            vid.play();
            // userFace.drawToCanvas()
        }
        vid.onresize = function () {
            // userFace.adjustVideoProportions();
            // if (userFace.trackingStarted) {
            //     ctrack.stop();
            //     ctrack.reset();
            //     ctrack.start(vid);
            // }
        }

    },
    // 调用摄像头失败回调
    gumFail: function () {
        // fall back to video if getUserMedia failed
        userFace.insertAltVideo(vid);
        alert('该设备无调起摄像头权限或该设备没有摄像头！');
    },
    // 初始化
    init: function () {
        this.callTheCamera();
        
    },
    //画方框
    drawRect: function (x, y, w, h) {
        var rect = document.createElement('div');
        document.querySelector('#container').appendChild(rect);
        rect.classList.add('rect');
        rect.style.width = w + 'px';
        rect.style.height = h + 'px';
        rect.style.left = (img.offsetLeft + x) + 'px';
        rect.style.top = (img.offsetTop + y) + 'px';
        $('.tip').html('请眨眨眼！')
    },
}


