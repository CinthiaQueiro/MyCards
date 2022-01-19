const websiteUrl = location.origin + location.pathname.replace(/\/$/, '');

const apiUrl = websiteUrl + '/../account';

/* api client ================================================================*/
const api = {
    ajax(options) {
        return new Promise((resolve, reject) => {
            $.ajax(options).done(resolve).fail(reject);
        });
    },
    async saveUser(user) {
        console.log('endereço ', `${apiUrl}/SaveUser`);
        try {
            return await this.ajax({
                type: "POST",
                url: `${apiUrl}/SaveUser`,
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(user)
            });
        } catch {
            return null;
        }
    }
};
