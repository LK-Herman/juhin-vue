import { ref } from '@vue/reactivity'

const loginUser = (url) =>{
    
    const loginData = ref([])
    const error = ref('')
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
               
               if (!data.ok){
                   if(data.status === 400){
                       throw Error('Niepoprawne dane logowania (400)')}
                throw Error('Dane niedostępne')
                }
              loginData.value = await data.json()
              token.value = loginData.value.token
              error.value = ''
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {login, error, loginData, token}
}
export default loginUser