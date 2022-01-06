import { ref } from '@vue/reactivity'
import moment from 'moment'

const getOrders = (url, token) =>{

    const orders = ref([])
    const error = ref(null)
    const totalRecords = ref(1)

    const loadOrders = async (pageNo, recordsPerPage) => {
        await fetch(url + 'orders/', {
            headers: {'Authorization':'Bearer ' + token,
            'Accept':'*/*' }})
            .then(res => res.json())
            .then(data => totalRecords.value = data.length)
            .catch(err => error.value = err)

        await fetch(url + 'orders?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
            headers: {'Authorization':'Bearer ' + token,
            'Accept':'*/*' }})
            .then(res => res.json())
            .then(data => orders.value = data)
            .catch(err => error.value = err)
       
             console.log(orders.value)

      }
    //   console.log('TOTAL RECORDS:'+totalRecords.value)
      return {loadOrders, error, orders, totalRecords}
}
export default getOrders