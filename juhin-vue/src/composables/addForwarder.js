import { ref } from '@vue/reactivity'
import axios from 'axios'

const addForwarder = (url, token) =>{

    const error = ref(null)
    const createdId = ref('')

    const addNewForwarder =  async (forwarderData) => {

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
            let resp = await axios.post( url + 'forwarders/', forwarderData, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            createdId.value = resp.data.forwarderId
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {addNewForwarder, error, createdId}
}
export default addForwarder