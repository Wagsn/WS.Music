// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Get|Set from Html, Save|Load from Storage|Server

// 获取搜索表单数据
function getSearchData() {
    return {
        pageIndex: $('[name=pageIndex]').val().trim(),
        pageSize: $('[name=pageSize]').val().trim(),
        keyWord: $('[name=keyWord]').val().trim(),
    }
}
// 设置搜索表单数据  组件化与插件化
function setSearchData(data) {
    pageIndex: $('[name=pageIndex]').val(data.pageIndex)
    pageSize: $('[name=pageSize]').val(data.pageSize)
    keyWord: $('[name=keyWord]').val(data.keyWord)
}
// 设置歌曲列表数据
function setSongList(data) {
    //console.log("song list set data:", data)
    let songList = $('.song-list-body')
    let bodyHtml = ""
    data.forEach(v => {
        //console.log('song list item data: ', v)
        let rowHtml = ""
        rowHtml += '<tr>'
        rowHtml += '<td>' + v.name + '</td>'
        rowHtml += '<td>' + v.description + '</td>'
        rowHtml += '<td>' + v.duration + '</td>'
        rowHtml += '<td>' + v.releaseTime + '</td>'
        rowHtml += '<td>' + v.url + '</td>'
        rowHtml += '<td>' + '<a class="song-details-jump" >详情</a>' + '<a class="song-edit-jump" >编辑</a>' + '<a class="song-delete-jump" >删除</a>' + '</td>'
        rowHtml += '</tr>'
        bodyHtml += rowHtml;
    })
    songList.html(bodyHtml);
}
// 加载歌曲列表数据 query:{pageIndex: 0, pageSize: 10, keyWord: ""} loadSongList({}, setSongList)
function loadSongList(query = {}) {
    //console.log("song list load query:", query)
    var request = {
        pageIndex: query.pageIndex||0,
        pageSize: query.pageSize||10,
        keyWord: query.keyWord||""
    }
    //console.log("song list load request:", request)
    $.ajax({
        url: "../api/song/list/search",
        method: "post",
        contentType: "application/json",
        data: JSON.stringify(request),
        success: function (body) {
            //console.log("song list body:", body)
            if (body.code == "0") {
                setSongList(body.data);
            }
            else {
                alert('歌曲数据加载失败！')
            }
        },
        error: function (data) {
            alert('请求失败！' + JSON.stringify(data))
        }
    })
}

// 歌曲搜索请求
function songSearch() {
    //console.log('song search request:', getSearchData())
    loadSongList(getSearchData())
}

// 分页查询请求
class PageQuery {
    constructor() {
        this.pageIndex = 0
        this.pageSize = 10
        this.keyWord = ""
    }
}

'use strict';
layui.use(['jquery', 'layer', 'element'], function () {
    var element = layui.element;
    window.jQuery = window.$ = layui.jquery;
    window.layer = layui.layer;

    //登录加载用户名
    //$(function(){
    //	var str = sessionStorage.obj;
    //	if(str==null){
    //         window.location.href="index.html";
    //    }else{
    //         var obj = $.parseJSON(str);
    //         //alert(obj);
    //          $("#user").text(obj.username);
    //         }
    //})

    // larry-side-menu向左折叠
    $('.larry-side-menu').click(function () {
        var sideWidth = $('#larry-side').width();
        if (sideWidth === 200) {
            $('#larry-body').animate({
                left: '0'
            }); //admin-footer
            $('#larry-footer').animate({
                left: '0'
            });
            $('#larry-side').animate({
                width: '0'
            });
        } else {
            $('#larry-body').animate({
                left: '200px'
            });
            $('#larry-footer').animate({
                left: '200px'
            });
            $('#larry-side').animate({
                width: '200px'
            });
        }
    });
    $(function () {
        // 刷新iframe操作
        $("#refresh_iframe").click(function () {
            $(".layui-tab-content .layui-tab-item").each(function () {
                if ($(this).hasClass('layui-show')) {
                    $(this).children('iframe')[0].contentWindow.location.reload(true);
                }
            });
        });

        $('#lock').mouseover(function () {
            layer.tips('请按Alt+L快速锁屏！', '#lock', {
                tips: [1, '#FF5722'],
                time: 4000
            });
        })
        // 快捷键锁屏设置
        $(document).keydown(function (e) {
            if (e.altKey && e.which == 76) {
                lockSystem();
            }
        });
        function startTimer() {
            var today = new Date();
            var h = today.getHours();
            var m = today.getMinutes();
            var s = today.getSeconds();
            m = m < 10 ? '0' + m : m;
            s = s < 10 ? '0' + s : s;
            $('#time').html(h + ":" + m + ":" + s);
            t = setTimeout(function () { startTimer() }, 500);
        }
        // 锁屏状态检测
        function checkLockStatus(locked) {
            // 锁屏
            if (locked == 1) {
                $('.lock-screen').show();
                $('#locker').show();
                $('#layui_layout').hide();
                $('#lock_password').val('');
            } else {
                $('.lock-screen').hide();
                $('#locker').hide();
                $('#layui_layout').show();
            }
        }

        checkLockStatus('0');
        // 锁定屏幕
        function lockSystem() {

            var url = '';
            $.post(
                url,
                function (data) {
                    if (data == '1') {
                        checkLockStatus(1);
                    } else {
                        layer.alert('锁屏失败，请稍后再试！');
                    }
                });
            startTimer();
        }
        //解锁屏幕
        function unlockSystem() {
            // 与后台交互代码已移除，根据需求定义或删除此功能

            checkLockStatus(0);
        }
        // 点击锁屏
        $('#lock').click(function () {
            lockSystem();
        });
        // 解锁进入系统
        $('#unlock').click(function () {
            unlockSystem();
        });
        // 监控lock_password 键盘事件
        $('#lock_password').keypress(function (e) {
            var key = e.which;
            if (key == 13) {
                unlockSystem();
            }
        });
    });
});