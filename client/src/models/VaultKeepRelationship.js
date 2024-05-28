import { Keep } from "./Keep.js";

export class VaultKeepRelationship extends Keep {
  constructor(data) {
    super(data)
    this.vaultKeepId = data.vaultKeepId
  }
}