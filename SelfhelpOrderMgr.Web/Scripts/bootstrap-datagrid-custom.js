(function ($) {
    $.fn.DataGrid = function (options) {
        const defaults = {
            url: "",              // 数据请求地址
            totalItems: 0,        // 总数据量
            pageSizeOptions: [10, 20, 50, 100], // 分页选项
            pageSize: 10,         // 默认分页大小
            columns: [],          // 表格列配置（包含title、field属性）
            onLoad: function () { } // 数据加载回调
        };

        const settings = $.extend({}, defaults, options);
        let currentPage = 1;

        // 核心渲染函数
        function renderGrid(container) {
            const $container = $(container);
            const totalPages = Math.ceil(settings.totalItems / settings.pageSize);

            // 清空容器
            $container.empty();

            // 1. 表格结构
            const tableHtml = `
                <table class="table table-bordered table-striped">
                    <thead><tr>${settings.columns.map(c => `<th>${c.title}</th>`).join('')}</tr></thead>
                    <tbody id="datagrid-body"></tbody>
                </table>
            `;
            $container.append(tableHtml);

            // 2. 分页控制栏
            const paginationHtml = `
                <div class="pagination-controls clearfix">
                    <div class="pull-left">
                        共 <strong>${settings.totalItems}</strong> 条
                        <div class="btn-group btn-group-sm">
                            ${settings.pageSizeOptions.map(size =>
                `<button class="btn btn-default ${size === settings.pageSize ? 'active' : ''}">${size}</button>`
            ).join('')}
                        </div>
                    </div>
                    <ul class="pagination pull-right"></ul>
                </div>
            `;
            $container.append(paginationHtml);

            // 3. 分页按钮逻辑
            const $pagination = $container.find('.pagination');
            $pagination.empty();
            for (let i = 1; i <= totalPages; i++) {
                $pagination.append(`<li class="${i === currentPage ? 'active' : ''}"><a href="#">${i}</a></li>`);
            }

            // 4. 事件绑定
            $container.find('.btn-group button').click(function () {
                const newSize = parseInt($(this).text());
                if (newSize !== settings.pageSize) {
                    settings.pageSize = newSize;
                    currentPage = 1;
                    settings.onLoad(currentPage, settings.pageSize); // 触发数据加载
                    renderGrid(container);
                }
            });

            $pagination.find('a').click(function (e) {
                e.preventDefault();
                const newPage = parseInt($(this).text());
                if (newPage !== currentPage) {
                    currentPage = newPage;
                    settings.onLoad(currentPage, settings.pageSize); // 触发数据加载
                    renderGrid(container);
                }
            });
        }

        // 数据更新方法
        this.updateData = function (data) {
            console.log("updateData方法被调用了");
            const $body = $(this).find('#datagrid-body');
            $body.empty();
            data.forEach(row => {
                const rowHtml = `<tr>${settings.columns.map(c => `<td>${row[c.field]}</td>`).join('')}</tr>`;
                $body.append(rowHtml);
            });
        };

        // 初始化
        return this.each(function () {
            renderGrid(this);
            settings.onLoad(currentPage, settings.pageSize); // 首次加载数据
        });
    };
})(jQuery);



//调用示例
//<div id="myDataGrid"></div>

//<script>
//$('#myDataGrid').DataGrid({
//    totalItems: 100,
//    columns: [
//        { title: "ID", field: "id" },
//        { title: "名称", field: "name" }
//    ],
//    onLoad: function(page, pageSize) {
//        // 模拟异步数据加载（实际应替换为Ajax请求）
//        const mockData = Array.from({length: pageSize}, (_,i) => ({
//            id: (page-1)*pageSize + i + 1,
//            name: `Item ${(page-1)*pageSize + i + 1}`
//        }));
//        $('#myDataGrid').updateData(mockData);
//    }
//});
//</script>
