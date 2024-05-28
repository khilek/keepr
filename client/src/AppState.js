import { reactive } from 'vue'
import { Keep } from "./models/Keep.js"
import { Vault } from "./models/Vault.js"
import { VaultKeep } from "./models/VaultKeep.js"
import { Profile } from "./models/Profile.js"
import { VaultKeepRelationship } from "./models/VaultKeepRelationship.js"

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  /**@type {import('@bcwdev/auth0provider-client').Identity} */
  identity: null,
  /** @type {import('./models/Account.js').Account} user info from the database*/
  account: null,

  /**@type {Keep[]} */
  keeps: [],

  /**@type {Keep[]} */
  profileKeeps: [],

  /**@type {Vault[]} */
  profileVaults: [],

  /**@type {Keep} */
  activeKeep: null,

  /**@type {Vault[]} */
  vaults: [],

  /**@type {Vault} */
  activeVault: null,

  /**@type {VaultKeepRelationship[]} */
  activeKIPV: [],

  /**@type {VaultKeepRelationship[]} */
  activeKIPrivV: [],

  /**@type {VaultKeep[]} */
  vaultKeeps: [],

  /**@type {Profile} */
  activeProfile: null,
})