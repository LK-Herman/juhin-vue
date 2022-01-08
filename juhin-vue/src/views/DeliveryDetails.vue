<template>
  <div v-if="!error">
        
        <div class="delivery-details-container">
            <div class="item-v">
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                    <div class="vendor-header" >
                        <img src="../assets/images/vendorIcon.png">
                        <h1 class="text-yellow">{{order.vendorName}}</h1>
                    </div>
                </div>
            </div>
            
            <div class="item-o">
                <h4>NR ZMÓWIENIA</h4> 
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                        <p>{{order.orderNumber}}</p>
                </div>
            </div>

            <div class="item-po">
                <h4>UTWORZONA</h4> 
                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                    <p>{{delivery.createdAt}}</p>
                </div>
            </div>
            
            <div class="item-e">
                <h4>ETA</h4>
                <p>{{delivery.etaD}} {{delivery.etaT}}</p>
            </div>
            <div class="item-dd">
                <h4>DATA DOSTAWY</h4>
                <p v-if="delivery.statusId == 3" class="text-yellow">{{delivery.deliveryDate}}</p>
                <p v-else>(brak)</p>
            </div>
                    
            
            <div class="item-s" :class="{statusDelivered:delivery.statusId==3, statusInTransit:delivery.statusId==2, statusCreated:delivery.statusId==1}">
                <h3>{{delivery.status}}</h3>
            </div>
            
            <div class="item-p">
                <h4>LISTA TOWARÓW: </h4>    

                <div v-if="!error">
                <div class="divTable blueTable">
                    <div class="divTableHeading">
                        <div class="divTableRow">
                            <div class="divTableHead firstCol center-text">POZYCJA</div>
                            <div class="divTableHead secondCol">NR TOWARU</div>
                            <div class="divTableHead thirdCol">OPIS TOWARU</div>
                            <div class="divTableHead fourthCol">ILOŚĆ</div>
                            <div class="divTableHead fifthCol center-text">JEDN.</div>
                        </div>
                    </div>
                    <div class="divTableBody">
                        <div class="divTableRow" v-for="item in delivery.packedItems" :key="item.itemId">
                            <div class="divTableCell firstCol center-text">{{item.counter}}</div>
                            <div class="divTableCell secondCol">{{ item.partNumber.toUpperCase() }}</div>
                            <div class="divTableCell thirdCol">{{ item.description.toUpperCase() }}</div>
                            <div class="divTableCell fourthCol">{{ item.quantity }}</div>
                            <div class="divTableCell fifthCol center-text">{{ item.unitMeasure.toUpperCase() }}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

            <div class="item-f">
                <h4>PRZWOŹNIK</h4> 
                <p>{{delivery.forwarderName}}</p>
            </div>
            <div class="item-prio">
                <h4>PRIORYTET</h4> 
                <p v-if="!delivery.isPriority">STANDARDOWA DOSTAWA</p>
                <p v-else class="text-yellow">PIERWSZEŃSTWO ROZŁADUNKU</p>
            </div>
            <div class="item-c">
                <h4>KOMENTARZ</h4> 
                <p>{{delivery.comment}}</p>
            </div>
            <div class="item-btn">
                <button @click="handleBack"> <span class="material-icons">keyboard_backspace</span> Powrót</button>
                <button class="edit-btn">Edytuj</button>
                <button class="sub-btn">Subskrybuj</button>
                <button v-if="delivery.statusId == 1" class="delete-btn" @click="handleDelete">Usuń</button>
            </div>
        </div>         
  </div>
    <div>
        <DeliveryDelete :userToken="userToken" :id="id" :orders="delivery.purchaseOrders"/>
    </div>
</template>

<script>
import getDeliveryDetails from '../composables/getDeliveryDetails.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, ref } from '@vue/runtime-core'
import {useRouter} from 'vue-router'
import DeliveryDelete from '../components/DeliveryDelete.vue'

