layui.use(['form', 'jquery'], function () {
    var form = layui.form;
    $ = layui.jquery;

    let saveUrl = '/api/artist/save'
        , listUrl = '/api/artist/list'

    $(document).ready(function () {
        // 加载信息
        loadForm({
            pageIndex: 0, pageSize: 5, ids: [
                store.load('artist-edit').id
            ]
        })
    })
    // 加载表单数据
    function loadForm(query = { pageIndex: 0, pageSize: 5}) {
        console.log('Loading data, query:', query)
        $.post(
            listUrl,
            query,
            function (resbody) {
                console.log('Response Body:', resbody)
                if (resbody.data.length > 0) {
                    renderForm(resbody.data[0])
                }
                else {
                    alert('没有数据')
                }
            }
        )
    }
    // 点击保存
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
    // 加载表单数据
    function loadFormData() {
        return {
            id: $('.id').val() || ''
            , name: $(".name").val()
            , description: $(".desc").val()
            , debutTime: $(".time").val()
        }
    }
    // 添加提交
    function saveItem() {
        let request = {
            artist: loadFormData()
        }
        console.log('Save request:', JSON.stringify(request))
        $.post(saveUrl, request,
            function (resbody) {
                console.log('Response Body:', resbody);
                if (resbody.code == "0") {
                    alert('保存成功！');
                    //window.location.reload(); //刷新当前页面
                    parent.location.reload();
                } else {
                    alert('保存失败');
                }
            })
    }
    // 渲染表单数据
    function renderForm(data) {
        $('.id').val(data.id)
        $(".name").val(data.name)
        $(".desc").val(data.description)
        $(".time").val(data.debutTime)
        form.render()
    }
});