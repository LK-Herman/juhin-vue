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
    
    <form id="parts-form" @submit.prevent="handleSubmit">
        <h3 class="center-text">Wprowadź dane towarowe</h3>
        <div class="double1">
            <div>
                <label>Numer towaru</label>
                <input type="text" v-model="partNumber" required>
            </div>
            <div>
                <label>Numer rewizji</label>
                <input type="number" v-model="revNo" required>
            </div>
        </div>
        <label>Opis</label>
        <input type="text" v-model="description" required>
        <div class="double2">
        <div>
                <label>Kod HS</label>
                <input type="text" v-model="hsCode" required>
        </div>
        <div>
                <label>Opis HS</label>
                <input type="text" v-model="hsDescription" required>
        </div>
        </div>
        <div v-if="vendor" class="triple">
            <div>
                <label>Kraj pochodzenia (origin)</label>
                <input type="text" v-model="origin" required>
            </div>
            <div>
                <label>Magazyn docelowy</label>
                <select v-if="!wherror" v-model="warehouseId">
                    <option v-for="house in warehouses" :key="house.warehouseId" :value="house.warehouseId">
                        {{house.shortName}} - {{house.description}}
                    </option>
                    
                </select>
            </div>
            <div id="parts-checkboxes">
                <div>
                    <input type="checkbox" :value="isActive" v-model="isActive">
                    <label>Aktywna</label>
                </div>
                <div>
                    <input type="checkbox" :value="isICP" v-model="isICP">
                    <label>WSK (ICP)</label>
                </div>
            </div>
        </div>
        <div class="parts-division"></div>
        <div class="triple">
            <div>
                <label>Maksymalna ilość na palecie oryginalnej (ori)</label>
                <input type="number" v-model="maxOri" required>
            </div>
            <div>
                <label>Maksymalna ilość na palecie euro (eur)</label>
                <input type="number" v-model="maxEur" required>
            </div>
            <div>
                <label>Rodzaj palety</label>
                <select v-if="!palerror" v-model="palletId">
                    <option v-for="pallet in pallets" :key="pallet.palletId" :value="pallet.palletId">
                        {{pallet.name}} ({{pallet.xdimension}}x{{pallet.ydimension}})
                    </option>
                </select>
            </div>
        </div>

        <div class="triple">
            <div>
                <label>Cena jednostkowa</label>
                <input type="number" v-model="price">
            </div>
            <div>
                <label>Waluta</label>
                <select v-if="!curerror" v-model="currencyId">
                    <option v-for="curr in currencyList" :key="curr.currencyId" :value="curr.currencyId">
                        {{curr.code}}
                    </option>
                </select>
            </div>
            <div>
                <label>Jednostka</label>
                <select v-if="!unierror" v-model="unitId">
                    <option v-for="unit in units" :key="unit.unitId" :value="unit.unitId">{{unit.shortName}} - {{unit.name}}</option>
                </select>
            </div>
        </div>
        
        <button>OK</button>
    </form>

</template>

<script>
import { onMounted, ref } from '@vue/runtime-core'
import getWarehouses from '../composables/getWarehouses.js'
import getCurrency from '../composables/getCurrency.js'
import getUnits from '../composables/getUnits.js'
import getPallets from '../composables/getPallets.js'
import addItem from '../composables/addItem.js'
import urlHolder from '../composables/urlHolder.js'
import {useRouter} from 'vue-router'

export default {
    props:['userToken', 'user', 'vend'],
    setup(props){
        const router = useRouter()
        const vendor = ref(null)
        const mainUrl = urlHolder;
        const warehouseId = ref(1)
        const currencyId = ref(1)
        const unitId = ref(1)
        const palletId = ref(1)
        const isActive = ref(true)
        const isICP = ref(false)
        const partNumber = ref('')
        const revNo = ref(1)
        const description = ref('')
        const hsCode = ref('')
        const hsDescription = ref('')
        const origin = ref('')
        const maxOri = ref(null)
        const maxEur = ref(null)
        const price = ref(null)

        const {loadWarehouses, warehouses, error:wherror} = getWarehouses(mainUrl, props.userToken)
        const {loadCurrency, currencyList, error:curerror} = getCurrency(mainUrl, props.userToken)
        const {loadUnits, units, error:unierror} = getUnits(mainUrl, props.userToken)
        const {loadPallets, pallets, error:palerror} = getPallets(mainUrl, props.userToken)
        const {addNewItem, error:addError, response} = addItem(mainUrl, props.userToken)

        onMounted(()=>{
            if(props.userToken === '' || props.vend === undefined ){
                router.push({name:'Main'})
            }else{
                vendor.value = JSON.parse(props.vend)
                origin.value = vendor.value.country
                loadWarehouses(1,50)
                loadCurrency(1,50)
                loadUnits(1,50)
                loadPallets(1,50)
            }
        })

        const handleSubmit = async () =>{
            let itemData = {
                name: partNumber.value,
                description: description.value,
                revisionNumber: revNo.value,
                price: price.value,
                maxEuroPalQty: maxEur.value,
                isICP: isICP.value,
                hsCode: hsCode.value,
                hsCodeDescription: hsDescription.value,
                countryOfOrigin: origin.value,
                palletQty: maxOri.value,
                isActive: isActive.value,
                warehouseId: warehouseId.value,
                vendorId: vendor.value.vendorId,
                currencyId: currencyId.value,
                palletId: palletId.value,
                unitId: unitId.value
            }
            
            await addNewItem(itemData)
                .then(function(){
                    console.log(response.value)
                })

        }


        return{
            vendor, 
            warehouses, 
            units,
            pallets, 
            currencyList,
            palerror, 
            curerror, 
            wherror, 
            unierror, 
            warehouseId, 
            currencyId, 
            unitId,
            palletId, 
            isActive, 
            isICP,
            partNumber,
            description,
            revNo,
            hsCode,
            hsDescription,
            origin,
            maxEur,
            maxOri,
            price,
            handleSubmit
            }
    }

}
</script>

<style >
#parts-form{
    background: none;
    box-shadow: none;
    padding-top: 0px ;
}

#parts-form h3{
    margin-bottom:25px;
    
}
#parts-form #parts-checkboxes{
    padding-bottom: 10px;

}
#parts-form .parts-division{
    margin-top: 60px;
}
#parts-form .double1{
    display: grid;
    grid-template-columns: 4fr 1fr;
    column-gap: 30px;
    align-items: end;
}
#parts-form .double2{
    display: grid;
    grid-template-columns: 1fr 3fr;
    column-gap: 30px;
    align-items: end;

}
#parts-form .double{
    display: grid;
    grid-template-columns: 1fr 1fr;
    column-gap: 30px;
    align-items: end;
}
#parts-form .triple{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    column-gap: 30px;
    align-items: end;
    
}

</style>