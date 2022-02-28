const websiteUrl = location.origin + location.pathname.replace(/\/$/, '');

const apiUrlDecks = websiteUrl + '/../DeckCards';
const apiUrlCards = websiteUrl + '/../Cards';
const apiLogin = websiteUrl + '/../Account';

const auth = {
  redirectToLogin(returnUrl) {
    const redirectUri = `/admin/#${returnUrl}`;
    location.href = `${apiUrlDecks}/Login?ReturnUrl=${encodeURIComponent(redirectUri)}`;
  },
    redirectToLogout() {
        if (this.$store.hasContatoInAtendimento) {
            $.notify(this.$localizer("haAtendimentosEmAberto"), 'warn');
        } else {
            location.href = `${apiUrlDecks}/logout`;            
        }
    },

    changeLanguage(event) {
        this.$localizer.loadFile(event.target.value);
        store.language = event.target.value;
    }
};

/* api client ================================================================*/
const api = {
  ajax(options) {
    return new Promise((resolve, reject) => {
      $.ajax(options).done(resolve).fail(reject);
    });
  },
  async getDeckCards() {
    try {
      return await this.ajax({
        type: "GET",
        url: `${apiUrlDecks}/GetDeckCards`,
        dataType: "json"
      });
    } catch {
      return null;
    }
  },
  saveDeck(deck) {
    return this.ajax({
        type: "POST",
        url: `${apiUrlDecks}/SaveDeck`,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deck)
    });
  },
  editDeckCard(deck) {
    return this.ajax({
        type: "POST",
        url: `${apiUrlDecks}/EditDeckCard`,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deck)
    });
  },
  deleteDeckCard(deck) {
    return this.ajax({
        type: "POST",
        url: `${apiUrlDecks}/DeleteDeckCard`,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deck)
    });
  },
  async saveLogin(user) {
    try {
      return this.ajax({
        type: "POST",
        contentType: 'application/json',
        crossDomain: true,
        url: `${apiLogin}/SaveLogin`,
        dataType: "json",
        data: JSON.stringify(user)
      });
    } catch {
      return null;
    }
  },
    async getMyUser() {
    try {
        return await this.ajax({
            type: "GET",
            url: `${apiLogin}/GetMyUser`,
            dataType: "json"
        });
    } catch {
        return null;
    }
  },
    saveCard(card) {
        return this.ajax({
            type: "POST",
            url: `${apiUrlCards}/SaveCard`,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(card)
        });
    },
    async getCards(idDeckCards) {
        try {
            return await this.ajax({
                type: "GET",
                url: `${apiUrlCards}/GetCards/${idDeckCards}`,
                dataType: "json"
            });
        } catch {
            return null;
        }
    },
    updateCard(card) {
        return this.ajax({
            type: "POST",
            url: `${apiUrlCards}/UpdateCard`,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(card)
        });
    }
};
