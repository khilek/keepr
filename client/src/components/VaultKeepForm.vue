<script setup>
import { computed, ref } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { useRoute } from "vue-router";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Keep } from "../models/Keep.js";

defineProps({ keep: { type: Keep, required: true } })

const route = useRoute()
const activeKeep = computed(() => AppState.activeKeep)

const myVaults = computed(() => AppState.profileVaults.filter(vault => vault.creatorId == AppState.account?.id))

const vaults = computed(() => AppState.vaults.filter(vault => vault.creatorId == AppState.account?.id))


const vaultKeepFormData = ref({
  vaultId: 0
})



async function createVaultKeep() {
  try {
    console.log(vaultKeepFormData.value, activeKeep.value.id)
    await vaultKeepsService.createVaultKeep(vaultKeepFormData.value)
    vaultKeepFormData.value = {
      vaultId: 0
    }
  }
  catch (error) {
    Pop.toast("Couldn't Create VaultKeep", 'error')
    logger.error(error)
  }
}




</script>


<template>
  <form @submit.prevent="createVaultKeep()">
    <select v-model="vaultKeepFormData.vaultId" class="form-select mb-3" aria-label="Default select example" required>
      <option v-for="myVault in myVaults" :key="myVault.id" :value="myVault.id">
        {{ myVault.name }}
      </option>
    </select>
    <button type="submit" class="btn btn-primary">Submit</button>
  </form>
</template>


<style lang="scss" scoped></style>
