<template>
<div class="item-v" v-if="!errorVendor">
    <h1>{{vendor.name}}</h1>
    <p>{{vendor.vendorCode}}</p>
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