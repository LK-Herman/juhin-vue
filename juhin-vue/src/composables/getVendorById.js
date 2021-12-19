import { ref } from '@vue/reactivity'

const getVendorById = (url, token) =>{

    const vendor = ref([])
    const error = ref(null)

    const loadVendor = async (id) => {
        try {
              let data = await fetch(url + 'vendors/' + id, {
                headers: {'Authorization':'Bearer ' + token,
                          'Accept':'*/*'
                }
              })
               if (!data.ok){
                throw Error('No data available')
                }
              vendor.value = await data.json()
          
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }
      return {loadVendor, error, vendor}
}
export default getVendorById