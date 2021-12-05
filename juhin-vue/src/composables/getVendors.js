import { ref } from '@vue/reactivity'

const getVendors = (url, token) =>{

    const vendors = ref([])
    const error = ref(null)

    const loadVendors = async () => {
        try {
              let data = await fetch(url + 'vendors/', {
                headers: {'Authorization':'Bearer ' + token,
                          'Accept':'*/*'
                }
              })
               if (!data.ok){
                throw Error('No data available')
                }
              vendors.value = await data.json()
          
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {loadVendors, error, vendors}
}

export default getVendors