import { ref } from '@vue/reactivity'
import axios from 'axios'

const deleteDelivery = (url, token) =>{

    const error = ref(null)

    const delDel =  async (deliveryId, orders) => {

        var requestOptions = {
        method: 'DELETE',
        headers: {
            "Accept":"*/*",
            'Content-Type':'application/json',
            "Access-Control-Allow-Origin": "*",
            "Authorization":"Bearer " + token
            },
        mode:'cors',
        }
        if(orders.length != 0){
            orders.forEach(async order => {
                try {
                    let resp = await axios.delete( url + 'deliveries/' + deliveryId + ',' + order.orderId, requestOptions)
                    if (resp.status <200 & resp.status > 300){
                        throw Error('Coś poszło nie tak..')
                    }
                    console.log(resp)
                } catch (err) {
                    error.value = err.message
                    console.log(error.value)
                }    
                
            });
        }else{
            error.value = "Błędny nr zamówienia"
            console.log(error.value)
        }
    }
      return {delDel, error}
}
export default deleteDelivery