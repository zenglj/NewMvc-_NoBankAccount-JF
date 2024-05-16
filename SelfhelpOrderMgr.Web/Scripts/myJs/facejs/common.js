var ctrack = new clm.tracker();
ctrack.init();
var vid = document.getElementById('videoel'),
    overlay = document.getElementById('overlay');
var userFace = {
    lastTime: 0, //时间因素
    last_DIF: 0, //记录上一次左右两边距离差值
    last_dis_eye_norse: 0,
    last_nose_left: 0,
    last_nose_top: 0,
    counter: 0,
    // vid: document.getElementById('videoel'),
    vid_width: vid.width,
    vid_height: vid.height,
    overlay: document.getElementById('overlay'),
    overlayCC: overlay.getContext('2d'),
    trackingStarted: false,
    positions: [],
    faceFlag: 10, //11 眨眼睛 12摇头 13收集重组数据,
    turningPoint: '',//0 为向右 1为向左
    turningPointVal: [],//出现水平转折点的数组
    turningPointNum:0
}
// 当摄像头调用不起来时的使用默认视频
userFace.insertAltVideo = function () {
    if (supports_video()) {
        if (supports_webm_video()) {
            $('video').attr('src', "./media/cap12_edit.webm");
        } else if (supports_h264_baseline_video()) {
            $('video').attr('src', "./media/cap12_edit.mp4");
        } else {
            return false;
        }
        return true;
    } else {
        return false;
    }
}
// 画布宽和视频的宽保持相等，高度自适应
userFace.adjustVideoProportions = function () {
    var proportion = vid.width / vid.height;
    this.vid_width = this.vid_height * proportion;
    this.overlay.width = this.vid_width;
}
// 调用摄像头成功回调
userFace.gumSuccess = function (stream) {
    var that = this;
    if ("srcObject" in vid) {
        vid.srcObject = stream;
    } else {
        vid.src = (window.URL && window.URL.createObjectURL(stream));
    }
    vid.onloadedmetadata = function () {
        userFace.adjustVideoProportions();
        vid.play();
        // userFace.drawToCanvas()
    }
    vid.onresize = function () {
        userFace.adjustVideoProportions();
        if (userFace.trackingStarted) {
            ctrack.stop();
            ctrack.reset();
            ctrack.start(vid);
        }
    }

}
// 调用摄像头失败回调
userFace.gumFail = function () {
    // fall back to video if getUserMedia failed
    userFace.insertAltVideo(vid);
    alert('该设备无调起摄像头权限或该设备没有摄像头！');
}
// 调用摄像头
userFace.callTheCamera = function () {
    var _this = this;
    navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
    // window.URL = window.URL || window.webkitURL || window.msURL || window.mozURL;
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

    // set up video
    if (navigator.mediaDevices) {
        navigator.mediaDevices.getUserMedia({ video: true }).then(_this.gumSuccess).catch(_this.gumFail);
    } else if (navigator.getUserMedia) {
        navigator.getUserMedia({ video: true }, _this.gumSuccess, _this.gumFail);
    } else {
        _this.insertAltVideo(vid);
        alert("当前设备不支持打开摄像头或无此权限！");
    }

    // vid.addEventListener('canplay', _this.enablestart, false);
}
// 初始化函数
userFace.init = function () {
    this.counter = 0;
    this.lastTime = 0;
    this.last_dis_eye_norse = 0;
    this.last_nose_left = 0;
    this.last_nose_top = 0;
    this.last_DIF = 0;
    DIF = 0;
    dis_eye_norse = 0;
    ctrack.stop();
    ctrack.reset();
    vid.play();
    ctrack.start(vid);
    this.trackingStarted = true;
    this.drawLoop();
    localStorage.removeItem('positions')
    this.turningPointVal = [];
}
// 关闭人脸识别
userFace.closeFace = function () {
    ctrack.stop();
    ctrack.reset();
    vid.pause();
    // $('.reidentification').addClass('active');
}
// 绘画
userFace.drawface = function (positions) {
    for (var p = 0; p < 71; p++) {
        this.overlayCC.beginPath();
        this.overlayCC.arc(positions[p][0].toFixed(2), positions[p][1].toFixed(2), 2, 0, Math.PI * 2, true);
        this.overlayCC.closePath();
        this.overlayCC.fillStyle = '#00FF00';
        this.overlayCC.fill();
    }
    //左眼中间
    for (var p = 27; p <= 27; p++) {
        this.overlayCC.beginPath();
        this.overlayCC.arc(positions[p][0].toFixed(2), positions[p][1].toFixed(2), 2, 0, Math.PI * 2, true);
        this.overlayCC.closePath();
        this.overlayCC.fillStyle = 'red';
        this.overlayCC.fill();
    }

    //鼻子中间
    for (var p = 62; p <= 62; p++) {
        this.overlayCC.beginPath();
        this.overlayCC.arc(positions[p][0].toFixed(2), positions[p][1].toFixed(2), 2, 0, Math.PI * 2, true);
        this.overlayCC.closePath();
        this.overlayCC.fillStyle = 'red';
        this.overlayCC.fill();
    }
    //嘴巴上
    for (var p = 57; p <= 57; p++) {
        this.overlayCC.beginPath();
        this.overlayCC.arc(positions[p][0].toFixed(2), positions[p][1].toFixed(2), 2, 0, Math.PI * 2, true);
        this.overlayCC.closePath();
        this.overlayCC.fillStyle = 'red';
        this.overlayCC.fill();
    }
    //嘴巴下
    for (var p = 60; p <= 60; p++) {
        this.overlayCC.beginPath();
        this.overlayCC.arc(positions[p][0].toFixed(2), positions[p][1].toFixed(2), 2, 0, Math.PI * 2, true);
        this.overlayCC.closePath();
        this.overlayCC.fillStyle = 'red';
        this.overlayCC.fill();
    }
}
// 绘制人脸
userFace.drawLoop = function () {
    requestAnimFrame(this.drawLoop.bind(this));
    this.overlayCC.clearRect(0, 0, this.vid_width, this.vid_height);
    // ctrack.getCurrentPosition()检测不到人脸时返回false，反之返回脸部72个坐标点
    if (ctrack.getCurrentPosition()) {
        // 绘制人脸
        ctrack.draw(this.overlay);
        // this.drawface(ctrack.getCurrentPosition())
        this.positions = ctrack.getCurrentPosition() || []
        // overlaythis.overlayCC.drawImage(video, 0, 0,vid_width, vid_height);
        // 摇头判断
        if (this.faceFlag == '11') {
            this.twinkle(ctrack.getCurrentPosition());
        } else if (this.faceFlag == '12') {
            this.shakeHead(ctrack.getCurrentPosition());
        } else if (this.faceFlag == '13') {
            // console.log(ctrack.getConvergence())
            // console.log(ctrack.getCurrentParameters())
            // return
            //截取一段时间内的相片
            this.dataReconstitution(ctrack.getCurrentPosition());
        } else if (this.faceFlag == '14') {
            //住宿屏幕，直接拍照获取人脸信息
            this.getuserface(ctrack.getCurrentPosition());
        }

    }
}
userFace.drawToCanvas = function (effect) {
    requestAnimFrame(this.drawToCanvas.bind(this));
    var video = vid,
        ctx = this.overlayCC,
        canvas = this.overlay,
        i;

    ctx.drawImage(video, canvas.width, canvas.height);

    this.pixels = this.overlayCC.getImageData(0, 0, canvas.width, canvas.height);
    // for (i = 0; i < this.pixels.data.length; i = i + 4) {
    //     this.pixels.data[i + 0] = this.pixels.data[i + 0] * 3;
    //     this.pixels.data[i + 1] = this.pixels.data[i + 1] * 2;
    //     this.pixels.data[i + 2] = this.pixels.data[i + 2] - 10;
    // }
    // ctx.putImageData(this.pixels, 0, 0);
    var comp = ccv.detect_objects({
        "canvas": overlay,
        "cascade": cascade,
        "interval": 2,
        "min_neighbors": 1
    });
    console.log(comp)

    // Draw glasses on everyone!
    for (i = 0; i < comp.length; i++) {
        this.overlayCC.drawImage(vid, comp[i].x, comp[i].y, comp[i].width, comp[i].height);
        var snapData = overlay.toDataURL('image/png');
        var imgSrc = "data:image/png;" + snapData;
        console.log(imgSrc)
        $('.imgbox img').attr('src', imgSrc)
    }


},

    // 判断摇头
