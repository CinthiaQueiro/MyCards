<template>
  <div class="pb-5 container">
      <button type="button" class="btn btn-primary" @click="createDeckCard">Adicionar novo Baralho</button>
      <ul>
          <li v-for="card in deckCards" @click.stop.prevent="deckCardEdit($event,card.id)" class="list-group-item d-flex justify-content-between align-items-start">
              <div class="ms-2 me-auto">
                  <div v-show="(idDeskCardEdit != card.id)" class="fw-bold">{{card.description}}</div>
                  <div v-show="idDeskCardEdit == card.id && !saving" class="input-group mb-6">
                      <input @keyup="editDeckCard(card)" v-model="card.description" type="text" class="form-control" value="card.description" placeholder="Username" aria-label="Username" aria-describedby="basic-addon1">
                  </div>
                  <div v-show="idDeskCardEdit == card.id && saving" class="fw-bold saving"><i class="icon-spinner"><span>{{$localizer('saving')}}...</span></i></div>
              </div>
              <span class="badge bg-success rounded-pill">{{card.easy}}</span>
              <span class="badge bg-primary rounded-pill">{{card.medium}}</span>
              <span class="badge bg-danger rounded-pill">{{card.hard}}</span>
          </li>
      </ul>
      <ul class="edit list-group" @mouseleave="closeEdit">
          <li class="list-group-item">
              <router-link :to="'/Cards'">{{$localizer('estudar')}}</router-link>
          </li>
          <li class="list-group-item" @click.prevent.stop="deleteDeckCard">{{$localizer('excluirbaralho')}}</li>
      </ul>
      <modal-create-deck-cards :is-open="createDeckCards" @is-open="createDeckCards=$event" @deck-saved="deckSaved($event)"></modal-create-deck-cards>
      <div class="addCard" :title="$localizer('adicionarcard')"><router-link :to="'/CreateCard'"><i class="icon-plus"></i></router-link></div>
  </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        deckCards: [],
        createDeckCards: false,
        idDeskCardEdit: 0,
        timer: null,
        saving: false
    };
  },
  components: {
     ModalCreateDeckCards: httpVueLoader('components/modals/ModalCreateDeckCards.vue')
  },
  methods: {
      loadData() {
          api.getMyUser().then(u => { this.$store.myUser = u; });
          api.getDeckCards().then(c => { this.deckCards = c});
      },
      closeEdit() {
          $(".edit").css({display: "none" });
      },
      deleteDeckCard() {
          var thisVue = this;
          swal(thisVue.$localizer("excludedeck"), {
              buttons: {
                  cancel: thisVue.$localizer("no"),
                  catch: {
                      text: thisVue.$localizer("yes"),
                      value:"Sim",
                  }
              },
          })
              .then((value) => {
                  switch (value) {
                      case "Sim":
                          //swal("Chama o back!");
                          var card = this.deckCards.find(d => { return d.id == this.idDeskCardEdit });                 
                          api.deleteDeckCard(card).then(d => {
                              thisVue.saving = false;
                              if (d.isSuccess) {
                                  app.$notyf.success(thisVue.$localizer("deckdeleted"));
                                  thisVue.deckCards = thisVue.deckCards.filter(d => { return d.id != thisVue.idDeskCardEdit })
                              } else {
                                  app.$notyf.error(d.returnMessage);
                              }
                          });
                          break;
                  }
              });
      },
      deckCardEdit(event, id) {
          this.idDeskCardEdit = id;
          $(".edit").css({ top: event.clientY, left: event.clientX, display: "block" });
      },
      createDeckCard() {
          this.createDeckCards = true;
      },
      deckSaved(deckCard) {
          console.log('chegou ', deckCard);
          if (deckCard.isSuccess) {
              app.$notyf.success(app.$localizer("decksaved"));
              this.deckCards.unshift(deckCard.data);
          } else {
              app.$notyf.success(deckCard.returnMessage);
          }
          this.createDeckCards = false;
      },
      editDeckCard(deckCard) {
          var thisVue = this;
          if (deckCard.description.length >= 3) {
              var newCard = this.$util.clone(deckCard);
              newCard.User = new Object();
              newCard.User.id = this.$store.myUser.id;
              if(this.timer) clearTimeout(this.timer);
              this.timer = setTimeout(() => {
                  this.saving = true;
                  api.editDeckCard(newCard).then(d => {
                      thisVue.saving = false;
                      if (d.isSuccess) {
                          app.$notyf.success(thisVue.$localizer("decksaved"));
                      } else {
                          app.$notyf.success(d.returnMessage);
                      }                      
                  });
              }, 2000)
          }
      }
  },
  created(){     
     this.loadData();
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
    .list-group-item a {
        color: black;
        text-decoration:none;
    }
    .edit {
        position: absolute;
        display: none;
    }
    .saving span {
        margin-left: 2%;
        font-weight: bold;
    }
    .saving i {
        color: green;
        font-size:1.2em;
    }
    .addCard {        
        display: flex;
        justify-content: flex-end;
        position: absolute;
        top: 0px;
        right: 1%;
        font-size: 0.8em;
        color: white;
        border-radius: 100%;
        border: 1px solid blue;
        background-color: blue;
        padding: 0.6%;
    }
    .addCard:hover {
        cursor: pointer;
    }
    .addCard a {
        color: white;
        text-decoration: none;
    }
    .container {
        position: relative;
    }
</style>