package net.wagsn.util;

import android.util.Log;

import java.lang.reflect.ParameterizedType;
import java.lang.reflect.Type;

/**
 * 泛型工具<br/>
 * Created by Wagsn on 2019/5/12.
 */
public class GenericsUtil {
    private static final String TAG = "GenericsUtil";

    /**
     * 通过反射,获得定义Class时声明的父类的范型参数的类型.
     * 如public BookManager extends GenricManager<Book>
     *
     * @param clazz The class to introspect
     * @return the first generic declaration, or <code>Object.class</code> if cannot be determined
     */
    public static Class getSuperClassGenricType(Class clazz) {
        return getSuperClassGenricType(clazz, 0);
    }

    /**
     * 通过反射,获得定义Class时声明的父类的范型参数的类型.
     * 如public BookManager extends GenricManager<Book>
     *
     * @param clazz clazz The class to introspect
     * @param index the Index of the generic ddeclaration,start from 0.
     */
    public static Class getSuperClassGenricType(Class clazz, int index) throws IndexOutOfBoundsException {

        Type genType = clazz.getGenericSuperclass();

        if (!(genType instanceof ParameterizedType)) {
            Log.d(TAG, "getSuperClassGenricType: !(genType instanceof ParameterizedType)");
            return Object.class;
        }

        Type[] params = ((ParameterizedType) genType).getActualTypeArguments();

        if (index >= params.length || index < 0) {
            Log.d(TAG, "getSuperClassGenricType: index >= params.length || index < 0");
            return Object.class;
        }
        Log.d(TAG, "getSuperClassGenricType: params[index]: "+params[index]);
        Log.d(TAG, "getSuperClassGenricType: params[index].getClass(): "+params[index].getClass());
        if (!(params[index] instanceof Class)) {
            Log.d(TAG, "getSuperClassGenricType: !(params[index] instanceof Class)");
            return Object.class;
        }
        return (Class) params[index];
    }
}
