import moment from 'moment'
import { ref } from '@vue/runtime-core'
const formatDate = () =>{

    const dayOfWeek = ref('')
    const time = ref('')
    const date = ref('')

    const format = (d) => {
        date.value = moment(d).format('DD-MM-YYYY')
        dayOfWeek.value = moment(d).format('dddd')
        time.value = moment(d).format('hh:mm')
      }

      return {date, time, dayOfWeek, format}
}

export default formatDate