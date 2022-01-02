<template>
<router-link :to="{name:'DeliveryDetails', params:{id:id}}">
    <div v-if="!error" class="small-container" :class="{delayed:delivery.isDelayed==true, delivered:delivery.statusId==3 }">
        <div class="item-order">
            <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                <p>PO: {{order.orderNumber}}</p>    
            </div>
        </div>        
        <div class="item-eta">
            <p>{{delivery.etaD}} / {{delivery.etaT}}</p>
        </div>
        <div class="item-sped">
            
            <p>{{delivery.forwarderName}}</p>
        </div>
        <div class="item-vendor">
            <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                <p>{{order.vendorName}}</p>       
            </div>
        </div>
        <div class="item-pallets" :class="{prio : delivery.isPriority}">
            <p>{{delivery.oripal}}</p>
            <p>{{delivery.eurpal}}</p>
        </div>
    </div>
</router-link>
</template>

<script>
import urlHolder from '../composables/urlHolder.js'
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import { onMounted } from '@vue/runtime-core'

export default {
    props: ['userToken', 'id'],
    setup(props){
        const mainUrl = urlHolder
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, props.userToken)

        onMounted (()=>{
            loadDetails(props.id, true)
           
        })

        return {delivery, error}
    }
}
</script>





<style>
.small-container{
    display: grid;
    max-width: 380px;
    min-width: 380px;
    grid-template-columns: 60px 60px 60px 80px 60px 60px;
    grid-template-rows: auto;
    grid-template-areas: 
    "eta eta eta order pallets pallets"
    "vendor vendor vendor vendor pallets pallets"
    "sped sped sped sped pallets pallets";
    padding: 10px 14px;
    font-size: 16px;
    box-shadow: 2px 2px 3px rgba(10,10,10,0.6);
    background-color: var(--dark-blue);
    background-image: linear-gradient(to right, var(--dark-blue),var(--dark-blue),#2f3d61);
}
.delivered{
    background-color: var(--green);
    background-image: linear-gradient(to right, var(--green),var(--green),#3a5215);
}

.delayed{
    background-color: var(--delay);
    background-image: linear-gradient(to right, var(--delay),var(--delay),#612525);
}
.small-container .item-order {
    grid-area: order;
}
.small-container .item-eta {
    grid-area: eta;
}
.small-container .item-sped {
    grid-area: sped;
    /* font-size: 12px; */
}
.small-container .item-vendor {
    grid-area: vendor;
    font-size: 18px;
    font-weight: 600;
    padding: 5px 5px 5px 0px;
    align-self: center;
}
.small-container .item-pallets {
    grid-area: pallets;
    justify-self: end;
    align-self: center;
    text-align: center;
    font-size: 24px;
    font-weight: 500;
    background-color: #5c5c5c;
    /* background-image: linear-gradient(to bottom right, #5a5a5a, #333333);  */
    padding:  10px 8px;
    margin: 6px;
    box-shadow: 2px 2px 3px rgba(20,20,20,0.7) inset;
    border-radius: 50%;
    width: 66px;
}
.small-container .prio{
    background-color: var(--orange);
    /* background-image: linear-gradient(to bottom right, var(--orange), #834100);  */
}
</style>