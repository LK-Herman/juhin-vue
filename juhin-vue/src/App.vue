<template>
<div class="main-container">
    <div class="nav-container">
        <Navbar :isLogged="isLogged" :email="userEmail" />
    </div>
    <div :class="{'sub-container':isLogged}">
        <div v-if="isLogged">
            <MenuBar />
        </div>
        
        <div class="body-container">
            <router-view @loginEvent="handleLogin" />
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
import { useRouter } from 'vue-router'

export default {
    components: { MenuBar, Navbar, Endbar},
    
    setup(prop, emit) {
        const isLogged = ref(false)
        const mainUrl = urlHolder
        const userEmail = ref('')
        const router = useRouter()
        
        const handleLogin = () =>{
            isLogged.value = true
            userEmail.value = 'user@email.com'
            router.push({name:"Upcomming"})
        }

       
    return { isLogged, mainUrl, handleLogin, userEmail}
  }
}
</script>


<style>

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
