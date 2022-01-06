<template>
  <div>
      <form @submit.prevent="handleSubmit" class="forwarder-form">
            <div id="forwarder-addform-header">
                <img src="../assets/images/orderIcon.png" alt="">
                <h2>Dodaj zamówienie</h2>
            </div>
            <div v-if="error" class="error">
                <p>{{error}}</p>
            </div>
            <label>Nazwa pełna</label>
            <input type="text" v-model="formName">
            <div v-if="formError[0] != ''" class="error">
                <p>{{formError[0]}}</p>
            </div>
            
            <label>Adres email</label>
            <input type="email" v-model="formEmail" >
            <div v-if="formError[1] != ''" class="error">
                <p>{{formError[1]}}</p>
            </div>
            
            <label>Nr telefonu</label>
            <input type="tel" v-model="formPhoneNumber">
           
            <div class="submit">
                <button>Dodaj</button>
            </div>
      </form>
  </div>

  
</template>

<script>
import { ref } from '@vue/reactivity'
import addForwarder from '../composables/addForwarder.js'
import urlHolder from '../composables/urlHolder.js'
import { useRouter } from 'vue-router'
import { onUpdated, watch } from '@vue/runtime-core'
export default {
    props:['userToken', 'user'],
    setup(props){
        
        const router = useRouter()
        const mainUrl = urlHolder
        const formName = ref('')
        const formEmail = ref('')
        const formPhoneNumber = ref('')
        const formError = ref([])

        const {addNewForwarder, error, createdId} = addForwarder(mainUrl, props.userToken)

        const handleSubmit = async () =>{
                error.value = null
                formError.value[0] = ''
                formError.value[1] = ''
                if(formName.value.length < 3) formError.value[0] = "Nazwa jest za krótka (co najmniej 3 znaki)"
                if(!formEmail.value.includes('@') || !formEmail.value.includes('.') || formEmail.value.includes(' ')) formError.value[1] = "Adres email jest nieprawidłowy"
                if (formError.value[0] == '' && formError.value[1] == ''){
                    const forwarderData = {
                        Name : formName.value,
                        Email : formEmail.value,
                        PhoneNumber : formPhoneNumber.value,
                        Rating: 0
                    }
                    await addNewForwarder(forwarderData)
                    if(!error.value){
                        router.push({name:'Created'})
                    }
            }
        }

        return {handleSubmit, formName, formEmail, formPhoneNumber, formError, error}
    }

}
</script>
<style>
input[type='checkbox']{
    display: inline-block;
    width: 16px;
    position: relative;
    margin: 0 10px 0 0;
    top: 2px;
}

.forwarder-form h2{
    text-align: center;
}
.forwarder-form{
    max-width: 600px;
}
    .forwarder-form .error p{
        font-size: 1.2em;
        color: var(--warning);
        text-align: center;
    }

.forwarder-form #forwarder-addform-header{
    text-align: center;
    background-color: #00642a;
    padding: 15px;
    margin: 0 0 15px 0  ;
}
</style>