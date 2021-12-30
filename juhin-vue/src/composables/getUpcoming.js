import { ref } from '@vue/reactivity'
import axios from 'axios'

const getUpcoming = (url, token) =>{

    const upcomingDeliveries = ref([])
    const error = ref(null)
    
    const loadUpcoming = async (date) => {
        try {
            let resp = await axios.get(url + 'deliveries/upcoming/' + date, {
                headers: {'Authorization':'Bearer ' + token,
                'Accept':'*/*',
                'Access-Control-Allow-Origin':'*' }})
            if (resp.status <200 & resp.status > 300){
            throw Error('Coś poszło nie tak..')
            }
            upcomingDeliveries.value = resp.data
           
            
            //console.log(upcomingDeliveries.value)
            
        } catch (er) {
            error.value = er.message
            //console.log(error.value)
        }
    }
    
    return {loadUpcoming, error, upcomingDeliveries }
}

export default getUpcoming