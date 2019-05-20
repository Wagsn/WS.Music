package net.wagsn.music.model;

/**
 * 封装歌单View需要的数据
 */
public class PlaylistCard {
    /**
     * 歌单ID
     */
    private String id;
    /**
     * 榜单名
     */
    private String name;

    /**
     * 来源：Baidu，Wagsn
     */
    private String source;

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

    public String getSource() {
        return source;
    }

    public void setSource(String source) {
        this.source = source;
    }
}
