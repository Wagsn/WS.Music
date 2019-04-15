package net.wagsn.music.storage;

import android.content.Context;

import net.wagsn.util.FileUtil;
import net.wagsn.util.JsonUtil;

import java.io.File;
import java.io.Serializable;
import java.util.HashMap;

/**
 * JSON 序列化与反序列化存储工具
 * Created by Wagsn on 2018/11/15 11:40.
 */
public class JsonHelper {

    /**
     * JsonHelper配置文件
     */
    public JsonConfig cfg;

    /**
     * JSON文件后缀名
     */
    private String jsonExt =".json";

    /**
     * 记录TAG于Class的映射关系，序列化时可以保存TAG与ClassName（net.wagsn.model.Song）
     */
    private HashMap<String, Class<?>> tagClass = new HashMap<>();

    /**
     * 获取全局唯一的JsonHelper，TODO 添加获取非唯一JsonHelper的方法
     * @return
     */
    public static JsonHelper get(){
        return SingletonHolder.instance;
    }

    private static class SingletonHolder {
        private static JsonHelper instance = new JsonHelper();
    }

    private JsonHelper(){
        cfg = new JsonConfig();
    }

    /**
     * 上下文初始化，必须要初始化
     * @param config
     */
    public void init(JsonConfig config){
        this.cfg = config;
    }

    /**
     * 通过TAG标签和类型来加载对象,TODO: 在保存时记录TAG对象的Class信息
     * @param tag
     * @param clazz
     * @param <T>
     * @return
     */
    public <T> T load (String tag, Class<T> clazz){
        if (cfg.context==null){
            return JsonUtil.toObject(FileUtil.load(cfg.jsonRoot + File.separator + tag + jsonExt).toString(), clazz);
        }else {
            return JsonUtil.toObject(FileHelper.load(cfg.context, cfg.jsonRoot + File.separator+tag + jsonExt), clazz);
        }
    }

    /**
     * 保存对象
     * @param tag
     * @param obj
     */
    public void save(String tag, Object obj){
        if (cfg.context==null){
            FileUtil.save(cfg.jsonRoot+File.separator+tag+jsonExt, JsonUtil.toJson(obj), false);
        } else {
            FileHelper.save(cfg.context, cfg.jsonRoot+File.separator+tag+jsonExt, JsonUtil.toJson(obj));
        }
    }

    /**
     * 可序列化的参数配置
     */
    public class JsonConfig implements Serializable {

        // 序列化ID
        //private long

        /**
         * JSON文件存储根路径，默认为"/data/data/appPackageName/files/json"<br/>
         * Android 7.0 可能是保存于 /Android/data/appPackageName/files/json
         */
        public String jsonRoot = "./json";

        /**
         * JsonHelper运行上下文
         */
        public Context context =null;

        /**
         * 将本配置文件输出为JSON字符串
         * @return JSON string.
         */
        public String toJson(){
            return JsonUtil.toJson(this);
        }

        /**
         * 通过JSON字符串初始化
         * @param cfgJson
         */
        public void init(String cfgJson){
            JsonConfig cfg = JsonUtil.toObject(cfgJson, JsonConfig.class);
            this.context =cfg.context;
            this.jsonRoot =cfg.jsonRoot;
        }
    }

    /**
     * 检查环境是否正常
     */
    private void check(){
        if (cfg.context==null){
            throw new RuntimeException("JsonConfig.context not Initialization");
        }
    }
}
