package me.wcy.music.model;

import java.io.Serializable;

/**
 * 歌单概要信息（榜单信息）
 * Created by wcy on 2015/12/20.
 * Rename by Wagsn on 2019/5/12.
 */
public class Playlist implements Serializable {
    /**
     * #主打榜单
     * 1.新歌榜
     * 2.热歌榜
     * #分类榜单
     * 20.华语金曲榜
     * 21.欧美金曲榜
     * 24.影视金曲榜
     * 23.情歌对唱榜
     * 25.网络歌曲榜
     * 22.经典老歌榜
     * 11.摇滚榜
     * #媒体榜单
     * 6.KTV热歌榜
     * 8.Billboard
     * 18.Hito中文榜
     * 7.叱咤歌曲榜
     */
    private String id;
    /**
     * 榜单名
     */
    private String name;
    /**
     * 封面URL
     */
    private String coverUrl;
    private String music1;
    private String music2;
    private String music3;

    public String getName() {
        return name;
    }

    public void setName(String title) {
        this.name = title;
    }

    public String getId() {
        return id;
    }

    public void setId(String type) {
        this.id = type;
    }

    public String getCoverUrl() {
        return coverUrl;
    }

    public void setCoverUrl(String coverUrl) {
        this.coverUrl = coverUrl;
    }

    public String getMusic1() {
        return music1;
    }

    public void setMusic1(String music1) {
        this.music1 = music1;
    }

    public String getMusic2() {
        return music2;
    }

    public void setMusic2(String music2) {
        this.music2 = music2;
    }

    public String getMusic3() {
        return music3;
    }

    public void setMusic3(String music3) {
        this.music3 = music3;
    }
}
