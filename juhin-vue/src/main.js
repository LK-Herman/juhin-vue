import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './assets/main.css'
import './assets/tables.css'
import axios from 'axios'
import urlHolder from'./composables/urlHolder.js'

createApp(App).use(router).mount('#app')
