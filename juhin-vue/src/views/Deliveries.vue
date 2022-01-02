<template>
  <h2>Dostawy</h2>
  <br>
  <div v-if="!error"  class="table-list">
            <div>
                <div class="table-container table-header">
                    <div>
                        <p>NR ZAMÓWIENIA</p>
                    </div>
                    <div>
                        <p>DOSTAWCA</p>
                    </div>
                    <div>
                        <p>NR DOSTAWCY</p>
                    </div>
                    <div>
                        <p>UTWORZONO</p>
                    </div>
                    <div>
                        <p>ETA</p>
                    </div>
                    <div>
                        <p>PRZEWOŹNIK</p>
                    </div>
                    <div>
                        <p>OCENA</p>
                    </div>
                    <div>
                        <p>STATUS</p>
                    </div>
                    <div>
                        <p>PRIO</p>
                    </div>
                </div>
                <div v-for="delivery in deliveries" :key="delivery.deliveryId" >
                    <router-link :to="{name:'DeliveryDetails', params:{id:delivery.deliveryId}}">

                        <div class="table-container">    
                            <div class="sub-table-list" >
                                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                    <p>{{order.orderNumber}}</p>
                                </div>
                            </div>
                            <div>
                                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                    <p v-if="order.vendorData">{{order.vendorData.name}}</p>
                                </div>
                            </div>
                            <div>
                                <div v-for="order in delivery.purchaseOrders" :key="order.orderId">
                                    <p v-if="order.vendorData">{{order.vendorData.vendorCode}}</p>
                                </div>
                            </div>
                            <div>
                                <p>{{delivery.createdAt}}</p>
                            </div>
                            <div>
                                <p>{{delivery.etaDate}}</p>
                            </div>
                            <div>
                                <p v-if="delivery.forwarderName">{{delivery.forwarderName}}</p>
                            </div>
                            <div>
                                <p>{{delivery.rating}}</p>
                            </div>
                            <div>
                                <p v-if="delivery.statusName">{{delivery.statusName}}</p>
                            </div>
                            <div>
                                <p>{{delivery.isPriority?"TAK":"NIE"}}</p>
                            </div>

                        </div>
                    </router-link>
                </div>
          </div>
          <div class="table-buttons">
                
                <button @click="handlePages(20)"><span class="material-icons">list</span>20</button>
                <button @click="handlePages(50)"><span class="material-icons">list</span>50</button>
                <button @click="handlePages(100)"><span class="material-icons">list</span>100</button>
                <button v-if="pageNo==1"><span class="material-icons">keyboard_double_arrow_left</span>cofnij</button>
                <button v-else @click="handlePreviousPage"><span class="material-icons">keyboard_double_arrow_left</span>cofnij</button>

                <div class="table-page-numbers">
                     <div v-for="page in lastPage" :key="page" @click="handleGoToPage(page)">
                        <span :class="{'active-page-no' : pageNo==page}">{{page}}</span>
                    </div>
                    <div> | </div>
                    <div v-if="lastPage>=pageNo" @click="handleGoToPage(lastPage)"> {{lastPage}}</div>
                    
                </div>
                <button v-if="pageNo<lastPage" @click="handleNextPage">dalej<span class="material-icons">keyboard_double_arrow_right</span></button>
                <button v-else >koniec<span class="material-icons">keyboard_double_arrow_right</span></button>
          </div>

      </div>
</template>

<script>
import { onMounted, ref, watch } from '@vue/runtime-core'
import getDeliveriesList from '../composables/getDeliveriesList.js'
import urlHolder from '../composables/urlHolder.js'


export default {
  components: {  },
  props:['userToken', 'user'],
  setup(props) {
    const url = urlHolder
    const {deliveries, error, loadDeliveries, totalRecords} = getDeliveriesList(url, props.userToken)
    const pageNo = ref(1)
    const recordsPerPage = ref(10)
    const lastPage = ref(1)
    
    const calculatePageCount = (pageSize, totalCount) => {
        //console.log('RPP '+pageSize)
        //console.log('TR '+totalCount)
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }

    onMounted(async () => {
      await loadDeliveries(pageNo.value,recordsPerPage.value)
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        // console.log(lastPage.value)
        console.log(lastPage.value)
    })
  

    watch((deliveries), async () =>{
        
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        if(vendors.value){
             await handleGoToPage(lastPage.value)
         }
    })

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            await loadDeliveries(pageNo.value, recordsPerPage.value)
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
        await loadDeliveries(pageNo.value, recordsPerPage.value)
        }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        await loadDeliveries(pageNo.value, recordsPerPage.value)
    
        
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        await loadDeliveries(page, recordsPerPage.value)
    }

    return { deliveries, error, pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage }
  }
}
</script>

<style>



</style>