import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {



  async getKeeps() {
    const response = await api.get("api/keeps")
    logger.log("Getting Keeps", response.data)
    const keeps = response.data.map(keepData => new Keep(keepData))
    AppState.keeps = keeps
  }
















}
export const keepsService = new KeepsService()