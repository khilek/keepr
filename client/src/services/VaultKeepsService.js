import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {





  async createVaultKeep(vaultId, keepId) {
    const response = await api.post("api/vaultkeeps", keepId)
    logger.log("Getting VaultKeeps in Public Vault", response.data)
    const newVK = new VaultKeep(response.data)
    if (AppState.activeKeep.id == newVK.vaultKeepId) {
      AppState.vaultKeeps.push(newVK)
    }
    const newKept = AppState.keeps.find(kept => kept.id == newVK.keepId)

    if (!newKept) { return }

    newKept.kept++



  }











}
export const vaultKeepsService = new VaultKeepsService()