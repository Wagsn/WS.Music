package net.wagsn.util;

import org.junit.Test;

public class JsonUtilTest {

    @Test
    public void toJson() {
        Logger.i(JsonUtil.toJson(new Person()));
    }

    public class Person{
        public String name = "zhangsan";
        public int age =18;
    }

    @Test
    public void toObject() {
    }

    @Test
    public void toObject1() {
    }
}