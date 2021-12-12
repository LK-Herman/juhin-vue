import { ref } from '@vue/reactivity'

const getCurrentUser = (url) =>{

    const user = ref([])
    const error = ref(null)

    const getUser = async () => {
        var myHeaders = new Headers();
        myHeaders.append("Accept", "*/*")
        myHeaders.append("Access-Control-Allow-Origin", "*")
        myHeaders.append("Accept-Encoding", "gzip, deflate, br")
        myHeaders.append("Connection", "keep-alive")

        var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        
        mode:'cors'
        };
          
          await fetch(url + "accounts/userInfo/", requestOptions)
            .then(response => response.json())
            .then(result => console.log(result))
            .then(response => response = user.value)
            .catch(error => console.log('error', error));
    }

      return {getUser, error, user}
}

export default getCurrentUser