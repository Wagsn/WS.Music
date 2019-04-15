package net.wagsn.music.storage;

import android.util.Log;

import net.wagsn.music.model.Song;
import net.wagsn.util.Logger;

import org.junit.Test;

import java.util.ArrayList;
import java.util.List;

import static org.junit.Assert.*;

public class StorageManagerTest {

    @Test
    public void save() {
        StorageManager manager = StorageManager.get();
        Song song =new Song("1254", "你好啊");
        Song song2 = new Song("15477", "再也不见");
        Song song3 = new Song("26461", "转角遇见爱");
        List<Song> songs= new ArrayList<>();
        songs.add(song);
        songs.add(song2);
        songs.add(song3);
        SongHolder songHolder = new SongHolder(songs);
//        Logger.i(song);
//        manager.save("net.wagsn.music.Song", song);
//        Song loadSong =manager.load("net.wagsn.music.Song", Song.class);
//        Logger.i(loadSong);
        Logger.i(songHolder);
        String holderTag = "SongHolder";
        manager.save(holderTag, songHolder);

        // 场景1 连续的读取存储模拟
        // 加载
        SongHolder loadSongHolder = manager.load(holderTag, SongHolder.class);
        Logger.i(loadSongHolder);
        // 添加
        loadSongHolder.add(new Song("2654165", "偏爱"));
        // 保存
        manager.save(holderTag, loadSongHolder);
        // 加载
        SongHolder loadSongHolder2 = manager.load(holderTag, SongHolder.class);
        Logger.i(loadSongHolder2);
        // 添加
        loadSongHolder2.add(new Song("16845", "我爱你"));
        // 保存
        manager.save(holderTag, loadSongHolder2);
        // 加载
        SongHolder loadSongHolder3 = manager.load(holderTag, SongHolder.class);
        Logger.i(loadSongHolder3);
    }

    static class SongHolder extends ModelHolder<Song>{

        public SongHolder(List<Song> songs){
            super(songs, Song.class);
        }
        public SongHolder(){}

        public boolean add(Song song){
            return super.add(song);
        }
    }

    /**
     * 模型容器
     * @param <E>
     */
    static class ModelHolder<E>{

        public final List<E> models = new ArrayList<>();

        public Class clazz;

        public String version = "1.0.0";

        private ModelHolder(){}

        public ModelHolder(List<? extends E> models, Class<E> clazz){
            this.clazz = clazz;
            this.models.addAll(models);
        }

        public boolean add(E model){
            clazz =model.getClass();
            return models.add(model);
        }

        public boolean remove(E model){
            return models.remove(model);
        }

        public boolean addAll(List<? extends E> models){
            return this.models.addAll(models);
        }
    }

    @Test
    public void load() {
    }
}