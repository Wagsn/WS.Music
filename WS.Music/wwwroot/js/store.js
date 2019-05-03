// 本地存储
var store = {
    save: function (name, data) {
        if (data instanceof Object) {
            sessionStorage.setItem(name, JSON.stringify(data))
        }
        else {
            sessionStorage.setItem(name, data)
        }
    },
    load: function (name) {
        return JSON.parse(sessionStorage.getItem(name))
    }
}
//export default store;
