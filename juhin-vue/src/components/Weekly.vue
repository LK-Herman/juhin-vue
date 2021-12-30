<template>
  <div v-for="delivery in upcomingDeliveries" :key="delivery.deliveryId">
      <div class="weekly">
        <DeliverySmall :userToken="userToken" :id="delivery.deliveryId" />
      </div>
  </div>
   
</template>

<script>
import { onMounted, ref, watch } from '@vue/runtime-core'
import DeliverySmall from '../components/DeliverySmall.vue'
import getUpcoming from '../composables/getUpcoming.js'
import urlHolder from '../composables/urlHolder.js'

export default {
    props: ['userToken', 'user', 'date'],
    components: { DeliverySmall },
    setup(props){
        const mainUrl = urlHolder
       
        const {loadUpcoming, error, upcomingDeliveries} = getUpcoming(mainUrl, props.userToken)
        
    onMounted(()=>{
        loadUpcoming(props.date)
    })
  
        return {error, upcomingDeliveries}
    }

}
</script>

<style>
.weekly{
    margin: 20px 0;
}
</style>