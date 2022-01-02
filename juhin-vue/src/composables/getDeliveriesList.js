import { ref } from '@vue/reactivity'
import getVendorById from '../composables/getVendorById'
import moment from 'moment'
import axios from 'axios'

const getDeliveriesList = (url, token) =>{

    const deliveries = ref([])
    const error = ref(null)
    const totalRecords = ref('')
    const lastPage = ref('')

    const loadDeliveries = async (pageNo, recordsPerPage) => {

        try {
            let resp = await axios.get(url + 'deliveries?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
                headers: {'Authorization':'Bearer ' + token,
                'Accept':'*/*',
                'Access-Control-Allow-Origin':'*' }})
            //console.log(resp)
            if (resp.status <200 & resp.status > 300){
            throw Error('Coś poszło nie tak..')
            }
            totalRecords.value = resp.headers["all-records"]
            lastPage.value = resp.headers["totalamountpages"]
            deliveries.value = resp.data
            
        } catch (er) {
            error.value = er.message
            //console.log(error.value)
            }

        deliveries.value.forEach(delivery => {
            delivery.etaDate = moment(delivery.etaDate).format('YYYY-MM-DD')
            delivery.createdAt = moment(delivery.createdAt).format('YYYY-MM-DD')
            delivery.purchaseOrders.forEach(order => {
                fetch(url + 'vendors/' + order.vendorId, { headers: {'Authorization':'Bearer ' + token, 'Accept':'*/*', 'Access-Control-Allow-Origin':'*' }})
                    .then((res)=>res.json())
                    .then(data => order['vendorData'] = data)
                    .catch(err => console.log(err.message))
            });
            fetch(url+'forwarders/'+delivery.forwarderId, { headers: {'Authorization':'Bearer ' + token, 'Accept':'*/*', 'Access-Control-Allow-Origin':'*' }})
                .then((res)=>res.json())
                .then(data => delivery['forwarderName'] = data.name)
                .catch(err => console.log(err.message))
            fetch(url+'statuses/'+delivery.statusId, { headers: {'Authorization':'Bearer ' + token, 'Accept':'*/*', 'Access-Control-Allow-Origin':'*' }})
                .then((res)=>res.json())
                .then(data => delivery['statusName'] = data.name)
                .catch(err => console.log(err.message))
            // console.log(delivery)
        });
      }
    //   console.log('TOTAL RECORDS:'+totalRecords.value)
      return {loadDeliveries, error, deliveries, totalRecords}
}
export default getDeliveriesList