import { ref } from '@vue/reactivity'

const getDeliveryDetails = (url, token) =>{

    const delivery = ref([])
    const error = ref(null)
   

    const loadDetails = async (id) => {
        try {
              let data = await fetch(url + 'deliveries/' + id, {
                headers: {'Authorization':'Bearer ' + token,
                          'Accept':'*/*'
                }
              })
               if (!data.ok){
                throw Error('No data available')
                }
              delivery.value = await data.json()
          
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {loadDetails, error, delivery}
}

export default getDeliveryDetails