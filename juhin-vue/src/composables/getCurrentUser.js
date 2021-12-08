import { ref } from '@vue/reactivity'

const getCurrentUser = (url) =>{

    const user = ref([])
    const error = ref(null)

    const getUser = async () => {
        var myHeaders = new Headers();
        myHeaders.append("Cookie", ".AspNetCore.Identity.Application=CfDJ8ErSUNaOIRpFuhZRypMlBQIQRjsP1xsjAnSoZb2xs3bes_lRxUAZs-cpMQxYrdWZXRLER_8DEchOFGD1VWDJDZJTPD4izCxEc9gWDWTDG_FT1e_BrFJ_u0t3DqMJpspjlSvXZSEWDiGXw-Y5N4XdxRz7PqIlhOEmhHrUaiDfFZYEslgTEaECI4rcriZbNlLyWFbde9ttI85G0Y4q9rK89QgguFPk-uqJz5Ky6J-h2N_7aTZ39b5Ge3CW-4EAXF35Jy-I358GvWcSV7riv9qsThK7ByLFg7xzr_MN--7k4TNi20Skn4s8bjADrOJhmvSm0HJmdkW3eLNuZe6DfYHHPF3zhOpJ-us5lrZoAnAmCKAvcqK4n-0Cd28fdhVgYboJb1_Ae2wimdXgolPru73VDZJCmGuaHUHRlRbZj9I003Jxt1hYGjk-T-L-OgdTkBJxXboikCRoTaEZcURcmW2JIai-p44JEUU0_m-LedhHf80aFWHKxTcQ2DAfA_R5qrEEcC6Ao4u5Dd2a665D8L4N4yfglZN4WFkp9EOy0O2fXPaeVId_cQtvOzvs2zit4u6D_KraaEAtzIridH7S_ju03ZxgT4MbBPpGMiPmiu7FEmH9087UEnJWXY2HylHPUFgJOgr4ebFaj7uk4Mek--wF6t9ARVcYLHcAciyndOnUomd7U63HDkMRV-OZjVOT9Ulk0w");
        myHeaders.append("Access-Control-Allow-Origin", "http://localhost:8080")
        myHeaders.append("Accept", "*/*")
        myHeaders.append("Accept-Encoding", "gzip, deflate, br")
        myHeaders.append("Connection", "keep-alive")

        var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow',
        mode:'cors'
        };
          
          await fetch("https://juhin.zerwijzfrankiem.pl/api/accounts/userInfo/", requestOptions)
            .then(response => response.json())
            .then(result => console.log(result))
            .then(response => response = user.value)
            .catch(error => console.log('error', error));
    }

      return {getUser, error, user}
}

export default getCurrentUser