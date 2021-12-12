<template>
  
  <div v-if="!error">
        
        <div class="delivery-details-container">
            <div class="item-v">
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                    <DeliveryDetailsVendor :userToken='userToken' :vendorId='order.vendorId' />
                </div>
            </div>
            
            <div class="item-o">
                <h4>Nr zamówienia:</h4> 
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                        <p>{{order.orderNumber}}</p>
                </div>
            </div>

            <div class="item-po">
                <h4>Utworzone:</h4> 
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                    <p>{{dateCreated}}</p>
                </div>
            </div>
            
            <div class="item-e">
                <h4>ETA:</h4>
                <p>{{etaDate}}</p>
            </div>
            <div class="item-dd">
                <h4>Data dostawy:</h4>
                <p>{{dateDelivered}}</p>
            </div>
                    
            
            <div class="item-s">
                <h2>W DRODZE</h2>
            </div>
            
            <div class="item-p">
                <h4>Lista towarów:</h4>    
            </div>

            <div class="item-pl1">
                <p id="opis-tabeli">L.P.</p>
                <div v-for="item in delivery.packedItems" :key="item.partNumber">
                    <p>{{counter++}}.</p>
                </div>
            </div>

            <div class="item-pl2">
                <p id="opis-tabeli">NR TOWARU</p>
                <div v-for="item in delivery.packedItems" :key="item.partNumber">
                    <p>{{item.partNumber}}</p>
                </div>
            </div>
            
            <div class="item-pl3">
                <p id="opis-tabeli">ILOŚĆ</p>
                <div v-for="item in delivery.packedItems" :key="item.partNumber">
                    <p>{{item.quantity}} {{item.unitMeasure}}</p>
                </div>
            </div>
            <div class="item-c">
                <h4>Komentarz:</h4> 
                <p>{{delivery.comment}}</p>
            </div>

        </div>         
  </div>
</template>

<script>
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, ref } from '@vue/runtime-core'
import DeliveryDetailsVendor from './DeliveryDetailsVendor.vue'
import moment from 'moment'

export default {
    props: ['userToken', 'id'],
    components :{DeliveryDetailsVendor},
    setup(props){
        let counter = 1
        const mainUrl = urlHolder
        
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, props.userToken)
        
        const etaDate = ref('')
        const dateCreated = ref('')
        const dateDelivered = ref('')

        onMounted (()=>{
            loadDetails(props.id)
            counter = 0
            etaDate.value = moment(delivery.value.etaDate).format('DD-MM-YYYY hh:mm')
            dateCreated.value = moment(delivery.value.createdAt).format('DD-MM-YYYY hh:mm')
            dateDelivered.value = moment(delivery.value.deliveryDate).format('DD-MM-YYYY hh:mm')
            
        })
        return{delivery, error, counter, etaDate, dateCreated, dateDelivered}
    }

}
</script>

<style>


.delivery-details-container .item-o {
  grid-area: order;
}
.delivery-details-container .item-pl1 {
  grid-area: counter;
  text-align: right;
  padding: 10px 10px 10px 0;
  
  background-color: var(--back-dark);
}
.delivery-details-container .item-pl2 {
  grid-area: items;
  background-color: var(--back-dark);
  padding: 10px 0;
}
.delivery-details-container .item-pl3 {
  grid-area: qty;
  background-color: var(--back-dark);
  padding: 10px 0;
}
.delivery-details-container .item-dd {
  grid-area: deliver;
}
.delivery-details-container .item-po {
  grid-area: po-date;
}
.delivery-details-container .item-e {
  grid-area: eta;
}
.delivery-details-container .item-v {
  grid-area: vendor;
  margin: 0  0 15px 0;
}
.delivery-details-container .item-p {
  grid-area: plist;
  margin: 35px 0 10px 0;
}
.delivery-details-container .item-c {
  grid-area: comment;
  margin-top: 25px;
  padding: 10px 0;
}
.delivery-details-container .item-s {
  grid-area: status;
  display: flex;
  align-self: flex-start;
  justify-content: center;
  background-color: var(--back-grey);
  padding: 5px;

}
.delivery-details-container #opis-tabeli{
    font-size: 0.8em;
    margin-bottom: 8px;    
}
.delivery-details-container {
    
    
    
    
    margin: 15px 0;
    max-width: 600px;
    display: grid;
    grid-template-columns: 150px 150px 150px 150px;
    grid-template-rows: auto;
    grid-template-areas: 
    "vendor vendor vendor status"
    "order po-date eta deliver"
    "plist plist plist plist"
    "counter items qty qty"
    "comment comment comment comment";
}

.delivery-details-container .head{
    font-size: 1.1em;
    font-weight: 600;
}
</style>