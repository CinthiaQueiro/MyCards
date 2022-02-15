<template>
  <div class="pb-5">
      <button type="button" class="btn btn-primary" @click="createDeckCard">Adicionar novo Baralho</button>
      <ul>
          <li v-for="card in deckCards" class="list-group-item d-flex justify-content-between align-items-start">
              <div class="ms-2 me-auto">
                  <div class="fw-bold">{{card.description}}</div>
              </div>
              <span class="badge bg-success rounded-pill">{{card.easy}}</span>
              <span class="badge bg-primary rounded-pill">{{card.medium}}</span>
              <span class="badge bg-danger rounded-pill">{{card.hard}}</span>
          </li>
      </ul>
      <modal-create-deck-cards :is-open="createDeckCards" @is-open="createDeckCards=$event" @deck-saved="deckSaved($event)"></modal-create-deck-cards>
  </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        deckCards: [],
        createDeckCards: false
    };
  },
  components: {
     ModalCreateDeckCards: httpVueLoader('components/modals/ModalCreateDeckCards.vue')
  },
  methods: {
      loadData() {
          api.getDeckCards().then(c => { this.deckCards = c});
      },
      createDeckCard() {
          this.createDeckCards = true;
      },
      deckSaved(deckCard) {
          console.log('chegou ', deckCard);
          if (deckCard.isSuccess) {
              app.$notyf.success("Card salvo com sucesso");
              this.deckCards.unshift(deckCard.data);
          } else {
              app.$notyf.success(deckCard.returnMessage);
          }
          this.createDeckCards = false;
      }
  },
  created(){
      console.log('Load data');
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
</style>