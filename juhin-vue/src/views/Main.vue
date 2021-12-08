<template>
    <h2>Strona startowa</h2>
    <div>
        <button class="btn" @click="deliveryFlag=!deliveryFlag">Pokaż szczegóły zamówienia</button>
        <div v-if="deliveryFlag">
            <DeliveryDetails :userToken="userToken" />
        </div>
    </div>
    <br>
    <br>
    <p>Lorem ipsum dolor sit amet consectetur, adipisicing elit. Eos excepturi fugit saepe exercitationem ipsam voluptas sequi ab, harum modi, quo veniam minima dicta fugiat deserunt eaque officiis. Quaerat, esse veritatis?</p>
    <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Voluptate quae sed tempore placeat nobis pariatur similique cumque fuga nulla ad. Dolor corporis non ut rerum id officiis nobis ea facere qui ex, mollitia quae, laborum accusantium, dolore voluptatem esse. Similique illum, cupiditate asperiores fugiat dolores dolorem iste voluptate, corrupti atque sapiente quia, dolorum porro tenetur impedit perspiciatis nihil dolore vitae fugit vero itaque nostrum! Molestiae illo mollitia autem dicta aut accusantium? Explicabo molestiae commodi, nobis ea vero facere dolore! Ipsam, voluptate nihil. Iusto molestias adipisci provident architecto exercitationem sit eveniet consequatur dicta, illum suscipit enim culpa aliquam dolore ut quos?</p>
    
    
    
</template>

<script>
import { ref } from '@vue/reactivity'
import DeliveryDetails from '../components/DeliveryDetails.vue'
import { onMounted } from '@vue/runtime-core'
import { useRouter } from 'vue-router'
export default {
  props: ['userToken'],
  components: { DeliveryDetails },
  setup(props) {
      const isVisible = ref(false)
      const router = useRouter()
      const deliveryFlag = ref(false)
    
    onMounted(()=>{
        if (!props.userToken){
            isVisible.value = false
            router.push({name:"Login"})
        }
        else{
            isVisible.value = true
        }
    })

    return { isVisible, deliveryFlag }
  }
}
</script>
