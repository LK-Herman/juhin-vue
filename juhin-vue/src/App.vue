<template>
<div class="main-container">
    <div class="nav-container">
        <Navbar :isLogged="isLogged" :email="userEmail" @logoutEvent="handleLogout"/>
    </div>
    <button @click="handleClick">Get User by HttpContext.User</button>
    <div :class="{'sub-container':isLogged}">
        <div v-if="isLogged">
            <MenuBar />
        </div>
        
        <div class="body-container">
            <router-view @loginEvent="handleLogin" :userToken="userToken"  />
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
import getCurrentUser from './composables/getCurrentUser.js'
import { onUpdated } from '@vue/runtime-core'

export default {
    components: { MenuBar, Navbar, Endbar},
    
    setup(prop, emit) {
        const isLogged = ref(false)
        const mainUrl = urlHolder
        const userEmail = ref('')
        const router = useRouter()
        const userToken = ref('')
        const {getUser, error, user} = getCurrentUser(mainUrl)
        
        const handleLogin = (email, token) =>{
            isLogged.value = true
            userEmail.value = email
            userToken.value = token
            router.push({name:"Upcomming"})
        }
        const handleLogout = () =>{
            isLogged.value = false
            router.push({name:'Main'})
        }
        onUpdated(()=>{
            getUser()
            console.log(user.value)
        })
        const handleClick = ()=>{
            getUser()
            console.log(user.value)
        }

       
    return { isLogged, mainUrl, handleLogin, handleLogout, userEmail, userToken, error, getUser, handleClick}
  }
}
</script>


<style>

#nav a.router-link-exact-active {
  color: #42b983;
}
</style>
