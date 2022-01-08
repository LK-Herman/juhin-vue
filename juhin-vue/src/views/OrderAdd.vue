<template>
   <div v-if="vendor">
        <div class="vendor-details-container">
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
        </div>
    </div>

    <form @submit.prevent="handleSubmit" id="parts-form">
        <div class="order-double">
            <div>
                <label>Wprowadź numer zamówienia</label>
                <input type="text" v-model="orderNumber">
            </div>
            <div>
                <button>Dodaj</button>
            </div>
        </div>
        <div v-if="addError" >
            <p class="error">{{addError}}</p>
            <p class="error">Prawdopodobnie nie wprowadzono numeru lub zamówienie o tym numerze już istnieje</p>
        </div>
        <div v-if="isOK">
            
            <div class="add-del-comment">
                <div class="item-a">
                    <p class="green"><span class="material-icons">task_alt</span> Zamówienie nr {{tempOrderNumber}} zostało utworzone.</p>
                </div>
                <div class="item-b">
                    <p>Twoje zamówienie nie posiada jeszcze informacji o dostawie. Dodaj teraz dostawę do utworzonego zamówienia, lub zrób to później w sekcji Zamówienia > Przeglądaj > Dodaj dostawę.</p>
                </div>
                <div class="item-c">
                    <button class="btn sub-btn" @click.prevent="handleAddDelivery" >Dodaj dostawę</button>
                </div>
            </div>
            
        </div>
    </form>
  
</template>

<script>
import { ref } from '@vue/reactivity'
import addOrder from '../composables/addOrder.js'
import urlHolder from '../composables/urlHolder.js'
import { useRouter } from 'vue-router'
import { onMounted, onUpdated, watch } from '@vue/runtime-core'
export default {
    props:['userToken', 'user', 'vend'],
    setup(props){
        
        const router = useRouter()
        const mainUrl = urlHolder
        const orderNumber = ref('')
        const vendor = ref(null)
        const isOK = ref(false)
        const tempOrderNumber = ref('')
        const {addNewOrder, error:addError, response} = addOrder(mainUrl, props.userToken)
        
         onMounted(()=>{
            if(props.userToken === '' || props.vend === undefined ){
                router.push({name:'Main'})
            }else{
                vendor.value = JSON.parse(props.vend)
            }
        })
        const handleSubmit = async () =>{
            addError.value=null
            let orderData = {
                orderNumber: orderNumber.value,
                vendorId: vendor.value.vendorId,
                userId: props.user.userId
            }
            
            await addNewOrder(orderData)
        }

        const handleAddDelivery = () =>{

             router.push({name:'DeliveryAdd', params:{
                                        vId: vendor.value.vendorId, 
                                        user: props.user, 
                                        vendorName: vendor.value.name, 
                                        orderNo:tempOrderNumber.value,
                                        orderId:response.value.data.orderId}}
             )
        }

       watch(response,()=>{
           if(response.value.toString().includes('rror')) isOK.value = false
           else {
               isOK.value=true
               tempOrderNumber.value = orderNumber.value
               orderNumber.value = ''}
       })

        return { orderNumber, addError, response, handleSubmit, vendor, isOK, tempOrderNumber, handleAddDelivery }
    }

}
</script>
<style>
.green{
    margin-top: 16px;
    color: chartreuse;
}
.order-double{
    display: grid;
    grid-template-columns: 300px 200px;
    gap: 20px;
    align-items: end;
}
.order-double button, .order-double input[type="text"]{
    margin-bottom: 0;
    min-height: 40px;
    
}


.add-del-comment{
    display: grid;
    grid-template-areas: 
    "com com btn"
    "tex tex btn";
    grid-template-columns: 220px 220px 220px;
    background: rgb(44, 55, 88);
    margin-top: 40px;
    padding: 15px 30px;
    transition: ease-in 400;
    box-shadow: 2px 2px 4px rgba(10,10,10,0.4);
    justify-content: space-between;
}
.add-del-comment .item-a{
    grid-area: com;
    margin: 0;
    padding: 0 10px 20px 10px;
}
.add-del-comment .item-b{
    grid-area: tex;
    padding: 0 20px 20px 10px;
}
.add-del-comment .item-c{
    grid-area: btn;
    align-self: center;
    justify-self: center;
}
.add-del-comment p{
    text-align: justify;

}
</style>