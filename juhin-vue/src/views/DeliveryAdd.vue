<template>
  
  <div>
      <form @submit.prevent="handleSubmit">
          <div class="add-form-header">
              <img src="../assets/images/vendorIcon.png" alt="">
              <div>
                <h2>{{vendorName}}</h2>
                <p>Numer zamówienia: {{" " +orderNo}}</p>
              </div>
            <div>
            </div> 
          </div>

           <div v-if="error" class="error">
                <p>{{error}}</p>
            </div>
          
          
          <label>Wprowadź datę dostawy</label>
          <input type="date" v-model="eta" required>
          <label>Dodaj komentarz (opcjonalnie)</label>
          <textarea class="comment" v-model="comment"></textarea>
          <label>Wybierz przewoźnika</label>
          
          <select v-model="selectedForwarderId" required>
              <option v-for="forwarder in forwarders" :key="forwarder.forwarderId" :value="forwarder.forwarderId">{{forwarder.name}}</option>
          </select>
          <div class="submit">
            <button>Dalej</button>
          </div>
      </form>
  </div>
  
</template>

<script>
import moment from 'moment'
import { ref } from '@vue/reactivity'
import getForwarders from '../composables/getForwarders.js'
import addDelivery from '../composables/addDelivery.js'
import { onMounted } from '@vue/runtime-core'
import urlHolder from '../composables/urlHolder.js'

export default {
    props:['userToken', 'vId','vendorName', 'user', 'orderNo', 'orderId'],
    setup(props){
        const mainUrl = urlHolder
        //const poNumber = ref('')
        const eta = ref(moment().format("YYYY-MM-DD"))
        const comment = ref('')
        const selectedForwarderId = ref('')
        
        const {loadForwarders, error, forwarders} = getForwarders(mainUrl, props.userToken)
        const {addNewDelivery, error:deliveryError, responseErrors} = addDelivery(mainUrl, props.userToken)
        
        onMounted(()=>{
            loadForwarders(1,100)
        })
        
            const handleSubmit = async () =>{
                let createdAt = moment().toISOString()
                responseErrors.value = null
                error.value = null

            const deliveryData = {
                createdAt: createdAt,
                etaDate: eta.value,
                deliveryDate: eta.value,
                rating: 0,
                comment: comment.value,
                forwarderId: selectedForwarderId.value,
                statusId: 1
            }
            await addNewDelivery(deliveryData, props.orderId )
            if(!responseErrors.value && !error.value){
                console.log('NR PO: ' + props.orderId)
                // router.push({name:'Created'})
            }

            }

        return{handleSubmit, eta, comment, forwarders, error, deliveryError, responseErrors, selectedForwarderId}
    }

}
</script>

<style>
form .comment{
    height: 140px;
    resize: none;
}
</style>