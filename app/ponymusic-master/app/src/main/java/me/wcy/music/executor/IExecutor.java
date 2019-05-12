package me.wcy.music.executor;

/**
 * 执行器接口
 * Created by hzwangchenyan on 2017/1/20.
 */
public interface IExecutor<T> {
    /**
     * 执行
     */
    void execute();

    /**
     * 执行准备
     */
    void onPrepare();

    /**
     * 执行成功
     * @param t
     */
    void onExecuteSuccess(T t);

    /**
     * 执行失败
     * @param e
     */
    void onExecuteFail(Exception e);
}
