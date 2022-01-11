import { ref } from '@vue/reactivity'
import axios from 'axios'

const getStatuses = (url, token) =>{

    const statuses = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadStatuses = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'statuses?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
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
                statuses.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        console.log(statuses.value)

      }

      return {loadStatuses, error, statuses, totalRecords}
}

export default getStatuses