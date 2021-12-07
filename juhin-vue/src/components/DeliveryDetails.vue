<template>
  <h2>Szczegóły dostawy</h2>
  <div v-if="!error">
    <p>ID: {{delivery.deliveryId}}</p>
    <p>CREATED: {{delivery.createdAt}}</p>
    <p>ETA: {{delivery.etaDate}}</p>
    <p>DELIVERY DATE: {{delivery}}</p>
    <p>COMMENT: {{delivery.comment}}</p>
    <p>PO: {{delivery.purchaseOrders}}</p>
    <p>{{delivery}}</p>

  </div>
</template>

<script>
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted } from '@vue/runtime-core'
export default {
    props: ['userToken'],
    setup(prop){
        const mainUrl = urlHolder
        const id = 'a0bc3e8b-f4de-454e-286a-08d996ff5716'
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, prop.userToken)
        onMounted (()=>{
            loadDetails(id)
        })
        return{delivery, error}
    }

}
</script>

<style>

</style>