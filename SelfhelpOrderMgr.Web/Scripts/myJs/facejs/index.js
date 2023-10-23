/**
 * 0-14		轮廓
 * 7		下吧，最下
 * 2		左半边轮廓线的中间点
 * 12		右半边轮廓线的中间点
 * 15-18 	右边眉毛（右到左）
 * 19-22 	左边眉毛（左到右）
 * 24(29) 	左(右)眼上眼皮的中间点 
 * 26(31) 	左(右)眼下眼皮的中间点 
 * 27(32) 	左(右)眼水平垂直中心点
 * 62   	鼻尖点
 * 60		上嘴唇bot中间点
 * 57		下嘴唇top中间点
 * */

var lastTime = 0; //时间因素
var last_DIF = 0; //记录上一次左右两边距离差值
var last_dis_eye_norse = 0,
	last_nose_left = 0,
	last_nose_top = 0;
var counter = 0;
var vid = document.getElementById('videoel');
var vid_width = vid.width;
var vid_height = vid.height;
var overlay = document.getElementById('overlay');
var overlayCC = overlay.getContext('2d');
function enablestart() {
	var startbutton = document.getElementById('startbutton');
	startbutton.value = "start";
	startbutton.disabled = null;
}

var insertAltVideo = function(video) {
	// insert alternate video if getUserMedia not available
	if (supports_video()) {
		if (supports_webm_video()) {
			video.src = "./media/cap12_edit.webm";
		} else if (supports_h264_baseline_video()) {
			video.src = "./media/cap12_edit.mp4";
		} else {
			return false;
		}
		return true;
	} else return false;
}

function adjustVideoProportions() {
	var proportion = vid.videoWidth/vid.videoHeight;
	vid_width = Math.round(vid_height * proportion);
	vid.width = vid_width;
	overlay.width = vid_width;
}

function gumSuccess( stream ) {
	// add camera stream if getUserMedia succeeded
	if ("srcObject" in vid) {
		vid.srcObject = stream;
	} else {
		vid.src = (window.URL && window.URL.createObjectURL(stream));
	}
	vid.onloadedmetadata = function() {
		adjustVideoProportions();
		vid.play();
	}
	vid.onresize = function() {
		adjustVideoProportions();
		if (trackingStarted) {
			ctrack.stop();
			ctrack.reset();
			ctrack.start(vid);
		}
	}
}

function gumFail() {
	// fall back to video if getUserMedia failed
	insertAltVideo(vid);
	alert("There was some problem trying to fetch video from your webcam, using a fallback video instead.");
}

navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.mozGetUserMedia || navigator.msGetUserMedia;
window.URL = window.URL || window.webkitURL || window.msURL || window.mozURL;

// set up video
if (navigator.mediaDevices) {
	navigator.mediaDevices.getUserMedia({video : true}).then(gumSuccess).catch(gumFail);
} else if (navigator.getUserMedia) {
	navigator.getUserMedia({video : true}, gumSuccess, gumFail);
} else {
	insertAltVideo(vid);
	alert("当前设备不支持打开摄像头或无此权限！");
}

vid.addEventListener('canplay', enablestart, false);

/*********** Code for face tracking *********/

var ctrack = new clm.tracker();
ctrack.init();
var trackingStarted = false;

function startVideo() {
	// start video
	resetFun();
	trackingStarted = true;
	// start loop to draw face
	drawLoop();
}

function drawLoop() {
	requestAnimFrame(drawLoop);
	overlayCC.clearRect(0, 0, vid_width, vid_height);
	// ctrack.getCurrentPosition()检测不到人脸时返回false，反之返回脸部71个坐标点
	if (ctrack.getCurrentPosition()) {
		// 绘制人脸
		ctrack.draw(overlay);
		// overlayCC.drawImage(video, 0, 0,vid_width, vid_height);
		// 摇头判断
		// shakeHead(ctrack.getCurrentPosition());
		twinkle(ctrack.getCurrentPosition())
	}
}

