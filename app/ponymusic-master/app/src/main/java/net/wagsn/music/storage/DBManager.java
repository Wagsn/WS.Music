package net.wagsn.music.storage;

import android.content.Context;

import org.greenrobot.greendao.database.Database;

import net.wagsn.music.storage.db.greendao.DaoMaster;
import net.wagsn.music.storage.db.greendao.DaoSession;
import net.wagsn.music.storage.db.greendao.MusicDao;

/**
 * 数据库管理<br/>
 * Created by wcy on 2019/5/12.
 */
public class DBManager {
    private static final String DB_NAME = "database";
    private MusicDao musicDao;

    public static DBManager get() {
        return SingletonHolder.instance;
    }

    private static class SingletonHolder {
        private static DBManager instance = new DBManager();
    }

    public void init(Context context) {
        DaoMaster.DevOpenHelper helper = new DaoMaster.DevOpenHelper(context, DB_NAME);
        Database db = helper.getWritableDb();
        DaoSession daoSession = new DaoMaster(db).newSession();
        musicDao = daoSession.getMusicDao();
    }

    private DBManager() {
    }

    public MusicDao getMusicDao() {
        return musicDao;
    }
}
