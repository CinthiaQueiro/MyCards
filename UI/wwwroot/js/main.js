httpVueLoader.httpRequest = function (url) {
	const version = '20210915.08';
	return new Promise(function (resolve, reject) {
		$.get(url + '?v=' + version).done(resolve).fail(reject);
	});
};

Vue.mixin({
	data() {
		return { store };
	}
});

const router = new VueRouter({
	routes: [
		{
			path: '/', component: httpVueLoader('../pages/DeckCard.vue')
		},
	],
	linkExactActiveClass: 'active'
});

router.beforeEach(async (to, from, next) => {
	store.loading = true;
	next();
	store.loading = false;
});

var app = new Vue({
	el: '#app',
	router: router,
	created() {
		store.language = 'pt-br';
		this.$localizer.loadFile(store.language);		
	}
});
