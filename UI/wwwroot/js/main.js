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
				{ path: '/login', name: 'login', component: httpVueLoader('pages/Login.vue') },
				{ path: '/deckCards', name: "decks", component: httpVueLoader('pages/DeckCard.vue') },
				{ path: '/createCard', name: "createCard", component: httpVueLoader('pages/CreateCard.vue') },
				{ path: '/cards/:id', name: "toStudyCards", component: httpVueLoader('pages/Cards.vue') },
				{ path: '/loginEmail', name: 'loginEmail', component: httpVueLoader('pages/LoginEmail.vue') },
			]
		},
	],
	linkExactActiveClass: 'active'
});

router.beforeEach(async (to, from, next) => {
	store.loading = true;

	if (to.path == "/login" && store.myUser != null) {
		next({ path: `/deckCards` });
	} else {
		next();
    }

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
