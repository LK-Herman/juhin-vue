import { ref } from '@vue/reactivity'
import axios from 'axios'

const addPackedItem = (url, token) =>{

    const error = ref(null)

    const addItem =  async (packedItemData) => {

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
            let resp = await axios.post( url + 'packed/', packedItemData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            //console.log(resp.data)
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {addItem, error}
}
export default addPackedItem