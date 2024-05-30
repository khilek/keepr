<script setup>
import { computed, ref } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { useRoute } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Keep } from "../models/Keep.js";
import { Modal } from "bootstrap";

defineProps({ keep: { type: Keep, required: true } })

const route = useRoute()
const activeKeep = computed(() => AppState.activeKeep)

// FIXME don't filter the array of vaults, you will need a seperate array of vaults for the logged in user, and you just need to compute that value in
const myVaults = computed(() => AppState.profileVaults.filter(vault => vault.creatorId == AppState.account?.id))

// FIXME don't filter the array of vaults, you will need a seperate array of vaults for the logged in user, and you just need to compute that value in
const vaults = computed(() => AppState.vaults.filter(vault => vault.creatorId == AppState.account?.id))


const vaultKeepFormData = ref({
  vaultId: 0,
  keepId: activeKeep.value.id
})



async function createVaultKeep() {
  try {
    console.log(vaultKeepFormData.value.vaultId, vaultKeepFormData.value.keepId)
    vaultKeepFormData.value.keepId = activeKeep.value.id
    await vaultKeepsService.createVaultKeep(vaultKeepFormData.value)
    Modal.getOrCreateInstance('#vaultKeepForm').hide
    Pop.success("VaultKeep Created!")
  }
  catch (error) {
    Pop.toast("Couldn't Create VaultKeep", 'error')
    logger.error(error)
  }
}




</script>


<template>
  <form @submit.prevent="createVaultKeep()">
    <div class="col-6 text-center">
      <select id="vaultKeepForm" v-model="vaultKeepFormData.vaultId" class="form-select mb-3"
        aria-label="Default select example" required>
        <option v-for="vault in vaults" :key="vault.id" :value="vault.id">
          {{ vault.name }}
        </option>
      </select>
      <button type="submit" class="btn btn-primary">Submit</button>
    </div>
  </form>
</template>


<style lang="scss" scoped></style>
