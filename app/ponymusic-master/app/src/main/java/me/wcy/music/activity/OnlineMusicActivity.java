package me.wcy.music.activity;

import android.graphics.Bitmap;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.animation.GlideAnimation;
import com.bumptech.glide.request.target.SimpleTarget;
import com.google.gson.Gson;

import net.wagsn.music.model.CommonRequest;
import net.wagsn.music.model.SongListResponse;
import net.wagsn.util.binding.Bind;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import me.wcy.music.R;
import me.wcy.music.adapter.OnMoreClickListener;
import me.wcy.music.adapter.OnlineMusicAdapter;
import me.wcy.music.constants.Extras;
import me.wcy.music.enums.LoadStateEnum;
import me.wcy.music.executor.DownloadOnlineMusic;
import me.wcy.music.executor.PlayOnlineMusic;
import me.wcy.music.executor.ShareOnlineMusic;
import me.wcy.music.http.HttpCallback;
import me.wcy.music.http.HttpClient;
import me.wcy.music.model.Music;
import me.wcy.music.model.OnlineMusic;
import me.wcy.music.model.OnlinePlaylist;
import me.wcy.music.model.Playlist;
import me.wcy.music.service.AudioPlayer;
import me.wcy.music.utils.FileUtils;
import me.wcy.music.utils.ImageUtils;
import me.wcy.music.utils.ScreenUtils;
import me.wcy.music.utils.ToastUtils;
import me.wcy.music.utils.ViewUtils;
import me.wcy.music.widget.AutoLoadListView;

/**
 * 在线音乐列表 - (在线音乐-榜单列表进入)
 */
