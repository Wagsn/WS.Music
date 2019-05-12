package me.wcy.music.executor;

import android.app.Activity;
import android.text.TextUtils;

import java.io.File;

import me.wcy.music.http.HttpCallback;
import me.wcy.music.http.HttpClient;
import me.wcy.music.model.DownloadInfo;
import me.wcy.music.model.OnlineMusic;
import me.wcy.music.utils.FileUtils;

/**
 * 下载在线音乐
 * Created by wcy on 2016/1/3.
 */
public abstract class DownloadOnlineMusic extends DownloadMusic {
    private OnlineMusic mOnlineMusic;

    /**
     * 下载在线音乐
     * @param activity
     * @param onlineMusic
     */
    public DownloadOnlineMusic(Activity activity, OnlineMusic onlineMusic) {
        super(activity);
        mOnlineMusic = onlineMusic;
    }

    /**
     * 下载在线音乐<br/>
     * 添加专辑封面和歌词下载回调，以避免空指针异常<br/>
     * Updated by Wagsn on 2019/5/12.
     */
    @Override
    protected void download() {
        final String artist = mOnlineMusic.getArtist_name();
        final String title = mOnlineMusic.getTitle();

        // 下载歌词
        String lrcFileName = FileUtils.getLrcFileName(artist, title);
        File lrcFile = new File(FileUtils.getLrcDir() + lrcFileName);
        if (!TextUtils.isEmpty(mOnlineMusic.getLrclink()) && !lrcFile.exists()) {
            HttpClient.downloadFile(mOnlineMusic.getLrclink(), FileUtils.getLrcDir(), lrcFileName, new HttpCallback<File>() {
                @Override
                public void onSuccess(File file) {

                }

                @Override
                public void onFail(Exception e) {

                }
            });
        }

        // 下载封面
        String albumFileName = FileUtils.getAlbumFileName(artist, title);
        final File albumFile = new File(FileUtils.getAlbumDir(), albumFileName);
        String picUrl = mOnlineMusic.getPic_big();
        if (TextUtils.isEmpty(picUrl)) {
            picUrl = mOnlineMusic.getPic_small();
        }
        if (!albumFile.exists() && !TextUtils.isEmpty(picUrl)) {
            HttpClient.downloadFile(picUrl, FileUtils.getAlbumDir(), albumFileName, new HttpCallback<File>() {
                @Override
                public void onSuccess(File file) {

                }

                @Override
                public void onFail(Exception e) {

                }
            });
        }

        // 获取歌曲下载信息并下载歌曲
        HttpClient.getMusicDownloadInfo(mOnlineMusic.getSong_id(), new HttpCallback<DownloadInfo>() {
            @Override
            public void onSuccess(DownloadInfo response) {
                if (response == null || response.getBitrate() == null) {
                    onFail(null);
                    return;
                }
                // 下载歌曲
                downloadMusic(response.getBitrate().getFile_link(), artist, title, albumFile.getPath());
                onExecuteSuccess(null);
            }

            @Override
            public void onFail(Exception e) {
                onExecuteFail(e);
            }
        });
    }
}
