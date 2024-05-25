import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultsService {




  async createVault(vaultData) {
    const response = await api.post("api/vaults", vaultData)
    logger.log("Creating Vault", response.data)
    const newVaults = new Vault(response.data)
    AppState.vaults.push(newVaults)
    return newVaults
  }


  async getVaultById(vaultId) {
    AppState.activeVault = null
    const res = await api.get(`api/vaults/${vaultId}`)
    console.log(vaultId)
    logger.log("Getting Vault By Id", res.data)
    const activeVault = new Vault(res.data)
    AppState.activeVault = activeVault
  }









}


export const vaultsService = new VaultsService()