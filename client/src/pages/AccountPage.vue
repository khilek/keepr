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









async function getVaultById(vaultId) {
  try {
    route.params.vaultId
    await vaultsService.getVaultById(vaultId)
  } catch (error) {
    Pop.toast("Couldn't Get Vaults By Id", 'error');
    logger.error(error)
  }
}




onMounted(() => {
  getVaultById()
})


</script>

<template>

  <div class="container">
    <section class="row">
      <div class="col-12" v-for="activeVault in activeVaults" :key="activeVault.id">
        <VaultCard :activeVault="activeVault" />
      </div>
    </section>
  </div>
  <button @click="getVaultById()"></button>

  <div class="about text-center">
    <div v-if="account">
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>

  <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#AccountFormModal">
    Launch demo modal
  </button>
  <AccountFormModal />

</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}


.cover-img {
  width: 100%;
  height: 20vh;
  margin-bottom: -10vh;
  object-fit: cover;
  object-position: center;
}

.profile-img {
  height: 100px;
  width: 100px;
  object-fit: cover;
  object-position: center;
}
</style>
