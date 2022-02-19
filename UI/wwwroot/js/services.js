const websiteUrl = location.origin + location.pathname.replace(/\/$/, '');

const apiUrl = websiteUrl + '/../DeckCards';
const apiLogin = websiteUrl + '/../Account';

const auth = {
  redirectToLogin(returnUrl) {
    const redirectUri = `/admin/#${returnUrl}`;
    location.href = `${apiUrl}/Login?ReturnUrl=${encodeURIComponent(redirectUri)}`;
  },
    redirectToLogout() {
        if (this.$store.hasContatoInAtendimento) {
            $.notify(this.$localizer("haAtendimentosEmAberto"), 'warn');
        } else {
            location.href = `${apiUrl}/logout`;            
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
        url: `${apiUrl}/GetDeckCards`,
        dataType: "json"
      });
    } catch {
      return null;
    }
  },
  saveDeck(deck) {
    return this.ajax({
        type: "POST",
        url: `${apiUrl}/SaveDeck`,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deck)
    });
  },
  editDeckCard(deck) {
    return this.ajax({
        type: "POST",
        url: `${apiUrl}/EditDeckCard`,
        dataType:"json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(deck)
    });
  },
  deleteDeckCard(deck) {
    return this.ajax({
        type: "POST",
        url: `${apiUrl}/DeleteDeckCard`,
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
  }
};
