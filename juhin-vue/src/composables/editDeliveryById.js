import { ref } from '@vue/reactivity'
import axios from 'axios'

const editDeliveryById = (url, token) =>{

    const error = ref(null)

    const editDelivery =  async (deliveryData, deliveryId) => {

        var requestOptions = {
        method: 'PUT',
        headers: {
            "Accept":"*/*",
            'Content-Type':'application/json',
            "Access-Control-Allow-Origin": "*",
            "Authorization":"Bearer " + token
        },
        mode:'cors',
        };
      
        try {
            let resp = await axios.put( url + 'deliveries/'+ deliveryId, deliveryData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {editDelivery, error}
}
export default editDeliveryById