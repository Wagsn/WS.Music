package net.wagsn.music.storage;

import android.content.Context;
import android.os.Environment;

import net.wagsn.util.FileUtil;

import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.Reader;
import java.io.Serializable;
import java.io.Writer;

/**
 * 文件助手类，原生的Java静态文件处理方法放在{@link FileUtil}里面
 * Created by Wagsn on 2018/11/15 13:31.
 */
public class FileHelper {

    private Context context;

    public void init(Context context) {
        this.context = context;
    }

    /**
     * 获取在SD卡中的应用根路径
     * @return
     */
    private static String getAppDir(){
        return Environment.getExternalStorageState()+"/Wagsn/MilkMusic";
    }

    private static String getSongDir(){
        return getAppDir()+"/Song";
    }

    private static String getLrcDir(){
        return getAppDir() +"/Lyric";
    }

    private static String getAlbumDir(){
        return getAppDir()+"/Album";
    }

    /**
     * 可序列化的文件配置
     */
    static class FilePathConfig implements Serializable {
        public String appDir = Environment.getExternalStorageState()+"/Wagsn/MilkMusic";
        public String songDir = appDir+"/Song";
        public String lrcDir = appDir+"/Lyric";
        public String albumDir = appDir+"/Album";
        public String logDir =appDir+"/Log";
        public String splashDir = appDir+"/Splash";
    }

    /**
     * 得到文件的Reader
     *
     * @param filePath
     * @return
     */
    public static Reader getReader(String filePath) {
        FileReader fr = null;
        try {
            fr = new FileReader(filePath);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return fr;
    }

    /**
     * 得到文件的Writer，如果得不到将返回null
     *
     * @param filePath
     * @return
     */
    public static Writer getWriter(String filePath) {
        FileWriter fw = null;
        try {
            fw = new FileWriter(filePath);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return fw;
    }

    /**
     * 在本应用程序文件夹读取文本文件
     * @param context app context.
     * @param filePath file path, format "dir/dir/file.ext"
     * @return
     */
    public static String load(Context context, String filePath) {
        // 获取/data|Android/data/appPackageName/files的File对象，getDir() 获取/data|Android/data/appPackageName的File对象
        return FileUtil.load(context.getFilesDir().toString()+"/"+filePath).toString();
    }

    /**
     * 保存
     * @param filePath
     * @param content
     */
    public static void save(Context context, String filePath, String content){
        FileUtil.save(context.getFilesDir().toString()+"/"+filePath, content, false);
    }
}

