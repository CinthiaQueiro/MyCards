<template>
    <div class="pb-5">
        <div class="g-signin2" data-onsuccess="onSignIn" data-theme="dark"></div>
    </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
    };
  },
  watch: {
      "$store.loadUser": function() {
          console.log('mudou my user', this.$store.myUser);
          var thisVue = this;
          if (this.$store.myUser != null && this.$store.myUser.Id == undefined ) {
              api.saveLogin(this.$store.myUser).then(s => {
                  console.log('retorno', s);
                  if (s.isSuccess) {
                      thisVue.$store.myUser = s.data;
                      console.log('user', thisVue.$store.myUser);
                      console.log('retorno teste ', thisVue, thisVue.router);
                      thisVue.$router.push("/DeckCards");
                  }
              });
          }
      }
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
</style>