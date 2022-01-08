import { ref } from '@vue/reactivity'
import axios from 'axios'

const getItems = (url, token) =>{

    const items = ref([])
    const error = ref(null)
    const vednorError =ref(null)
    const totalRecords = ref(1)
    const lastPage = ref('')

    
    const loadItems = async (pageNo, recordsPerPage) => {

        try {
                let resp = await axios.get(url + 'items?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
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
                items.value = resp.data
                
            } catch (er) {
        error.value = er.message
        
        }

        //vendor
        items.value.forEach(async item => {
            try {
                let resp = await axios.get(url + 'vendors/' + item.vendorId, {
                    headers: {'Authorization':'Bearer ' + token,
                              'Accept':'*/*'
                            }
                    })
                if (resp.status<200 || resp.status>300){ throw Error('No data available. Error: ' + resp.status) }
                else
                {   
                    item['vendorName'] = resp.data.name
                }
            } catch (err) {
                vednorError.value=err.message
                console.log(vednorError.value)
            }
        })


        // console.log(items.value)

      }

      return {loadItems, error, items, totalRecords, lastPage}
}

export default getItems