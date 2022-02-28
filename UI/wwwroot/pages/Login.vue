<template>
    <div class="container pb-5">
        <div class="row">
            <div class="col col-12 p-2 m-2">
                <img src="../images/deck_cards4.png"/><label class="titlesite">My Cards</label>
            </div>
            <div class="col col-12 p-2 m-2">
                <div class="g-signin2" data-onsuccess="onSignIn" data-theme="dark"></div>
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
  watch: {
      "$store.loadUser": function() {
          var thisVue = this;
          if (this.$store.myUser != null && this.$store.myUser.Id == undefined ) {
              api.saveLogin(this.$store.myUser).then(s => {
                  if (s.isSuccess) {
                      thisVue.$store.myUser = s.data;
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
</style>