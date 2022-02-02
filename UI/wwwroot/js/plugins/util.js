const util = {
    clone(obj) {
        return JSON.parse(JSON.stringify(obj));
    }
}

Vue.prototype.$util = util;