userFace.shakeHead = function (positions) {

    if (positions.length == 0) {
        return;
    }
    // 指定时间段内收集参数
    if (this.lastTime == 0 || (new Date().getTime() - this.lastTime > 500 && new Date().getTime() - this.lastTime < 10000)) {
        // console.log(positions[62][0])
        // 计算鼻尖和左边轮廓线中间点的水平差值
        var l_diff_x = positions[62][0] - positions[2][0];
        // 计算鼻尖和左边轮廓线中间点的垂直差值
        var l_diff_y = positions[62][1] - positions[2][1];
        // 计算鼻尖点与左边轮廓线中间点的距离
        var l_distance = Math.pow((l_diff_x * l_diff_x + l_diff_y * l_diff_y), 0.5);
        // 计算鼻尖和右边轮廓线中间点的水平差值
        var r_diff_x = positions[12][0] - positions[62][0];
        // 计算鼻尖点和右边轮廓线中间点的垂直差值
        var r_diff_y = positions[12][1] - positions[62][1];
        // 计算鼻尖与右边轮廓线中间点的距离
        var r_distance = Math.pow((r_diff_x * r_diff_x + r_diff_y * r_diff_y), 0.5);
        // 计算出左右轮廓线中间点的水平差值
        var lr_diff_x = positions[12][0] - positions[2][0];
        // 计算出左右轮廓线中间点的垂直差值
        var lr_diff_y = positions[12][1] - positions[2][1];
        // 计算出左右轮廓线两中间点的直线距离
        var lr_distance = Math.pow((lr_diff_x * lr_diff_x + lr_diff_y * lr_diff_y), 0.5);
        // 计算出左右两轮廓线中间点距离鼻尖的差值
        var DIF = l_distance - r_distance;
        // if (Math.abs(DIF) > 2) {
        //     this.counter++
        // }
        if ((this.last_DIF > 0 && DIF < 0) || (this.last_DIF < 0 && DIF > 0)) {
            this.counter++
        }
        console.log(this.last_DIF, Math.abs(DIF), lr_distance / 6);
        // 验证是否摇头
        if (this.last_DIF > 1 && this.counter >= 1 && Math.abs(DIF) > lr_distance / 6) {
            console.log('摇头已验证通过')
            this.getPhoto()
            this.counter = 0;
        }
        // 重置时间因素 记录当前数据为上一次的记录
        this.last_DIF = DIF;
        this.lastTime = new Date().getTime();
    }
}
// 判断眨眼
userFace.twinkle = function (positions) {
    if (positions.length == 0) {
        return;
    }
    if (this.lastTime == 0 || (new Date().getTime() - this.lastTime > 10)) {
        var xdiff1 = positions[62][0] - positions[24][0];
        var ydiff1 = positions[62][1] - positions[24][1];
        // 计算出做左眼睛上眼皮中间点距离鼻尖的距离
        var dis_eye_norse1 = Math.pow((xdiff1 * xdiff1 + ydiff1 * ydiff1), 0.5);
        var xdiff2 = positions[62][0] - positions[29][0];
        var ydiff2 = positions[62][1] - positions[29][1];
        // 计算出做左眼睛上眼皮中间点距离鼻尖的距离
        var dis_eye_norse2 = Math.pow((xdiff2 * xdiff2 + ydiff2 * ydiff2), 0.5);
        // 计算出左右两个眼睛距离同一处鼻尖的距离之和
        var dis_eye_norse = (dis_eye_norse1 + dis_eye_norse2);
        if (this.last_nose_left > 0 && this.last_nose_top > 0 && Math.abs(positions[62][0] - this.last_nose_left) < 0.5 &&
            Math.abs(positions[62][1] - this.last_nose_top) < 0.5) {
            console.log(dis_eye_norse, this.last_dis_eye_norse, Math.abs(dis_eye_norse - this.last_dis_eye_norse), dis_eye_norse * 1 / 60)

            if (this.last_dis_eye_norse > 1 && (Math.abs(dis_eye_norse - this.last_dis_eye_norse) > dis_eye_norse * 1 / 60)) {
                //alert('眼睛验证通过') //不需要弹框zenglj 20211102改
                this.getPhoto()
            }
        }
        this.last_nose_left = positions[62][0];
        this.last_nose_top = positions[62][1];
        this.last_dis_eye_norse = dis_eye_norse;
        this.lastTime = new Date().getTime();

    }
}

