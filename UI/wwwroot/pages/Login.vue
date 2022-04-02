<template>
    <div class="container pb-5">
        <div class="row">
            <div class="col col-12 p-2 m-2">
                <img src="../images/deck_cards3.png" /><label class="titlesite">My Cards</label>
            </div>
            <div class="col col-12 p-2 m-2 login">
                <button class="btn btn-primary p-1"  id="g-signin2" data-theme="dark"><i class="icon-google"></i>Login via Google</button>
                <button class="btn btn-primary p-1" style="display:none">
                    <router-link to="/loginEmail">{{$localizer('tentarEmail')}}</router-link>
                </button>
                <button class="btn btn-primary p-1" @click="loginTest">
                    {{$localizer('usuarioteste')}}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        auth2: null
    };
  }, 
  methods: {
      loginTest() {
          var user = { Name: "User teste", Email: "userteste@teste.com" };
          this.$store.myUser = user;
          this.$store.loadUser = true;
          var thisVue = this;
          api.saveLogin(this.$store.myUser).then(s => {
              if (s.isSuccess) {
                  thisVue.$actions.setCache("user", s.data);
                  thisVue.$store.myUser = s.data;
                  thisVue.$router.push("/deckCards");
              }
          });
      },
      attachSignin(element) {
          var thisVue = this;
          this.auth2.attachClickHandler(element, {},
              function (googleUser) {
                  var profile = googleUser.getBasicProfile();
                  // The ID token you need to pass to your backend:
                  var id_token = googleUser.getAuthResponse().id_token;
                  var user = { Name: profile.getName(), Email: profile.getEmail() };
                  app.store.myUser = user;
                  app.store.loadUser = true;
                  api.saveLogin(app.store.myUser).then(s => {
                      if (s.isSuccess) {
                          app.$actions.setCache("user", s.data);
                          app.store.myUser = s.data;
                          app.$router.push("/deckCards");
                      }
                  });
              }, function (error) {
                  alert(JSON.stringify(error, undefined, 2));
              });
      }
  },
    mounted() {
        var thisVue = this;
        gapi.load('auth2', function () {
            // Retrieve the singleton for the GoogleAuth library and set up the client.
            thisVue.auth2 = gapi.auth2.init({
                client_id: '1004139561983-jnk68700g5lr2qdfcl0bhlci4um78jeq.apps.googleusercontent.com',            
                scope: 'profile'
            });
            thisVue.attachSignin(document.getElementById('g-signin2'));
        });       
    }
}
</script>
<style lang="css" scoped>
    ul {
        margin-top: 20px;
    }
    .list-group-item {
        cursor: pointer;
    }
    .list-group-item:hover {
        background-color:#ccc;
    }
    .container {
        height: 700px;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .titlesite {
        font-size: 1.5rem;
        font-family: 'Hubballi'
    }
    .btn-primary a {
        color: white;
        text-decoration: none;
    }
    .btn-primary {
        margin-left: 5px;
    }
    .login {
        display: flex;
        justify-content: flex-start;
    }
    .icon-google {
        margin-right: 5px;
    }
</style>