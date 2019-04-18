/**
 * @file Logger.java
 * @package util
 * @project WSC
 * @author Wagsn
 * @date 2018年8月12日
 * @version 1.0.0
 */
package net.wagsn.util;

import android.annotation.SuppressLint;

import java.io.Serializable;
import java.text.SimpleDateFormat;
import java.util.Date;

/**
 * 用来打印日志到项目根目录下的[log]文件下。
 * TODO 日志输出配置，创建LogConfig序列化类来保存配置，可以指定总日志和错误日志等输出路径
 * @class Logger
 * @package util
 * @project WSC
 * @author Wagsn
 * @date 2018年8月12日
 * @version 1.0.0
 */
public class Logger {

	/**
	 * 全局日志记录器
	 * @return
	 */
	public static Logger get(){
		return SingletonHolder.instance;
	}

	/**
	 * 私有日志记录器，（logRoot="./log/tag")
	 * @param config
	 * @return
	 */
	public static Logger get(LogConfig config){
		return new Logger();
	}

	public static class LogConfig implements Serializable {
		private String tag = "default";
		private SimpleDateFormat timeSDF = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS");
		private SimpleDateFormat nameSDF = new SimpleDateFormat("yyyy-MM-dd");
		private String logRoot = "./log/default/log";
		private String errRoot = "./log/default/err";

		public LogConfig(){}

		/**
		 * 构造函数
		 * @param tag 日志标签
		 * @param timeSDF 时间格式
		 * @param nameSDF 日志文件的格式，可以间接限制日志文件大小
		 * @param logRoot 日志输出根路径
		 */
		public LogConfig(String tag, SimpleDateFormat timeSDF, SimpleDateFormat nameSDF, String logRoot) {
			this.tag = tag;
			this.timeSDF = timeSDF;
			this.nameSDF = nameSDF;
			this.logRoot = logRoot;
		}
	}

	private static class SingletonHolder{
		public static Logger instance = new Logger();
	}

	private LogConfig cfg = new LogConfig();

	/**
	 * 方法开始
	 */
	public void methodStart() {
		System.out.println(">>Start-> "+parentMethodName());
	}
	
	/**
	 * 方法结束
	 */
	public void methodEnd() {
		System.out.println("<<End-> "+parentMethodName());
	}
	
	/**
	 * 获取当前方法名称
	 * @return 获取当前方法名
	 */
	@SuppressWarnings("unused")
	private static String currMethodNmae() {
		return Thread.currentThread().getStackTrace()[2].toString();
	}
	
	/**
	 * 返回父调用方法名
	 */
	private static String parentMethodName() {
		return Thread.currentThread().getStackTrace()[3].toString();
	}
	
	/**
	 * 时间格式化
	 */
    @SuppressLint("SimpleDateFormat")
    private static final SimpleDateFormat timeSDF = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss.SSS");

	/**
	 * 文件名采用日期的格式
	 */
	private static final SimpleDateFormat nameSDF= new SimpleDateFormat("yyyy-MM-dd");

	/**
	 * 日志文件夹路径，项目根路径
	 */
	private static final String logPath ="./log";

	/**
	 * 将日志打印到[./log/yyyy-MM-dd.log]文件里面，并在控制台打印日志
	 * 打印一般消息
	 * @param content Information message
	 */
	public static void i(String content) {
		Date date = new Date();
		System.out.println("--Log-> Info-> "+content);
		FileUtil.saveText(logPath+"/" + nameSDF.format(date)+".log", "["+timeSDF.format(date)+"] [INFO] "+content+"\r\n", true);  // append
	}

    /**
     * 打印日志（并输出到运行时根路径下log文件夹下，对象将被序列化成JSON字符串）
     * @param object 打印对象
     */
	public static void i(Object object){
	    if (object==null){
	        i("null");
        }else {
	        i(JsonUtil.toJson(object));
        }
    }
	
	/**
	 * 打印警告消息
	 * @param content Warning message
	 */
	public static void w(String content) {
		Date date = new Date();
		System.out.println("--Log-> Warn-> "+content);
		FileUtil.saveText(logPath+"/" + nameSDF.format(date)+".log", "["+timeSDF.format(date)+"] [WARN] "+content+"\r\n", true);  // append
	}
	
	/**
	 * 打印错误消息
	 * @param content 错误消息
	 */
	public static void e(String content) {
		Date date = new Date();
		System.err.println("--Log-> Error-> "+content);
		FileUtil.saveText(logPath+"/" + nameSDF.format(date)+".log", "["+timeSDF.format(date)+"] [ERROR] "+content+"\r\n", true);  // append
	}

    /**
     * 打印错误日志
     * @param object 打印对象
     */
	public static void e(Object object){
        Date date = new Date();
        System.err.println("--Log-> Error-> "+JsonUtil.toJson(object));
        FileUtil.saveText(logPath+"/" + nameSDF.format(date)+".log", "["+timeSDF.format(date)+"] [ERROR] "+JsonUtil.toJson(object)+"\r\n", true);  // append
    }
}
