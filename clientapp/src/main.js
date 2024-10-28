import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import vSelect from "vue-select";
import "vue-select/dist/vue-select.css";
import "bootstrap/dist/css/bootstrap.min.css"
import "bootstrap"
createApp(App).use(router).component("v-select", vSelect).mount('#app')
