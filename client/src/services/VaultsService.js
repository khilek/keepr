import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { VaultKeepRelationship } from "../models/VaultKeepRelationship.js"
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
    const vault = new Vault(res.data)
    AppState.activeVault = vault
  }


  async updateVault(vaultId, vaultData) {
    const response = await api.put(`api/vaults/${vaultId}`, vaultData)
    logger.log("Updating Vault", response.data)
    const vaultToUpdate = new Vault(vaultId)
    AppState.activeVault = vaultToUpdate
    return vaultToUpdate
  }

  async eraseVault(vaultId) {
    const response = await api.delete(`api/vaults/${vaultId}`)
    logger.log("Erasing Vault", response.data)
    const vaults = AppState.vaults
    const vaultToIndex = vaults.findIndex(vault => vault.id == vaultId)
    if (vaultToIndex == -1) throw new Error("Couldn't Find Index")
    vaults.splice(vaultToIndex, 1)
  }


  async getKeepsInPubVault(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log("Getting Keeps in Public Vault", response.data)
    const vaultkeeps = response.data.map(vk => new VaultKeepRelationship(vk))
    AppState.activeKIPV = vaultkeeps
    // return vaultkeeps
  }


  async getKeepsInPrivVault(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log("Getting Keeps in Private Vault", response.data)
    const privateVK = response.data.map(pvk => new VaultKeepRelationship(pvk))
    AppState.activeKIPrivV = privateVK
  }
}


export const vaultsService = new VaultsService()