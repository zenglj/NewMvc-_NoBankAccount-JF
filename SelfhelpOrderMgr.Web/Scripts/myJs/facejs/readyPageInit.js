$(document).ready(function () {
            userFace.callTheCamera();
            userFace.init();
            $('.twinkle').click(function () {
                userFace.faceFlag = '11';
            })
            $('.dataReconstitution').click(function () {
                userFace.faceFlag = '13';
            })
            $('.reidentification').click(function () {
                userFace.init();
            })
            $('.shakeHead').click(function () {
                this.counter = 0;
                this.lastTime = 0;
                this.last_dis_eye_norse = 0;
                this.last_nose_left = 0;
                this.last_nose_top = 0;
                this.last_DIF = 0;
                userFace.faceFlag = '12';
            })
            $('#btnReadAddr').click(function () {
                console.log($("#faceImage").attr("src"));
                $("#imageSrc").val($("#faceImage").attr("src"));
                //alert("Image width " + $("#faceImage").attr("src"));
            })
        });