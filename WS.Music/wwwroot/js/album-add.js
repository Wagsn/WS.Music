layui.use(['form', 'jquery'], function () {
    var form = layui.form;
    $ = layui.jquery;

    // Net Data Url
    let saveUrl = '/api/album/save'
    //let listUrl = '/api/album/list'
    let artList = '/api/artist/list'

    // Local Store Key
    //let key_edit = 'album-edit'

    // Render Form 渲染表单数据
    function renderForm(data) {
        //$('.id').val(data.id)
        $(".name").val(data.name)
        $(".artist-name").val(data.artistName)
        $(".desc").val(data.description)
        $(".time").val(data.birthTime)
        form.render()
    }
    // Load Form 加载表单数据
    function loadFormData() {
        return {
            //id: $('.id').val() || '',
            name: $(".name").val(),
            artistId: $('.artist-select').find("option:selected").val(),
            artistName: $(".artist-name").val(),
            description: $(".desc").val(),
            releaseTime: $(".time").val()
        }
    }
    // Page Init 页面初始化
    $(document).ready(function () {
        // 加载信息
        //loadForm({
        //    pageIndex: 0, pageSize: 5, ids: [
        //        store.load(key_edit).id
        //    ]
        //})
        // 加载下拉框
        // loadSelect(album)
        $.post(
            artList,
            {
                pageIndex: 0,
                pageSize: 1000
            },
            function (resbody) {
                if (resbody.code == "0") {
                    renderSelect('.artist-select', resbody.data, 'id', 'name')
                }
                else {
                    console.log('查询失败：' + resbody)
                }
            }
        )
    })
    // Save Click 点击保存
    $("#saveform").click(function () {
        console.log("saveform clicked");
        var index = parent.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            saveItem();
            //关闭加载层
            parent.layer.close(index);
            //提示成功
            parent.layer.msg("添加成功！");
            layer.closeAll("iframe");
            //刷新父页面
            parent.location.reload();
        }, 1000);
        return false;
    })
    // Save Item 添加提交
    function saveItem() {
        let request = {
            album: loadFormData()
        }
        console.log('Save Request:', JSON.stringify(request))
        $.post(saveUrl, request,
            function (resbody) {
                console.log('Save Response Body:', resbody);
                if (resbody.code == "0") {
                    alert('保存成功！');
                    parent.location.reload();
                } else {
                    alert('保存失败');
                }
            })
    }
    // 通用的选择器渲染
    function renderSelect(selector, data, value, text) {
        console.log("通用选择器渲染开始，数据为：", data)
        let select = $(selector)
        let select_html = ('<option value="">直接选择或搜索选择</option>');
        for (let item of data) {
            select_html += '<option value="' + item[value] + '">' + item[text] + '</option>';
        }
        select.html(select_html);
        form.render();
    }
});