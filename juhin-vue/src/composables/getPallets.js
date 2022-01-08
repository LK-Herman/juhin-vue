import { ref } from '@vue/reactivity'
import axios from 'axios'

const getPallets = (url, token) =>{

    const pallets = ref([])
    const error = ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadPallets = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'pallets?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
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
                pallets.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }
        // console.log(pallets.value)

      }

      return {loadPallets, error, pallets, totalRecords}
}

export default getPallets
