/**
 * 基于 Bootstrap 3 的 Alert 提示框插件 (修正版：支持居中或右下角显示)
 * @param {string} type - 提示框类型，可选值: 'success', 'info', 'warning', 'danger'
 * @param {string} message - 提示框显示的消息内容
 * @param {number} delay - 自动关闭的延迟时间，单位为毫秒，默认为 3000 (3秒)
 * @param {string} position - 提示框显示位置，可选值: 'center' (居中), 'bottom-right' (右下角)。默认为 'center'
 */
function showMyAlert(type, message, delay, position) {
    // 设置默认参数
    delay = delay || 3000;
    position = position || 'center'; // 默认居中显示

    var $existingAlert = $('#my-custom-alert');

    // 如果页面上已存在提示框，则先将其移除
    if ($existingAlert.length > 0) {
        $existingAlert.remove();
    }

    // 创建提示框的 HTML 结构
    var alertHtml = '<div id="my-custom-alert" class="alert alert-' + type + ' alert-dismissible" role="alert" style="position: fixed; z-index: 9999;">' +
        '<button type="button" class="close" data-dismiss="alert" aria-label="Close">' +
        '<span aria-hidden="true">&times;</span>' +
        '</button>' + message +
        '</div>';

    // 将提示框添加到 body 中
    var $alert = $(alertHtml).appendTo('body');

    // 根据 position 参数设置不同的 CSS 样式
    if (position === 'bottom-right') {
        // 右下角定位
        $alert.css({
            'bottom': '20px',
            'right': '20px'
        });
    } else {
        // 居中定位 (默认，已修正)
        $alert.css({
            'top': '50%',
            'left': '50%',
            'transform': 'translate(-50%, -50%)'
        });
    }

    // 设置定时器，在指定时间后关闭并移除提示框
    setTimeout(function () {
        $alert.alert('close');
    }, delay);
}