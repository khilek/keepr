import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Vault } from "../models/Vault.js"
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }



  async getAccountVaults() {
    const response = await api.get("/account/vaults")
    logger.log("Getting Accounts Vaults", response.data)
    const vaults = response.data.map(vault => new Vault(vault))
    AppState.vaults = vaults
  }


  async saveAccount(accountData) {
    const response = await api.put(`/account`, accountData)
    logger.log('Saving Account', response.data)
    AppState.account = new Account(response.data)

  }




}

export const accountService = new AccountService()
