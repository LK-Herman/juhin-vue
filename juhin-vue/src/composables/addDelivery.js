import { ref } from '@vue/reactivity'

const addDelivery = (url, token) =>{

    const error = ref(null)
    const responseErrors =ref(null)
    const res = ref(null)

    let isJSON = false

    const addNewDelivery =  async (deliveryData, poId) => {
        var myHeaders = new Headers();
        myHeaders.append("Accept", "*/*")
        myHeaders.append('Content-Type', 'application/json')
        myHeaders.append("Access-Control-Allow-Origin", "*")
        myHeaders.append("Accept-Encoding", "gzip, deflate, br")
        myHeaders.append("Connection", "keep-alive")
        myHeaders.append("Authorization", "Bearer " + token)

        var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        mode:'cors',
        body: JSON.stringify(deliveryData)
        };
        
        try {
            let data = await fetch(url + "deliveries/"+ poId, requestOptions)
            console.log(data)
            res.value = await data.text()
            isJSON = false

            if((res.value.startsWith('{'))){
                res.value = JSON.parse(res.value)
                isJSON = true
            }
            console.log(res.value)

            if (!data.ok){
                responseErrors.value = res.value.errors
                throw Error(res.value.title?res.value.title:res.value)
            }
            
        } catch (er) {
            if(er.message.includes('validation')){
                er.message = 'błędnie wypełnione pola formularza'
            }
            error.value = er.message
            
            console.log(error.value)
        }
        
        if(responseErrors.value && isJSON){
            if(responseErrors.value.hasOwnProperty('VendorCode') && responseErrors.value.VendorCode[0].startsWith('V'))responseErrors.value.VendorCode[0] = 'Kod dostawcy jest wymagany'
            if(responseErrors.value.hasOwnProperty('VendorCode') && responseErrors.value.VendorCode[0].includes('maximum'))responseErrors.value.VendorCode[0] = 'Kod dostawcy powinien zawierać maksymalnie 5 znaków'
            if(responseErrors.value.hasOwnProperty('Country') && responseErrors.value.Country[0].startsWith('V'))responseErrors.value.Country[0] = 'Kraj dostawcy jest wymagany'
            if(responseErrors.value.hasOwnProperty('Name') && responseErrors.value.Name[0].startsWith('V'))responseErrors.value.Name[0] = 'Nazwa dostawcy jest wymagana'
            if(responseErrors.value.hasOwnProperty('Address') && responseErrors.value.Address[0].startsWith('V'))responseErrors.value.Address[0] = 'Dane adresowe są wymagane'
            if(responseErrors.value.hasOwnProperty('ShortName') && responseErrors.value.ShortName[0].startsWith('S'))responseErrors.value.ShortName[0] = 'Krótka nazwa jest wymagana'
        }
        if(error.value && !isJSON){
            if(error.value.includes('uplicate'))error.value = 'Podany kod dostawcy już istnieje'
        }
        
    }
      return {addNewDelivery, error, responseErrors}
}
export default addDelivery