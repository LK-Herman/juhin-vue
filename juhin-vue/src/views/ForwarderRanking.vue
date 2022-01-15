<template>
  <h2>RANKING PRZEWOŹNIKÓW</h2>
  <br>
  <div v-if="!error"  class="table-list forwarders-list">
            <div>
                <div id="forwarders-header" class="table-container table-header for-ranking-head">
                    <div class="center-text">
                        <p>POZYCJA</p>
                    </div>
                    <div>
                        <p>OCENA</p>
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
                </div>
                <div v-for="forwarder in forwarders" :key="forwarder.forwarderId" >
                    <div id="forwarders-container" class="table-container for-ranking">    
                        <div class="sub-table-list center-item" >
                            <p v-if="forwarder.counter == 1" ><span class="material-icons first-place">emoji_events</span></p>
                            <p v-if="forwarder.counter == 2" ><span class="material-icons second-place">military_tech</span></p>
                            <p v-if="forwarder.counter == 3" ><span class="material-icons third-place">military_tech</span></p>
                            <p v-if="forwarder.counter == 4 || forwarder.counter == 5"><span class="material-icons stars">star</span></p>
                            <p v-if="forwarder.counter > 5">{{forwarder.counter}}</p>
                        </div>
                        <div>
                            <p :class="{firsttext:forwarder.counter == 1}">{{forwarder.rating}}</p>
                        </div>
                        <div>
                            <p :class="{firsttext:forwarder.counter == 1}" >{{forwarder.name}}</p>
                        </div>
                        <div>
                            <p :class="{firsttext:forwarder.counter == 1}" >{{forwarder.email}}</p>
                        </div>
                        <div>
                            <p :class="{firsttext:forwarder.counter == 1}" >{{forwarder.phoneNumber}}</p>
                        </div>
                        
                    </div>
                </div>
          </div>
         

      </div>
</template>

<script>
import getForwarders from '../composables/getForwarders.js'
import urlHolder from '../composables/urlHolder.js'
import { onMounted } from '@vue/runtime-core'

export default {
  props:['userToken', 'user'],
  setup(props) {
    const url = urlHolder
    const {loadForwarders, error, forwarders, totalRecords} = getForwarders(url, props.userToken)
    let counter = 1

    onMounted(async () => {
        counter = 1
      await loadForwarders(1,50)
            .then(function(){
                    forwarders.value.sort(function(a, b){return (b.rating - a.rating)})
                    forwarders.value.forEach(item =>{
                                            item['counter']=counter++
                                            
                                            })  
            })
   
    })

    return { forwarders, error }
  }
}
</script>

<style>

.table-list .table-header#forwarders-header.for-ranking-head{
    background-color: #00976a;
    grid-template-columns: 80px 80px 260px 240px 140px;
    
}
.table-list .table-header#forwarders-header.for-ranking-head p{
    
    font-weight: 600;
}
.table-container#forwarders-container.for-ranking{
    grid-template-columns: 80px 80px 260px 240px 140px;
}
.table-container .icon-group{
    color: rgb(97, 97, 97);
}
.table-list .firsttext{
    color: #ffd932;
    font-size: 16px;

}
.table-list .first-place{
    color: #fdcf00;
    font-size: 40px;
}
.table-list .second-place{
    color: #e2cc6d;
    font-size: 36px;
}
.table-list .third-place{
    color: #e4e4e4;
    font-size: 30px;
}
.table-list .stars{
    color: #ececec;
    font-size: 18px;
}
.table-container .center-item {
    justify-self: center;
}

</style>