package me.wcy.music.activity;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.preference.Preference;
import android.preference.PreferenceFragment;

import me.wcy.music.BuildConfig;
import me.wcy.music.R;

/**
 * 关于界面
 */
public class AboutActivity extends BaseActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_about);

        getFragmentManager().beginTransaction().replace(R.id.ll_fragment_container, new AboutFragment()).commit();
    }

    /**
     * 关于
     */
    public static class AboutFragment extends PreferenceFragment implements Preference.OnPreferenceClickListener {
        private Preference mVersion;
        private Preference mShare;
        private Preference mStar;
        private Preference mWeibo;
        private Preference mCsdn;
        private Preference mGithub;

        /**
         * 关于
         * @param savedInstanceState
         */
        @Override
        public void onCreate(Bundle savedInstanceState) {
            super.onCreate(savedInstanceState);
            addPreferencesFromResource(R.xml.preference_about);

            mVersion = findPreference("version");
            mShare = findPreference("share");
            mStar = findPreference("star");
//            mWeibo = findPreference("weibo");
            mCsdn = findPreference("csdn");
            mGithub = findPreference("github");

            mVersion.setSummary("v " + BuildConfig.VERSION_NAME);
            setListener();
        }

        /**
         * 设置监听
         */
        private void setListener() {
            mShare.setOnPreferenceClickListener(this);
            mStar.setOnPreferenceClickListener(this);
//            mWeibo.setOnPreferenceClickListener(this);
            mCsdn.setOnPreferenceClickListener(this);
            mGithub.setOnPreferenceClickListener(this);
        }

        /**
         * 当点击到关于里面的各个选项
         * @param preference
         * @return
         */
        @Override
        public boolean onPreferenceClick(Preference preference) {
            if (preference == mShare) {
                share();
                return true;
            } else if (preference == mStar) {
                openUrl(getString(R.string.about_project_url));
                return true;
            } else if (preference == mCsdn || preference == mGithub) {
                openUrl(preference.getSummary().toString());
                return true;
            }
            return false;
        }

        /**
         * 分享到
         */
        private void share() {
            Intent intent = new Intent(Intent.ACTION_SEND);
            intent.setType("text/plain");
            intent.putExtra(Intent.EXTRA_TEXT, getString(R.string.share_app, getString(R.string.app_name)));
            startActivity(Intent.createChooser(intent, getString(R.string.share)));
        }

        /**
         * 通过URL打开页面
         * @param url
         */
        private void openUrl(String url) {
            Intent intent = new Intent(Intent.ACTION_VIEW);
            intent.setData(Uri.parse(url));
            startActivity(intent);
        }
    }
}
