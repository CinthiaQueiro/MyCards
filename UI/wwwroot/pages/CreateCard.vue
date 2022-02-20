<template>
    <div class="pb-5 container">
        <label class="description">Baralho:</label>
        <select class="form-select" aria-label="Selecione um baralho">
            <option selected>Selecione um baralho</option>
            <option v-for="card in deckCards" :value="card.id">{{card.description}}</option>
        </select>
        <label class="description">
            Pergunta:
            <span class="attachment">
                <button class="btn btn-light"><i class="icon-attachment"></i></button>
            </span>
        </label>

        <div class="input-group mb-3">
            <span class="input-group-text" id="basic-addon1"></span>
            <input type="text" class="form-control" placeholder="" aria-label="">
        </div>

        <div class="row">
            <div class="col-1"><button class="btn btn-light"><i class="icon-mic"></i></button></div>
            <div class="col-1"><button class="btn btn-light"><i class="icon-play"></i></button></div>
            <div class="col-1"><button class="btn btn-light"><i class="icon-stop2"></i></button></div>
        </div>
        <div class="row">
            <input id="attachmentButtonQuestion" type="file" v-on:change="attachmentImage($event,'question')" />
            <img :src="dataQuestion"/>
        </div>

        <label class="description">
            Resposta:
            <span class="attachment">
                <button class="btn btn-light"><i class="icon-attachment"></i></button>
            </span>
        </label>

        <div class="input-group mb-3">
            <span class="input-group-text" id="basic-addon1"></span>
            <input type="text" class="form-control" placeholder="" aria-label="">
        </div>

        <div class="row">
            <div class="col-1"><button class="btn btn-light"><i class="icon-mic"></i></button></div>
            <div class="col-1"><button class="btn btn-light"><i class="icon-play"></i></button></div>
            <div class="col-1"><button class="btn btn-light"><i class="icon-stop2"></i></button></div>
        </div>

        <div class="row">
            <input id="attachmentButtonAnswer" type="file" v-on:change="attachmentImage($event ,'answer')" />
            <img :src="dataAnswer" />
        </div>

    </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        deckCards: {},
        typeMessage: "",
        maxWidthAttachmentFiles: 1,
        dataQuestion: "",
        dataAnswer: ""
    };
  },
  methods: {
      loadData() {
          //api.getMyUser().then(u => { this.$store.myUser = u; });
          api.getDeckCards().then(c => { this.deckCards = c});
      },
      attachmentImage: function (event, type) {
          var thisVue = this;
          var reader = new FileReader();
          reader.onload = function (event) {
              if (event.loaded == 0) {
                  swal({
                      title: thisVue.$localizer("erro"),
                      text:  thisVue.$localizer("arquivovazio"),
                      icon: "error",
                  });
                  return;
              }

              var validFile = false;
              var validSize = true;
              var files = {'data:image': 'IMAGE' };
              for (var i in files) {
                  if (event.target.result.indexOf(i) >= 0) {
                      validFile = true;
                      thisVue.typeMessage = files[i];
                  }
              }
              //valida tamanho
              input = (type == 'question') ? document.getElementById('attachmentButtonQuestion') : document.getElementById('attachmentButtonAnswer');
              var file = input.files[0];
              console.log('tamanho ', file);
              if ((file.size / 1024 / 1024) > thisVue.maxWidthAttachmentFiles) {
                  validSize = false;
              }

              if (!validFile) {
                  swal({
                      title: thisVue.$localizer("erro"),
                      text: thisVue.$localizer("extensaoinvalida"),
                      icon: "error",
                  });
                  return;
              }

              if (!validSize) {
                  swal({
                      title: thisVue.$localizer("erro"),
                      text: thisVue.$localizer("tamanholimite"),
                      icon: "error",
                  });
                  return;
              }

              //thisVue.messageContent = event.target.result;
              console.log('TESTE ', event.target.result);

             // input.value = '';
              if (type == 'question') {
                  thisVue.dataQuestion = event.target.result;
              }
              else {
                  thisVue.dataAnswer = event.target.result;
              }
          };
          thisVue.nameArchive = event.target.files[0].name;
          reader.readAsDataURL(event.target.files[0]);
      }
  },
  created(){
     this.loadData();
  }
}
</script>
<style lang="css" scoped>
    .description {
        margin-top: 20px;
        width: 100%;
    }
    .attachment {
        display: inline-flex;
        justify-content: flex-end;
    }
</style>