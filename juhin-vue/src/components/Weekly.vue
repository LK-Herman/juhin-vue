<template>
    <div class="pallet-sum" v-if="totalOri >= 0">
        <p>{{totalOri}} - Ilość palet oryginalnych</p>
        <p>{{totalEur}} - Ilość palet euro</p>
    </div>
    <div v-for="delivery in upcomingDeliveries" :key="delivery.deliveryId">
        <div class="weekly">
            <DeliverySmall :userToken="userToken" :id="delivery.deliveryId" @pallet-event="handlePalletEvent"/>
        </div>
        <!-- <p v-if="totalOri !== 0">{{totalOri}} / {{totalEur}}</p> -->
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
        const totalOri = ref(0)
        const totalEur = ref(0)
        const {loadUpcoming, error, upcomingDeliveries} = getUpcoming(mainUrl, props.userToken)
        
    onMounted(()=>{
        loadUpcoming(props.date)
    })
  
    const handlePalletEvent = (pallets)=>{
        if(pallets.totalOri !== NaN && pallets.totalEur !== NaN){
            totalOri.value = totalOri.value + pallets.oripal
            totalEur.value = totalEur.value + pallets.eurpal
        }
        // console.log(props.date + " ORI: " + pallets.oripal)
    }

        return {error, upcomingDeliveries, handlePalletEvent, totalOri, totalEur}
    }

}
</script>

<style>
.weekly{
    margin: 20px 0;
}
.pallet-sum{
    margin: 15px 0;
    padding: 10px;
}
</style>