<template>
  <div>
      <form @submit.prevent="handleSubmit" class="forwarder-form">
            <div id="forwarder-addform-header">
                <img src="../assets/images/forwarderIcon.png" alt="">
                <h2>Dodaj przewoźnika</h2>
            </div>
            <div v-if="error" class="error">
                <p>{{error}}</p>
            </div>
            <label>Nazwa pełna</label>
            <input type="text" v-model="formName">
            
            <label>Adres email</label>
            <input type="email" v-model="formEmail">
            
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
        

        const {addNewForwarder, error, createdId} = addForwarder(mainUrl, props.userToken)

        const handleSubmit = async () =>{
                error.value = null
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

        return {handleSubmit, formName, formEmail, formPhoneNumber, error}
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
    background-color: #7e39a7;
    padding: 15px;
    margin: 0 0 15px 0  ;
}
</style>