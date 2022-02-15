<template lang="html">
    <div class="modal fade loadImg" id="modallCreateCards" tabindex="-1" role="dialog" aria-labelledby="modallCreateCards" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Create Deck</h5>
                    <button type="button" class="btn-close" @click="$emit('is-open', !isOpen)" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="input-group mb-3">
                        <span class="input-group-text" id="basic-addon1">Nome</span>
                        <input v-model="description" type="text" class="form-control" placeholder="Nome" aria-label="Nome" aria-describedby="basic-addon1">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" @click="$emit('is-open', !isOpen)" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" @click="$emit('is-open', !isOpen);saveDeck()">Save changes</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    module.exports = {
        props: ["isOpen"],
        data: function () {
            return {
                description: ""
            };
        },
        methods: {
            showLoading() {
                if (this.isOpen) $('#modallCreateCards').modal('show');
                else $('#modallCreateCards').modal('hide');
            },
            saveDeck() {
                var thisVue = this;
                let objectDeck = { description: this.description, User: { id: this.$store.myUser.id}};                
                api.saveDeck(objectDeck).then(d => {
                    thisVue.description = "";
                    console.log('retorno ', d);
                    thisVue.$emit("deck-saved", d);
                });
            }
        },
        watch: {
            isOpen: function () {
                this.showLoading();
            }
        },
        mounted() {
            this.showLoading();
        }
    }
</script>
<style lang="css" scoped>
    
</style>