<template>
  <div class="pb-5">
    TESTE CINTHIA ....    
      <ul>
          <li v-for="card in deckCards">{{card.description}}</li>
      </ul>
  </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        deckCards: []
    };
  },
  methods: {   
      loadData() {
          api.getDeckCards().then(c => { this.deckCards = c});
      }
  },
  created(){
    this.loadData();
  }
}
</script>
