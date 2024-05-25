<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import VaultCard from "../components/VaultCard.vue";
import { useRoute } from "vue-router";

const account = computed(() => AppState.account)
const activeVaults = computed(() => AppState.activeVault)
// const vaults = computed(() => AppState.vaults)
const route = useRoute()







async function getVaultById() {
  try {
    await vaultsService.getVaultById(route.params.vaultId)
  }
  catch (error) {
    Pop.toast("Couldn't Get Vaults By Id", 'error');
    logger.error(error)
  }
}




onMounted(() => {
  getVaultById()
})
</script>


<template>
  TESTING
</template>


<style lang="scss" scoped></style>