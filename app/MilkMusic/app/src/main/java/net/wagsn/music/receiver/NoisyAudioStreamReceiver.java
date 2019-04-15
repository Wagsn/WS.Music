package net.wagsn.music.receiver;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

import net.wagsn.music.service.AudioPlayer;

/**
 *  来电/耳机拔出时暂停播放（接收广播）
 * Created by Wagsn on 2018/11/15 10:11.
 */
//public class NoisyAudioStreamReceiver extends BroadcastReceiver {
//
//    @Override
//    public void onReceive(Context context, Intent intent) {
//        AudioPlayer.get().playPause();
//    }
//}