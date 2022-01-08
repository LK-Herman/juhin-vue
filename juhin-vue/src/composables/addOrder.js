import { ref } from '@vue/reactivity'
import axios from 'axios'

const addOrder = (url, token) =>{

    const error = ref(null)
    const response = ref(null)
    const addNewOrder =  async (orderData) => {

        var requestOptions = {
        method: 'POST',
        headers: {
            "Accept":"*/*",
            'Content-Type':'application/json',
            "Access-Control-Allow-Origin": "*",
            "Authorization":"Bearer " + token
        },
        mode:'cors',
        };
      
        try {
            let resp = await axios.post( url + 'orders', orderData, requestOptions)
            .then(value =>{
                response.value = value
            },reason =>{
                response.value = reason
                throw Error('Coś poszło nie tak.. ')
            } )
            
            // console.log(resp)
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {addNewOrder, error, response}
}
export default addOrder