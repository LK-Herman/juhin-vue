<template>
<div v-if="!errorVendor">
  <p>VENDOR CODE: {{vendor.vendorCode}}</p>
  <p>VENDOR NAME: {{vendor.name}}</p>
  <p>VENDOR ADDRESS: {{vendor.address}}</p>
  <p>VENDOR COUNTRY: {{vendor.country}}</p>
  <p>VENDOR EMAIL: {{vendor.email}}</p>
  <p>VENDOR PHONE NO: {{vendor.phoneNumber}}</p>
</div>
</template>

<script>
import urlHolder from '../composables/urlHolder.js'
import getVendorById from '../composables/getVendorById.js'
import { onMounted } from '@vue/runtime-core'
export default {
props:['userToken', 'vendorId'],
setup(props){
    const mainUrl = urlHolder
    const {vendor, error:errorVendor, loadVendor} = getVendorById(mainUrl,props.userToken)
    
    onMounted (()=>{
            loadVendor(props.vendorId)
        })

    return {vendor, errorVendor}
}
}
</script>

<style>

</style>