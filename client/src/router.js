import { createRouter, createWebHashHistory } from 'vue-router'
import { authGuard } from '@bcwdev/auth0provider-client'

function loadPage(page) {
  return () => import(`./pages/${page}.vue`)
}

const routes = [
  {
    path: '/',
    name: 'Home',
    component: loadPage('HomePage')
  },
  {
    path: '/about',
    name: 'About',
    component: loadPage('AboutPage')
  },
  {
    path: '/vaults/:vaultId',
    name: 'Vault',
    component: loadPage('VaultDetailsPage')
  },
  {
    path: '/profiles/:profileId',
    name: 'Profile',
    component: loadPage('ProfilePage')
  },
  // {
  //   path: '/vaults',
  //   name: 'Vault',
  //   component: loadPage('VaultsPage')
  // },
  {
    path: '/account',
    name: 'Account',
    component: loadPage('AccountPage'),
    beforeEnter: authGuard
  }
]

export const router = createRouter({
  linkActiveClass: 'router-link-active',
  linkExactActiveClass: 'router-link-exact-active',
  history: createWebHashHistory(),
  routes
})
