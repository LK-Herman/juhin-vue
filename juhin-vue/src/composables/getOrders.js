import { ref } from '@vue/reactivity'
import axios from 'axios'
import moment from 'moment'

const getOrders = (url, token) =>{

    const orders = ref([])
    const error = ref(null)
    const totalRecords = ref()
    const lastPage = ref()

    const loadOrders = async (pageNo, recordsPerPage) => {
       
        await axios.get(url + 'orders?Page='+pageNo+'&RecordsPerPage='+recordsPerPage, {
            headers: {'Authorization':'Bearer ' + token,
            'Accept':'*/*' }})
            .then(res => {
                orders.value = res.data
                totalRecords.value = res.headers["all-records"]
                lastPage.value = res.headers["totalamountpages"]
                })
            .catch(err => error.value = err)
       
              console.log(orders.value)

      }
    //   console.log('TOTAL RECORDS:'+totalRecords.value)
      return {loadOrders, error, orders, totalRecords, lastPage}
}
export default getOrders