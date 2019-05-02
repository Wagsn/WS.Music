layui.use(['form', 'jquery'], function () {
    console.log("artist-add.html is loaded");
    var form = layui.form;
    $ = layui.jquery;

    let saveUrl = '/api/artist/save'

    //$(document).ready(function () {
    //})
    // 点击保存
    $("#saveform").click(function () {
        console.log("saveform clicked");
        var index = parent.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            addItem();
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
    function getFormData() {
        return {
            name: $(".name").val()
            , description: $(".description").val()
            , releaseTime: $(".releaseTime").val()
        }
    }
    // 添加提交
    function addItem() {
        let request = {
            artist: getFormData()
        }
        console.log('', request)
        $.post(saveUrl, request,
            function (resbody) {
                console.log('Response Body:', resbody);
                if (resbody.code == "0") {
                    alert('添加成功！');
                    console.log(resbody)
                    //window.location.reload(); //刷新当前页面
                    parent.location.reload();
                } else {
                    alert('添加失败');
                }
            })
    }
    // 监听专业选择器的变化
    form.on('select(major_select)', function (data) {
        // 获取班级列表 class/list
        $.ajax({
            type: "get",
            async: true,
            url: "api/class/list/major/" + data.value,
            contentType: 'application/json',
            dataType: 'json',
            success: function (body) {
                //console.log(data);
                if (body.code == '200') {
                    renderSelect(".class_select", body.data, 'id', 'className');
                } else {
                    alert('班级列表获取失败');
                }
            },
            error: function (e) {
                alert("发生错误：" + e.status);
            }
        });
    });
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
    // 渲染表单数据
    function renderFormData(data) {
        $(".name").val(data.name)
        $(".description").val(data.description)
        $(".releaseTime").val(data.releaseTime)
    }
});