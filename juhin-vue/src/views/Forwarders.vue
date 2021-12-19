<template>
  <h2>Przewoźnicy</h2>
  <br>
  <div v-if="!error">
      <ol >
          <li v-for="ven in vendors" :key="ven.vendorId">{{ven.name}} - {{ven.address}}</li>
      </ol>
  </div>
</template>

<script>
import { onMounted } from '@vue/runtime-core'
import getAllVendors from '../composables/getAllVendors.js'
import urlHolder from '../composables/urlHolder.js'
export default {
    props:['userToken', 'user'],
    setup(props){

        const mainUrl = urlHolder
        const {loadAllVendors, error, vendors, totalRecords} =getAllVendors(mainUrl, props.userToken)

      onMounted(async () => {
      await loadAllVendors()
      console.log(vendors.value)
    })

        return {loadAllVendors, error, vendors, totalRecords}
    }

}
</script>

<style>

</style>