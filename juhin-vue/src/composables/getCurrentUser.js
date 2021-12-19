import { ref } from '@vue/reactivity'

const getCurrentUser = () =>{

    const user = ref(null)
    const error = ref(null)

    const getUser = async (url, token) => {
        var myHeaders = new Headers();
        myHeaders.append("Accept", "*/*")
        myHeaders.append("Access-Control-Allow-Origin", "*")
        myHeaders.append("Accept-Encoding", "gzip, deflate, br")
        myHeaders.append("Connection", "keep-alive")
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        mode:'cors'
        };

        try {
            let data = await fetch(url + "accounts/userInfo/", requestOptions)
             if (!data.ok){
              throw Error('No data available')
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