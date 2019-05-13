package me.wcy.music.adapter;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;

import net.wagsn.util.binding.Bind;
import net.wagsn.util.binding.ViewBinder;

import java.util.List;

import me.wcy.music.R;
import me.wcy.music.http.HttpCallback;
import me.wcy.music.http.HttpClient;
import me.wcy.music.model.OnlineMusic;
import me.wcy.music.model.OnlinePlaylist;
import me.wcy.music.model.Playlist;

/**
 * 歌单列表适配器
 * Created by wcy on 2015/12/19.
 */
public class SheetAdapter extends BaseAdapter {
    private static final int TYPE_PROFILE = 0;
    private static final int TYPE_MUSIC_LIST = 1;
    private Context mContext;
    private List<Playlist> mData;

    /**
     * 数据初始化
     * @param data
     */
    public SheetAdapter(List<Playlist> data) {
        mData = data;
    }

    @Override
    public int getCount() {
        return mData.size();
    }

    @Override
    public Object getItem(int position) {
        return mData.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    /**
     * 是否可以点击
     * @param position
     * @return
     */
    @Override
    public boolean isEnabled(int position) {
        return getItemViewType(position) == TYPE_MUSIC_LIST;
    }

    /**
     * 获取列表项视图类型
     * @param position
     * @return
     */
    @Override
    public int getItemViewType(int position) {
        if (mData.get(position).getId().equals("#")) {
            return TYPE_PROFILE;
        } else {
            return TYPE_MUSIC_LIST;
        }
    }

    /**
     * 获取列表项视图类型数量
     * @return
     */
    @Override
    public int getViewTypeCount() {
        return 2;
    }

    /**
     * 获取指定位置列表项视图
     * @param position
     * @param convertView
     * @param parent
     * @return
     */
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        mContext = parent.getContext();
        ViewHolderProfile holderProfile;
        ViewHolderMusicList holderMusicList;
        Playlist playlist = mData.get(position);
        int itemViewType = getItemViewType(position);
        switch (itemViewType) {
            case TYPE_PROFILE: // 分类信息
                if (convertView == null) {
                    convertView = LayoutInflater.from(mContext).inflate(R.layout.view_holder_sheet_profile, parent, false);
                    holderProfile = new ViewHolderProfile(convertView);
                    convertView.setTag(holderProfile);
                } else {
                    holderProfile = (ViewHolderProfile) convertView.getTag();
                }
                holderProfile.tvProfile.setText(playlist.getName());
                break;
            case TYPE_MUSIC_LIST:  // 歌单信息
                if (convertView == null) {
                    convertView = LayoutInflater.from(mContext).inflate(R.layout.view_holder_sheet, parent, false);
                    holderMusicList = new ViewHolderMusicList(convertView);
                    convertView.setTag(holderMusicList);
                } else {
                    holderMusicList = (ViewHolderMusicList) convertView.getTag();
                }
                getPlaylistInfo(playlist, holderMusicList);
                holderMusicList.vDivider.setVisibility(isShowDivider(position) ? View.VISIBLE : View.GONE);
                break;
        }
        return convertView;
    }

    /**
     * 是否显示分割线
     * @param position
     * @return
     */
    private boolean isShowDivider(int position) {
        return position != mData.size() - 1;
    }

    /**
     * 获取歌单（榜单）信息<br/>
     * Rename by Wagsn on 2019/5/12.
     * @param playlist
     * @param holderMusicList
     */
    private void getPlaylistInfo(final Playlist playlist, final ViewHolderMusicList holderMusicList) {
        if (playlist.getCoverUrl() == null) {
            holderMusicList.tvMusic1.setTag(playlist.getName());
            holderMusicList.ivCover.setImageResource(R.drawable.default_cover);
            holderMusicList.tvMusic1.setText("1.加载中…");
            holderMusicList.tvMusic2.setText("2.加载中…");
            holderMusicList.tvMusic3.setText("3.加载中…");
            // 根据歌单ID，获取前三个音乐信息
            HttpClient.getSongListInfoFromBaidu(playlist.getId(), 3, 0, new HttpCallback<OnlinePlaylist>() {
                @Override
                public void onSuccess(OnlinePlaylist response) {
                    if (response == null || response.getSong_list() == null) {
                        return;
                    }
                    if (!playlist.getName().equals(holderMusicList.tvMusic1.getTag())) {
                        return;
                    }
                    parse(response, playlist);
                    setData(playlist, holderMusicList);
                }

                @Override
                public void onFail(Exception e) {
                }
            });
        } else {
            holderMusicList.tvMusic1.setTag(null);
            setData(playlist, holderMusicList);
        }
    }

    /**
     * 将响应的歌单信息解析成榜单项（榜单列表的列表项）
     * @param response
     * @param playlist
     */
    private void parse(OnlinePlaylist response, Playlist playlist) {
        List<OnlineMusic> onlineMusics = response.getSong_list();
        playlist.setCoverUrl(response.getBillboard().getPic_s260());
        if (onlineMusics.size() >= 1) {
            playlist.setMusic1(mContext.getString(R.string.song_list_item_title_1,
                    onlineMusics.get(0).getTitle(), onlineMusics.get(0).getArtist_name()));
        } else {
            playlist.setMusic1("");
        }
        if (onlineMusics.size() >= 2) {
            playlist.setMusic2(mContext.getString(R.string.song_list_item_title_2,
                    onlineMusics.get(1).getTitle(), onlineMusics.get(1).getArtist_name()));
        } else {
            playlist.setMusic2("");
        }
        if (onlineMusics.size() >= 3) {
            playlist.setMusic3(mContext.getString(R.string.song_list_item_title_3,
                    onlineMusics.get(2).getTitle(), onlineMusics.get(2).getArtist_name()));
        } else {
            playlist.setMusic3("");
        }
    }

    /**
     * 将榜单信息的数据刷新到界面上去
     * @param playlist
     * @param holderMusicList
     */
    private void setData(Playlist playlist, ViewHolderMusicList holderMusicList) {
        holderMusicList.tvMusic1.setText(playlist.getMusic1());
        holderMusicList.tvMusic2.setText(playlist.getMusic2());
        holderMusicList.tvMusic3.setText(playlist.getMusic3());
        Glide.with(mContext)
                .load(playlist.getCoverUrl())
                .placeholder(R.drawable.default_cover)
                .error(R.drawable.default_cover)
                .into(holderMusicList.ivCover);
    }

    /**
     * 榜单分类视图持有器
     */
    private static class ViewHolderProfile {
        @Bind(R.id.tv_profile)
        private TextView tvProfile;

        public ViewHolderProfile(View view) {
            ViewBinder.bind(this, view);
        }
    }

    /**
     * 榜单项视图持有器
     */
    private static class ViewHolderMusicList {
        @Bind(R.id.iv_cover)
        private ImageView ivCover;
        @Bind(R.id.tv_music_1)
        private TextView tvMusic1;
        @Bind(R.id.tv_music_2)
        private TextView tvMusic2;
        @Bind(R.id.tv_music_3)
        private TextView tvMusic3;
        @Bind(R.id.v_divider)
        private View vDivider;

        public ViewHolderMusicList(View view) {
            ViewBinder.bind(this, view);
        }
    }
}