// 判断摇头
function shakeHead(positions) {
	// 指定时间段内收集参数
	if (lastTime == 0 || (new Date().getTime() - lastTime > 500 && new Date().getTime() - lastTime < 10000)){
		// console.log(positions[62][0])
		// 计算鼻尖和左边轮廓线中间点的水平差值
		var l_diff_x = positions[62][0] - positions[2][0];
		// 计算鼻尖和左边轮廓线中间点的垂直差值
		var l_diff_y = positions[62][1] - positions[2][1];
		// 计算鼻尖点与左边轮廓线中间点的距离
		var l_distance = Math.pow((l_diff_x*l_diff_x + l_diff_y*l_diff_y),0.5);
		// 计算鼻尖和右边轮廓线中间点的水平差值
		var r_diff_x = positions[12][0] - positions[62][0];
		// 计算鼻尖点和右边轮廓线中间点的垂直差值
		var r_diff_y = positions[12][1] - positions[62][1];
		// 计算鼻尖与右边轮廓线中间点的距离
		var r_distance = Math.pow((r_diff_x*r_diff_x + r_diff_y*r_diff_y),0.5);
		// 计算出左右轮廓线中间点的水平差值
		var lr_diff_x = positions[12][0] - positions[2][0];
		// 计算出左右轮廓线中间点的垂直差值
		var lr_diff_y = positions[12][1] - positions[2][1];
		// 计算出左右轮廓线两中间点的直线距离
		var lr_distance = Math.pow((lr_diff_x*lr_diff_x + lr_diff_y*lr_diff_y),0.5);
		// 计算出左右两轮廓线中间点距离鼻尖的差值
		var DIF = l_distance - r_distance;
		if (Math.abs(DIF) > 2) { 
			counter++
		}
		// if((DIF<0 && Math.abs(DIF)>2) || counter>2){
		// 	counter++
		// }
		console.log(last_DIF,Math.abs(DIF),lr_distance/5);
		// 验证是否摇头
		// DIF = Math.abs(DIF);
		if(last_DIF>1 && counter>5 && Math.abs(DIF)>lr_distance/5){
			console.log('摇头已验证通过')
			getPhoto()
			counter=0;
		}else if(last_DIF<0 && counter>10 && Math.abs(DIF)>lr_distance/5){
			
		}
		// 重置时间因素 记录当前数据为上一次的记录
		last_DIF = DIF;
		lastTime = new Date().getTime();
	}
}
// 判断眨眼
function twinkle (positions) {
	if(lastTime == 0 || (new Date().getTime() - lastTime > 10)) {
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
		// Math.abs(positions[62][0] - last_nose_left) < 0.5 &&  Math.abs(positions[62][1] - last_nose_top) < 0.5 保证脸部浮动不要过大即鼻尖位置变化不能超过0.5
		// if(last_nose_left > 0 && last_nose_top > 0 &&
		// 	Math.abs(positions[62][0] - last_nose_left) < 0.5 &&
		// 	Math.abs(positions[62][1] - last_nose_top) < 0.5
		// ) {
		// 	if(last_dis_eye_norse > 0 && (Math.abs(dis_eye_norse - last_dis_eye_norse) > dis_eye_norse * 1 / 30)) {

		// 		console.log('眼睛验证通过')
		// 		getPhoto()
		// 	}
		// }
		console.log(dis_eye_norse,last_dis_eye_norse, Math.abs(dis_eye_norse - last_dis_eye_norse), dis_eye_norse * 1 / 30)
		if (last_nose_left > 0 && last_nose_top > 0 &&Math.abs(positions[62][0] - last_nose_left) < 0.5 &&
			Math.abs(positions[62][1] - last_nose_top) < 0.5){ 
			if (last_dis_eye_norse > 70 && (Math.abs(dis_eye_norse - last_dis_eye_norse) > dis_eye_norse * 1 / 30)) {
				console.log('眼睛验证通过')
				getPhoto()
			}
		}
		last_nose_left = positions[62][0];
		last_nose_top = positions[62][1];

		last_dis_eye_norse = dis_eye_norse;
		lastTime = new Date().getTime();

	}
}
//截取图图片
function getPhoto(){
	overlayCC.clearRect(0, 0,  vid_width, vid_height);
	overlayCC.drawImage(vid, 0,0, vid_width, vid_height)
	// let dataUrl = overlay.toDataURL('image/jpeg', 1);
	// imgbase64 = dataUrl.replace(/^data:image\/\w+;base64,/, "");
	var snapData = overlay.toDataURL('image/png');
	var imgSrc = "data:image/png;" + snapData;
	console.log(imgSrc)
	$('.imgbox img').attr('src',imgSrc)
	// drawLoop = function(){};
	resetFun();
	// ctrack.stop();
	// ctrack.reset();
}
// 重置
function resetFun() {
	counter = 0;
	lastTime = 0;
	last_dis_eye_norse = 0;
	ctrack.stop();
	ctrack.reset();
	vid.play();
	ctrack.start(vid);
}
