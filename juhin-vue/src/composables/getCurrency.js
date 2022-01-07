import { ref } from '@vue/reactivity'
import axios from 'axios'

const getCurrency = (url, token) =>{

    const currencyList = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadCurrency = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'currencies?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
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
                currencyList.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        // console.log(currencyList.value)

      }

      return {loadCurrency, error, currencyList, totalRecords}
}

export default getCurrency
