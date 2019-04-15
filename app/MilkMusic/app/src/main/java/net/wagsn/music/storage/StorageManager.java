package net.wagsn.music.storage;

import android.content.Context;

import net.wagsn.util.FileUtil;
import net.wagsn.util.JsonUtil;

/**
 * 全局唯一的存储管理器，选择通过JSON（当前默认）或DB或其它形式存储数据，是否加密等等
 * Created by Wagsn on 2018/11/15 10:32.
 */
public class StorageManager {

    public JsonHelper jsonHelper = JsonHelper.get();

    /**
     * 得到全局存储管理器
     * @return
     */
    public static StorageManager get(){
        return SingletonHolder.instance;
    }

    private static class SingletonHolder {
        private static StorageManager instance = new StorageManager();
    }

    /**
     * 持久化存储Java对象，一个TAG一个对象，TAG将被保存为JSON文件名，所以格式有要求
     * @param tag 不得含有特殊字符及空格，最好用英文即类名
     * @param obj 要存储的对象
     */
    public void save(String tag, Object obj){
        FileUtil.save("./json/"+tag+".json", JsonUtil.toJson(obj), false);
    }

    /**
     * 加载对象，通过标签和类型
     * @param tag
     * @param clazz
     * @param <T>
     * @return
     */
    public <T> T load(String tag, Class<T> clazz){
        return JsonUtil.toObject(FileUtil.load("./json/"+tag+".json").toString(), clazz);
    }

    /**
     * 加载Java对象
     * @param tag
     * @return
     */
    public Object load(String tag){
        return JsonUtil.toObject(FileUtil.load("./json/"+tag+".json").toString());
        //throw new RuntimeException("Not Support");
    }

    /**
     * 初始化
     * @param context
     */
    public void init(Context context){

    }

    /**
     * 私有构造器
     */
    private StorageManager(){}
}
