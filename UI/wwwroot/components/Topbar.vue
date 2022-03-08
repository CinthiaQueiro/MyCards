<template lang="html">
	<nav v-if="nameUser != ''" class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0 shadow">
		<router-link to="/" class="navbar-brand mr-0 px-3 py-2">
			<img src="images/deck_cards2.png" height="32" alt="logo" />
			<label class="titlesite">My Cards</label>
		</router-link>
		<div class="sessionUser">
			
			<div class="btn-group">
				<button type="button" class="btn btn-danger dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
					<img src="images/myPerfil.png" /><span class="nameUser">{{nameUser}}</span>
				</button>
				<ul class="dropdown-menu">
					<li><a class="dropdown-item" href="#">Editar Perfil</a></li>
					<li><hr class="dropdown-divider"></li>
					<li>
						<button class="dropdown-item" @click="signOut">
							Logout
						</button>
					</li>
				</ul>
			</div>

		</div>
	</nav>
</template>

<script>
	module.exports = {
		data() {
			return {				
				lang: this.$store.language
			};
		},
		computed: {
			nameUser() {
                return (this.$store.myUser) ? this.$store.myUser.name : "";
            }
		},
		methods: {
			signOut() {
                console.log("signOut");
				var auth2 = gapi.auth2.getAuthInstance();
				var thisVue = this;
                auth2.signOut().then(function () {
					console.log('User signed out.');
					thisVue.myUser = null;
                    thisVue.$router.push("/login");
                });
            }
        }
	}
</script>
<style lang="css" scoped>
    .titlesite {
        font-size: 1.5rem;
        font-family: 'Hubballi'
    }
	.nameUser{
		color: white;
		margin-left: 2px;
	}
	.sessionUser {
		margin-right: 10px;
	}
</style>