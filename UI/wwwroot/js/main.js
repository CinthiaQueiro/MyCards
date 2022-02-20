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
			path: '/', component: httpVueLoader('pages/Route.vue'), children: [
				{ path: '', name: 'login', component: httpVueLoader('pages/Login.vue') },
				{ path: '/DeckCards', name: "decks", component: httpVueLoader('pages/DeckCard.vue') },
				{ path: '/CreateCard', name: "createCard", component: httpVueLoader('pages/CreateCard.vue') }
			]
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
	async created() {
		store.language = 'pt-br';
		this.$localizer.loadFile(store.language);		
	}
});
