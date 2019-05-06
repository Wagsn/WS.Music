layui.use(['form', 'jquery'], function () {
    var form = layui.form;
    $ = layui.jquery;

    // Net Data Url
    let saveUrl = '/api/song/save'

    // Load Form Data 加载表单数据
    function loadFormData() {
        let formData = new FormData()
        formData.append('name', $(".name").val())
        formData.append('artistName', $(".artist-name").val())
        formData.append('albumName', $(".album-name").val())
        formData.append('description', $(".desc").val())
        formData.append('duration', $(".duration").val())
        formData.append('releaseTime', $(".time").val())
        formData.append('file', document.getElementById("file").files[0])
        return formData;
        //return {
        //    name: $(".name").val(),
        //    artistName: $(".artist-name").val(),
        //    albumName: $(".album-name").val(),
        //    description: $(".desc").val(),
        //    duration: $(".duration").val(),
        //    releaseTime: $(".time").val(),
        //    url: $(".url").val(),
        //}
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
        //let request = loadFormData()
        let formData = new FormData()
        formData.append('song[name]', $(".name").val())
        formData.append('song[artistName]', $(".artist-name").val())
        formData.append('song[albumName]', $(".album-name").val())
        formData.append('song[description]', $(".desc").val())
        formData.append('song[duration]', $(".duration").val())
        formData.append('song[releaseTime]', $(".time").val())
        formData.append('file', document.getElementById("file").files[0])
        console.log('Save Request:', JSON.stringify(formData))

        $.ajax({
            url: saveUrl,
            type: "POST",
            data: formData,
            /**
            *必须false才会自动加上正确的Content-Type
            */
            contentType: false,
            /**
            * 必须false才会避开jQuery对 formdata 的默认处理
            * XMLHttpRequest会对 formdata 进行正确的处理
            */
            processData: false,
            success: function (resbody) {
                console.log('Save Response Body:', resbody);
                if (resbody.code == "0") {
                    alert('保存成功！');
                    parent.location.reload();
                } else {
                    alert('保存失败');
                }
            },
            error: function () {
                alert("上传失败！");
                //$("#imgWait").hide();
            }
        });
        //$.post(saveUrl, request,
        //    function (resbody) {
        //        console.log('Save Response Body:', resbody);
        //        if (resbody.code == "0") {
        //            alert('保存成功！');
        //            parent.location.reload();
        //        } else {
        //            alert('保存失败');
        //        }
        //    })
    }
});