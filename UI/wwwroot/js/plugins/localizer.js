Vue.prototype.$localizer = function (key) {
  return store.localizerStrings[key] || key;
};
Vue.prototype.$localizer.loadFile = function (lang) {
  $.get(`../lang/${lang}.json`).done(function (strings) {
    store.localizerStrings = strings;
  }).fail(function () {
    store.localizerStrings = {};
  });
};
