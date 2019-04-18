package net.wagsn.util;

import org.junit.Test;

public class FileUtilTest {

    @Test
    public void save() {
//        FileUtil.save("././json/Person.json", JsonUtil.toJson(new Person()), false);
//        // end, ProjectName/app/json/Person.json
//        Person p = JsonUtil.toObject(FileUtil.load("././json/Person.json").toString(), Person.class);
//        Logger.i(p);
//        Logger.i(FileUtil.exist("././json/Person.json"));
        Logger.i("src file size: 512B, dst: "+FileUtil.convertUnit(512, "B", "KB")+"KB.");
    }

    static class Person{
        public String name = "lisi";
        public int age =19;
    }
}