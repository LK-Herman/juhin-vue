<template>
  <div>
      <form id="login-form" @submit.prevent="handleSubmit">
          <h4>JUHIN WAREHOUSE MANAGEMENT</h4>
          <h2>Zaloguj się</h2>

          <label >Adres Email</label>
          <input v-model="email" type="email" required placeholder="adres@email.pl">
          <label for="">Hasło</label>
          <input v-model="password" type="password" required placeholder="mojeH@slo123">
          <div class="login-links">
              <p>Zarejestruj się</p>
              <p>Nie pamiętasz hasła?</p>
          </div>
          <button id="login-btn">Zaloguj</button>
          <div v-if="error">
              <div class="error-msg">{{error}}</div>
          </div>
      </form>
      
  </div>
</template>

<script>
import { ref } from '@vue/reactivity'
import { useRouter } from 'vue-router'
import urlHolder from '../composables/urlHolder.js'
import loginUser from '../composables/loginUser.js'


export default {
    props: [],
    setup(props, context){
        const mainUrl = urlHolder
        const router = useRouter()
        const email = ref('')
        const password =ref('')
        
        const {login, loginData, error, token} = loginUser(mainUrl)
        
        const handleSubmit = async () =>{
            await login(email.value, password.value)
            
            if(!error.value){
                console.log('User '+ email.value + ' logged in succesfully')
                context.emit('loginEvent', email.value, token.value)
                
            }
        } 
        
        return {email, password, handleSubmit, token, loginData, error}
    }
}
</script>

<style>

#login-form{
    display: flex;
    flex-flow: column;
    background-image: linear-gradient(#282828, var(--back-grey), var(--back-grey2));
}
#login-form .error-msg{
    color: var(--warning);
    font-size: 12px;
    text-align: center;
}
#login-form h4{
    font-family: 'Amaranth', sans-serif;
    font-size: 0.8em;
    font-weight: 300;
    align-self: center;
    margin:5px 0 5px 0;
}
#login-form h2{
    font-weight: 400;
    align-self: center;
    margin:15px 0 25px 0;
}

#login-form .login-links{
    margin: 10px 0;
    display: flex;
    flex-flow: row;
    justify-content: space-between;
}
#login-form #login-btn{
    margin: 20px 5px;
    width: 40%;
    align-self: center;
    box-shadow: 2px 2px 4px rgba(10,10,10, 0.5);
}
#login-form #login-btn:hover{
    box-shadow: 0px 0px 0px rgba(10, 10, 10, 0.5);
    background-color: #6D8CDC;
}
</style>