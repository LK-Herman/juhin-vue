import { ref } from '@vue/reactivity'

const getDeliveriesList = (url, token) =>{

    const deliveries = ref([])
    const error = ref(null)

    const loadDeliveries = async () => {
        try {
              let data = await fetch(url + 'deliveries/', {
                headers: {'Authorization':'Bearer ' + token,
                          'Accept':'*/*'
                }
              })
               if (!data.ok){
                throw Error('No data available')
                }
              deliveries.value = await data.json()
              console.log(deliveries.value)
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }
      return {loadDeliveries, error, deliveries}
}
export default getDeliveriesList