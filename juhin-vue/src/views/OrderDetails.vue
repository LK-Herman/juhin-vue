<template>
  <p>Order Details</p>
  <div v-if="!error">
      <p>{{order.orderId}}</p>
      <p>{{order.orderNumber}}</p>
  </div>
</template>

<script>
import { onMounted } from '@vue/runtime-core'
import getOrderById from '../composables/getOrderById'
import urlHolder from '../composables/urlHolder.js'
export default {
    props: ['userToken', 'user', 'orderId'],
    setup(props){
        const mainUrl = urlHolder
        const {getOrder, error, order} = getOrderById(mainUrl, props.userToken)
        
        onMounted(async () => {
            await getOrder(props.orderId)
        })
        
        return {error, order}
    }
}
</script>

<style>

</style>