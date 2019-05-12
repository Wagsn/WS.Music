package me.wcy.music.executor;

/**
 * 音乐下载信息<br/>
 * Created by hzwangchenyan on 2017/8/11.
 */
public class DownloadMusicInfo {
    /**
     * 歌名，不包含其它
     */
    private String title;
    /**
     * 歌曲路径
     */
    private String musicPath;

    /**
     * 封面路径
     */
    private String coverPath;

    public DownloadMusicInfo(String title, String musicPath, String coverPath) {
        this.title = title;
        this.musicPath = musicPath;
        this.coverPath = coverPath;
    }

    public String getTitle() {
        return title;
    }

    public String getMusicPath() {
        return musicPath;
    }

    public String getCoverPath() {
        return coverPath;
    }
}
