layui.use(['form', 'jquery'], function () {
    var form = layui.form;
    $ = layui.jquery;

    // Net Data Url
    let saveUrl = '/api/song/save'
    let listUrl = '/api/song/list'

    // Local Store Key
    let key_edit = 'song-curr'

    // log
    let log_s_res_b = 'Save Request Body:'
    let log_s_rep_b = 'Save Response Body:'
    let log_save_c = "saveform clicked"
    let log_l_q = 'Loading Query:'
    let log_res_b = 'Response Body:'

    // msg
    let msg_saved = '保存成功！'
    let msg_save_f = '保存失败！'
    let msg_saving = '数据提交中，请稍候'
    let msg_n_d = '没有数据'

    // Selector
    let saveBtn = '#saveform'
    let id = '.id'
    let name = '.name'
    let art_n = '.artist-name'
    let alb_n = '.album-name'
    let desc = '.desc'
    let dura = '.duration'
    let time = '.time'
    let = url = '.url'

    // Render Form 渲染表单数据
    function renderForm(data) {
        $(id).val(data.id)
        $(name).val(data.name)
        $(art_n).val(data.artistName)
        $(alb_n).val(data.albumName)
        $(desc).val(data.description)
        $(dura).val(data.duration)
        $(time).val(data.releaseTime)
        $(url).val(data.url)
        form.render()
    }
    // Load Form Data 加载表单数据
    function loadFormData() {
        return {
            id: $(id).val() || '',
            name: $(name).val(),
            artistName: $(art_n).val(),
            albumName: $(alb_n).val(),
            description: $(desc).val(),
            duration: $(dura).val(),
            releaseTime: $(time).val(),
            url: $(url).val()
        }
    }
    // Page Init 页面初始化
    $(document).ready(function () {
        // 加载信息
        loadForm({
            pageIndex: 0, pageSize: 5, ids: [
                store.load(key_edit).id
            ]
        })
    })
    // Load Form 加载表单
    function loadForm(query = { pageIndex: 0, pageSize: 5 }) {
        console.log(log_l_q, query)
        $.post(
            listUrl,
            query,
            function (resbody) {
                console.log(log_res_b, resbody)
                if (resbody.data.length > 0) {
                    renderForm(resbody.data[0])
                }
                else {
                    alert(msg_n_d)
                }
            }
        )
    }
    // Save Click 点击保存
    $(saveBtn).click(function () {
        console.log(log_save_c);
        var index = parent.layer.msg(msg_saving, { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            saveItem({
                song: loadFormData()
            });
            //关闭加载层
            parent.layer.close(index);
            //提示成功
            parent.layer.msg(msg_saved);
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        }, 1000);
        return true;
    })
    // Save Item 添加提交
    function saveItem(reqbody) {
        console.log(log_s_res_b, JSON.stringify(reqbody))
        $.post(saveUrl, reqbody,
            function (resbody) {
                console.log(log_s_rep_b, resbody);
                if (resbody.code == "0") {
                    alert(msg_saved);
                    parent.location.reload();
                } else {
                    alert(msg_save_f);
                }
            })
    }
});