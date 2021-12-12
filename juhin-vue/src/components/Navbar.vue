<template>
<div class="navbar">
    <div class="logo">
        JUHIN
    </div>
    <div class="logobar-out">
        <div class="logobar-in">
            <div>
                <p id="sublogo"> WAREHOUSE MANAGEMENT </p>
            </div>
            <div class="logobar-links">
                <p v-if="isLogged" class="links-item" @click="handleClick">Get User by HttpContext.User</p>
                <p v-if="isLogged" class="links-item" id="email-address">{{userEmail}}</p>
                <p v-if="!isLogged" class="links-item">Zarejestruj się</p>
                <a v-if="isLogged" class="links-item" @click="handleNavLogout">Wyloguj</a>
                <router-link v-else :to="{name:'Login'}" class="links-item">Zaloguj się</router-link>
            </div>
        </div>
    </div>
        
</div>
</template>

<script>
import logoutUser from '../composables/logoutUser.js'
import { useRouter } from 'vue-router'
import getCurrentUser from '../composables/getCurrentUser.js'
import urlHolder from '../composables/urlHolder.js'
export default {
    props:['isLogged', 'userEmail'],
    emits:["logout-event","login-event"],
    setup(props, context){
        const router = useRouter()
        const mainUrl = urlHolder
        const {logout, error, logoutData} = logoutUser()
        const {getUser, user} = getCurrentUser(mainUrl)
        
        const handleNavLogout = () =>{
            context.emit('logout-event')
            logout()
            router.push({name:'Login'})
        }
         const handleClick = () =>{
            getUser()
        }

        return {  handleNavLogout, handleClick}
    }

}
</script>

<style>

.navbar{
    display: grid;
    grid-template-rows: 60px 60px;
 
    column-gap: 0;

    
}
.navbar .logo
{
    font-family: 'Amaranth', sans-serif;
    font-size: 61px;
    z-index: 10;
    padding: 0px 0 0 15px;
    
}
.navbar .logobar-out{
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    background-color: #2865d6;
     
}
.navbar .logobar-in{
    display: flex;
    align-items: center;
    margin: 0px auto;
    width: 100%;
    padding: 10px 0px;
    
    margin: auto;
    
}
.navbar .logobar-in .logobar-links{
    margin-left: auto;
    display: flex;
    align-items: center;
    padding-right: 36px;
}
.navbar .logobar-in .logobar-links .links-item{
    padding-left: 36px;
    font-size: 14px;
    font-weight: 500;
    cursor: pointer;
    text-shadow: 1px 1px 2px rgb(0, 0, 0);
}
.navbar .logobar-in .logobar-links .links-item#email-address{
    cursor: default;
}
.navbar .logobar-in .logobar-links .links-item#email-address:hover{
    cursor: default;
    color: var(--primary);
}
.navbar .logobar-in .logobar-links .links-item:hover{
    color: rgb(255, 192, 56);
}
#sublogo{
    font-family: 'Amaranth', sans-serif;
    font-size: 16px;
    padding-left: 36px;
}
</style>