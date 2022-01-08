import { ref } from '@vue/reactivity'
import axios from 'axios'

const deleteVendorById = (url, token) =>{

    const error = ref(null)

    const deleteVendor =  async (vId) => {

        var requestOptions = {
        method: 'DELETE',
        headers: {
            "Accept":"*/*",
            'Content-Type':'application/json',
            "Access-Control-Allow-Origin": "*",
            "Authorization":"Bearer " + token
        },
        mode:'cors',
        };
        
        try {
            let resp = await axios.delete( url + 'vendors/'+ vId, requestOptions)
            if (resp.status <200 & resp.status > 300){
                throw Error('Coś poszło nie tak..')
            }
            console.log(resp)
        } catch (err) {
            error.value = err.message
            console.log(error.value)
        }    
    }
      return {deleteVendor, error}
}
export default deleteVendorById