<template>
    <div v-if="!isDeleted">
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
                    <button @click="handleEditVendor">Edytuj dane dostawcy</button>
                </div>
                <div class="btns-items">
                    <p>Wprowadź nowe zamówienie dla tego dostawcy </p>
                    <button @click="handleAddOrder">Dodaj zamówienie</button>
                </div>
                <div class="btns-items">
                    <p>Dodaj nowe materiały, które dostarcza {{vendor.name}} </p>
                    <button @click="handleAddItem">Dodaj materiały</button>
                    <!-- <router-link :to="{name:'ItemAdd', params:{vId:vId}}" :userToken="userToken" :user="user">Dodaj materiały</router-link> -->
                </div>
            </div>
        </div>
        <div v-if="editFlag">
            <form @submit.prevent="handleSubmit" class="vendor-form edit-vendor" >
                <div>
                    <h3>Edytuj dane dostawcy</h3>
                </div>
                <div v-if="putError" class="error">
                    <p>{{putError}}</p>
                </div>
                <div class="double">
                    <div>
                        <label>Kod dostawcy</label>
                        <input type="text" v-model="formVendorCode">
                    </div>    
                    <div>
                        <label>Nazwa (forma krótka) </label>
                        <input type="text" v-model="formShortName" >
                    </div>
                </div>
                <div v-if="responseErrors && responseErrors.hasOwnProperty('VendorCode')" class="error">
                    <p>{{responseErrors.VendorCode[0]}}</p>
                </div>
                <div v-if="responseErrors && responseErrors.hasOwnProperty('ShortName')" class="error">
                    <p>{{responseErrors.ShortName[0]}}</p>
                </div>
                <label>Nazwa pełna</label>
                <input type="text" v-model="formName">
                <div v-if="responseErrors && responseErrors.hasOwnProperty('Name')" class="error">
                    <p>{{responseErrors.Name[0]}}</p>
                </div>
                <label>Adres</label>
                <input type="text" v-model="formAddress">
                <div v-if="responseErrors && responseErrors.hasOwnProperty('Address')" class="error">
                    <p>{{responseErrors.Address[0]}}</p>
                </div>
                <div  class="triple">
                    <div>
                        <label>Kraj</label>
                        <input type="text" v-model="formCountry">
                    </div>
                    <div>
                        <label>Adres email</label>
                        <input type="email" v-model="formEmail">
                    </div>
                    <div>
                        <label>Numer telefonu</label>
                        <input type="tel" v-model="formPhoneNumber">
                    </div>
                </div>
                <div v-if="responseErrors && responseErrors.hasOwnProperty('Country')" class="error">
                    <p>{{responseErrors.Country[0]}}</p>
                </div>
            
                <div class="submit">
                    <button>Dodaj</button>
                </div>
                <div>
                    <button class="delete-btn" @click="handleDelete">Usuń dostawcę</button>
                </div>
        </form>

        <div id="myModal" class="modal">
            <div class="modal-content">

                    <div class="modal-question">
                        <p>Czy na pewno chcesz usunąć?</p>
                    </div>
                    <div class="modal-buttons" >
                        <button id="yes" @click="handleYes">Tak</button>
                        <button id="no" @click="handleNo">Nie</button>
                    </div>
                
            </div>

        </div>
        </div>


    </div>
    <div v-else>
        <h2>Dostawca został usunięty</h2>    
    </div>
    


</template>

<script>

import urlHolder from '../composables/urlHolder.js'
import getVendorById from '../composables/getVendorById.js'
import { onMounted, ref } from '@vue/runtime-core'
import {useRouter} from 'vue-router'
import editVendor from '../composables/editVendor.js'
import deleteVendorById from '../composables/deleteVendorById'

export default {
  components: {  },
  props:['userToken', 'user', 'vId'],
  setup(props) {
    const mainUrl = urlHolder
    const {loadVendor, error, vendor} = getVendorById(mainUrl, props.userToken)
    const {deleteVendor, error:delError} = deleteVendorById(mainUrl, props.userToken)
    const router = useRouter()
    const editFlag = ref(false)
    const isDeleted = ref(false)

    const formVendorCode = ref('')
    const formShortName = ref('')
    const formName = ref('')
    const formAddress = ref('')
    const formCountry = ref('')
    const formEmail = ref('')
    const formPhoneNumber = ref('')
    const formIsActive = ref(true)

    onMounted(async()=>{
        if(props.vId != ''){
            await loadVendor(props.vId)
                // .then(function(){

                // })
        }
        else
        {
            router.push({name:"Vendors"})
        }
    })

    const handleAddItem = () =>{
        
        router.push({name:'ItemAdd', params:{
            
                userToken:props.userToken, 
                user:props.user, 
                vend:JSON.stringify(vendor.value)
                }})
    }
    const handleAddOrder = () =>{
        
        router.push({name:'OrderAdd', params:{
            
                userToken:props.userToken, 
                user:props.user, 
                vend:JSON.stringify(vendor.value)
                }})
    }

        //edit vendor
        

        const handleEditVendor = () =>{
            formVendorCode.value = vendor.value.vendorCode
            formShortName.value = vendor.value.shortName
            formName.value = vendor.value.name
            formAddress.value = vendor.value.address
            formCountry.value = vendor.value.country
            formEmail.value = vendor.value.email
            formPhoneNumber.value = vendor.value.phoneNumber
            formIsActive.value = vendor.value.IsActive
            editFlag.value = true
            responseErrors.value = ''
            putError.value = ''
        }

        const {putVendor, error:putError, responseErrors} = editVendor(mainUrl, props.userToken)

        const handleSubmit = async () =>{
                responseErrors.value = null
                putError.value = null
            const vendorData = {
                VendorCode : formVendorCode.value,
                ShortName : formShortName.value,
                Name : formName.value,
                Address : formAddress.value,
                Country : formCountry.value,
                Email : formEmail.value,
                PhoneNumber : formPhoneNumber.value,
                IsActive : formIsActive.value
            }
            
            await putVendor(props.vId, vendorData)
            if(!responseErrors.value && !error.value){
                // editFlag.value = false
                // router.push({name:'Created'})
            }
                await loadVendor(props.vId)
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
     const handleYes = async () =>{
            await deleteVendor(props.vId)
            .finally(function(){ isDeleted.value = true })
        }
        const handleNo = () =>{
             var modal = document.getElementById("myModal");
             modal.style.display = "none"
        }

    
    return {    error,
                putError,
                responseErrors,
                vendor, 
                handleAddItem, 
                handleAddOrder, 
                handleSubmit,
                handleEditVendor,
                handleDelete,
                handleYes,
                handleNo, 
                editFlag,
                isDeleted,
                formName,
                formShortName,
                formAddress,
                formVendorCode,
                formCountry,
                formEmail,
                formPhoneNumber,
                formIsActive
                }
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
    margin: 60px 0 40px 0;
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
    background-color: var(--orange);
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

.edit-vendor{
    max-width: 1010px;
    padding: 0 5px;
    margin: 0;
    background: none;
    box-shadow: none;
}
.edit-vendor .double{
    display: grid;
    grid-template-columns: 1fr 1fr;
    gap: 20px;
}
.edit-vendor .triple{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 20px;
}
.edit-vendor h3{
    margin: 20px 0;
}
</style>

