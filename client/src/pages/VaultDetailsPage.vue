<script setup>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import VaultCard from "../components/VaultCard.vue";
import { useRoute } from "vue-router";
import { Modal } from "bootstrap";
import KeepsCard from "../components/KeepsCard.vue";

const account = computed(() => AppState.account)
const vault = computed(() => AppState.activeVault)
const vks = computed(() => AppState.activeKIPV)
// const vaults = computed(() => AppState.vaults)


const route = useRoute()





async function getVaultById() {
  try {
    console.log(route.params.vaultId)
    await vaultsService.getVaultById(route.params.vaultId)
  }
  catch (error) {
    Pop.toast("Couldn't Get Vaults By Id", 'error');
    logger.error(error)
  }
}

async function getKeepsInPubVault() {
  try {
    await vaultsService.getKeepsInPubVault(route.params.vaultId)
  }
  catch (error) {
    Pop.toast("Couldn't Get Keeps in Public Vault By Id", 'error');
    logger.error(error)
  }
}

onMounted(() => {
  getVaultById()
  getKeepsInPubVault()
})
</script>


<template>
  <div class="container">
    <section class="row">
      <div v-if="vault" class="col-12 p-3 mt-3 d-flex justify-content-center">
        <VaultCard :vault="vault" />
      </div>
    </section>
    <section class="row d-flex ">
      <div class="col-4 p-3 mt-3 img-fluid " v-for="vk in vks" :key="vk.id">
        <KeepsCard :keep="vk" />
      </div>
    </section>
  </div>

</template>


<style lang="scss" scoped>
.keep-img {
  height: 100%;
  width: 100%;
  object-fit: contain;

}
</style>