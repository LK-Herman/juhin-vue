<template>
    <h2>Lista dostawców</h2>
    <div v-if="!error"  class="table-list">
            <div>
                <div id="vendors-header" class="table-container table-header">
                    <div>
                        <p>KOD DOSTAWCY</p>
                    </div>
                    <div>
                        <p>SKRÓT</p>
                    </div>
                    <div>
                        <p>NAZWA</p>
                    </div>
                    <div>
                        <p>ADRES</p>
                    </div>
                    <div>
                        <p>KRAJ</p>
                    </div>
                    <div>
                        <p>EDYTUJ</p>
                    </div>
                   
                    
                </div>
                <div v-for="vendor in vendors" :key="vendor.vendorId" >
                    <div id="vendors-container" class="table-container">    
                        <div class="sub-table-list" >
                            <p>{{vendor.vendorCode}}</p>
                        </div>
                        <div>
                            <p>{{vendor.shortName}}</p>
                        </div>
                        <div>
                            <p>{{vendor.name}}</p>
                        </div>
                        <div>
                            <p>{{vendor.address}}</p>
                        </div>
                        <div>
                            <p>{{vendor.country}}</p>
                        </div>
                        <div >
                            <button>Edytuj</button>
                        </div>
                        
                    </div>
                </div>
          </div>
          <div class="table-buttons">
                
                <button @click="handlePages(10)"><span class="material-icons">list</span>10</button>
                <button @click="handlePages(20)"><span class="material-icons">list</span>20</button>
                <button @click="handlePages(50)"><span class="material-icons">list</span>50</button>

                <button v-if="pageNo==1"><span class="material-icons">keyboard_double_arrow_left</span>cofnij</button>
                <button v-else @click="handlePreviousPage"><span class="material-icons">keyboard_double_arrow_left</span>cofnij</button>

                <div class="table-page-numbers">
                    <div v-for="page in lastPage" :key="page" @click="handleGoToPage(page)">{{page}}</div>
                    <div> | </div>
                    <div v-if="lastPage>=pageNo" @click="handleGoToPage(lastPage)"> {{lastPage}}</div>
                    
                </div>
                <button v-if="pageNo<lastPage" @click="handleNextPage">dalej<span class="material-icons">keyboard_double_arrow_right</span></button>
                <button v-else >koniec<span class="material-icons">keyboard_double_arrow_right</span></button>
          </div>

      </div>


</template>

<script>
import getVendors from '../composables/getVendors.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, onUpdated, ref, watch, watchEffect } from '@vue/runtime-core'

export default {
  components: {  },
  props:['userToken', 'user'],
  setup(props) {
    const url = urlHolder
    const {vendors, error, loadVendors, totalRecords} = getVendors(url, props.userToken)
    const pageNo = ref(1)
    const recordsPerPage = ref(20)
    const lastPage = ref(1)
    
    const calculatePageCount = (pageSize, totalCount) => {
        
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }

    onMounted(async () => {
      await loadVendors(pageNo.value,recordsPerPage.value)
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        // console.log(lastPage.value)
        console.log(lastPage.value)
    })
  

    watch((vendors), async () =>{
        
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        console.log(lastPage.value)
        // if(vendors.value.length == 0){
        //     await handleGoToPage(1)
        // }
    })
   

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            await loadVendors(pageNo.value, recordsPerPage.value)
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
        await loadVendors(pageNo.value, recordsPerPage.value)
    }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        await loadVendors(pageNo.value, recordsPerPage.value)
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        await loadVendors(pageNo.value, recordsPerPage.value)
    }

    return { vendors, error, pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage }
  }
}
</script>
<style scoped>
.table-list .table-header#vendors-header{
    background-color: #2e570c;
    grid-template-columns: 100px 100px 240px 350px 160px 100px ;
}
.table-container#vendors-container{
    grid-template-columns: 100px 100px 240px 350px 160px 100px ;
}
.table-container .icon-group{
    color: rgb(97, 97, 97);
}

</style>>

