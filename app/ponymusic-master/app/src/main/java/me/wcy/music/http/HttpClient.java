package me.wcy.music.http;

import android.content.Context;
import android.graphics.Bitmap;
import android.support.annotation.NonNull;
import android.util.Log;

import com.google.gson.Gson;
import com.zhy.http.okhttp.OkHttpUtils;
import com.zhy.http.okhttp.callback.BitmapCallback;
import com.zhy.http.okhttp.callback.FileCallBack;

import net.wagsn.http.JsonCallback;
import net.wagsn.model.ResponseMessage;
import net.wagsn.music.model.CommonRequest;
import net.wagsn.music.model.PlaylistListResponse;

import java.io.File;
import java.util.concurrent.TimeUnit;

import me.wcy.music.model.ArtistInfo;
import me.wcy.music.model.DownloadInfo;
import me.wcy.music.model.Lrc;
import me.wcy.music.model.Music;
import me.wcy.music.model.OnlinePlaylist;
import me.wcy.music.model.SearchMusic;

import net.wagsn.music.model.SongListResponse;

import me.wcy.music.model.Splash;
import me.wcy.music.storage.preference.Preferences;
import okhttp3.Call;
import okhttp3.OkHttpClient;

/**
 * HTTP 客户端<br/>
 * Created by hzwangchenyan on 2017/2/8.
 */
public class HttpClient {

    private static final String TAG = "HttpClient";

    private static final String SPLASH_URL = "http://cn.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1";
    private static final String BASE_URL = "http://tingapi.ting.baidu.com/v1/restserver/ting";
    private static final String METHOD_GET_MUSIC_LIST = "baidu.ting.billboard.billList";
    private static final String METHOD_DOWNLOAD_MUSIC = "baidu.ting.song.play";
    private static final String METHOD_ARTIST_INFO = "baidu.ting.artist.getInfo";
    private static final String METHOD_SEARCH_MUSIC = "baidu.ting.search.catalogSug";
    private static final String METHOD_LRC = "baidu.ting.song.lry";
    private static final String PARAM_METHOD = "method";
    private static final String PARAM_TYPE = "type";
    private static final String PARAM_SIZE = "size";
    private static final String PARAM_OFFSET = "offset";
    private static final String PARAM_SONG_ID = "songid";
    private static final String PARAM_TING_UID = "tinguid";
    private static final String PARAM_QUERY = "query";

//    private static final String API_URL = Preferences.getBaseUrl() +"/api";  //"http://192.168.100.254:5001/api";
//    private static final String SONG_URL = API_URL +"/song/list";
//    private static final String PLAYLIST_LIST_URL = API_URL +"/playlist/list";
    private static final String PAGE_SIZE = "pageSize";
    private static final String PAGE_INDEX = "pageIndex";
    private static final String KEY_WORD = "keyWord";

    static {
        OkHttpClient okHttpClient = new OkHttpClient.Builder()
                .connectTimeout(10, TimeUnit.SECONDS)
                .readTimeout(10, TimeUnit.SECONDS)
                .writeTimeout(10, TimeUnit.SECONDS)
                .addInterceptor(new HttpInterceptor())
                .build();
        OkHttpUtils.initClient(okHttpClient);
    }

    public static String getApiUrl() {
        return Preferences.getBaseUrl() +"/api";
    }

    public static String getSongListUrl(){
        return getApiUrl() +"/song/list";
    }

    public static String getPlaylistUrl(){
        return getApiUrl() +"/playlist/list";
    }

    /**
     * 上传本地音乐
     */
    public static void uploadLocalSong(Music music){

    }

    /**
     * 检查服务器连接<br/>
     * Created by Wagsn on 2019/5/12.
     */
    public static void checkServer(){
        OkHttpUtils.get().url(getApiUrl() +"/check").build()
                .execute(new JsonCallback<ResponseMessage>() {
                    @Override
                    public void onResponse(ResponseMessage response, int id) {
                        Log.d(TAG, "检查服务器是否在线: onResponse: "+new Gson().toJson(response));
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                    }

                    @Override
                    public void onAfter(int id) {
                    }
                });
    }


