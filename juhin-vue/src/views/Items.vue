<template>
  <h2>Lista towarów</h2>
  <br>
  <div v-if="!error"  class="table-list items-list">
            <div>
                <div id="items-header" class="table-container table-header">
                    <div class="center-text">
                        <p>POZYCJA</p>
                    </div>
                    <div>
                        <p>NAZWA</p>
                    </div>
                    <div>
                        <p>REW.</p>
                    </div>
                    <div>
                        <p>OPIS</p>
                    </div>
                    <div>
                        <p>DOSTAWCA</p>
                    </div>
                    <div>
                        <p>KRAJ</p>
                    </div>
                    <div>
                        <p>ORI / EUR</p>
                    </div>
                 
                   
                    
                </div>
                <div v-for="item in items" :key="item.itemId" >
                    <div id="items-container" class="table-container" :class="{inactive:!item.isActive}">    
                        <div class="sub-table-list center-text" >
                            <p>{{item.counter}}</p>
                        </div>
                        <div>
                            <p>{{item.name}}</p>
                        </div>
                        <div>
                            <p>{{item.revisionNumber}}</p>
                        </div>
                        <div>
                            <p>{{item.description}}</p>
                        </div>
                        <div>
                            <p>{{item.vendorName}} </p>
                        </div>
                        <div>
                            <p>{{item.countryOfOrigin}} </p>
                        </div>
                        <div>
                            <p>{{item.palletQty}} / {{item.maxEuroPalQty}} </p>
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
import getItems from '../composables/getItems.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, onUpdated, ref, watch, watchEffect } from '@vue/runtime-core'

export default {
  components: {  },
  props:['userToken', 'user'],
  setup(props) {
    const url = urlHolder
    const {loadItems, error, items, totalRecords} = getItems(url, props.userToken)
    
    const pageNo = ref(1)
    const recordsPerPage = ref(10)
    const lastPage = ref(1)
    let counter = 1
    
    const calculatePageCount = (pageSize, totalCount) => {
        
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }

    onMounted(async () => {
        counter = 1
      await loadItems(pageNo.value,recordsPerPage.value)
            .then(function(){
                  items.value.forEach(item =>{item['counter']=counter++})  
            })
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    watch((items), async () =>{
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            counter = 1
            await loadItems(pageNo.value, recordsPerPage.value)
                .then(function(){
                    items.value.forEach(item =>{item['counter']=counter++})  
                })
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
            counter = 1
        await loadItems(pageNo.value, recordsPerPage.value)
            .then(function(){
                  items.value.forEach(item =>{item['counter']=counter++})  
            })
    }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        counter = 1
        await loadItems(pageNo.value, recordsPerPage.value)
            .then(function(){
                  items.value.forEach(item =>{item['counter']=counter++})  
            })
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        counter = 1
        await loadItems(pageNo.value, recordsPerPage.value)
        .then(function(){
                  items.value.forEach(item =>{item['counter']=counter++})  
            })
    }

    return { items, error, pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage }
  }
}
</script>

<style>
.table-list .items-list{
    max-width: 1200px;
}
.table-list .table-header#items-header{
    background-color: #00376b;
    grid-template-columns: 80px 120px 60px 240px 240px 100px 120px;
}
.table-container#items-container{
    grid-template-columns: 80px 120px 60px 240px 240px 100px 120px;
}
.table-container#items-container.inactive div p{
    color: #7c7c7c;
}

.table-container .icon-group{
    color: rgb(97, 97, 97);
}

</style>