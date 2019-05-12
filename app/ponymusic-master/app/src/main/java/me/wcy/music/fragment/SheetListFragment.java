package me.wcy.music.fragment;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ListView;

import com.google.gson.Gson;

import net.wagsn.music.model.PlaylistListResponse;
import net.wagsn.util.binding.Bind;

import java.util.List;

import me.wcy.music.R;
import me.wcy.music.activity.OnlineMusicActivity;
import me.wcy.music.adapter.SheetAdapter;
import me.wcy.music.application.AppCache;
import me.wcy.music.constants.Extras;
import me.wcy.music.constants.Keys;
import me.wcy.music.http.HttpCallback;
import me.wcy.music.http.HttpClient;
import me.wcy.music.model.Playlist;

/**
 * 在线音乐（榜单列表）<br/>
 *
 * Created by wcy on 2015/11/26.
 */
public class SheetListFragment extends BaseFragment implements AdapterView.OnItemClickListener {

    private static final String TAG ="SheetListFragment";

    @Bind(R.id.lv_sheet)
    private ListView lvPlaylist;

    /**
     * 歌单列表
     */
    private List<Playlist> mPlaylists;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_sheet_list, container, false);
    }

    /**
     * 当Activity被创建
     * @param savedInstanceState
     */
    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);

        // 首先访问缓存
        mPlaylists = AppCache.get().getSheetList();
        // 首次数据加载 - 改成从网络加载数据
        if (mPlaylists.isEmpty()) {
            loadPlaylist();
        }
    }

    /**
     * 加载歌单列表<br/>
     * Created by Wagsn on 2019/5/12.
     */
    public void loadPlaylist(){
        HttpClient.getPlaylistInfoList(0, 100, "", new HttpCallback<PlaylistListResponse>() {
            @Override
            public void onSuccess(PlaylistListResponse response) {
                List<Playlist> playlists = response.getData();
                Log.d(TAG, "onSuccess: 获取歌单列表成功："+ new Gson().toJson(response));
                Log.d(TAG, "onActivityCreated: playlist list: "+new Gson().toJson(mPlaylists));
                mPlaylists.addAll(playlists);
                SheetAdapter adapter = new SheetAdapter(mPlaylists);
                lvPlaylist.setAdapter(adapter);
            }
            @Override
            public void onFail(Exception e) {
                Log.e(TAG, "onFail: 获取歌单列表失败：", e);
            }
        });
        // 从资源文件中加载 - 被遗弃的
//        String[] titles = getResources().getStringArray(R.array.online_music_list_title);
//        String[] types = getResources().getStringArray(R.array.online_music_list_type);
//        for (int i = 0; i < titles.length; i++) {
//            Playlist info = new Playlist();
//            info.setName(titles[i]);
//            info.setId(types[i]);
//            mPlaylists.add(info);
//        }
        //Log.d(TAG, "onActivityCreated: playlist list: "+new Gson().toJson(mPlaylists));
    }

    /**
     * 设置监听器
     */
    @Override
    protected void setListener() {
        lvPlaylist.setOnItemClickListener(this);
    }

    /**
     * 列表项点击回调
     * @param parent
     * @param view
     * @param position
     * @param id
     */
    @Override
    public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
        Playlist playlist = mPlaylists.get(position);
        Intent intent = new Intent(getContext(), OnlineMusicActivity.class);
        intent.putExtra(Extras.MUSIC_LIST_TYPE, playlist);
        startActivity(intent);
    }

    /**
     *
     * @param outState
     */
    @Override
    public void onSaveInstanceState(Bundle outState) {
        int position = lvPlaylist.getFirstVisiblePosition();
        int offset = (lvPlaylist.getChildAt(0) == null) ? 0 : lvPlaylist.getChildAt(0).getTop();
        outState.putInt(Keys.PLAYLIST_POSITION, position);
        outState.putInt(Keys.PLAYLIST_OFFSET, offset);
    }

    /**
     *
     * @param savedInstanceState
     */
    public void onRestoreInstanceState(final Bundle savedInstanceState) {
        lvPlaylist.post(() -> {
            int position = savedInstanceState.getInt(Keys.PLAYLIST_POSITION);
            int offset = savedInstanceState.getInt(Keys.PLAYLIST_OFFSET);
            lvPlaylist.setSelectionFromTop(position, offset);
        });
    }
}
