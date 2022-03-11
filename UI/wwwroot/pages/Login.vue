<template>
    <div class="container pb-5">
        <div class="row">
            <div class="col col-12 p-2 m-2">
                <img src="../images/deck_cards3.png" /><label class="titlesite">My Cards</label>
            </div>
            <div class="col col-12 p-2 m-2 login">
                <div id="g-signin2" data-theme="dark"></div>
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
    };
  }, 
  methods: {
      onSuccess(googleUser) {
          // Useful data for your client-side scripts:
          var profile = googleUser.getBasicProfile();
          console.log("ID: " + profile.getId()); // Don't send this directly to your server!
          console.log('Full Name: ' + profile.getName());
          console.log('Given Name: ' + profile.getGivenName());
          console.log('Family Name: ' + profile.getFamilyName());
          console.log("Image URL: " + profile.getImageUrl());
          console.log("Email: " + profile.getEmail());

          // The ID token you need to pass to your backend:
          var id_token = googleUser.getAuthResponse().id_token;
          console.log("ID Token: " + id_token);
          var user = { Name: profile.getName(), Email: profile.getEmail() };
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
      onFailure() {
          console.log("signOut");
          var auth2 = gapi.auth2.getAuthInstance();
          var thisVue = this;
          auth2.signOut().then(function () {
              console.log('User signed out.');
              auth2.disconnect();
              window.location.href = websiteUrl;
          });
      },
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
      }
  },
    mounted() {
        var thisVue = this;
        gapi.signin2.render('g-signin2', {
            'scope': 'profile email',            
            'theme': 'dark',
            'onsuccess': thisVue.onSuccess,
            'onfailure': thisVue.onFailure
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
</style>