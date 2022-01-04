import { ref } from '@vue/reactivity'
import axios from 'axios'

const getForwarders = (url, token) =>{

    const forwarders = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadForwarders = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'forwarders?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
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
                forwarders.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        console.log(forwarders.value)

      }

      return {loadForwarders, error, forwarders, totalRecords}
}

export default getForwarders