import { ref } from '@vue/reactivity'

const getCurrentUser = (url) =>{

    const user = ref([])
    const error = ref(null)

    const getUser = async () => {
        try {
            
              let data = await fetch(url + 'accounts/userInfo', {
                headers: {
                          'Content-Type':'application/json',
                          'Accept':'*/*',
                          'Accept-Encoding':'gzip, deflate, br',
                          'Connection':'keep-alive'
                        }
                    })
              console.log(data)
               if (!data.ok){
                throw Error('Brak danych użytkownika')
                }
              user.value = await data.json()
          
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {getUser, error, user}
}

export default getCurrentUser