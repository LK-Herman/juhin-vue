import { ref } from '@vue/reactivity'

const loginUser = (url) =>{
    
    const loginData = ref([])
    const error = ref(null)
    const token = ref('')
    
    const login = async (userEmail, password) => {
        const userData = {emailAddress:userEmail, password:password}
        
        try {
              let data = await fetch(url + 'accounts/Login/', 
                { method: 'POST',
                  mode: 'cors',               
                  headers: {'Content-Type':'application/json',
                            'Accept':'*/*',
                            'Accept-Encoding':'gzip,deflate,br'
                            },
                  body: JSON.stringify(userData)
                })
               console.log(JSON.stringify(userData)) 
               if (!data.ok){
                throw Error('No data available')
                }
              loginData.value = await data.json()
              token.value = loginData.value.token
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {login, error, loginData, token}
}
export default loginUser