public class OnlineMusicActivity extends BaseActivity implements OnItemClickListener
        , OnMoreClickListener, AutoLoadListView.OnLoadListener {

    public static final String TAG = "OnlineMusicActivity";

    /**
     * 音乐分页大小
     */
    private static final int MUSIC_LIST_SIZE = 20;

    /**
     * 自动加载列表视图
     */
    @Bind(R.id.lv_online_music_list)
    private AutoLoadListView lvOnlineMusic;
    /**
     * 加载中视图
     */
    @Bind(R.id.ll_loading)
    private LinearLayout llLoading;
    /**
     * 失败加载视图
     */
    @Bind(R.id.ll_load_fail)
    private LinearLayout llLoadFail;
    /**
     * 头部视图
     */
    private View vHeader;
    /**
     * 歌单信息
     */
    private Playlist mPlaylistInfo;
    /**
     * 在线歌单信息
     */
    private OnlinePlaylist mOnlinePlaylistInfo;
    /**
     * 在线音乐列表
     */
    private List<OnlineMusic> mMusicList = new ArrayList<>();
    /**
     * 在线音乐列表适配器
     */
    private OnlineMusicAdapter mAdapter = new OnlineMusicAdapter(mMusicList);
    /**
     * 分页索引，pageIndex
     */
    private int mOffset = 0;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_online_music);
    }

    @Override
    protected void onServiceBound() {
        mPlaylistInfo = (Playlist) getIntent().getSerializableExtra(Extras.MUSIC_LIST_TYPE);
        // 设置页面标题
        setTitle(mPlaylistInfo.getName());

        initView();
        onLoad();
    }

    /**
     * 初始化页面
     */
    private void initView() {
        vHeader = LayoutInflater.from(this).inflate(R.layout.activity_online_music_list_header, null);
        AbsListView.LayoutParams params = new AbsListView.LayoutParams(ViewGroup.LayoutParams.MATCH_PARENT, ScreenUtils.dp2px(150));
        vHeader.setLayoutParams(params);
        lvOnlineMusic.addHeaderView(vHeader, null, false);
        lvOnlineMusic.setAdapter(mAdapter);
        lvOnlineMusic.setOnLoadListener(this);
        ViewUtils.changeViewState(lvOnlineMusic, llLoading, llLoadFail, LoadStateEnum.LOADING);

        lvOnlineMusic.setOnItemClickListener(this);
        mAdapter.setOnMoreClickListener(this);
    }

    /**
     * 在线歌曲列表
     * @param offset
     */
    private void getMusic(final int offset) {
        if(mPlaylistInfo.getSource().toLowerCase().equals("baidu")){
            HttpClient.getSongListInfoFromBaidu(mPlaylistInfo.getId(), MUSIC_LIST_SIZE, offset, new HttpCallback<OnlinePlaylist>() {
                @Override
                public void onSuccess(OnlinePlaylist response) {
                    lvOnlineMusic.onLoadComplete();
                    mOnlinePlaylistInfo = response;
                    if (offset == 0 && response == null) {
                        ViewUtils.changeViewState(lvOnlineMusic, llLoading, llLoadFail, LoadStateEnum.LOAD_FAIL);
                        return;
                    } else if (offset == 0) {
                        initHeader();
                        ViewUtils.changeViewState(lvOnlineMusic, llLoading, llLoadFail, LoadStateEnum.LOAD_SUCCESS);
                    }
                    if (response == null || response.getSong_list() == null || response.getSong_list().size() == 0) {
                        lvOnlineMusic.setEnable(false);
                        return;
                    }
                    Log.d(TAG, "onSuccess: 本次加载的音乐列表："+new Gson().toJson(response));
                    mOffset += MUSIC_LIST_SIZE;
                    mMusicList.addAll(response.getSong_list());
                    mAdapter.notifyDataSetChanged();
                }

                @Override
                public void onFail(Exception e) {
                    lvOnlineMusic.onLoadComplete();
                    if (e instanceof RuntimeException) {
                        // 歌曲全部加载完成
                        lvOnlineMusic.setEnable(false);
                        return;
                    }
                    if (offset == 0) {
                        ViewUtils.changeViewState(lvOnlineMusic, llLoading, llLoadFail, LoadStateEnum.LOAD_FAIL);
                    } else {
                        ToastUtils.show(R.string.load_fail);
                    }
                }
            });
        }
        else {
            CommonRequest request = new CommonRequest();
            // 测试歌曲列表数据加载
            HttpClient.getSongInfoList(request, new HttpCallback<SongListResponse>(){

                @Override
                public void onSuccess(SongListResponse response) {
                    Log.d(TAG, "onSuccess: 歌曲列表响应：" + new Gson().toJson(response));
                    ViewUtils.changeViewState(lvOnlineMusic, llLoading, llLoadFail, LoadStateEnum.LOAD_FAIL);
                }

                @Override
                public void onFail(Exception e) {
                    Log.e(TAG, "onFail: 加载歌曲列表失败：", e);
                }
            });
        }
    }

    /**
     * 当加载完成
     */
    @Override
    public void onLoad() {
        getMusic(mOffset);
    }

    /**
     * 在线音乐列表项点击事件
     * @param parent
     * @param view
     * @param position
     * @param id
     */
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        // 播放在线音乐
        play((OnlineMusic) parent.getAdapter().getItem(position));
    }

    /**
     * 当点击更多时
     * @param position
     */
    @Override
    public void onMoreClick(int position) {
        final OnlineMusic onlineMusic = mMusicList.get(position);
        AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle(mMusicList.get(position).getTitle());
        String path = FileUtils.getMusicDir() + FileUtils.getMp3FileName(onlineMusic.getArtist_name(), onlineMusic.getTitle());
        File file = new File(path);
        int itemsId = file.exists() ? R.array.online_music_dialog_without_download : R.array.online_music_dialog;
        dialog.setItems(itemsId, (dialog1, which) -> {
            switch (which) {
                case 0:// 分享
                    share(onlineMusic);
                    break;
                case 1:// 查看歌手信息
                    artistInfo(onlineMusic);
                    break;
                case 2:// 下载
                    download(onlineMusic);
                    break;
            }
        });
        dialog.show();
    }

    /**
     * 初始化歌单详情界面头部视图
     */
    private void initHeader() {
        final ImageView ivHeaderBg = vHeader.findViewById(R.id.iv_header_bg);
        final ImageView ivCover = vHeader.findViewById(R.id.iv_cover);
        TextView tvTitle = vHeader.findViewById(R.id.tv_title);
        TextView tvUpdateDate = vHeader.findViewById(R.id.tv_update_date);
        TextView tvComment = vHeader.findViewById(R.id.tv_comment);
        tvTitle.setText(mOnlinePlaylistInfo.getBillboard().getName());
        tvUpdateDate.setText(getString(R.string.recent_update, mOnlinePlaylistInfo.getBillboard().getUpdate_date()));
        tvComment.setText(mOnlinePlaylistInfo.getBillboard().getComment());
        Glide.with(this)
                .load(mOnlinePlaylistInfo.getBillboard().getPic_s640())
                .asBitmap()
                .placeholder(R.drawable.default_cover)
                .error(R.drawable.default_cover)
                .override(200, 200)
                .into(new SimpleTarget<Bitmap>() {
                    @Override
                    public void onResourceReady(Bitmap resource, GlideAnimation<? super Bitmap> glideAnimation) {
                        ivCover.setImageBitmap(resource);
                        ivHeaderBg.setImageBitmap(ImageUtils.blur(resource));
                    }
                });
    }

    /**
     * 播放在线音乐
     * @param onlineMusic
     */
    private void play(OnlineMusic onlineMusic) {
        new PlayOnlineMusic(this, onlineMusic) {
            @Override
            public void onPrepare() {
                showProgress();
            }

            @Override
            public void onExecuteSuccess(Music music) {
                cancelProgress();
                AudioPlayer.get().addAndPlay(music);
                ToastUtils.show("已添加到播放列表");
            }

            @Override
            public void onExecuteFail(Exception e) {
                cancelProgress();
                ToastUtils.show(R.string.unable_to_play);
            }
        }.execute();
    }

    /**
     * 分享
     * @param onlineMusic
     */
    private void share(final OnlineMusic onlineMusic) {
        new ShareOnlineMusic(this, onlineMusic.getTitle(), onlineMusic.getSong_id()) {
            @Override
            public void onPrepare() {
                showProgress();
            }

            @Override
            public void onExecuteSuccess(Void aVoid) {
                cancelProgress();
            }

            @Override
            public void onExecuteFail(Exception e) {
                cancelProgress();
            }
        }.execute();
    }

    /**
     * 专辑信息
     * @param onlineMusic
     */
    private void artistInfo(OnlineMusic onlineMusic) {
        ArtistInfoActivity.start(this, onlineMusic.getTing_uid());
    }

    /**
     * 下载在线音乐
     * @param onlineMusic 在线音乐信息
     */
    private void download(final OnlineMusic onlineMusic) {
        new DownloadOnlineMusic(this, onlineMusic) {
            @Override
            public void onPrepare() {
                // 显示进度条
                showProgress();
            }

            @Override
            public void onExecuteSuccess(Void aVoid) {
                // 关闭进度条
                cancelProgress();
                ToastUtils.show(getString(R.string.now_download, onlineMusic.getTitle()));
            }

            @Override
            public void onExecuteFail(Exception e) {
                cancelProgress();
                ToastUtils.show(R.string.unable_to_download);
            }
        }.execute();
    }
}
