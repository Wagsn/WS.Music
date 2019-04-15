package net.wagsn.music.util.binding;

import android.app.Activity;
import android.view.View;

import java.lang.reflect.Field;

/**
 * View绑定工具
 * Created by Wagsn on 2018/11/14.
 */
public class ViewBinder {

    /**
     * 绑定Activity
     * @param activity
     */
    public static void bind(Activity activity) {
        bind(activity, activity.getWindow().getDecorView());
    }

    /**
     * 通过反射将View和对象绑定起来
     * @param target
     * @param source
     */
    public static void bind(Object target, View source) {
        Field[] fields = target.getClass().getDeclaredFields();
        if (fields != null && fields.length > 0) {
            for (Field field : fields) {
                try {
                    field.setAccessible(true);
                    if (field.get(target) != null) {
                        continue;
                    }

                    Bind bind = field.getAnnotation(Bind.class);
                    if (bind != null) {
                        int viewId = bind.value();
                        field.set(target, source.findViewById(viewId));
                    }
                } catch (Exception e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
