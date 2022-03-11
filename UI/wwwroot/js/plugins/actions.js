const actions = {
    setCache(key, value) {
        localStorage.setItem(key, JSON.stringify(value));
    },
	getCache(key) {
		try {
			return JSON.parse(localStorage.getItem(key));
		} catch {
			return null;
		}
	}
};
Vue.prototype.$actions = actions;
