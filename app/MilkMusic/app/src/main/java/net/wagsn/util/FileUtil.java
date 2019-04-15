/**
 * @file FileUtil.java
 * @package util
 * @project WSC
 * @author Wagsn
 * @date 2018年8月9日
 * @version 1.0.0
 */
package net.wagsn.util;

import java.io.BufferedInputStream;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.util.Locale;

import com.alibaba.fastjson.JSONObject;

/**
 * 文件工具类，不可继承
 * 
 * @class FileUtil
 * @package util
 * @project WSC
 * @author Wagsn
 * @date 2018年8月9日
 * @version 1.0.0
 */
public final class FileUtil {

	/**
	 * 包村json文件的路径
	 */
	@SuppressWarnings("unused")
	private static String jsonPath = "./files/json";

//	@Test
	public void testJSONFormat() throws Exception {
		// json 字符串
		String s = "{\"code\":10000,\"msg\":null,\"data\":{\"id\":\"7aa0eb56-1026-4497-a42e-4c39f5e3dcf1\",\"topicId\":\"0876ab84-a478-417b-91bc-849843c191a5\",\"title\":null,\"commentId\":null,\"content\":\""
				+ "开发者平台自动化测试：针对帖子发表评论"
				+ "\",\"images\":\"\",\"time\":\"2017-10-15 18:09:56\",\"userId\":\"61028f94-de92-4c65-aad3-2fc8614e1d34\",\"userName\":\"devautotest\",\"commentNum\":0,\"status\":0}}";
		System.out.println("--Input-> " + s);
	}

	/**
	 * 根据层级得到制表符字符串
	 * 
	 * @param level
	 * @return
	 */
	@SuppressWarnings("unused")
	private static String getLevelStr(int level) {
		StringBuffer levelStr = new StringBuffer();
		for (int levelI = 0; levelI < level; levelI++) {
			levelStr.append("\t");
		}
		return levelStr.toString();
	}

	/**
	 * 格式化文本
	 * 
	 * @param content
	 *            文本内容
	 * @param type
	 *            文本类型，如：JSON，XML等
	 */
	public static void format(String content, String type) {
	}

	/**
	 * 格式化正确格式的JSON字符串
	 * 
	 * @param json
	 */
	public static void formatJSON(String json) {
		// 1. 检查JSON字符串是否合法，调用检查函数
		// 2. 紧凑化JSON字符串（去掉不必要的空格换行等）利用正则表达式或replace方法
		// " : " -> ":" " , " -> "," { " -> {"
		// 3. 利用正则表达式或String的replace方法格式化文本
	}

	/**
	 * 示例文件路径
	 */
	public static String path = "./files/expression_src.ws";

	/**
	 * 在控制台打印info信息
	 * @param content
	 * @param msg
	 */
	public static void print(String content, String msg) {
		System.out.println("--Info-> " + msg);
		System.out.println(content);
	}
	
	/**
	 * 将文件路径下的数据解析成JSONObject
	 * @param path
	 * @return
	 */
	public static JSONObject loadToJSON(String path) {
		return JSONObject.parseObject(load(path).toString());
	}

	/**
	 * 加载文本文件通过路径从文件系统。 TODO 将srcPath扩展为URL，并增加路径检查
	 * 
	 * @param path
	 * @return
	 * @throws Exception
	 */
	public static StringBuffer load(String path) {
		StringBuffer buffer = new StringBuffer("");
		try {
			String code = charset(path);
			BufferedReader br = new BufferedReader(new InputStreamReader(new FileInputStream(new File(path)), code));
			String temp = null;
			while ((temp = br.readLine()) != null) {
				buffer.append(temp).append("\r\n"); // 添加回车换行符
			}
			br.close();
			return buffer;
		} catch (Exception e) {
			Logger.e("Failed to load text file [" + path + "]!");
			e.printStackTrace();
		}
		return buffer; // buffer.length()==0
	}

