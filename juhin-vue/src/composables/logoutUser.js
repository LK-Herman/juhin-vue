import { ref } from '@vue/reactivity'

const logoutUser = (url) =>{
    
    const logoutData = ref([])
    const error = ref('')
    
    const logout = async () => {
        
        try {
              let data = await fetch(url + 'accounts/Logout/', 
                { method: 'GET',
                  mode: 'cors'
                })
               
               if (!data.ok){
                   if(data.status === 404){
                       throw Error('Nieudana próba wylogowania (404)')}
                throw Error('Nastąpił błąd podczas próby wylogowania')
                }
              logoutData.value = await data.json()
              error.value = ''
        } catch (er) {
          error.value = er.message
          console.log(error.value)
        }
      }

      return {logout, error, logoutData}
}
export default logoutUser