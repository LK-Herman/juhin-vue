import { ref } from '@vue/reactivity'
import axios from 'axios'

const getUnits = (url, token) =>{

    const units = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadUnits = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'units?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
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
                units.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        console.log(units.value)

      }

      return {loadUnits, error, units, totalRecords}
}

export default getUnits
