<template>
  
  <div class="delivery-details" v-if="!error">
    <h2>Szczegóły dostawy</h2>
    <br>
    <p>ID:              {{delivery.deliveryId}}</p>
    <p>CREATED:         {{delivery.createdAt}}</p>
    <p>ETA:             {{delivery.etaDate}}</p>
    <p>DELIVERY DATE:   {{delivery.deliveryDate}}</p>
    <p>COMMENT:         {{delivery.comment}}</p> 
    <br>
    <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
        <p>ORDER NUMBER:{{order.orderNumber}}</p>
        <DeliveryDetailsVendor :userToken='userToken' :vendorId='order.vendorId' />
    </div>
    <p>PACKING LIST:</p>    
    <div v-for="item in delivery.packedItems" :key="item.partNumber">
        <p>{{counter++}}. {{item.partNumber}} {{item.quantity}} {{item.unitMeasure}}</p>
   </div>
  </div>
</template>

<script>
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, ref } from '@vue/runtime-core'
import DeliveryDetailsVendor from './DeliveryDetailsVendor.vue'


export default {
    props: ['userToken'],
    components :{DeliveryDetailsVendor},
    setup(props){
        let counter = 1
        const mainUrl = urlHolder
        const id = 'a0bc3e8b-f4de-454e-286a-08d996ff5716'
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, props.userToken)
        
        onMounted (()=>{
            loadDetails(id)
            counter = 0
            
        })
        return{delivery, error, counter}
    }

}
</script>

<style>
.delivery-details{
    display: block;
    width: 400px;
    height: inherit;
    padding: 25px;
    box-shadow: 4px 4px 5px rgba(10,10,10,0.4);
    background-color: rgb(22, 53, 93);
    margin: 15px 0;
    
}
</style>