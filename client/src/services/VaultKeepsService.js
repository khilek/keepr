import { AppState } from "../AppState.js"
import { VaultKeep } from "../models/VaultKeep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {




  async createVaultKeep(keepId, vaultId) {

    const response = await api.post("api/vaultkeeps")
    logger.log("Getting VaultKeeps in Public Vault", response.data)
    const vk = response.data.map(vk => new VaultKeep(vk))
    AppState.vaultKeeps = vk
  }











}
export const vaultKeepsService = new VaultKeepsService()