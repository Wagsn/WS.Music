layui.use(['form', 'jquery'], function () {
    var form = layui.form
    $ = layui.jquery
    let listUrl = '/api/artist/list'
    let ditails = 'artist-details'
    let msg_nodata = '没有数据'
    let log_query = 'Loading data, query:'
    let log_resbody = 'Response Body:'
    let class_id = '.id'
    let class_name = ".name"
    let class_desc = ".desc"
    let class_time = '.time'

    // 初始化
    $(document).ready(function () {
        // 加载信息
        loadForm({
            pageIndex: 0, pageSize: 5, ids: [
                store.load(ditails).id
            ]
        })
    })
    // 加载表单数据
    function loadForm(query = { pageIndex: 0, pageSize: 5 }) {
        console.log(log_query, query)
        $.post(
            listUrl,
            query,
            function (resbody) {
                console.log(log_resbody, resbody)
                if (resbody.data.length > 0) {
                    renderForm(resbody.data[0])
                }
                else {
                    alert(msg_nodata)
                }
            }
        )
    }
    // 渲染表单数据
    function renderForm(data) {
        $(class_id).val(data.id)
        $(class_name).val(data.name)
        $(class_desc).val(data.description)
        $(class_time).val(data.debutTime)
        form.render()
    }
});