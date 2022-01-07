<template>

    <div v-if="!error">
       
        <div class="vendor-details-container">
            <div >
                    <h4>KOD DOSTAWCY</h4>
                    <h2>{{vendor.vendorCode}}</h2>
            </div>
            
            <div class="item-short">
                <h4>SKRÓT</h4>
                <h2>{{vendor.shortName}}</h2>
            </div>    
            <div class="item-head">
                <div class="vendor-header">
                    <img src="../assets/images/vendorIcon.png" alt="" />
                    <div>
                        <h4>PEŁNA NAZWA DOSTAWCY</h4>
                        <h2>{{vendor.name}}</h2>
                    </div>
                </div>
            </div>
            <div>
                <h4>ADRES</h4>
                <p>{{vendor.address}}</p>
                <p>{{vendor.country}}</p>
            </div>
            <div>
                <h4>ADRESS EMAIL</h4>
                <p>{{vendor.email}}</p>
            </div>
            <div>
                <h4>NUMER TELEFONU</h4>
                <p>{{vendor.phoneNumber}}</p>
            </div>

        </div>
        <div class="vendor-btns-container">
            <div class="btns-items">
                <p>Edytuj dane dostawcy, nazwę, adres,  dane kontaktowe itd.</p>
                <button>Edytuj dane dostawcy</button>
            </div>
            <div class="btns-items">
                <p>Wprowadź nowe zamówienie dla tego dostawcy </p>
                <button>Dodaj zamówienie</button>
            </div>
            <div class="btns-items">
                <p>Dodaj nowe materiały, które dostarcza {{vendor.name}} </p>
                <button @click="handleAddItem">Dodaj materiały</button>
                <!-- <router-link :to="{name:'ItemAdd', params:{vId:vId}}" :userToken="userToken" :user="user">Dodaj materiały</router-link> -->
            </div>
        </div>
    </div>
    


</template>

<script>

import urlHolder from '../composables/urlHolder.js'
import getVendorById from '../composables/getVendorById.js'
import { onMounted } from '@vue/runtime-core'
import {useRouter} from 'vue-router'

export default {
  components: {  },
  props:['userToken', 'user', 'vId'],
  setup(props) {
    const url = urlHolder
    const {loadVendor, error, vendor} = getVendorById(url, props.userToken)
    const router = useRouter()

    onMounted(async()=>{
        await loadVendor(props.vId)
    })

    const handleAddItem = () =>{
        
        router.push({name:'ItemAdd', params:{
            
                userToken:props.userToken, 
                user:props.user, 
                vend:JSON.stringify(vendor.value)
                }})
    }
    
    return {error, vendor, handleAddItem}
  }
}
</script>
<style >
.vendor-details-container{
    display: grid;
    grid-template-columns: 150px 340px 240px 200px;
    row-gap: 0px;
    
    grid-template-areas: 
    "head head head short";
}
.vendor-details-container .vendor-header{
    display: flex;
    align-items: flex-end   ;
}
.vendor-details-container div{
    padding: 0 15px;
}
.vendor-details-container .item-head{
    grid-area: head;
    margin-bottom: 25px ;
    border-bottom: solid #999;
    padding: 20px 0px 25px 25px;
    background-color: rgb(4, 53, 0);
    background-image: linear-gradient(#282828,#292929, #353535);
}
.vendor-details-container .item-head h2{
    color: #ffbb00;
    
}
.vendor-btns-container{
    margin: 80px 0;
    display: flex;
    justify-content: space-between;
}
.vendor-btns-container .btns-items{
    display: flex;
    flex-flow: column;
    max-width: 220px;
    background-color: #383838;
    align-items: stretch;
    justify-content: space-between;
    padding: 25px 25px;
    margin: 5px;
    text-align: center;
    box-shadow: 5px 5px 8px rgba(10,10,10,0.5);
}
.vendor-btns-container .btns-items button{
    margin: 15px 0 0 0;
    background-color: var(--dark-blue);
}
.vendor-btns-container .btns-items button:hover{
    background-color: #356d00;
}
.vendor-details-container .item-short{
    background-color: rgb(36, 0, 53);
    grid-area: short;
    margin-bottom: 25px ;
    border-bottom: solid #999;
    text-align: right;
    padding: 20px 25px 25px 0;
    background-image: linear-gradient(#282828,#292929, #353535);
    
}
.vendor-details-container h4{
    font-weight: 500;
    font-size: 0.8em;
    color: #999;
    margin: 6px 0;
}
</style>

