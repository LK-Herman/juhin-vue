import { ref } from '@vue/reactivity'
import axios from 'axios'

const addItem = (url, token) =>{

    const error = ref(null)
    const response = ref(null)
    const addNewItem =  async (itemData) => {

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
            let resp = await axios.post( url + 'items/', itemData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            response.value = resp.data
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
        // console.log(response.value)
    }
      return {addNewItem, error, response}
}
export default addItem