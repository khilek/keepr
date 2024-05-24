import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {

  async createKeep(keepData) {
    const response = await api.post('api/keeps', keepData)
    logger.log("Creating Keep", response.data)
    const newKeep = new Keep(response.data)
    AppState.keeps.push(newKeep)
    return newKeep
  }

  async getKeeps() {
    const response = await api.get("api/keeps")
    logger.log("Getting Keeps", response.data)
    const keeps = response.data.map(keepData => new Keep(keepData))
    AppState.keeps = keeps
  }

  async getKeepById(keepId) {
    AppState.activeKeep = null
    const response = await api.get(`api/keeps/${keepId}`)
    logger.log("Getting Keeps", response.data)
    const activeKeep = new Keep(response.data)
    AppState.activeKeep = activeKeep
  }

  async eraseKeep(keepId) {
    const response = await api.delete(`api/keeps/${keepId}`)
    logger.log("Erasing Keep", response.data)
    const keeps = AppState.keeps
    const keepToIndex = keeps.findIndex(keep => keep.id == keepId)
    if (keepToIndex == -1) throw new Error("Couldn't Find Index")
    keeps.splice(keepToIndex, 1)
  }












}
export const keepsService = new KeepsService()