    /**
     * 获取音乐列表<br/>
     * Created by Wagsn on 2019/5/12.
      * @param callback
     */
    public static void getSongInfoList(CommonRequest request, @NonNull final HttpCallback<SongListResponse> callback){
        OkHttpUtils.post().url(getSongListUrl())
                .addParams(PAGE_INDEX, String.valueOf(request.getPageIndex()))
                .addParams(PAGE_SIZE, String.valueOf(request.getPageSize()))
                .addParams(KEY_WORD, request.getKeyword())
                .build()
                .execute(new JsonCallback<SongListResponse>() {
                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }
                    @Override
                    public void onResponse(SongListResponse response, int id) {
                        callback.onSuccess(response);
                    }
                });
    }

    /**
     * 获取启动页<br/>
     * 未使用
     * @param callback
     */
    public static void getSplash(@NonNull final HttpCallback<Splash> callback) {
        OkHttpUtils.get().url(SPLASH_URL).build()
                .execute(new JsonCallback<Splash>(Splash.class) {
                    @Override
                    public void onResponse(Splash response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 下载文件<br/>
     * Updated by Wagsn on 2019/5/12.
     * @param url
     * @param destFileDir
     * @param destFileName
     * @param callback
     */
    public static void downloadFile(String url, String destFileDir, String destFileName, @NonNull final HttpCallback<File> callback) {
        OkHttpUtils.get().url(url).build()
                .execute(new FileCallBack(destFileDir, destFileName) {
                    @Override
                    public void inProgress(float progress, long total, int id) {
                    }

                    @Override
                    public void onResponse(File file, int id) {
                        if(file != null) callback.onSuccess(file);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 获取歌单概要信息列表<br/>
     * Created by Wagsn on 2019/5/8.
     * @param pageIndex page index based zero.
     * @param pageSize page size.
     * @param callback http call back
     * @param keyword keyword of song name for search song.
     */
    public static void getPlaylistInfoList(int pageIndex, int pageSize, String keyword, @NonNull final HttpCallback<PlaylistListResponse> callback){
        OkHttpUtils.post().url(getPlaylistUrl())
                .addParams(PAGE_INDEX, String.valueOf(pageIndex))
                .addParams(PAGE_SIZE, String.valueOf(pageSize))
                .addParams(KEY_WORD, keyword)
                .build()
                .execute(new JsonCallback<PlaylistListResponse>() {
                    @Override
                    public void onResponse(PlaylistListResponse response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 通过类型获取歌曲信息列表
     * @param type
     * @param size
     * @param offset
     * @param callback
     */
    public static void getSongListInfoFromBaidu(String type, int size, int offset, @NonNull final HttpCallback<OnlinePlaylist> callback) {
        OkHttpUtils.get().url(BASE_URL)
                .addParams(PARAM_METHOD, METHOD_GET_MUSIC_LIST)
                .addParams(PARAM_TYPE, type)
                .addParams(PARAM_SIZE, String.valueOf(size))
                .addParams(PARAM_OFFSET, String.valueOf(offset))
                .build()
                .execute(new JsonCallback<OnlinePlaylist>(OnlinePlaylist.class) {
                    @Override
                    public void onResponse(OnlinePlaylist response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 通过歌曲ID获取歌曲下载信息
     * @param songId
     * @param callback
     */
    public static void getMusicDownloadInfo(String songId, @NonNull final HttpCallback<DownloadInfo> callback) {
        OkHttpUtils.get().url(BASE_URL)
                .addParams(PARAM_METHOD, METHOD_DOWNLOAD_MUSIC)
                .addParams(PARAM_SONG_ID, songId)
                .build()
                .execute(new JsonCallback<DownloadInfo>() {
                    @Override
                    public void onResponse(DownloadInfo response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 下载位图
     * @param url
     * @param callback
     */
    public static void getBitmap(String url, @NonNull final HttpCallback<Bitmap> callback) {
        OkHttpUtils.get().url(url).build()
                .execute(new BitmapCallback() {
                    @Override
                    public void onResponse(Bitmap bitmap, int id) {
                        callback.onSuccess(bitmap);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 下载歌词
     * @param songId
     * @param callback
     */
    public static void getLrc(String songId, @NonNull final HttpCallback<Lrc> callback) {
        OkHttpUtils.get().url(BASE_URL)
                .addParams(PARAM_METHOD, METHOD_LRC)
                .addParams(PARAM_SONG_ID, songId)
                .build()
                .execute(new JsonCallback<Lrc>(Lrc.class) {
                    @Override
                    public void onResponse(Lrc response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 搜索音乐
     * @param keyword
     * @param callback
     */
    public static void searchMusic(String keyword, @NonNull final HttpCallback<SearchMusic> callback) {
        OkHttpUtils.get().url(BASE_URL)
                .addParams(PARAM_METHOD, METHOD_SEARCH_MUSIC)
                .addParams(PARAM_QUERY, keyword)
                .build()
                .execute(new JsonCallback<SearchMusic>() {
                    @Override
                    public void onResponse(SearchMusic response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }

    /**
     * 获取艺人信息
     * @param tingUid
     * @param callback
     */
    public static void getArtistInfo(String tingUid, @NonNull final HttpCallback<ArtistInfo> callback) {
        OkHttpUtils.get().url(BASE_URL)
                .addParams(PARAM_METHOD, METHOD_ARTIST_INFO)
                .addParams(PARAM_TING_UID, tingUid)
                .build()
                .execute(new JsonCallback<ArtistInfo>(ArtistInfo.class) {
                    @Override
                    public void onResponse(ArtistInfo response, int id) {
                        callback.onSuccess(response);
                    }

                    @Override
                    public void onError(Call call, Exception e, int id) {
                        callback.onFail(e);
                    }

                    @Override
                    public void onAfter(int id) {
                        callback.onFinish();
                    }
                });
    }
}
