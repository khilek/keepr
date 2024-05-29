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
import { router } from "../router.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

const account = computed(() => AppState.account)
const vault = computed(() => AppState.activeVault)
const vks = computed(() => AppState.activeKIPV)
// const vaults = computed(() => AppState.vaults)
const vaultKeep = computed(() => AppState.vaultKeeps)

const route = useRoute()





async function getVaultById() {
  try {
    console.log(route.params.vaultId)
    await vaultsService.getVaultById(route.params.vaultId)
  }
  catch (error) {
    if (error.response.data.includes('😉')) {
      router.push({ name: 'Home' })
      Pop.toast("Couldn't Get Vaults By Id", 'error');

    } else {

      logger.error(error)

    }
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

async function getKeepsInPrivVault() {
  try {

    await vaultsService.getKeepsInPrivVault(route.params.vaultId)
  }
  catch (error) {

    Pop.toast("Couldn't Get Keeps in Private Vault", 'error');
    logger.error(error)
  }
}

async function eraseVaultKeep(vaultKeepId) {
  try {

    const wantsToErase = await Pop.confirm('Are you sure you want to Erase?', 'There is no undoing this...', 'ERASE', "question")

    if (!wantsToErase) return

    logger.log('ERASING KEEP',)

    await vaultKeepsService.eraseVaultKeep(vaultKeepId)

  } catch (error) {
    Pop.toast("Couldn't Erase VaultKeep")
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
        <button class="text-start w-25" @click="eraseVaultKeep(vk.vaultKeepId)"> Erase Vault Keep</button>
      </div>
    </section>
  </div>
  <KeepCardModal />
</template>


<style lang="scss" scoped>
.keep-img {
  height: 100%;
  width: 100%;
  object-fit: contain;

}
</style>