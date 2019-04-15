package net.wagsn.music.model;

import java.io.Serializable;

/**
 * Created by Wagsn on 2018/11/16 09:06.
 */
public class Song {  // implements Serializable
    public String id;
    public String name;

    public Song(){}

    public Song(String id, String name){
        this.id = id;
        this.name =name;
    }
}
