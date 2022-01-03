<template>
    <div v-if="isVisible">
        <div v-if="!error && items.length !=0">
            <form @submit.prevent="handleAddItemsSubmit" class="parts-form">
                <div  id="parts">
                    <label>Wybierz pozycję towarową</label>
                    <select v-model="itemIdNo" required>
                        <option v-for="item in items" :key="item.itemId" :value="item.itemId">{{item.name}} {{item.description}}</option>
                    </select>
                </div>
                <div id="quantity">
                    <label for="">Ilość</label>
                    <input type="number" v-model="quantity">
                </div>
                <div>
                    <button>Dodaj</button>
                </div>
            </form>
        </div>
        <div class="center-text" v-else>
            <br>
            <h3 class="warning">Brak pozycji towarowych do wyboru.</h3>
            <p>(dodaj pozycje towarowe dla danego dostawcy w sekcji TOWARY)</p>
        </div>
     
    </div>
</template>

<script>
import urlHolder from '../composables/urlHolder.js'
import getItemsByVendor from '../composables/getItemsByVendor.js'
import { onMounted, ref } from '@vue/runtime-core'
import addPackedItem from '../composables/addPackedItem.js'

export default {
    emits:['item-added-event'],
    props:['userToken', 'vId' ,'id' ],
    setup(props,context){
        const mainUrl = urlHolder
        const {loadItems, error, items} = getItemsByVendor(mainUrl, props.userToken)
        const isVisible = ref(true)
        const quantity = ref(0)
        const itemIdNo = ref('')
        const {addItem, error:addItemError} = addPackedItem(mainUrl, props.userToken)

        onMounted(()=>{
            loadItems(props.vId)
        })
         
         const handleAddItemsSubmit = async () =>{

             let packedItemData = {
                 deliveryId: props.id,
                itemId: itemIdNo.value,
                quantity: quantity.value
             }
             await addItem(packedItemData).finally(
                 function(){
                     quantity.value=0
                 })
            if(!addItemError.value){
                context.emit('item-added-event')
            }

        }
        return {handleAddItemsSubmit, items, error, addItemError, isVisible, quantity, itemIdNo}
    }

}
</script>

<style>

.parts-form{
    display: flex;
    flex-direction: row;
    max-width: 700px; 
    justify-content: space-between;
    align-items: flex-end;
    margin: 0px auto;
    padding: 10px;
    background-color: transparent;
    box-shadow: none;
}
.parts-form #parts{
    width: 500px;
    margin: 0 15px;
}
.parts-form #quantity{
    max-width: 160px;
}
.parts-form button{
    margin: 10px 10px 10px 40px;
    height: 40px;
    
}
</style>