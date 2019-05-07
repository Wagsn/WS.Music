layui.use(['form', 'jquery'], function () {
    var form = layui.form;
    $ = layui.jquery;

    // Net Data Url
    let saveUrl = '/api/song/save'

    // Load Form Data 加载表单数据
    function loadFormData() {
        //let formData = new FormData()
        //formData.append('name', $(".name").val())
        //formData.append('artistName', $(".artist-name").val())
        //formData.append('albumName', $(".album-name").val())
        //formData.append('description', $(".desc").val())
        //formData.append('duration', $(".duration").val())
        //formData.append('releaseTime', $(".time").val())
        //formData.append('file', document.getElementById("file").files[0])
        //return formData;
        return {
            name: $(".name").val(),
            artistName: $(".artist-name").val(),
            albumName: $(".album-name").val(),
            description: $(".desc").val(),
            duration: $(".duration").val(),
            releaseTime: $(".time").val(),
            //url: $(".url").val(),
        }
    }

    // Save Click 点击保存
    $("#saveform").click(function () {
        console.log("saveform clicked");
        var index = parent.layer.msg('数据提交中，请稍候', { icon: 16, time: false, shade: 0.8 });
        setTimeout(function () {
            saveItem(index);
        }, 1000);
        return true;
    })
    // 保存文件 callback(fileinfo): 成功回调, 加载层
    function saveFile(callback, index) {
        console.log('save file start:')
        let formData = new FormData()
        formData.append('file', document.getElementById("file").files[0])
        $.ajax({
            url: '/File/Index',
            type: "POST",
            data: formData,
            //dataType: "",
            cache: false,
            timeout: 1000,
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
                    //parent.layer.msg("添加成功！");
                    callback(resbody.data, index)
                } else {
                    alert('保存失败');
                }
            },
            error: function (data) {
                alert("请求失败！");
                console.log('Save Err: ' + JSON.stringify(data))
                parent.layer.close(index);
            }
        });
        console.log('save file end:')
    }
    function saveInfo(data, index) {
        console.log('save info start')
        let item = loadFormData()
        item.url = data.url
        $.post(saveUrl, { song: item },
            function (resbody) {
                console.log('Save Response Body:', resbody);
                //关闭加载层
                parent.layer.close(index);
                if (resbody.code == "0") {
                    //提示成功
                    parent.layer.msg("添加成功！");
                    layer.closeAll("iframe");
                    parent.location.reload();
                } else {
                    parent.layer.msg("添加失败！");
                }
            })
        console.log('save info end')
    }

    // Save Item 添加提交
    function saveItem(index) {
        // 先保存文件-再保存歌曲信息
        saveFile(saveInfo, index)
    }
});