package net.wagsn.music.model;

import java.io.Serializable;
import java.sql.Time;

/**
 * 歌曲信息<br/>
 * Created by Wagsn on 2019/5/12.
 */
public class Song implements Serializable {

    /**
     * 歌曲GUID
     */
    private String id;

    /**
     * 歌曲名称
     */
    private String name;

    /**
     * 歌曲描述
     */
    private String description;

    /**
     * 持续时间
     */
    private long duration;

    /**
     * 发布时间
     */
    private Time releaseTime;

    /**
     * 歌曲文件的在线路径
     */
    private String url;

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public long getDuration() {
        return duration;
    }

    public void setDuration(long duration) {
        this.duration = duration;
    }

    public Time getReleaseTime() {
        return releaseTime;
    }

    public void setReleaseTime(Time releaseTime) {
        this.releaseTime = releaseTime;
    }

    public String getUrl() {
        return url;
    }

    public void setUrl(String url) {
        this.url = url;
    }

}
