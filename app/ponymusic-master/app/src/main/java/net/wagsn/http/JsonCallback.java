package net.wagsn.http;

import android.util.Log;

import com.google.gson.Gson;
import com.zhy.http.okhttp.callback.Callback;

import net.wagsn.util.GenericsUtil;

import java.io.IOException;
import java.lang.reflect.ParameterizedType;

import okhttp3.Call;
import okhttp3.Response;

/**
 * Json回调封装
 * Created by wcy on 2015/12/20.
 */
public abstract class JsonCallback<T> extends Callback<T> {
    private static final String TAG ="JsonCallback<T>";

    private Class<T> clazz;
    private Gson gson;

    /**
     * 单参数泛型Json回调构造器<br/>
     * Created by Wagsn on 2019/1/12.
     */
    public JsonCallback(){
//        this.clazz = new Class<T>();
//        ParameterizedType type = (ParameterizedType) this.getClass()
//                .getGenericSuperclass();
//        Log.d(TAG, "JsonCallback: type: "+type);
        Class<T> responseClass = GenericsUtil.getSuperClassGenricType(getClass());
        Log.d(TAG, "JsonCallback: responseClass: "+responseClass);
        this.clazz = responseClass;
        gson = new Gson();
    }

    public JsonCallback(Class<T> clazz) {
        this.clazz = clazz;
        gson = new Gson();
    }

    /**
     * 将响应体解析成指定类型的对象
     * @param response Http响应体
     * @param id
     * @return
     * @throws IOException
     */
    @Override
    public T parseNetworkResponse(Response response, int id) throws IOException {
        try {
            String jsonString = response.body().string();
            //Log.d(TAG, "parseNetworkResponse: "+jsonString);
            return gson.fromJson(jsonString, clazz);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }
}
