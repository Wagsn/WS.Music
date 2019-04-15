package net.wagsn.util;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONArray;
import com.alibaba.fastjson.JSONObject;

import java.util.List;

/**
 * Json 解析工具，需要引入 FastJson<br/>
 * 用于与外部Json解析包解耦<br/>
 * Created by Wagsn on 2018/11/15 17:41.
 */
public class JsonUtil {

    /**
     * 将对象转化成 JSON 字符串<br/>
     * 注：非公有的字段不会被序列化，TODO 查询一下是不是FastJson的配置问题
     * @param object
     * @return
     */
    public static String toJson(Object object){
        return JSON.toJSONString(object);
    }

    /**
     * 将字符串转换成JavaObject
     * @param jsonString
     * @param clazz
     * @param <T>
     * @return
     */
    public static <T> T toObject(String jsonString, Class<T> clazz){
        return JSON.parseObject(jsonString, clazz);
    }

    /**
     * 获取List
     * @param json
     * @param clazz
     * @param <T>
     * @return
     */
    public static <T> List<T> toList(String json, Class<T> clazz){
        return JSON.parseArray(json, clazz);
    }

    /**
     * 获取List或者Object
     * @param json
     * @param clazz
     * @param isList
     * @param <T>
     * @return
     */
    public static <T> Object toObject (String json, Class<T> clazz, boolean isList){
        if (isList){
            return JSON.parseArray(json, clazz);
        } else {
            return JSON.parseObject(json, clazz);
        }
    }

    public static JSONObject toObject(String json){
        return JSON.parseObject(json);
    }

    public static JSONArray toList (String json){
        return JSON.parseArray(json);
    }
}
