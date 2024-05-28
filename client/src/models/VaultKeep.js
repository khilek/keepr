export class VaultKeep {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.vaultKeepId = data.vaultKeepId
    this.creatorId = data.creatorId

  }
}