export default {
    props: ['userToken','user', 'id'],
    components: {DeliveryDelete},
    setup(props){
        let counter = 1
        const mainUrl = urlHolder
        const router = useRouter()
        const {delivery, loadDetails, error} = getDeliveryDetails(mainUrl, props.userToken)
        // const deleteFlag = ref(false)
        onMounted (()=>{
            counter = 1
            loadDetails(props.id)
                .then(function(){
                    delivery.value.packedItems.forEach(item =>{
                        item['counter'] = counter++
                    })
                })
        })
        const handleBack = ()=>{
            router.back()
        }
        const handleDelete = ()=>{
            // deleteFlag.value = true
            var modal = document.getElementById("myModal");
            modal.style.display = "block" 
            
            window.onclick = function(event) {
            if (event.target == modal) {
            modal.style.display = "none";
            // deleteFlag.value = false
            }
        }
        }

        return{delivery, error, counter,handleDelete,  handleBack}
    }

}
</script>

<style>

.delivery-details-container {
    margin: 25px auto;
    max-width: 880px;
    display: grid;
    grid-template-columns: 220px 220px 220px 220px;
    grid-template-rows: auto;
    grid-template-areas: 
    "vendor vendor vendor status"
    "order po-date eta deliver"
    "plist plist plist plist"
    "counter items items qty"
    "forwarder forwarder prio prio"
    "comment comment comment comment"
    "buttons buttons buttons buttons";
    padding: 0px 0px;
    
}
.delivery-details-container .vendor-header{
    display: flex;
    align-items: center;
}
.delivery-details-container .vendor-header h1{
    padding-left: 24px;
}
.delivery-details-container .vendor-header img{
    padding-left: 6px;
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
    border-top: 3px solid var(--back-grey2) ;
    padding: 20px 0 ;
    grid-area: plist;
    margin: 25px 0 10px 30px;
}
.delivery-details-container .item-c {
    border-top: 3px solid var(--back-grey2);
    border-bottom: 3px solid var(--back-grey2) ;
    padding: 20px 0px;
    grid-area: comment;
    margin: 0px 0px 25px 30px;
    
}
.delivery-details-container .item-f {
    border-top: 3px solid var(--back-grey2);
    padding: 20px 0px;
    grid-area: forwarder;
    margin: 0px 0px 0px 30px;
}
.delivery-details-container .item-prio {
    border-top: 3px solid var(--back-grey2);
    padding: 20px 0px;
    grid-area: prio;
    align-self: flex-end;
    text-align: left;
}
.delivery-details-container .item-v {
    grid-area: vendor;
    
    margin: 0 0 25px 30px;
    
    /* background-color: rgb(81, 82, 80); */
    display: flex;
    align-items: center;
    justify-content: flex-start;
    
    border-top: 3px solid var(--back-grey2);
    border-bottom: 3px solid var(--back-grey2);
}
.delivery-details-container .item-s {
  grid-area: status;
  display: flex;
  align-self: flex-start;
  align-items: center;
  justify-content: center;
  background-color: var(--dark-blue);
  height: 60px;
  margin-bottom: 25px;
    border-top: 3px solid var(--back-grey2);
    border-bottom: 3px solid var(--back-grey2);
}
.delivery-details-container .item-btn {
  grid-area: buttons;
  display: flex;
  align-self: flex-start;
  align-items: center;
  justify-content: space-between;
  /* background-color:rgb(74, 90, 63); */
  padding: 10px 0 ;
  margin-left: 30px;
  margin-bottom: 25px;
}
.delivery-details-container .item-btn span{
    padding-right: 10px;
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

.delivery-details-container h4{
    font-size: 12px;
    font-weight: 500;
    padding-bottom: 6px ;
}
.delivery-details-container .head{
    font-size: 1.1em;
    font-weight: 600;
}
.delivery-details-container .statusDelivered{
    background-color: #5ca101;
}
.delivery-details-container .statusCreated{
    background-color: #5f5f5f;
}
.delivery-details-container .statusInTransit{
    background-color: var(--dark-blue);
}


</style>