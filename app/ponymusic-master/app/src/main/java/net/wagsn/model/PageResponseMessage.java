package net.wagsn.model;

import net.wagsn.model.ResponseMessage;

import java.util.List;

/**
 * 分页响应体
 * @param <T>
 */
public class PageResponseMessage<T> extends ResponseMessage<List<T>> {
    private int pageIndex;
    private int pageSize;
    private long totalCount;

    public int getPageIndex() {
        return pageIndex;
    }

    public void setPageIndex(int pageIndex) {
        this.pageIndex = pageIndex;
    }

    public int getPageSize() {
        return pageSize;
    }

    public void setPageSize(int pageSize) {
        this.pageSize = pageSize;
    }

    public long getTotalCount() {
        return totalCount;
    }

    public void setTotalCount(long totalCount) {
        this.totalCount = totalCount;
    }

}
