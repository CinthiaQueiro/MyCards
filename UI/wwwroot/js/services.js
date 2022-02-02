const websiteUrl = location.origin + location.pathname.replace(/\/$/, '');

const apiUrl = websiteUrl + '/../DeckCards';

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
  }
};
