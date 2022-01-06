<template>
  <h2>Przewoźnicy</h2>
  <br>
  <div v-if="!error"  class="table-list forwarders-list">
            <div>
                <div id="forwarders-header" class="table-container table-header">
                    <div class="center-text">
                        <p>POZYCJA</p>
                    </div>
                    <div>
                        <p>NAZWA</p>
                    </div>
                    <div>
                        <p>EMAIL</p>
                    </div>
                    <div>
                        <p>NR TELEFONU</p>
                    </div>
                    <div>
                        <p>OCENA</p>
                    </div>
                </div>
                <div v-for="forwarder in forwarders" :key="forwarder.forwarderId" >
                    <div id="forwarders-container" class="table-container">    
                        <div class="sub-table-list center-text" >
                            <p>{{forwarder.counter}}</p>
                        </div>
                        <div>
                            <p>{{forwarder.name}}</p>
                        </div>
                        <div>
                            <p>{{forwarder.email}}</p>
                        </div>
                        <div>
                            <p>{{forwarder.phoneNumber}}</p>
                        </div>
                        <div>
                            <p>{{forwarder.rating}}</p>
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
import getForwarders from '../composables/getForwarders.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted, onUpdated, ref, watch, watchEffect } from '@vue/runtime-core'

export default {
  components: {  },
  props:['userToken', 'user'],
  setup(props) {
    const url = urlHolder
    const {loadForwarders, error, forwarders, totalRecords} = getForwarders(url, props.userToken)
    const pageNo = ref(1)
    const recordsPerPage = ref(10)
    const lastPage = ref(1)
    let counter = 1
    
    const calculatePageCount = (pageSize, totalCount) => {
        
        return totalCount < pageSize ? 1 : Math.ceil(totalCount / pageSize)
    }

    onMounted(async () => {
        counter = 1
      await loadForwarders(pageNo.value,recordsPerPage.value)
            .then(function(){
                  forwarders.value.forEach(item =>{item['counter']=counter++})  
            })
    })
    watch(pageNo, () => {
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    watch((forwarders), async () =>{
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
    })

    const handleNextPage = async () => {
        if(pageNo.value < lastPage.value){
            pageNo.value++
            counter = 1
            await loadForwarders(pageNo.value, recordsPerPage.value)
                .then(function(){
                    forwarders.value.forEach(item =>{item['counter']=counter++})  
                })
        }
    }
    const handlePreviousPage = async () => {
        if(pageNo.value > 1){
            pageNo.value--
            counter = 1
        await loadForwarders(pageNo.value, recordsPerPage.value)
            .then(function(){
                  forwarders.value.forEach(item =>{item['counter']=counter++})  
            })
    }
    }
    const handlePages = async (pages) => {
        recordsPerPage.value = pages
        pageNo.value =1
        lastPage.value = calculatePageCount(recordsPerPage.value, totalRecords.value)
        counter = 1
        await loadForwarders(pageNo.value, recordsPerPage.value)
            .then(function(){
                  forwarders.value.forEach(item =>{item['counter']=counter++})  
            })
    }
    const handleGoToPage = async (page) => {
        pageNo.value = page
        counter = 1
        await loadForwarders(pageNo.value, recordsPerPage.value)
        .then(function(){
                  forwarders.value.forEach(item =>{item['counter']=counter++})  
            })
    }

    return { forwarders, error, pageNo, recordsPerPage, handleNextPage, handlePreviousPage, handlePages, handleGoToPage, lastPage }
  }
}
</script>

<style>
.table-list .forwarders-list{
    max-width: 1200px;
}
.table-list .table-header#forwarders-header{
    background-color: #422464;
    grid-template-columns: 80px 260px 240px 140px 80px ;
}
.table-container#forwarders-container{
    grid-template-columns: 80px 260px 240px 140px 80px ;
}
.table-container .icon-group{
    color: rgb(97, 97, 97);
}

</style>