export class Keep {
  constructor(data) {
    this.id = data.id
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.views = data.views
    this.creatorId = data.creatorId
    this.creator = data.creator
  }
}