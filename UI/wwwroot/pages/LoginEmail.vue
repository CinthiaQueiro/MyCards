<template lang="html">
    <div class="container">
        <div class="input-group mb-3">
            <div class="input-group-prepend">
                <span class="input-group-text" id="basic-addon1">@</span>
            </div>
            <input type="text" v-model="email" :class="[!emailValid ? 'emailInValid' : '', 'form-control']" placeholder="E-mail" aria-label="E-mail" aria-describedby="basic-addon1">
        </div>
        <div class="buttons">
            <button class="btn btn-primary" v-show="emailValid" @click="login">Logar</button>
            <button class="btn btn-primary"><router-link to="/login">Voltar</router-link></button>
        </div>
    </div>
</template>

<script>
    module.exports = {
        data() {
            return {
                email: ""
            };
        },
        computed: {
            emailValid() {
                if (this.email == '') return false;
                if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(this.email)) {
                    return true;
                }
                return false;
            }
        },
        methods: {
            login() {
                var user = { Name: this.email.split("@")[0], Email: this.email };
                this.$store.myUser = user;
                this.$store.loadUser = true;
                var thisVue = this;
                api.saveLogin(this.$store.myUser).then(s => {
                    if (s.isSuccess) {
                        thisVue.$store.myUser = s.data;
                        thisVue.$router.push("/deckCards");
                    }
                });
            }
        }
    }
</script>

<style lang="css" scoped>
    .btn-primary a {
        color: white;
        text-decoration: none;
    }
    .btn {
        margin-right: 2px;
    }
    .buttons {
        display: flex;
        justify-content:flex-end;
    }
    .emailInValid {
        border: 1px solid red;
    }
</style>