	/**
	 * 判断文本文件的字符集，文件开头三个字节表明编码格式。 <br/>
	 * <a href="http://blog.163.com/wf_shunqiziran/blog/static/176307209201258102217810/">参考的博客地址</a>
	 * 
	 * @param path text file path.
	 * @return charset for text file.
	 * @throws Exception
	 * @throws Exception
	 */
	public static String charset(String path) {
		String charset = "GBK";
		byte[] first3Bytes = new byte[3];
		try {
			boolean checked = false;
			BufferedInputStream bis = new BufferedInputStream(new FileInputStream(path));
			bis.mark(0); // 读者注： bis.mark(0);修改为 bis.mark(100);我用过这段代码，需要修改上面标出的地方。
							// Wagsn注：不过暂时使用正常，遂不改之
			int read = bis.read(first3Bytes, 0, 3);
			if (read == -1) {
				bis.close();
				return charset; // 文件编码为 ANSI
			} else if (first3Bytes[0] == (byte) 0xFF && first3Bytes[1] == (byte) 0xFE) {
				charset = "UTF-16LE"; // 文件编码为 Unicode
				checked = true;
			} else if (first3Bytes[0] == (byte) 0xFE && first3Bytes[1] == (byte) 0xFF) {
				charset = "UTF-16BE"; // 文件编码为 Unicode big endian
				checked = true;
			} else if (first3Bytes[0] == (byte) 0xEF && first3Bytes[1] == (byte) 0xBB
					&& first3Bytes[2] == (byte) 0xBF) {
				charset = "UTF-8"; // 文件编码为 UTF-8
				checked = true;
			}
			bis.reset();
			if (!checked) {
				while ((read = bis.read()) != -1) {
					if (read >= 0xF0)
						break;
					if (0x80 <= read && read <= 0xBF) // 单独出现BF以下的，也算是GBK
						break;
					if (0xC0 <= read && read <= 0xDF) {
						read = bis.read();
						if (0x80 <= read && read <= 0xBF) // 双字节 (0xC0 - 0xDF)
							// (0x80 - 0xBF),也可能在GB编码内
							continue;
						else
							break;
					} else if (0xE0 <= read && read <= 0xEF) { // 也有可能出错，但是几率较小
						read = bis.read();
						if (0x80 <= read && read <= 0xBF) {
							read = bis.read();
							if (0x80 <= read && read <= 0xBF) {
								charset = "UTF-8";
								break;
							} else
								break;
						} else
							break;
					}
				}
			}
			bis.close();
		} catch (Exception e) {
			e.printStackTrace();
		}
		System.out.println("--Info-> file [" + path + "] 's charset is [" + charset + "]");
		return charset;
	}

	/**
	 * 保存文本content到目标路径path中，以追加方式写文件 TODO 通用化处理
	 * @param path text file path.
	 * @param content
	 * @return
	 */
	public static boolean save(String path, String content) {
		return saveText(path, content, true);
	}

	/**
	 * 选择是否以追加模式保存
	 * @param path
	 * @param content
	 * @param append
	 * @return
	 */
	public static boolean save(String path, String content, boolean append) {
		return saveText(path, content, append);
	}

	/**
	 * 保存文本文件
	 * @param path Save target file path
	 * @param content Text content to be saved
	 * @param append if <code>true</code>, then content will be written to the end of the file rather than the beginning
	 * @return if <code>true</code>, save content successful.
	 */
	public static boolean saveText(String path, String content, boolean append) {
		if (!create(path, (!append))) { // 追加则不覆盖
			return false;
		}
		try {
			BufferedWriter bw = new BufferedWriter(new FileWriter(new File(path).getAbsoluteFile(), append));
			// OutputStreamWriter out = new OutputStreamWriter(new FileOutputStream(new
			// File(path)), "UTF-8"); // 指定文本编码输出
			bw.write(content);
			bw.close();
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			Logger.e("Failed to save text file [" + path + "]\r\n"+e.getMessage());
		}
		return false;
	}

	/**
	 * 复制文件从路径srcFile到路径dstFile，包含路径检查 TODO 未完成
	 * 
	 * @param srcPath
	 * @param dstPath
	 */
	public static void copy(String srcPath, String dstPath) {
		// TODO Auto-generated method stub
	}

	/**
	 * 复制文件从文件srcFile到路径dstFile，包含文件检查 TODO 未完成
	 * @param src
	 * @param dst
	 */
	public static void copy(File src, File dst) {
		// TODO Auto-generated method stub
	}

	/**
	 * 判断文件是否存在
	 * 
	 * @param path
	 * @return
	 */
	public static boolean exist(String path) {
		return new File(path).exists();
	}

	/**
	 * 检查路径格式是否正确，即格式以及能否在文件系统中存在 TODO 未完成
	 * @param path
	 * @return
	 */
	public static boolean check(String path) {
		return false;
	}
	
	/**
	 * 创建文件，存在则覆盖
	 * @param file
	 * @return
	 */
	public static boolean create(File file) {
		return create(file, true);
	}

