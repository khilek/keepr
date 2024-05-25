<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";


const router = useRouter()



const vaultData = ref({
  name: '',
  description: '',
  img: '',
  isPrivate: ''
})

function resetForm() {
  vaultData.value = { name: '', description: '', img: '', isPrivate: '' }
}

async function createVault() {
  try {
    const newVaults = await vaultsService.createVault(vaultData.value)
    resetForm()

    vaultData.value = {
      name: '',
      description: '',
      img: '',
      isPrivate: ''
    }

    Modal.getOrCreateInstance('#VaultFormModal').hide()

    Pop.toast("Vault Created!", 'success')



  } catch (error) {
    Pop.toast("Couldn't Create Keep", 'error')
    logger.error(error)
  }

}


</script>


<template>



  <div class="modal fade" id="VaultFormModal" tabindex="-1" aria-labelledby="VaultFormModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="VaultFormModalLabel">Create a new Vault!</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">

          <form @submit.prevent="createVault()">

            <div class="form-floating mb-3">
              <input type="text" v-model="vaultData.name" class="form-control"
                placeholder="What's the name of your recipe?" id="vaultName" required minLength="2" maxLength="500">
              <label for="vaultName">Vault Name</label>
            </div>
            <div class="form-floating mb-3">
              <textarea type="text" v-model="vaultData.description" class="form-control"
                placeholder="Description of Keep" id="vaultDescription" required minLength="2"
                maxLength="1000"></textarea>
              <label for="vaultDescription">Vault Description</label>
            </div>
            <div class="form-floating mb-3">
              <textarea v-model="vaultData.img" type="url" class="form-control" required placeholder="Image"
                id="vaultImg" maxLength="1000"></textarea>
              <label for="vaultImg">Vault Cover Image</label>
            </div>
            <div class="form-check form-switch">
              <input v-model="vaultData.isPrivate" class="form-check-input" type="checkbox" role="switch"
                id="vaultIsPrivate" required>
              <label class="form-check-label" for="vaultIsPrivate">Make A Private Vault?</label>
            </div>





            <div class="text-end">
              <button class="btn btn-success" type="submit">Submit</button>
            </div>
          </form>


        </div>
      </div>
    </div>
  </div>



</template>


<style lang="scss" scoped></style>