import { ref } from '@vue/reactivity'
import axios from 'axios'
import moment from 'moment'

const getDeliveryDetails = (url, token) =>{

    const delivery = ref([])
    const error = ref(null)
    const perror = ref(null)
    
    
    
    const loadDetails = async (id, palFlag) => {
        try {
            let resp = await axios.get(url + 'deliveries/detailed/' + id, {
                headers: {'Authorization':'Bearer ' + token,
                'Accept':'*/*',
                'Access-Control-Allow-Origin':'*' }})
            if (resp.status <200 & resp.status > 300){
            throw Error('Coś poszło nie tak..')
            }
            delivery.value = resp.data
            delivery.value['etaT'] = moment(delivery.value.etaDate).format('hh:mm')
            delivery.value['etaD'] = moment(delivery.value.etaDate).format('DD-MM-YYYY')
            delivery.value['createdAt'] = moment(delivery.value.createdAt).format('DD-MM-YYYY hh:mm')
            delivery.value['deliveryDate'] = moment(delivery.value.deliveryDate).format('DD-MM-YYYY hh:mm')
            if(delivery.value.etaDate < moment().toISOString() && delivery.value.statusId !== 3) {delivery.value['isDelayed'] = true}
            else{
                delivery.value['isDelayed'] = false
            }
            // console.log(delivery.value.etaDate)
            
            
        } catch (er) {
            error.value = er.message
            //console.log(error.value)
        }

        async function getPallets (itemId)  {
            let pallets = {eur:0,ori:0}
            try {
                let resp = await axios.get(url + 'items/' + itemId , {
                    headers: {'Authorization':'Bearer ' + token,
                    'Accept':'*/*',
                    'Access-Control-Allow-Origin':'*' }})
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                
                pallets["eur"] = resp.data.maxEuroPalQty
                pallets["ori"] = resp.data.palletQty
                
                } catch (er) {
                    perror.value = er.message
                }
            return pallets
        }

         // PALLETS
         if(palFlag){
            let eurPalletSum = 0
            let oriPalletSum = 0
            delivery.value["eurpal"] = eurPalletSum
            delivery.value["oripal"] = oriPalletSum
            delivery.value.packedItems.forEach(item => {
                //console.log(item)
                getPallets(item.itemId)
                    .then(result => {
                                eurPalletSum+= Math.ceil(item.quantity / result.eur)
                                oriPalletSum+= Math.ceil(item.quantity / result.ori)
                                //console.log(eurPalletSum)
                                delivery.value["eurpal"] = eurPalletSum
                                delivery.value["oripal"] = oriPalletSum
                                })
            });
                // delivery.value["eurpal"] = eurPalletSum,
                // delivery.value["oripal"] = oriPalletSum
            

            
            
        }
        
        // STATUS
        try {
            let resp = await axios.get(url + 'statuses/' + delivery.value.statusId, {
                headers: {'Authorization':'Bearer ' + token,
                'Accept':'*/*',
                'Access-Control-Allow-Origin':'*' }})
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                //console.log(resp.data)
                delivery.value['status'] = 'N/A'
                if(resp.data.name == 'Delivered') delivery.value['status'] = 'DORĘCZONA'
                if(resp.data.name == 'In transit') delivery.value['status'] = 'W DRODZE'
                if(resp.data.name == 'Created') delivery.value['status'] = 'NOWA'
            } catch (er) {
                error.value = er.message
            }
        
        // FORWARDER
        try {
            let resp = await axios.get(url + 'forwarders/' + delivery.value.forwarderId, {
                headers: {'Authorization':'Bearer ' + token,
                'Accept':'*/*',
                'Access-Control-Allow-Origin':'*' }})
                if (resp.status <200 & resp.status > 300){
                    throw Error('Coś poszło nie tak..')
                }
                delivery.value['forwarderName'] = resp.data.name
            } catch (er) {
                error.value = er.message
            }

       
        //console.log(delivery.value)
    }
    
    return {loadDetails, error, delivery}
}

export default getDeliveryDetails