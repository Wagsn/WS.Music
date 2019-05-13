package me.wcy.music.model;

import java.io.Serializable;

/**
 * 歌单概要信息（榜单信息）
 * Created by wcy on 2015/12/20.
 * Rename by Wagsn on 2019/5/12.
 */
public class Playlist implements Serializable {
    /**
     * 歌单ID
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
    // 核心歌曲1
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
