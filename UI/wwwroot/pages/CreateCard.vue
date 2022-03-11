<template>
    <div class="pb-5 container">
        <label class="description">Baralho:</label>
        <select class="form-select" v-model="deckCard" aria-label="Selecione um baralho">
            <option value="">{{$localizer('selecionebaralho')}}</option>
            <option v-for="card in deckCards" :value="card.id">{{card.description}}</option>
        </select>
        <label class="description">
            {{$localizer('pergunta')}}:
            <span class="attachment">
                <button @click.stop.prevent="openOptionsAttachment($event, 'question')" class="btn btn-light"><i class="icon-attachment"></i></button>
            </span>
        </label>

        <div class="input-group mb-3">
            <span class="input-group-text edit">
                <button @click="editText" class="btn btn-light"><i class="icon-pencil" title="editar"></i></button>
            </span>
            <input v-model="dataQuestion" :disabled="typeQuestion == 'AUDIO' || typeQuestion == 'IMG'" type="text" class="form-control" placeholder="" aria-label="">
        </div>

        <div class="row" v-show="showAttachmentAudioQuestion">
            <div class="col-6">
                <div class="row">
                    <div class="col-1 align-self-center"><button class="btn btn-light" @click="recordMessage"><i :class="[{ recording: recording, pulse: pulse && recording}, 'icon-mic']"></i></button></div>
                    <div class="col-1 align-self-center"><button class="btn btn-light" @click="cancelAudioCard"><i class="icon-stop2"></i></button></div>
                    <div class="col-1" v-if="audioUrl.length > 0">
                        <audio controls>
                            <source v-bind:src="audioUrl" type="audio/mp3" />
                            seu navegador não suporta HTML5
                        </audio>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" v-show="showAttachmentImageQuestion">
            <input id="attachmentButtonQuestion" type="file" v-on:change="attachmentImage($event,'question')" />
            <img class="attachmentimg" :src="dataQuestion" />
        </div>

        <label class="description">
            {{$localizer('resposta')}}:
            <span class="attachment">
                <button @click.stop.prevent="openOptionsAttachment($event, 'answer')" class="btn btn-light"><i class="icon-attachment"></i></button>
            </span>
        </label>

        <div class="input-group mb-3">
            <span class="input-group-text edit">
                <button @click="editText" class="btn btn-light"><i class="icon-pencil" title="editar"></i></button>
            </span>
            <input v-model="dataAnswer" :disabled="typeAnswer == 'AUDIO' || typeAnswer == 'IMG'" type="text" class="form-control" placeholder="" aria-label="">
        </div>

        <div class="row" v-show="showAttachmentAudioAnswer">            
            <div class="col-6">
                <div class="row">
                    <div class="col-1 align-self-center"><button class="btn btn-light" @click="recordMessage"><i :class="[{ recording: recording, pulse: pulse && recording}, 'icon-mic']"></i></button></div>
                    <div class="col-1 align-self-center"><button class="btn btn-light" @click="cancelAudioCard"><i class="icon-stop2"></i></button></div>
                    <div class="col-1" v-if="audioUrl.length > 0">
                        <audio controls>
                            <source v-bind:src="audioUrl" type="audio/mp3" />
                            seu navegador não suporta HTML5
                        </audio>
                    </div>
                </div>
            </div>
        </div>

        <div class="row" v-show="showAttachmentImageAnswer">
            <input id="attachmentButtonAnswer" type="file" v-on:change="attachmentImage($event ,'answer')" />
            <img class="attachmentimg" :src="dataAnswer" />
        </div>

        <div class="saveCard" >
            <button class="btn btn-primary">
                <router-link to="/DeckCards">Voltar</router-link>
            </button>
            <button v-show="dataQuestion != '' && dataAnswer != '' && deckCard != ''" class="btn btn-success" @click="saveCard">Salvar</button>
        </div>

        <ul class="optionsAttachment list-group" @mouseleave="closeOptions">
            <li class="list-group-item" @click="showAttachmentImg">
                {{$localizer('addimagem')}}
            </li>
            <li class="list-group-item" @click="showAttachmentAudio">{{$localizer('addaudio')}}</li>
        </ul>

    </div>
</template>

