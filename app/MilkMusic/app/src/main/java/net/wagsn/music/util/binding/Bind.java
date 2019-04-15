package net.wagsn.music.util.binding;

import java.lang.annotation.ElementType;
import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;
import java.lang.annotation.Target;

/**
 * 视图绑定注解，用于将View和XML文件绑定起来
 * Create by Wagsn on 2018/11/14
 */
@Target(ElementType.FIELD)
@Retention(RetentionPolicy.RUNTIME)
public @interface Bind {
    /**
     * XNL的ID
     * @return
     */
    int value();
}