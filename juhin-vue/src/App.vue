<template>
<div class="main-container">
    <div class="nav-container">
        <Navbar :mainUrl="mainUrl" />
    </div>
    <div class="sub-container">
        <div v-if="isLogged">
            <MenuBar />
        </div>
        <div class="body-container">
            <router-view/>
        </div>
    </div>
</div>
<div class="downbar">
    <Endbar />
</div>
  
</template>
<script>
import MenuBar from './components/MenuBar.vue'
import Navbar from './components/Navbar.vue'
import Endbar from './components/Endbar.vue'
import { ref } from '@vue/reactivity'
import urlHolder from './composables/urlHolder.js'

export default {
    components: { MenuBar, Navbar, Endbar},
    setup() {
        const isLogged = ref(true)
        const mainUrl = urlHolder
   
    return { isLogged, mainUrl}
  }
}
</script>


<style>

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
