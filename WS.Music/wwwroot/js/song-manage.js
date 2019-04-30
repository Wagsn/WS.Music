// 音乐管理界面

layui.use(['form', 'layer', 'laypage', 'layedit', 'laydate', 'element'], function () {
    var form = layui.form,
        element = layui.element,
        layer = parent.layer === undefined ? layui.layer : parent.layer,
        laypage = layui.laypage,
        layedit = layui.layedit,
        laydate = layui.laydate

    // 查询到的数据全局
    var studentData = ' ';

    let songListUrl = "/api/song/list"

    

    //jq初始化页面
    $(document).ready(function () {
        // 初始化列表
        initSongList()
        // 搜索按钮绑定点击事件
        $(".search-btn").click(function () {
            let searchFromData = getSearchFormData()
            console.log('search form data: ', searchFromData)
            if (searchFromData.keyWord != '') {
                var index = layer.msg('查询中，请稍候', { icon: 16, time: false, shade: 0.8 })
                setTimeout(function () {
                    loadSongPageData(searchFromData)
                    layer.close(index)
                }, 1000);  // 1000 ms 超时
            } else {
                layer.msg("请输入需要查询的内容")
            }
        })
    })

    // 初始化列表
    function initSongList() {
        loadSongPageData({
            pageIndex: 0,
            pageSize: 5
        })
    }

    // 加载音乐列表数据
    function loadSongPageData(query = { pageIndex: 0, pageSize: 5, keyWord: "" }) {
        // 数据来源 服务器
        // 首先要获取分页索引信息
        //$.post(songListUrl, query, function (data) {
        //    if (data == "ok") {
        //        alert("添加成功！");
        //    }
        //})
        $.post(
            songListUrl,
            query,
            function (resbody) {
                console.log("Loading song list response body: ", resbody);
                // 设置第一次页面的数据
                //setRollListData(body.data);
                console.log("song list page count: ", Math.ceil(resbody.totalCount / resbody.pageSize))
                // 其次获取实际数据
                laypage.render({
                    cont: 'page',
                    //总页数后台返回总的数据页数
                    pages: resbody.pageSize == 0 ? 1 : Math.ceil(resbody.totalCount / resbody.pageSize),  // 这里计算是不是出现了问题
                    //当前页数
                    curr: resbody.pageIndex,
                    //连续显示分页数
                    groups: 0,
                    // 这里是执行跳转的接口
                    jump: function (obj, first) {
                        //得到了当前页，用于向服务端请求对应数据
                        layer.msg(obj.curr + ' pages');
                        query.pageIndex = obj.curr - 1;
                        console.log("Jump to page " + obj.curr + ", the query is ", query)
                        loadSongListData(query, renderSongList)
                    }
                });
            }
        )
    }

    // 异步网络加载歌曲列表，callback: 页面渲染回调函数
    function loadSongListData(query, callback) {
        $.ajax({
            type: "post",
            async: true,
            url: songListUrl,
            dataType: 'json',
            contentType: "application/json",
            data: JSON.stringify(query),
            success: function (body) {
                console.log("Load list data for song on page " + body.pageIndex + ": ", body);
                rollData = body.data
                callback(body.data);
            }
        });
    }

    // 渲染歌曲列表数据
    function renderSongList(data) {
        console.log("Rendering Song List Data: ", data)
        let dataHtml = ""
        $.each(data, function (v, o) {
            dataHtml += '<tr>' +
                '<td><input name="checked" lay-skin="primary" lay-filter="choose" type="checkbox"><div class="layui-unselect layui-form-checkbox" lay-skin="primary"><i class="layui-icon"></i></div></td>' +
                '<td align="center">' + o.name + '</td>' +
                '<td>' + o.description + '</td>' +
                '<td>' + o.duration + '</td>' +
                '<td>' + o.classNo + '</td>' +
                '<td>' + o.releaseTime + '</td>' +
                '<td>' + o.url + '</td>' +
                '<td>' +
                '<a class="layui-btn layui-btn-normal layui-btn-mini song-details-btn" data-id="' + o.id + '"><i class="layui-icon">&#xe600;</i>详情</a>' +
                '<a class="layui-btn layui-btn-normal layui-btn-mini song-edit-btn" data-id="' + o.id + '"><i class="layui-icon">&#xe600;</i>编辑</a>' +
                '<a class="layui-btn layui-btn-danger layui-btn-mini song-del-btn" data-id="' + o.id + '"><i class="layui-icon">&#xe640;</i>删除</a>' +
                '</td>' +
                '</tr>';
        })
        $('#roll_list').html(dataHtml);
    }

    // 通过关键词获取学生搜索数据 会导致页数的变化，所以用setPageStudent
    function loadSongSearchData(query = { pageIndex: 0, pageSize: 5, keyWord: "" }) {
        $.ajax({
            type: "get",
            async: true,
            url: "api/student/list?pageIndex=" + (query.pageIndex || "0") + "&pageSize=" + (query.pageSize || "10") + "&keyWord=" + (query.keyWord),
            dataType: 'json',
            success: function (body) {
                console.log("响应数据：", body);
                console.log("通过关键字在服务器查询到的学生数据：", body.data)
                studentData = body.data
                setStudentListData(body.data);
            }
        });
    }

    // 获取搜索表单数据
    function getSearchFormData() {
        return {
            pageIndex: 0,
            pageSize: 5,
            keyWord: $('.search-input').val().trim() || ''
        }
    }

    // 设置搜索表单数据
    function setSearchFormData(data) {
        $('.search-input').val(data.keyWord)
    }

    function setStudentListData(data) {
        //console.log("学生数据：", studentData)
        let dataHtml = ""
        $.each(data, function (v, o) {
            dataHtml += '<tr>' +
                '<td><input name="checked" lay-skin="primary" lay-filter="choose" type="checkbox"><div class="layui-unselect layui-form-checkbox" lay-skin="primary"><i class="layui-icon"></i></div></td>' +
                '<td align="center">' + o.studentNo + '</td>' +
                '<td>' + o.name + '</td>' +
                '<td>' + o.password + '</td>' +
                '<td>' + o.majorName + '</td>' +
                '<td>' + o.classNo + '</td>' +
                '<td>' + o.email + '</td>' +
                '<td>' + o.mobileNumber + '</td>' +
                '<td>' +
                '<a class="layui-btn layui-btn-mini student_edit" data-id="' + o.id + '"><i class="iconfont icon-edit"></i> 编辑</a>' +
                '<a class="layui-btn layui-btn-normal layui-btn-mini shop_details" data-id="' + o.id + '"><i class="layui-icon">&#xe600;</i>详情</a>' +
                '<a class="layui-btn layui-btn-danger layui-btn-mini student_del" data-id="' + o.id + '"><i class="layui-icon">&#xe640;</i> 删除</a>' +
                '</td>' +
                '</tr>';
        })
        $('#student_list').html(dataHtml);

    }
    /*点击删除按钮删除该行数据*/
    $("body").on("click", ".student_del", function () { //删除
        console.log("teacher deleting")
        var _this = $(this);
        layer.confirm('确定删除此信息？', { icon: 3, title: '提示信息' }, function (index) {
            for (var i = 0; i < studentData.length; i++) {
                if (studentData[i].id == _this.attr("data-id")) {
                    var del_id = studentData[i].id;
                    delThisStudent(del_id);
                    studentData.splice(i, 1);
                    setStudentListData(studentData); //渲染数据
                }
                else {
                    layer.msg("删除失败！");
                }
            }
            layer.close(index);
        });
    })

    ///*提交删除改行数据的ajax请求*/
    function delThisStudent(id) {
        console.log('删除学生：' + id);
        //	alert(datajson);
        $.ajax({
            type: 'DELETE',
            url: "students/delete/" + id,
            dataType: 'json',
            success: function (data) {
                if (data.code == "200") {
                    //	alert(data);
                    console.log(data);
                    //alert('删除成功！');
                    layer.msg("删除成功");

                    console.log('删除学生：' + id + "，成功");
                } else {
                    alert('删除失败!');
                }
            },
            error: function (e) {
                alert("发生错误：" + e.status);
            }
        });
    }

    /*获取添加页面的id提交查询请求*/
    function getstudentidAndShow() {
        var thisURL = document.URL;
        var getval = thisURL.split('?')[1];
        var showval = getval.split("=")[1];
        function showvalf() {
            alert(showval);
            console.log(showval);
            selectValue = showval;
            getstudentManagePageInfo();
        }
    }


    //添加学生
    $(window).one("resize", function () {
        $(".studentAdd_btn").click(function () {
            console.log('studentAdd_btn clicked')
            //弹窗新增教师
            var index = layui.layer.open({
                title: "增加学生",
                type: 2,
                maxmin: true,
                shadeClose: true, //点击遮罩关闭层
                area: ['680px', '500px'],
                content: "studentAdd.html",
                success: function (layero, index) {
                    setTimeout(function () {
                        layui.layer.tips('点击此处返回学生列表', '.layui-layer-setwin .layui-layer-close', {
                            tips: 3
                        });
                    }, 500)
                }
            })
            //layui.layer.full(index);
        })
    }).resize();


    // 批量删除
    $(".batchDel").click(function () {

        var $checkbox = $('.student_list tbody input[type="checkbox"][name="checked"]');
        var $checked = $('.student_list tbody input[type="checkbox"][name="checked"]:checked');

        if ($('input[type="checkbox"]').is(':checked')) {
            layer.confirm('确定删除选中的信息？', { icon: 3, title: '提示信息' }, function (index) {
                var index = layer.msg('删除中，请稍候', { icon: 16, time: false, shade: 0.8 });
                setTimeout(function () {

                    //删除数据
                    for (var j = 0; j < $checked.length; j++) { ///选中的个数$checked.length
                        for (var i = 0; i < studentData.length; i++) { //返回数据的个数
                            //eq jq变量eq() 方法返回带有被选元素的指定索引号的元素 选择的第j个元素
                            if (studentData[i].id == $checked.eq(j).parents("tr").find(".student_del").attr("data-id")) {
                                //splice() 方法用于插入、删除或替换数组的元素。array.splice(index定位下标,howmany删除个数,item1添加的元素,.....,itemX)
                                delThisStudent(studentData[i].id);//循环调用删除
                                studentData.splice(i, 1);
                                setStudentListData(studentData); //渲染数据
                            }
                        }
                    }

                    //prop() 方法设置或返回被选元素的属性和值。
                    $('.shop_list thead input[type="checkbox"]').prop("checked", false);
                    form.render(); //更新渲染表单，form.render(type, filter);第一个参数：type，为表单的 type 类型
                    //第一个参数：type，为表单的 type 类型
                    layer.close(index); //关闭删除中
                    //layer.msg("删除成功");
                }, 2000);
            })
        } else {
            layer.msg("请选择需要删除的学生");
        }
    })

    //全选
    form.on('checkbox(allChoose)', function (data) {
        //data.elem); //得到select原始DOM对象  (data.value); //得到被选中的值  (data.othis); //得到美化后的DOM对象
        //(data.elem.checked); //是否被选中，true或者false
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"])');
        //each遍历 each() 方法为每个匹配元素规定要运行的函数。$(selector).each(function(index,element)) 
        //element - 当前的元素（也可使用 "this" 选择器）
        child.each(function (index, item) {
            item.checked = data.elem.checked;
        });
        form.render('checkbox'); ////渲染表单
    });
    //判断是否选中
    form.on("checkbox(choose)", function (data) {
        //parents() 方法返回被选元素的所有祖先元素。
        //$(selector).parents(filter) parent() - 返回被选元素的直接父元素
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"])');
        var childChecked = $(data.elem).parents('table').find('tbody input[type="checkbox"]:not([name="show"]):checked')
        if (childChecked.length == child.length) {
            $(data.elem).parents('table').find('thead input#allChoose').get(0).checked = true;
        } else {
            $(data.elem).parents('table').find('thead input#allChoose').get(0).checked = false;
        }
        form.render('checkbox');
    })


    //编辑学生信息
    $("body").on("click", ".student_edit", function () {
        var _this = $(this);
        layer.confirm('确定编辑此信息？', { icon: 3, title: '提示信息' }, function (index) {
            for (var i = 0; i < studentData.length; i++) {
                if (studentData[i].id == _this.attr("data-id")) {
                    //alert(bigcateData[i].cateId);
                    console.log("jump to student edit view: ", studentData[i]);  //获取到该行对象
                    sessionStorage.obj1 = JSON.stringify(studentData[i]);

                    layer.msg("加载成功");

                    var index = layui.layer.open({
                        title: "编辑学生信息",
                        type: 2,
                        maxmin: true,
                        shadeClose: true, //点击遮罩关闭层
                        area: ['800px', '500px'],
                        content: "studentChange.html",
                        success: function (layero, index) {
                            setTimeout(function () {
                                layui.layer.tips('点击此处返回学生列表', '.layui-layer-setwin .layui-layer-close', {
                                    tips: 3

                                });
                            }, 500)
                        }
                    })
                }

            }

            layer.close(index);
        });
    })

    //详情
    $("body").on("click", ".shop_details", function () {
        var _this = $(this);
        layer.confirm('查看该学生详情？', { icon: 3, title: '提示信息' }, function (index) {
            //	_this.parents("tr").remove();//移除显示
            for (var i = 0; i < shopData.length; i++) {
                if (shopData[i].id == _this.attr("data-id")) {
                    console.log(shopData[i]);//获取到该行对象
                    sessionStorage.obj2 = JSON.stringify(shopData[i]);

                    layer.msg("加载成功");

                    var index = layui.layer.open({
                        title: "学生详情",
                        type: 2,
                        maxmin: true,
                        shadeClose: false, //点击遮罩关闭层
                        area: ['600px', '420px'],
                        content: "shopDetails.html",
                        success: function (layero, index) {
                            setTimeout(function () {
                                layui.layer.tips('点击此处返回学生列表', '.layui-layer-setwin .layui-layer-close', {
                                    tips: 3

                                });
                            }, 500)
                        }
                    })

                }
            }

            layer.close(index);
        });
    })

});