// 注视屏幕，不判断眨眼直接拍照zenglj 20240113
userFace.getuserface = function (positions) {
    if (positions.length == 0) {
        return;
    }
    if (this.lastTime == 0 || (new Date().getTime() - this.lastTime > 10)) {
        var xdiff1 = positions[62][0] - positions[24][0];
        var ydiff1 = positions[62][1] - positions[24][1];
        // 计算出做左眼睛上眼皮中间点距离鼻尖的距离
        var dis_eye_norse1 = Math.pow((xdiff1 * xdiff1 + ydiff1 * ydiff1), 0.5);
        var xdiff2 = positions[62][0] - positions[29][0];
        var ydiff2 = positions[62][1] - positions[29][1];
        // 计算出做左眼睛上眼皮中间点距离鼻尖的距离
        var dis_eye_norse2 = Math.pow((xdiff2 * xdiff2 + ydiff2 * ydiff2), 0.5);
        // 计算出左右两个眼睛距离同一处鼻尖的距离之和
        var dis_eye_norse = (dis_eye_norse1 + dis_eye_norse2);
        if (this.last_nose_left > 0 && this.last_nose_top > 0 && Math.abs(positions[62][0] - this.last_nose_left) < 0.5 &&
            Math.abs(positions[62][1] - this.last_nose_top) < 0.5) {
            console.log(dis_eye_norse, this.last_dis_eye_norse, Math.abs(dis_eye_norse - this.last_dis_eye_norse), dis_eye_norse * 1 / 60)

            //if (this.last_dis_eye_norse > 1 && (Math.abs(dis_eye_norse - this.last_dis_eye_norse) > dis_eye_norse * 1 / 60)) {
            //    //alert('眼睛验证通过') //不需要弹框zenglj 20211102改
            //    this.getPhoto()
            //}
            //不判断直接拍照zenglj 20240113改
            this.getPhoto()
        }
        this.last_nose_left = positions[62][0];
        this.last_nose_top = positions[62][1];
        this.last_dis_eye_norse = dis_eye_norse;
        this.lastTime = new Date().getTime();

    }
}
//识别人脸拿到base64照片
userFace.getPhoto = function () {
    this.overlayCC.clearRect(0, 0, 200, 200);
    // var timer = null;
    // vid.addEventListener('play', function () { timer = window.setInterval(function () { userFace.overlaythis.overlayCC.drawImage(v, 0, 0, 270, 135) }, 20); }, false);
    // vid.addEventListener('pause', function () { window.clearInterval(timer); }, false);
    // vid.addEventListener('ended', function () { clearInterval(timer); }, false);
    this.overlayCC.drawImage(vid, 0, 0, 200, 150)
    // let dataUrl = overlay.toDataURL('image/jpeg', 1);
    // imgbase64 = dataUrl.replace(/^data:image\/\w+;base64,/, "");
    var snapData = overlay.toDataURL('image/png');
    var imgSrc = "data:image/png;" + snapData;
    console.log(imgSrc)
    $('.imgbox img').attr('src', imgSrc)
    this.closeFace();

    //======zenglj 20211107 修改,实现获取到相片后，上传后台验证=====start==================

    //CheckFaceFunc();//实现自动回调人脸验证

    //======zenglj 20211107 修改,实现获取到相片后，上传后台验证=====end==================

}
// 截取一段时间内的人脸对象
userFace.dataReconstitution = function (positions) {
    var  _this = this;
    // 由于数据
    if (positions.length == 0) {
        return
    }
    var temPos = [];
    var positionsArr = [];
    positions.map(function (element, index) {
        temPos.push({
            x: element[0],
            y: element[1]
        })
    })
    // if (positionsArr.length < 60) {
    //     positionsArr.push({
    //         face: temPos
    //     })
    //     this.judgeHead(positionsArr)
    // } else {
    //     positionsArr=[]
    // }
    /***************************************************************/ 
    if (localStorage.getItem('positions')) {
        var positionsArr = JSON.parse(localStorage.getItem('positions'));
    } else {
        var positionsArr = [];
    }
    if (positionsArr.length < 60) {
        positionsArr.push({
            face: temPos
        })
        localStorage.setItem('positions', JSON.stringify(positionsArr));
        this.judgeHead(positionsArr)
    } else {
        positionsArr = []
        localStorage.removeItem('positions')
    }
    /*********************************************/ 
}
userFace.dataTreating = function (data) {
    let _this = this;
    var tempAr = [],tempArr=[];
    data.map(function (element, index) {
        // return element.face_62 = { x: element.face[62].x, y: element.face[62].y }
        tempAr.push({
            face_62: { x: element.face[62].x, y: element.face[62].y },//鼻尖
            face_2: { x: element.face[2].x, y: element.face[2].y },//左轮廓
            face_12: { x: element.face[12].x, y: element.face[12].y },//右轮廓
            face_24: { x: element.face[24].x, y: element.face[24].y },//左上眼皮中间点
            face_29: { x: element.face[29].x, y: element.face[29].y },//右上眼皮中间点
            face_27: { x: element.face[27].x, y: element.face[27].y },//左眼睛中间
            face_60: { x: element.face[62].x, y: element.face[62].y },//上嘴唇下线中间点
            face_57: { x: element.face[62].x, y: element.face[62].y }//下嘴唇上线中间点
        })
    })
    tempAr.map(function (element, index) {
        if (index < tempAr.length && tempAr.length>0&&index>0) {
            tempAr[index].x_nosediff = + element.face_62.x - tempAr[index - 1].face_62.x;//鼻子水平移动距离差值
            tempAr[index].y_nosediff =  + element.face_62.y - tempAr[index - 1].face_62.y;//鼻子垂直移动距离差值
            tempAr[index].xl_eyediff = + element.face_24.x - tempAr[index - 1].face_24.x;//左上眼皮水平移动距离差值
            tempAr[index].yl_nosediff = + element.face_24.y - tempAr[index - 1].face_24.y;//左上眼皮垂直垂直移动距离差值
            tempAr[index].xr_eyediff = + element.face_29.x - tempAr[index - 1].face_29.x;//右上眼皮水平移动距离差值
            tempAr[index].yr_nosediff = + element.face_29.y - tempAr[index - 1].face_29.y;//右上眼皮垂直移动距离差值
            tempAr[index].xl_outlinediff =+ element.face_2.x - tempAr[index - 1].face_2.x ;//左轮廓线水平移动距离差值
            tempAr[index].yl_outlinediff = + element.face_2.y - tempAr[index - 1].face_2.y;//左轮廓线垂直移动距离差值
            tempAr[index].xr_outlinediff = + element.face_12.x- tempAr[index - 1].face_12.x ;//右轮廓线水平移动距离差值
            tempAr[index].yr_outlinediff =  + element.face_12.y- tempAr[index - 1].face_12.y;//右轮廓线垂直移动距离差值
            tempAr[index].l_outline_nose_dis = _this.calDistance(element.face_62.x - element.face_2.x, element.face_62.y - element.face_2.y);// 鼻尖点与左边轮廓线中间点的距离
            tempAr[index].r_outline_nose_dis = _this.calDistance(element.face_12.x - element.face_62.x, element.face_12.y - element.face_62.y);// 鼻尖点与右边轮廓线中间点的距离
            tempAr[index].lr_outline_dis = _this.calDistance(element.face_12.x - element.face_2.x, element.face_12.y - element.face_2.y);// 计算出左右轮廓线两中间点的直线距离
            // 计算出左右两轮廓线中间点距离鼻尖的差值  可通过对比该值判断左右摇头
            tempAr[index].lr_outline_nose_dis =element.l_outline_nose_dis -element.r_outline_nose_dis;

            // 左轮廓线中点到鼻尖的距离-有轮廓线到鼻尖的距离判断当前脸部是朝向左边（<0）还是右边(>0)
            if (element.lr_outline_nose_dis< 0) {
                tempAr[index].txt = '右'
            } else if (element.lr_outline_nose_dis > 0) {
                tempAr[index].txt = '左'
            } else if (element.lr_outline_nose_dis == 0) {
                if (tempAr.length > 0) {
                    tempAr[index].txt = tempAr[index - 1].txt
                }
            }
            if (tempAr.length > 1) {
                tempAr[tempAr.length - 1].txt = tempAr[tempAr.length - 2].txt;
            }
        }
        if (!element.txt) {
            // console.log(element)
        }
        tempArr.push(element)
        // console.log(element.lr_outline_nose_dis)
    })
    return tempArr;
}
// 判断
userFace.judgeHead = function (positionsArr) { 
    var _this = this;
    var positionsArr = this.dataTreating(positionsArr)
    var countNum = 0;
    // _this.turningPointVal = positionsArr[0].lr_outline_nose_dis
    _this.turningPointNum = 0;
    positionsArr.map(function (element, index) {
        console.log(element.txt)
        if (index >= positionsArr.length - 1) {
            return;
        }
        // 连续向左或向右至少4个点后突然转向定义为一个转折点   转折点计数器+1  转折后计数器清0
        // console.log(element, index);
        if ((element.txt == '左' && positionsArr[index + 1].txt == '右') || (element.txt == '右' && positionsArr[index + 1].txt == '左')) {
            if (countNum >5) {
                console.log(index + '转折点' + positionsArr[index + 1].lr_outline_nose_dis)
                countNum = 0
                positionsArr[index + 1].turningPointFlag = true;
                _this.turningPointVal.push(positionsArr[index + 1].lr_outline_nose_dis)
                _this.turningPointVal = [...new Set(_this.turningPointVal)];
            } else {
                countNum++
            }
        } else {
            countNum++
        }
    })
    // 判断转折点的个数
    if (_this.turningPointVal.length > 2 && this.judgeArrange()) {
        console.log(_this.turningPointVal)
        this.turningPointVal = [];
        _this.getPhoto()
    }
    // console.log(positionsArr)
    localStorage.setItem('positionsArr', JSON.stringify(positionsArr))
}
// 计算两点之间的距离
userFace.calDistance = function (x, y) {
    return Math.pow((x * x + y * y), 0.5);
}
// 求取积
userFace.quadrature = function (arrary) {
    if (arrary instanceof Array) {
        return arrary.reduce(function (a, b) {
            return a * b;
        })
    }
}
// 判断数组中是否有正负数
userFace.judgeArrange = function () {
    var num1 = 0,num2 =0;
    this.turningPointVal.map(function (element, index) {
        if (element > 0) {
            num1++
        }
        if (element < 0) {
            num2++
        }
    })
    if (num1 > 0 && num2 > 0) {
        return true;
    } else {
        return false;
    }
}
// console.log(quadrature(arr));


