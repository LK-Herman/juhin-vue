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
  setup() {
    const url = urlHolder
    const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoibGt1Y3ptYUBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJsa3Vjem1hQGdtYWlsLmNvbSIsIm15a2V5Ijoid2hhdGV2ZXIgdmFsdWUgSSB3YW50IiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2MzkwODA4OTV9.g0nk_Ge_pn6uKR9N99AC84UgnA-k0RrgRKO6g5YUxbI'
    
    const {vendors, error, loadVendors} = getVendors(url, token)
    

    onMounted(() => {
      loadVendors()
    })

    return { vendors, error }
  }
}
</script>
