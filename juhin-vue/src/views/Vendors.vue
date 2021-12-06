<template>
    
    <br>
    <div v-if="!error"> 
        <h2>Lista dostawców</h2>
        <div v-for="vendor in vendors" :key="vendor.vendorId"> 
        <br>
          <p>Id dostawcy: {{vendor.vendorId}}</p>
          <h4>{{vendor.shortName}} - {{vendor.name}} - {{vendor.vendorCode}}</h4>
          <p>{{vendor.address}} - {{vendor.country}}</p>
          <p>Email: {{vendor.email}}</p>
          <p>Number telefonu: {{vendor.phoneNumber}}</p>

        </div>
    </div>
</template>

<script>

import { onMounted } from '@vue/runtime-core'
import getVendors from '../composables/getVendors.js'
import urlHolder from '../composables/urlHolder.js'

export default {
  components: {  },
  props:['userToken'],
  setup(props) {
    const url = urlHolder
    const {vendors, error, loadVendors} = getVendors(url, props.userToken)

    onMounted(() => {
      loadVendors()
    })

    return { vendors, error }
  }
}
</script>
