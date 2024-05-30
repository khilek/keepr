<script setup>
import { useRoute } from "vue-router";
import { profilesService } from "../services/ProfilesService.js";
import Pop from "../utils/Pop.js";
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import { vaultsService } from "../services/VaultsService.js";
import { logger } from "../utils/Logger.js";
import VaultCard from "../components/VaultCard.vue";
import KeepsCard from "../components/KeepsCard.vue";


const route = useRoute()
// const keeps = computed(() => AppState.keeps)
// const vaults = computed(() => AppState.vaults)
const profileKeeps = computed(() => AppState.profileKeeps)
const profileVaults = computed(() => AppState.profileVaults)
const profile = computed(() => AppState.activeProfile)

async function getProfileById() {
  try {
    await profilesService.getProfileById(route.params.profileId)
  } catch (error) {
    Pop.toast("Couldn't get Profile By Id", 'error')
  }
}

async function getKeepsInPubVault() {
  try {
    await vaultsService.getKeepsInPubVault(route.params.keepId)
  }
  catch (error) {
    Pop.toast("Couldn't Get Keeps in Public Vault By Id", 'error');
    logger.error(error)
  }
}

async function getAUsersVaults() {
  try {
    await profilesService.getAUsersVaults(route.params.profileId)
  }
  catch (error) {
    Pop.toast("Couldn't Get User's Profile Vaults", 'error');
    logger.error(error)
  }
}

async function getAUsersKeeps() {
  try {
    await profilesService.getAUsersKeeps(route.params.profileId)
  }
  catch (error) {
    Pop.toast("Couldn't Get User's Profile Keeps", 'error');
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

onMounted(() => {
  getProfileById()
  getAUsersKeeps()
  getAUsersVaults()
})
</script>


<template>

  <div class="container">
    <section v-if="profile" class="row">
      <img class="cover-img mt-3" :src="profile.coverImg" alt="">
      <div class="col text-center">
        <img class="profile-img rounded-5" :src="profile.picture" alt="">
      </div>
    </section>
    <section v-if="profile" class="row">
      <div class="col text-center">
        <h1>{{ profile.name }}</h1>
        <p>{{ profile.bio }}</p>
        <p></p>
      </div>
    </section>
  </div>
  <div class="container">
    <section v-if="profile" class="row ">
      <h1>Vaults</h1>
      <div class="col-4" v-for="vault in profileVaults" :key="vault.id">
        <VaultCard :vault="vault" />
      </div>
    </section>
    <section v-if="profile" class="row ">
      <h1>Keeps</h1>
      <div class="col-4" v-for="keep in profileKeeps" :key="keep.id">
        <KeepsCard :keep="keep" />

      </div>
    </section>
  </div>
  <KeepCardModal />
</template>


<style lang="scss" scoped>
.cover-img {
  width: 100%;
  height: 35vh;
  object-fit: cover;
  position: center;
}

.profile-img {
  width: 100px;
  height: 100px;
  object-fit: cover;
  position: center;
}
</style>