	/**
	 * 创建文件
	 * @param file file
	 * @param over if
	 * @return
	 */
	public static boolean create(File file, boolean over){
		if (file.exists()) { // 文件存在 // TODO 文件类型类型检查
			if (!over) // 不覆盖
				return true;
			file.delete(); // 删除
		}
		if (!file.getParentFile().exists()) { // 父文件夹不存在
			file.getParentFile().mkdirs();
		}
		try {
			file.createNewFile(); // 创建文件
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			Logger.e("Failed to create file [" + path + "]\r\n"+e.getMessage());
		}
		return false;
	}
	
	/**
	 * 创建路径为path的文件，默认以覆盖模式创建，
	 * @see FileUtil#create(String, boolean) create(path, over)
	 * @param path file path.
	 * @return if <code>true</code>, then successful
	 */
	public static boolean create(String path) {
		return create(path, true);
	}

	/**
	 * 创建文件
	 * @param path 文件路径
	 * @param over 是否覆盖已存在的文件
	 * @return if <code>true</code>, then successful
	 */
	public static boolean create(String path, boolean over) {
		return create(new File(path), over);
	}

	/**
	 * 在控制台显示文件树状图 TODO 用制表符显示成树状
	 * @param path file path.
	 * @param prefix name prefix.
	 */
	public static void showFileTree(String path, String prefix) {
		File root = new File(path);
		prefix += "--";
		System.out.println(prefix + root.getName());
		if (root.isDirectory()) {
			String[] childs = root.list();
			for (int i = 0; i < childs.length; i++) {
				showFileTree(path + "\\" + childs[i], prefix);
			}
		}
	}

	/**
	 * clear text file.
	 * @param path text file path.
	 * @return if <code>true</code>, clear successful.
	 */
	public static boolean clear(String path) {
		try {
			File file = new File(path);
			if (!create(path)) {
				return false;
			}
			BufferedWriter bw = new BufferedWriter(new FileWriter(file.getAbsoluteFile()));
			bw.write("");
			bw.close();
			return true;
		} catch (IOException e) {
			e.printStackTrace();
			return false;
		}
	}

    /**
     * 创建文件夹
     * @param dir
     * @return
     */
    public static String mkdirs(String dir) {
        File file = new File(dir);
        if (!file.exists()) {
            file.mkdirs();
        }
        return dir;
    }

	/**
	 * 转换文件大小单位（B, KB, MB, GB, TB, PB, EB）
	 * @param srcSize 原始大小
	 * @param srcUnit 原始单位
	 * @param dstUnit 目标单位
	 * @return 目标单位的大小
	 */
    public static double convertUnit(double srcSize, String srcUnit, String dstUnit){
		double sizeOfB = srcSize;
		switch (srcUnit){
			case "b":
			case "B":
				break;
			case "KB":
			case "kb":
				sizeOfB=srcSize*1024;
				break;
			case "MB":
			case "mb":
				sizeOfB=srcSize*1024*1024;
				break;
			case "GB":
			case "gb":
				sizeOfB=srcSize*1024*1024*1024;
				break;
			case "TB":
			case "tb":
				sizeOfB=srcSize*1024*1024*1024*1024;
				break;
			default:
				throw new UnsupportedOperationException("Unsupported unit conversion");

		}
		return b2other(sizeOfB, dstUnit);
	}

	/**
	 * 转换文件尺寸大小单位<br/>
	 * 从Byte到其它
	 * @param sizeOfB Byte单位的大小
	 * @param dstUnit 目标单位
	 * @return 目标单位的大小
	 */
	public static double b2other(double sizeOfB, String dstUnit){
		switch (dstUnit){
			case "b":
			case "B":
				return sizeOfB;
			case "KB":
			case "kb":
				return sizeOfB/1024;
			case "MB":
			case "mb":
				return sizeOfB/1024/1024;
			case "GB":
			case "gb":
				return sizeOfB/1024/1024/1024;
			case "TB":
			case "tb":
				return sizeOfB/1024/1024/1024/1024;
			default:
				throw new UnsupportedOperationException("Unsupported unit conversion");
		}
    }

    /**
     * 存储单位转换<br/>
     * 将b转换成mb
     * @param b
     * @return
     */
    public static float b2mb(int b) {
        String mb = String.format(Locale.getDefault(), "%.2f", (float) b / 1024 / 1024);
        return Float.valueOf(mb);
    }
}
