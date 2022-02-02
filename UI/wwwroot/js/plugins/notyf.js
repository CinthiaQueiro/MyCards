// Create an instance of Notyf
const notyf = new Notyf({
  position: {
    x: 'right',
    y: 'top'
  },
  dismissible: true
});
Vue.prototype.$notyf = notyf;
