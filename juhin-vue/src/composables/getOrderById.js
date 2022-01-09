import { ref } from '@vue/reactivity'
import axios from 'axios'

const getOrderById = (url, token) =>{

    const order = ref([])
    const error = ref(null)
    
    const getOrder = async (id) => {

        try {
                let resp = await axios.get(url + 'orders/'+ id, {
                    headers: {'Authorization':'Bearer ' + token,
                            'Accept':'*/*'
                    }
                })
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                order.value = await resp.data
                // console.log(resp.data)
                
            } catch (er) {
        error.value = er.message
        console.log(error.value)
        }
        console.log(order.value)
        //vendors.value.sort(function(a, b){return a.vendorCode - b.vendorCode})

      }

      return {getOrder, error, order}
}

export default getOrderById