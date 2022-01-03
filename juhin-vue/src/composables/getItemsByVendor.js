import { ref } from '@vue/reactivity'
import axios from 'axios'

const getItemsByVendor = (url, token) =>{

    const items = ref([])
    const error = ref(null)
    
    const loadItems = async (id) => {

        try {
                let resp = await axios.get(url + 'items/vendor/'+ id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                items.value = await resp.data
                // console.log(resp.data)
                
            } catch (er) {
        error.value = er.message
        console.log(error.value)
        }
        //console.log(vendors.value)
        //vendors.value.sort(function(a, b){return a.vendorCode - b.vendorCode})

      }

      return {loadItems, error, items}
}

export default getItemsByVendor