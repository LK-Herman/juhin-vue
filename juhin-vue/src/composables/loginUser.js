import { ref } from '@vue/reactivity'
import VueCookies from 'vue-cookies'

const loginUser = (url) =>{
    
    const loginData = ref([])
    const error = ref('')
    const token = ref('')
    
    const login = async (userEmail, password) => {
        const userData = {emailAddress:userEmail, password:password}
        var requestOptions = {
            method: 'POST',
            mode: 'cors',               
            headers: {'Accept':'*/*',
            'Content-Type':'application/json',
            'Access-Control-Allow-Credentials': 'true',
            'Accept':'*/*',
            'Accept-Encoding':'gzip, deflate, br',
            'Connection':'keep-alive'
        },
        body: JSON.stringify(userData)
    }
    
    try {
        let data = await fetch(url + 'accounts/Login/', requestOptions)
        if (!data.ok){
            if(data.status === 400){
                throw Error('Niepoprawne dane logowania (400)')}
                throw Error('Dane niedostępne')
            }
            loginData.value = await data.json()
            token.value = loginData.value.token
            error.value = ''
            
            $cookies.set(".AspNetCore.Identity.Application", "CfDJ8ErSUNaOIRpFuhZRypMlBQIQRjsP1xsjAnSoZb2xs3bes_lRxUAZs-cpMQxYrdWZXRLER_8DEchOFGD1VWDJDZJTPD4izCxEc9gWDWTDG_FT1e_BrFJ_u0t3DqMJpspjlSvXZSEWDiGXw-Y5N4XdxRz7PqIlhOEmhHrUaiDfFZYEslgTEaECI4rcriZbNlLyWFbde9ttI85G0Y4q9rK89QgguFPk-uqJz5Ky6J-h2N_7aTZ39b5Ge3CW-4EAXF35Jy-I358GvWcSV7riv9qsThK7ByLFg7xzr_MN--7k4TNi20Skn4s8bjADrOJhmvSm0HJmdkW3eLNuZe6DfYHHPF3zhOpJ-us5lrZoAnAmCKAvcqK4n-0Cd28fdhVgYboJb1_Ae2wimdXgolPru73VDZJCmGuaHUHRlRbZj9I003Jxt1hYGjk-T-L-OgdTkBJxXboikCRoTaEZcURcmW2JIai-p44JEUU0_m-LedhHf80aFWHKxTcQ2DAfA_R5qrEEcC6Ao4u5Dd2a665D8L4N4yfglZN4WFkp9EOy0O2fXPaeVId_cQtvOzvs2zit4u6D_KraaEAtzIridH7S_ju03ZxgT4MbBPpGMiPmiu7FEmH9087UEnJWXY2HylHPUFgJOgr4ebFaj7uk4Mek--wF6t9ARVcYLHcAciyndOnUomd7U63HDkMRV-OZjVOT9Ulk0w")
            
        } catch (er) {
            error.value = er.message
            console.log(error.value)
        }
    }
    
    return {login, error, loginData, token}
}
export default loginUser