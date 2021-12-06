import { createRouter, createWebHistory } from 'vue-router'
import Main from '../views/Main.vue'
import Login from '../views/Login.vue'
import Upcomming from '../views/Upcomming.vue'
import Vendors from '../views/Vendors.vue'

const routes = [
  {
    path: '/',
    name: 'Main',
    component: Main
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
    props: true
  },
  {
      path: '/upcomming',
      name: 'Upcomming',
      component: Upcomming,
      props: true
  },
  {
    path: '/vendors',
    name: 'Vendors',
    component: Vendors,
    props: true
}
 
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
