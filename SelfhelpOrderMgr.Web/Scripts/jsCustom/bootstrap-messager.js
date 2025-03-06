(function ($) {
    const template = `
    <div class="modal fade messager-modal">
      <div class="modal-dialog modal-sm">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal">&times;</button>
            <h4 class="modal-title"></h4>
          </div>
          <div class="modal-body"></div>
          <div class="modal-footer messager-buttons"></div>
        </div>
      </div>
    </div>
  `;

    const Messager = {
        show: function ({ type = 'alert', title, content, defaultValue = '', callback }) {

            // 清理旧模态框
            $('.messager-modal').remove();

            const $modal = $(template).appendTo('body');
            const $body = $modal.find('.modal-body');
            const $footer = $modal.find('.modal-footer');

            // 动态内容插入
            $modal.find('.modal-title').text(title);
            if (type === 'prompt') {
                $body.html(`<input class="form-control messager-input" value="${defaultValue}">`);
            } else {
                $body.html(content);
            }

            // 按钮配置‌:ml-citation{ref="2,3" data="citationList"}
            const buttons = {
                alert: [{ text: '确定', class: 'btn-primary', action: 'ok' }],
                confirm: [
                    { text: '取消', class: 'btn-default', action: 'cancel' },
                    { text: '确定', class: 'btn-primary', action: 'ok' }
                ],
                prompt: [
                    { text: '取消', class: 'btn-default', action: 'cancel' },
                    { text: '提交', class: 'btn-primary', action: 'ok' }
                ]
            };

            buttons[type].forEach(btn => {
                $footer.append(`
          <button class="btn ${btn.class} btn-sm" 
                  data-action="${btn.action}">${btn.text}</button>
        `);
            });

            // 事件处理‌:ml-citation{ref="3,5" data="citationList"}
            $modal.on('click', '[data-action]', function () {
                const action = $(this).data('action');
                let result = action === 'ok';
                if (type === 'prompt' && action === 'ok') {
                    result = $body.find('input').val();
                }
                callback?.(result);
                //$modal.modal('hide').remove();
                // 仅触发隐藏，不立即移除
                $modal.modal('hide');
            });

            //$modal.modal('show').on('hidden.bs.modal', () => $modal.remove());
            // 隐藏完成后执行清理
            $modal.on('hidden.bs.modal', () => {
                $modal.remove();
                const $backdrop = $('.modal-backdrop');
                if ($backdrop.length) $backdrop.remove(); // 手动清理残留背景
            });

            $modal.modal('show');
        }
    };

    // 全局API暴露‌:ml-citation{ref="1,2" data="citationList"}
    window.Messager = {
        alert: (opt) => Messager.show({ ...opt, type: 'alert' }),
        confirm: (opt) => Messager.show({ ...opt, type: 'confirm' }),
        prompt: (opt) => Messager.show({ ...opt, type: 'prompt' })
    };
})(jQuery);


//调用示例
// Alert弹窗
//Messager.alert({
//    title: '操作成功',
//    content: '数据已保存！'
//});

//// Confirm弹窗
//Messager.confirm({
//    title: '删除确认',
//    content: '确认删除该记录？',
//    callback: (confirmed) => confirmed && console.log('执行删除')
//});

//// Prompt弹窗
//Messager.prompt({
//    title: '输入用户名',
//    defaultValue: 'guest',
//    callback: (value) => value && console.log('提交值:', value)
//});
