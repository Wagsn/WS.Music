layui.use(['form', 'jquery'], function () {
    var form = layui.form;
    $ = layui.jquery;

    // Net Data Url
    let saveUrl = '/api/song/save'

    // Load Form Data 加载表单数据
    function loadFormData() {
        return {
            name: $(".name").val(),
            artistName: $(".artist-name").val(),
            albumName: $(".album-name").val(),
            description: $(".desc").val(),
            duration: $(".duration").val(),
            releaseTime: $(".time").val(),
            url: $(".url").val()
        }
    }
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
        return true;
    })
    // Save Item 添加提交
    function saveItem() {
        let request = {
            song: loadFormData()
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
});