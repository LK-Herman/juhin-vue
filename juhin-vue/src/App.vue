<template>
<div class="main-container">
    <div class="nav-container">
        <Navbar :isLogged="isLogged" :userEmail="userEmail" @logout-event="handleLogout"/>
    </div>
    
    <div :class="{'sub-container':isLogged}">
        <div v-if="isLogged">
            <MenuBar :userToken='userToken' />
        </div>
        <div class="body-container">
            <div v-if="!isLogged">
                <Login @login-event="handleLogin" />
            </div>
            <div v-else>
                <router-view  :userToken="userToken"  />
            </div>
        </div>
    </div>
</div>
    <br>
    <br>
    <br>
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
import Login from './views/Login.vue'

export default {
    components: { MenuBar, Navbar, Endbar, Login},
    emits:["login-event", "logout-event"],
    props: [],
    setup(props,context) {
        const isLogged = ref(false)
        const mainUrl = urlHolder
        const userEmail = ref('')
        const router = useRouter()
        const userToken = ref('')
        
        const handleLogin = (userCred) =>{
            isLogged.value = true
            userEmail.value = userCred.email
            userToken.value = userCred.token
            router.push({name:"Main"})
        }
        const handleLogout = () =>{
            isLogged.value = false
            router.push({name:'Login'})
        }
       
    return { isLogged, handleLogin, handleLogout, userEmail, userToken}
  }
}
</script>


<style>

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
