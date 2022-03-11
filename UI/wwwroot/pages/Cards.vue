<template>
    <div>
        <div v-if="showCard && card != null" class="card p-3" style="width: 98%;">
            <img v-if="card.idTypeQuestion == 2" :src=card.dataQuestion class="card-img-top">
            <audio controls v-else-if="card.idTypeQuestion == 3">
                <source v-bind:src="card.dataQuestion" type="audio/mp3" />
                seu navegador não suporta HTML5
            </audio>
            <label v-else>{{card.dataQuestion}}</label>
            <div class="card-body">
                <button href="#" @click="showAnswer" class="btn btn-primary">{{$localizer('mostrarresposta')}}</button>
            </div>
        </div>
        <div v-if="!showCard && card != null" class="card p-3" style="width: 98%;">
            <img v-if="card.idTypeAnswer == 2" :src=card.dataAnswer class="card-img-top">
            <audio controls v-else-if="card.idTypeAnswer == 3">
                <source v-bind:src="card.dataAnswer" type="audio/mp3" />
                seu navegador não suporta HTML5
            </audio>
            <label v-else>{{card.dataAnswer}}</label>
            <div class="card-body">
                <button @click="setDateShow('easy')" class="btn btn-success">{{$localizer('facil')}}</button>
                <button @click="setDateShow('medium')" class="btn btn-primary">{{$localizer('medio')}}</button>
                <button @click="setDateShow('hard')" class="btn btn-danger">{{$localizer('dificil')}}</button>
            </div>
        </div>
        <button class="btn btn-primary turnback">
            <router-link to="/DeckCards">Voltar</router-link>
        </button>
    </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        card: null,
        showCard: true,
        listCards:[],
        indice: 0
    };
  },
  methods: {
      loadData() {
          var thisVue = this;
          api.getCards(this.$route.params.id).then(c => {
              if (c.isSuccess) {
                  thisVue.card = c.data[thisVue.indice];
                  thisVue.listCards = c.data;
              }
          });
      },
      showAnswer() {
          this.showCard = !this.showCard;
      },
      setDateShow(typeCard) {
          var time = this.card.classification.find(c => { return c.description == typeCard });
          var thisVue = this;
          this.card.dateShow = moment()
              .add(time.repeatTime, time.type).format("YYYY-MM-DD HH:mm:ss");
          this.card.idClassification = time.id;
          api.updateCard(this.card).then(c => {
              if (c.isSuccess) {
                  thisVue.indice++;
                  if (thisVue.listCards[thisVue.indice] != undefined) {
                      thisVue.showAnswer();
                      thisVue.card = thisVue.listCards[thisVue.indice];
                  } else {
                      window.location.href = `/#/DeckCards`;
                  }
              } else {
                  app.$notyf.error(app.$localizer("erro"));
              }
          })
      }
  },
  created(){
     this.loadData();
  }
}
</script>
<style lang="css" scoped>
    .card {
        width: 98%;
        align-items: center;
        min-height: 420px;
    }
    .card img {
        height: 400px;
        width: 400px;
    }
    .turnback {
        position: absolute;
        right: 3%;
        margin-top: 10px;
    }
    .turnback a {
        color: white;
        text-decoration: none;
    }
</style>