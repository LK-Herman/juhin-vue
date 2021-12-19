<template>
    <h2>Strona startowa</h2>
    <div>
        <p>{{user.userId}} - {{user.name}} - {{user.userRole}}</p>
        <br>
    </div>
    <div>
        <button class="btn" @click="deliveryFlag=!deliveryFlag">Pokaż szczegóły zamówienia</button>
        <div class="inner-container" v-if="deliveryFlag">
            <DeliveryDetails :userToken="userToken" :id="id" />
            <DeliveryDetails :userToken="userToken" :id="id2" />
            <DeliveryDetails :userToken="userToken" :id="id" />
        </div>
    </div>
    
    
    
    
</template>

<script>
import { ref } from '@vue/reactivity'
import DeliveryDetails from '../components/DeliveryDetails.vue'
import { onMounted } from '@vue/runtime-core'
import { useRouter } from 'vue-router'
export default {
  props: ['userToken', 'user'],
  components: { DeliveryDetails },
  setup(props) {
      const isVisible = ref(false)
      const router = useRouter()
      const deliveryFlag = ref(false)

      const id2 = ref('a0bc3e8b-f4de-454e-286a-08d996ff5716')
        const id= ref('cc30c0d7-e02e-407d-b628-08d997df4626')
    onMounted(()=>{
       
        if (props.userToken == ''){
            isVisible.value = false
            
        }
        else{
            isVisible.value = true
        }
    })


    return { isVisible, deliveryFlag, id, id2 }
  }
}
</script>
<style>
.inner-container{
    display: flex;
    flex-flow: column;
    align-items: center;
    margin: 40px 0;
}
</style>
