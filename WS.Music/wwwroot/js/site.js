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