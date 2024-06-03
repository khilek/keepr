<script setup>
import { computed } from "vue";
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";
import { useRoute } from "vue-router";
import { AppState } from '../AppState.js';

const pv = computed(() => AppState.profileVaults)
const vaults = computed(() => AppState.activeVault)
// const vaults = computed(() => AppState.activeVault)
// const vaults = computed(() => AppState.vaults)
const route = useRoute()


defineProps({ profileVault: { type: Vault, required: true } })

// async function eraseVault(vaultId) {
//   try {

//     const wantsToErase = await Pop.confirm('Are you sure you want to Erase?', 'There is no undoing this...', 'ERASE', "question")

//     if (!wantsToErase) return

//     logger.log('ERASING KEEP', vaultId)

//     await vaultsService.eraseVault(vaultId)

//   } catch (error) {
//     Pop.toast("Couldn't Erase Keep")
//     logger.error(error)
//   }
// }


</script>


<template>
  <div class="card ">
    <img class="keep-img" :src="profileVault.img" :alt="profileVault.description">
    <h3 class="bottom-left text-light fw-bold shadow rounded bg-dark ">
      <RouterLink :to="{ name: 'Vault', params: { vaultId: profileVault.id } }">
        {{ profileVault.name }}
      </RouterLink>
      <div>
        <button @click="eraseVault(vaults.id)" class=" btn btn-outline-danger" title="Full Send!">Erase</button>
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


.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
  text-decoration: underline;
}
</style>