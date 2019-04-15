package net.wagsn.music.service;

import android.app.Service;
import android.content.Context;
import android.content.Intent;
import android.os.Binder;
import android.os.IBinder;
import android.support.annotation.Nullable;
import android.util.Log;

import net.wagsn.music.constants.Actions;

/**
 * 音乐播放后台服务
 * Created by Wagsn on 2018/11/14.
 */
public class PlayService extends Service {

    private static final String TAG = "PlayService";

    public class PlayBinder extends Binder {
        public PlayService getService() {
            return PlayService.this;
        }
    }

    public PlayService() {
    }

    @Override
    public void onCreate() {
        super.onCreate();
        Log.i(TAG, "onCreate: " + getClass().getSimpleName());
        // 后台服务绑定组件及初始化
    }

    /**
     * 获取音乐服务的绑器
     * @param intent
     * @return
     */
    @Nullable
    @Override
    public IBinder onBind(Intent intent) {
        return new PlayBinder();
    }

    /**
     * 通过静态方法返回与Activity绑定的PlayService
     * @param context
     * @param action
     */
    public static void startCommand(Context context, String action) {
        Intent intent = new Intent(context, PlayService.class);
        intent.setAction(action);
        context.startService(intent);
    }

    @Override
    public int onStartCommand(Intent intent, int flags, int startId) {
        if (intent != null && intent.getAction() != null) {
            switch (intent.getAction()) {
                case Actions.ACTION_STOP:
                    stop();
                    break;
            }
        }
        return START_NOT_STICKY;
    }

    /**
     * 服务停止，关闭所有组件
     */
    private void stop() {
        //AudioPlayer.get().stopPlayer();
        //QuitTimer.get().stop();
        //Notifier.get().cancelAll();
    }
}
