<template>
  <button @click="handleBack"><span class="material-icons">keyboard_backspace</span> </button>
  <div v-if="!error">
        
        <div class="delivery-details-container">
            <div class="item-v">
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                    <div class="item-v">
                        <h1>{{order.vendorName}}</h1>
                    </div>
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
                    <p>{{delivery.createdAt}}</p>
                </div>
            </div>
            
            <div class="item-e">
                <h4>ETA:</h4>
                <p>{{delivery.etaD}} {{delivery.etaT}}</p>
            </div>
            <div class="item-dd">
                <h4>Data dostawy:</h4>
                <p>{{delivery.deliveryDate}}</p>
            </div>
                    
            
            <div class="item-s">
                <h3>{{delivery.status}}</h3>
            </div>
            
            <div class="item-p">
                <h4>Lista towarów: </h4>    
            </div>
             <!-- <span v-if="delivery.packedItems.length == 0">brak</span> -->
            <div class="item-pl1">
                <p id="opis-tabeli">L.P.</p>
                <div v-for="item in delivery.packedItems" :key="item.partNumber">
                    <p>{{counter++}}. {{item.partNumber.toUpperCase()}}</p>
                </div>
            </div>

            <div class="item-pl2">
                <p id="opis-tabeli">OPIS</p>
                <div v-for="item in delivery.packedItems" :key="item.partNumber">
                    <p>{{item.description.toUpperCase()}}</p>
                </div>
            </div>
            
            <div class="item-pl3">
                <p id="opis-tabeli">ILOŚĆ</p>
                <div v-for="item in delivery.packedItems" :key="item.partNumber">
                    <p>{{item.quantity}} {{item.unitMeasure.toUpperCase()}}</p>
                </div>
            </div>
            <div class="item-c">
                <h4>Komentarz:</h4> 
                <p>{{delivery.comment}}</p>
            </div>
            <div class="item-btn">
                <button class="edit-btn">Edytuj</button>
                <button class="sub-btn">Subskrybuj</button>
                <button class="delete-btn">Usuń</button>
            </div>

        </div>         
  </div>
</template>

<script>
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, ref } from '@vue/runtime-core'
import {useRouter} from 'vue-router'

export default {
    props: ['userToken','user', 'id'],
    
    setup(props){
        let counter = 1
        const mainUrl = urlHolder
        const router = useRouter()
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, props.userToken)

        onMounted (()=>{
            counter = 1
            loadDetails(props.id)
        })
        const handleBack = ()=>{
            router.back()
        }
        return{delivery, error, counter, handleBack}
    }

}
</script>

<style>

.delivery-details-container {
    margin: 15px 0;
    max-width: 620px;
    display: grid;
    grid-template-columns: 190px 140px 140px 150px;
    grid-template-rows: auto;
    grid-template-areas: 
    "vendor vendor vendor status"
    "order po-date eta deliver"
    "plist plist plist plist"
    "counter items items qty"
    "comment comment comment comment"
    "buttons buttons buttons buttons";
    padding: 0px 0px;
    
}

.delivery-details-container .item-pl1 {
  grid-area: counter;
  padding: 10px 0px 1px 0px;
  background-color: var(--back-dark);
  font-size: 12px;
}
.delivery-details-container .item-pl2 {
    grid-area: items;
  background-color: var(--back-dark);
  padding: 10px 0px 1px 0px;
  font-size: 12px;
}
.delivery-details-container .item-pl3 {
    grid-area: qty;
    background-color: var(--back-dark);
    padding: 10px 0px 1px 2px;
    text-align: right;
    font-size: 12px;
}
.delivery-details-container .item-dd {
    grid-area: deliver;
    margin: 0  20px 0 20px;
}
.delivery-details-container .item-po {
    grid-area: po-date;
    margin: 0  20px 0 20px;
}
.delivery-details-container .item-e {
    grid-area: eta;
    margin: 0  20px 0 20px;
}
.delivery-details-container .item-o {
    grid-area: order;
    margin: 0  0 0 30px;
}
.delivery-details-container .item-p {
    grid-area: plist;
  margin: 35px 0 10px 30px;
}
.delivery-details-container .item-c {
    grid-area: comment;
  margin: 25px 20px 25px 30px;
  padding: 10px 0;
}
.delivery-details-container .item-v {
    grid-area: vendor;
    margin: 0  0 0px 0px;
    background-color: rgb(81, 82, 80);
    display: flex;
    align-items: center;
    justify-content: center;
    height: 60px;
    
}
.delivery-details-container .item-s {
  grid-area: status;
  display: flex;
  align-self: flex-start;
  align-items: center;
  justify-content: center;
  background-color: #5ca101;
  height: 60px;
  margin-bottom: 25px;

}
.delivery-details-container .item-btn {
  grid-area: buttons;
  display: flex;
  align-self: flex-start;
  align-items: center;
  justify-content: space-between;
  /* background-color:rgb(74, 90, 63); */
  padding: 10px;
  margin-bottom: 25px;
}
.delivery-details-container .item-btn button {
    box-shadow: 2px 2px 3px rgba(20,20,20,0.5);
    width: 180px;
}

.delivery-details-container #opis-tabeli{
    font-size: 0.8em;
    margin-bottom: 8px;    
    padding: 0px 30px 0px 30px;
    
}
.delivery-details-container .item-pl1 div, .item-pl2 div, .item-pl3 div{
 background-color: rgb(56, 56, 56);
 margin: 2px 0px;
 padding: 5px 30px 5px 30px;
}

.delivery-details-container .head{
    font-size: 1.1em;
    font-weight: 600;
}
</style>