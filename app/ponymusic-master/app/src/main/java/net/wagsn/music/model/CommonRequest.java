package net.wagsn.music.model;

/**
 * 通用请求<br/>
 * Created by Wagsn on 2019/5/12.
 */
public class CommonRequest {

    private int pageIndex = 0;
    private int pageSize = 20;
    private String keyword = "";

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

    public String getKeyword() {
        return keyword;
    }

    public void setKeyword(String keyword) {
        this.keyword = keyword;
    }
}
