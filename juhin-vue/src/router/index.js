import { createRouter, createWebHistory } from 'vue-router'
import Main from '../views/Main.vue'
import Login from '../views/Login.vue'
import Error404 from '../views/Error404.vue'

import Upcomming from '../views/Upcomming.vue'
import Deliveries from '../views/Deliveries.vue'
import DeliverySearch from '../views/DeliverySearch.vue'
import DeliverySchedule from '../views/DeliverySchedule.vue'
import DeliveryAdd from '../views/DeliveryAdd.vue'
import DeliveryUser from '../views/DeliveryUser.vue'
import Orders from '../views/Orders.vue'
import OrderAdd from '../views/OrderAdd.vue'
import Items from '../views/Items.vue'
import ItemAdd from '../views/ItemAdd.vue'
import Vendors from '../views/Vendors.vue'
import VendorAdd from '../views/VendorAdd.vue'
import Forwarders from '../views/Forwarders.vue'
import ForwarderAdd from '../views/ForwarderAdd.vue'
import ForwarderRanking from '../views/ForwarderRanking.vue'
import Created from '../views/Created.vue'

const routes = [
  {
    path: '/',
    name: 'Main',
    component: Main,
    props: true
  },
  {
    path: '/*',
    name: 'Error404',
    component: Error404
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    props: true
  },
  {
      path: '/deliveries/upcomming',
      name: 'Upcomming',
      component: Upcomming,
      props: true
  },
  {
      path: '/deliveries',
      name: 'Deliveries',
      component: Deliveries,
      props: true
  },
  {
      path: '/deliveries/search',
      name: 'DeliverySearch',
      component: DeliverySearch,
      props: true
  },
  {
      path: '/deliveries/add',
      name: 'DeliveryAdd',
      component: DeliveryAdd,
      props: true
  },
  {
      path: '/deliveries/schedule',
      name: 'DeliverySchedule',
      component: DeliverySchedule,
      props: true
  },
  {
      path: '/deliveries/user-deliveries',
      name: 'DeliveryUser',
      component: DeliveryUser,
      props: true
  },
  {
      path: '/orders',
      name: 'Orders',
      component: Orders,
      props: true
  },
  {
      path: '/orders/add',
      name: 'OrderAdd',
      component: OrderAdd,
      props: true
  },
  {
      path: '/items',
      name: 'Items',
      component: Items,
      props: true
  },
  {
      path: '/items/add',
      name: 'ItemAdd',
      component: ItemAdd,
      props: true
  },
  {
    path: '/vendors',
    name: 'Vendors',
    component: Vendors,
    props: true
  },
  {
    path: '/vendors/add',
    name: 'VendorAdd',
    component: VendorAdd,
    props: true
  },
  {
    path: '/forwarders',
    name: 'Forwarders',
    component: Forwarders,
    props: true
  },
  {
    path: '/forwarders/add',
    name: 'ForwarderAdd',
    component: ForwarderAdd,
    props: true
  },
  {
    path: '/forwarders/ranking',
    name: 'ForwarderRanking',
    component: ForwarderRanking,
    props: true
  },
  {
    path: '/ok',
    name: 'Created',
    component: Created,
    props: true
  }
 
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
