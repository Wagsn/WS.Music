package net.wagsn.util;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by Wagsn on 2018/11/16 18:01.
 */
public class TextUtil {
    /**
     * 过滤特殊字符(\/:*?"<>|)
     */
    private static String stringFilter(String str) {
        if (str == null) {
            return null;
        }
        String regEx = "[\\/:*?\"<>|]";
        Pattern p = Pattern.compile(regEx);
        Matcher m = p.matcher(str);
        return m.replaceAll("").trim();
    }
}
