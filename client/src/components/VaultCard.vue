<script setup>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { AppState } from '../AppState.js';





const vaults = computed(() => AppState.activeVault)
// const vaults = computed(() => AppState.activeVault)
// const vaults = computed(() => AppState.vaults)
const route = useRoute()


defineProps({ vault: { type: Vault, required: true } })

async function eraseVault(vaultId) {
  try {
    // FIXME log your vaultId parameter and make sure it is not undefined

    const wantsToErase = await Pop.confirm('Are you sure you want to Erase?', 'There is no undoing this...', 'ERASE', "question")

    if (!wantsToErase) return

    logger.log('ERASING VAULT', vaultId)

    await vaultsService.eraseVault(vaultId)

  } catch (error) {
    Pop.toast("Couldn't Erase Vault")
    logger.error(error)
  }
}


</script>


<template>
  <div v-if="vault" class="card mb-3">
    <img class="keep-img" :src="vault.img" :alt="vault.description">
    <h3 class="bottom-left text-light fw-bold shadow rounded text-center ">
      <RouterLink :title="vault.name" :to="{ name: 'Vault', params: { vaultId: vault.id } }">
        {{ vault.name }}
      </RouterLink>
      <p :title="vault.creator.name">by {{ vault.creator.name }}</p>
      <div class="text-start w-100" v-if="vault.creatorId === AppState.account?.id">
        <button @click="eraseVault(vault.id)" class=" btn btn-outline-danger rounded rounded-pill"
          title="Full Send Delete!"><i class="mdi mdi-delete-outline"></i></button>
      </div>
    </h3>
  </div>
</template>


<style lang="scss" scoped>
.keep-img {
  height: 100%;
  width: 100%;
  object-fit: contain;

}

.top-right {
  position: absolute;
  top: 8px;
  right: 16px;
}

.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
  text-decoration: underline;
}
</style>