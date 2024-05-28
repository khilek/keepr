import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { Profile } from "../models/Profile.js"
import { Vault } from "../models/Vault.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class ProfilesService {


  async getProfileById(profileId) {
    AppState.activeProfile = null
    const response = await api.get(`api/profiles/${profileId}`)
    logger.log('Getting Profiles', response.data)
    AppState.activeProfile = new Profile(response.data)
    logger.log('Active Profile', AppState.activeProfile)
  }

  async getAUsersVaults(profileId) {
    const response = await api.get(`api/profiles/${profileId}/vaults`)
    logger.log("Getting User's Profile Vaults", response.data)
    const vaults = response.data.map(vault => new Vault(vault))
    AppState.profileVaults = vaults

  }

  async getAUsersKeeps(profileId) {
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    logger.log("Getting User's Profile Vaults", response.data)
    const keeps = response.data.map(keeps => new Keep(keeps))
    AppState.profileKeeps = keeps

  }

}

export const profilesService = new ProfilesService()