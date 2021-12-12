<template>
  <h2>Dostawy</h2>
  <br>
  <div v-if="!error">
            <ol>
                <div v-for="delivery in deliveries" :key="delivery.deliveryId" >
                    <li>    <br>
                            <p> ID dostawy: {{delivery.deliveryId}}</p>
                            <p> Utworzona: {{delivery.createdAt}}</p>
                            <p> ETA: {{delivery.etaDate}}</p>
                            <p> Przewoźnik: {{delivery.forwarderId}}</p>
                            <p> Ocena dostawy: {{delivery.rating}}</p>
                            <p> Status: {{delivery.statusId}}</p>
                            <p> Priorytet: {{delivery.isPriority}}</p>

                    </li>
                </div>
          </ol>
      </div>
</template>

<script>
import { onMounted } from '@vue/runtime-core'
import getDeliveriesList from '../composables/getDeliveriesList.js'
import urlHolder from '../composables/urlHolder.js'

export default {
  components: {  },
  props:['userToken'],
  setup(props) {
    const url = urlHolder
    const {deliveries, error, loadDeliveries} = getDeliveriesList(url, props.userToken)

    onMounted(() => {
      loadDeliveries()
    })

    return { deliveries, error }
  }
}
</script>

<style>

</style>