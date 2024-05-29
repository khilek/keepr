import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {





  async createVaultKeep(vaultKeepFormData) {
    console.log(vaultKeepFormData)
    const response = await api.post("api/vaultkeeps", vaultKeepFormData)
    logger.log("Creating VaultKeep", response.data)
    const newVK = new VaultKeep(response.data)
    if (AppState.activeKeep.id == newVK.vaultKeepId) {
      AppState.vaultKeeps.push(newVK)
    }
    const newKept = AppState.keeps.find(kept => kept.id == newVK.keepId)

    if (!newKept) { return }

    newKept.kept++

  }

  async eraseVaultKeep(vaultKeepId) {
    const response = await api.delete(`api/vaultkeeps/${vaultKeepId}`)
    logger.log("Erasing Vault", response.data)
    const vk = AppState.vaultKeeps
    const vaultKeepToIndex = vk.findIndex(vk => vk.vaultKeepId == vaultKeepId)
    if (vaultKeepToIndex == -1) throw new Error("Couldn't Find Index")
    vk.splice(vaultKeepToIndex, 1)
  }









}
export const vaultKeepsService = new VaultKeepsService()