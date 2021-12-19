import { ref } from '@vue/reactivity'
import axios from 'axios'

const getAllVendors = (url, token) =>{

    const vendors = ref([])
    const error = ref(null)
    const totalRecords = ref('')

    
    const loadAllVendors = async () => {
     
        try {
            let resp = await axios.get(url + 'vendors/', {
                headers: {'Authorization':'Bearer ' + token,
                          'Accept':'*/*',
                          'Access-Control-Expose-Headers':'all-records'
                        }})
            totalRecords.value = resp.headers["all-records"]
            vendors.value = resp.data
            }            
            catch (er) {
                error.value = er
            }
    }

      return { loadAllVendors, vendors, error, totalRecords }
}

export default getAllVendors