// 艺人管理界面

//import('/js/store.js').then(module => {
//    console.log('then store:', module.default)
//});

console.log('store:', store)

layui.use(['form', 'table', 'layer', 'laypage', 'layedit', 'laydate', 'element'], function () {
    // 组件加载
    var form = layui.form
    let element = layui.element
    let layer = parent.layer === undefined ? layui.layer : parent.layer
    let laypage = layui.laypage
    let layedit = layui.layedit
    let laydate = layui.laydate
    let table = layui.table

    // Net Data URL 网络数据源
    let listUrl = "/api/album/list"
    let delUrl = "/api/album/delete"
    let saveUrl = "/api/album/save"

    // Html 页面
    let addHtml = 'album-add.html'
    let editHtml = 'album-edit.html'
    let detailsHtml ='album-details.html'

    // Notice 提示信息
    let msg_search = '查询中，请稍候'
    let msg_return = '点击此处返回列表'
    let log_loading = 'Loading Query:'
    let log_pageInfo = 'Paging information:'
    let log_loaded = 'Loaded table data:'
    let log_render = 'Rendering Data: '

    // Selector 选择器
    let searchBtn = '.search-btn'
    let addBtn = '.add-btn'
    let editBtn = '.edit-btn'
    let delBtn = '.del-btn'
    let detailsBtn = '.details-btn'
    let searchInput = '.search-input'
    let pageTableBody = '#page-table-body'

    // Store Key
    let key_details = 'album-details'
    let key_edit = 'album-edit'
    let key_curr = 'album-curr'

    // 渲染表格
    function renderTable(data) {
        console.log(log_render, data)
        let dataHtml = ""
        $.each(data, function (v, o) {
            dataHtml += '<tr>' +
                '<td><input name="checked" lay-skin="primary" lay-filter="choose" type="checkbox"><div class="layui-unselect layui-form-checkbox" lay-skin="primary"><i class="layui-icon layui-icon-ok"></i></div></td>' +
                '<td>' + o.name + '</td>' +
                '<td>' + (o.artistName || '暂无') + '</td>' +
                '<td>' + (o.description || '暂无') + '</td>' +
                '<td>' + (o.releaseTime || '暂无') + '</td>' +
                '<td>' +
                '<a class="layui-btn layui-btn-normal layui-btn-sm details-btn" data-id="' + o.id + '"><i class="layui-icon">&#xe600;</i>详情</a>' +
                '<a class="layui-btn layui-btn-normal layui-btn-sm edit-btn" data-id="' + o.id + '"><i class="layui-icon">&#xe600;</i>编辑</a>' +
                '<a class="layui-btn layui-btn-danger layui-btn-sm del-btn" data-id="' + o.id + '"><i class="layui-icon">&#xe640;</i>删除</a>' +
                '</td>' +
                '</tr>';
        })
        $(pageTableBody).html(dataHtml)
        form.render()
    }
    // 初始化页面
    $(document).ready(function () {
        // 初始化列表
        loadPage()
        // 搜索按钮点击事件
        $(searchBtn).click(function () {
            let index = layer.msg(msg_search, { icon: 16, time: false, shade: 0.8 })
            setTimeout(function () {
                loadPage(loadSearchFormData())
                layer.close(index)
            }, 1000);
        })
    })
    // 加载分页
    function loadPage(query = { pageIndex: 0, pageSize: 5, keyWord: "" }) {
        // 数据来源 服务器
        console.log(log_loading, query)
        $.post(
            listUrl,
            query,
            function (resbody) {
                let pageInfo = {
                    count: Math.ceil(resbody.totalCount / resbody.pageSize)
                    , index: resbody.pageIndex
                    , total: resbody.totalCount
                    , limit: resbody.pageSize
                }
                console.log(log_pageInfo, pageInfo)
                laypage.render({
                    // 显示分页信息的标签
                    elem: 'page'
                    , limit: pageInfo.limit
                    , count: pageInfo.total
                    , pages: pageInfo.count
                    , curr: pageInfo.index
                    , layout: ['prev', 'page', 'next']
                    // 连续显示分页数
                    //,groups: 3
                    // 页面跳转
                    , jump: function (obj, first) {
                        //得到了当前页，用于向服务端请求对应数据
                        layer.msg('第' + obj.curr + '页')
                        query.pageIndex = (obj.curr - 1)
                        console.log("Jump to page " + obj.curr + ", query;", query)
                        loadTable(query, renderTable)
                    }
                })
            }
        )
    }
    // 加载表格数据，query: 查询条件 render: 页面渲染回调函数
    function loadTable(query, render) {
        $.post(listUrl, query,
            function (resbody) {
                console.log(log_loaded, resbody)
                render(resbody.data)
            })
    }
    // 获取搜索表单数据
    function loadSearchFormData() {
        return {
            pageIndex: 0,
            pageSize: 5,
            keyWord: $(searchInput).val().trim() || ''
        }
    }
    // 全选按钮绑定点击事件
    form.on('checkbox(all-choose)', function (data) {
        // (data.elem); // 得到select原始DOM对象  (data.value); // 得到被选中的值  (data.othis); // 得到美化后的DOM对象
        // (data.elem.checked); // 是否被选中，true或者false
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"])');
        //each遍历 each() 方法为每个匹配元素规定要运行的函数。$(selector).each(function(index,element)) 
        //element - 当前的元素（也可使用 "this" 选择器）
        child.each(function (index, item) {
            item.checked = data.elem.checked
        });
        // 渲染表单
        form.render('checkbox');
    })
    // 判断是否选中
    form.on("checkbox(choose)", function (data) {
        //parents() 方法返回被选元素的所有祖先元素。
        //$(selector).parents(filter) parent() - 返回被选元素的直接父元素
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"])')
        var childChecked = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"]):checked')
        if (childChecked.length == child.length) {
            $(data.elem).parents('table').find('thead input#all-choose').get(0).checked = true
        } else {
            $(data.elem).parents('table').find('thead input#all-choose').get(0).checked = false
        }
        form.render('checkbox')
    })
    // 添加按钮绑定点击事件
    $(window).one("resize", function () {
        $(addBtn).click(function () {
            console.log('add-btn clicked')
            //弹窗新增教师
            var index = layui.layer.open({
                title: "添加信息",
                type: 2,
                maxmin: true,
                shadeClose: true, //点击遮罩关闭层
                area: ['680px', '500px'],
                content: addHtml,
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips(msg_return, '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)
                }
            })
            //layui.layer.full(index);
        })
    }).resize();
    // 点击删除按钮删除该行数据
    $("body").on("click", delBtn, function () { //删除
        var _this = $(this);
        console.log("Deleting:", _this)
        layer.confirm('确定删除此信息？', { icon: 3, title: '提示信息' }, function (index) {
            deleteItem(_this.attr("data-id"))
            loadPage()
            layer.close(index)
        });
    })
    // 提交删除改行数据的ajax请求
    function deleteItem(id) {
        let delRequest = { albums: [{ id }] }
        console.log(`Delete Request ${JSON.stringify(delRequest)}`)
        $.post(delUrl, delRequest,
            function (resbody) {
                console.log('Deleted(' + id + '):', resbody);
                if (resbody.code == "0") {
                    layer.msg("删除成功")
                    location.reload()
                } else {
                    alert('删除失败!')
                }
            })
    }
    // 批量删除
    $(".batch-del-btn").click(function () {
        //var $checkbox = $('.page-table tbody input[type="checkbox"][name="checked"]');
        var $checked = $('#page-table-body input[type="checkbox"][name="checked"]:checked');

        if ($('input[type="checkbox"]').is(':checked')) {
            layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
                var index2 = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
                setTimeout(function () {
                    //删除数据
                    for (var j = 0; j < $checked.length; j++) { ///选中的个数$checked.length
                        deleteItem($checked.eq(j).parents("tr").find(".del-btn").attr("data-id"));//循环调用删除
                    }
                    //prop() 方法设置或返回被选元素的属性和值。
                    $('.page-table thead input[type="checkbox"]').prop("checked", false);
                    form.render(); //更新渲染表单，form.render(type, filter);第一个参数：type，为表单的 type 类型
                    //第一个参数：type，为表单的 type 类型
                    layer.close(index2); //关闭删除中
                    //layer.msg("删除成功");
                }, 2000);
            })
        } else {
            layer.msg("请选择需要删除的项");
        }
    })
    // 编辑项
    $("body").on("click", editBtn, function () {
        var _this = $(this);
        layer.confirm('确定编辑此信息？', { icon: 3, title: '提示信息' }, function (index) {
            let id = _this.attr("data-id")
            store.save(key_curr, { id })
            console.log("Jump to edit page: ", id);  //获取到该行对象
            layer.msg("加载成功");
            var index = layui.layer.open({
                title: "编辑信息",
                type: 2,
                maxmin: true,
                shadeClose: true, //点击遮罩关闭层
                area: ['800px', '500px'],
                content: editHtml,
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips(msg_return, '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)
                    //layer.close(index);
                }
            })
        });
    })
    // 详情项
    $("body").on("click", detailsBtn, function () {
        var _this = $(this);
        layer.confirm('查看该详情？', { icon: 3, title: '提示信息' }, function (index) {
            let id = _this.attr("data-id")
            store.save(key_curr, { id })
            var index2 = layui.layer.open({
                title: "详情",
                type: 2,
                maxmin: true,
                shadeClose: false, //点击遮罩关闭层
                area: ['600px', '420px'],
                content: detailsHtml,
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips(msg_return, '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)
                }
            })
            layer.close(index);
        });
    })
});