<script>
module.exports = {
  data: function () {
    return {
        deckCards: {},
        deckCard:"",
        typeMessage: "",
        maxWidthAttachmentFiles: 1,
        typeQuestion:"",
        dataQuestion: "",
        typeAnswer:"",
        dataAnswer: "",
        showAttachmentImageQuestion: false,
        showAttachmentImageAnswer: false,
        showAttachmentAudioQuestion: false,
        showAttachmentAudioAnswer: false,
        isQuestion: false,
        recording: false,
        timerInterval: null,
        timeLimit: 60,
        timePassed: 0,
        mediaRecorder: null,
        audioChunks: [],
        pulse: false,
        audioUrl:""
    };
  },
  methods: {
      loadData() {
          //api.getMyUser().then(u => { this.$store.myUser = u; });
          api.getDeckCards().then(c => { this.deckCards = c});
      },
      closeOptions() {
          $(".optionsAttachment").css({ display: "none" });
      },
      editText() {
          if (this.isQuestion) {
              this.typeQuestion = "TEXT";
              this.dataQuestion = "";
              this.showAttachmentImageQuestion = false;
              this.showAttachmentAudioQuestion = false;
          } else {
              this.typeAnswer = "TEXT";
              this.dataAnswer = "";
              this.showAttachmentImageAnswer = false;
              this.showAttachmentAudioAnswer = false;
          }
      },
      showAttachmentImg() {
          if (this.isQuestion) this.typeQuestion = "IMG"; else this.typeAnswer = "IMG";
          if (this.isQuestion) this.showAttachmentImageQuestion = !this.showAttachmentImageQuestion;
          if (!this.isQuestion) this.showAttachmentImageAnswer = !this.showAttachmentImageAnswer;
          if (this.isQuestion) this.showAttachmentAudioQuestion = false;
          if (!this.isQuestion) this.showAttachmentAudioAnswer = false;
      },
      showAttachmentAudio() {
          if (this.isQuestion) this.typeQuestion = "AUDIO"; else this.typeAnswer = "AUDIO";         
          if (this.isQuestion)this.showAttachmentAudioQuestion = !this.showAttachmentAudioQuestion;
          if (!this.isQuestion)this.showAttachmentAudioAnswer = !this.showAttachmentAudioAnswer;
          if (this.isQuestion) this.showAttachmentImageQuestion = false;
          if (!this.isQuestion) this.showAttachmentImageAnswer = false;
      },

      openOptionsAttachment(event, type) {
          this.isQuestion = (type == "question");
          $(".optionsAttachment").css({ top: event.clientY, left: event.clientX, display: "block" });
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

              if (type == 'question') {
                  thisVue.dataQuestion = event.target.result;
              }
              else {
                  thisVue.dataAnswer = event.target.result;
              }
          };
          thisVue.nameArchive = event.target.files[0].name;
          reader.readAsDataURL(event.target.files[0]);
      },
      recordMessage: function () {
          //if recording now, return;   
          var thisVue = this;
          if (thisVue.recording) return;

          if (typeof MediaRecorder === 'undefined' || !navigator.getUserMedia) {
              swal({
                  title: thisVue.$localizer("erro"),
                  text: thisVue.$localizer("versionchrome"),
                  icon: "error",
              });
              return;
          }

          var enumeratorPromise = navigator.mediaDevices.enumerateDevices().then(function (devices) {
              var mic = devices.find(function (device) {
                  return device.kind === "audiooutput";
              });
              if (mic == undefined) {
                  swal({
                      title: thisVue.$localizer("erro"),
                      text: thisVue.$localizer("audiodriver"),
                      icon: "error",
                  });
                  return;
              }

              thisVue.typeMessage = "AUDIO";
              thisVue.recording = true;
              var constraints = { video: false, audio: true };
              return navigator.mediaDevices.getUserMedia(constraints)
                  .then(function (stream) {
                      thisVue.mediaRecorder = new MediaRecorder(stream);                    
                      //start timer
                      thisVue.startTimer();
                      thisVue.mediaRecorder.start();

                      thisVue.audioChunks = [];
                      thisVue.mediaRecorder.addEventListener("dataavailable", event => {                         
                          thisVue.audioChunks.push(event.data);
                      });

                      thisVue.mediaRecorder.addEventListener("stop", () => {
                          thisVue.stopRecordMessage();
                      });

                  }).catch(function (err) {
                      swal({
                          title: thisVue.$localizer("erro"),
                          text: thisVue.$localizer("audiodriver"),
                          icon: "error",
                      });
                  });
          });
      },
      startTimer() {
          var thisVue = this;
          this.timerInterval = setInterval(function () {
              thisVue.pulse = !thisVue.pulse;
              if (thisVue.timePassed == thisVue.timeLimit) {
                  thisVue.mediaRecorder.stop();
                  return;
              }
              thisVue.timePassed += 1
          }, 1000);
      },
      stopRecordMessage: function () {
          var thisVue = this;
          clearInterval(thisVue.timerInterval);
          thisVue.timerInterval = null;
          thisVue.timePassed = 0;
          thisVue.recording = false;
          const audioBlob = new Blob(thisVue.audioChunks);
          thisVue.audioUrl = URL.createObjectURL(audioBlob);
          var reader = new FileReader();
          reader.readAsDataURL(audioBlob);
          reader.onloadend = function () {
              if (thisVue.isQuestion) thisVue.dataQuestion = reader.result; else thisVue.dataAnswer = reader.result;
          }
          
      },
      cancelAudioCard: function () {
          this.mediaRecorder.stop();
      },
      saveCard: function () {
          //object card 
          let objectCard = new Object();
          objectCard.IdDeck = this.deckCard;
          objectCard.DataQuestion = this.dataQuestion;
          objectCard.DataAnswer = this.dataAnswer;
          objectCard.IdTypeQuestion = (this.typeQuestion == "AUDIO") ? 3 : (this.typeQuestion == "IMG") ? 2 : 1;
          objectCard.IdTypeAnswer = (this.typeAnswer == "AUDIO") ? 3 : (this.typeAnswer == "IMG") ? 2 : 1;
          objectCard.DateShow = moment().format("YYYY-MM-DD HH:mm:ss");
          objectCard.Order = 1;
          objectCard.IdClassification = 1;
          api.saveCard(objectCard).then(c => {
              if (c.isSuccess) {
                  setTimeout(() => { window.location.href = `/#/DeckCards` }, 2000);
                  app.$notyf.success(app.$localizer("cardsaved"));
              }
              else {
                  app.$notyf.error(app.$localizer("erro"));
              }
          });
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
    .optionsAttachment {
        position: absolute;
        display: none;
        cursor: pointer;
    }
    .attachmentimg {
        width: 200px;
    }
    .recording {
        color: red;
    }
    .pulse {
        font-size: 15px;
        border-radius: 100px;
        border: 1px solid red;
        padding: 2px;
    }
    .input-group-text.edit {
        padding: 0;
    }
    .saveCard {
        display: flex;
        justify-content: flex-end;
    }
    .btn-primary {
        margin-right: 2px;
    }
    .btn-primary a {
        color: white;
        text-decoration: none;
    }
</style>