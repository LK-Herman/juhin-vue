import { ref } from '@vue/reactivity'
import axios from 'axios'

const getWarehouses = (url, token) =>{

    const warehouses = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadWarehouses = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'warehouses?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
                }
                totalRecords.value = resp.headers["all-records"]
                lastPage.value = resp.headers["totalamountpages"]
                warehouses.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        // console.log(warehouses.value)

      }

      return {loadWarehouses, error, warehouses, totalRecords}
}

export default getWarehouses