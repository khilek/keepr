<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import VaultCard from "../components/VaultCard.vue";
import { useRoute } from "vue-router";
import { accountService } from "../services/AccountService.js";

const account = computed(() => AppState.account)
const activeVaults = computed(() => AppState.activeVault)
const vaults = computed(() => AppState.vaults)
const route = useRoute()










async function getAccountVaults() {
  try {

    await accountService.getAccountVaults()
  } catch (error) {
    Pop.toast("Couldn't Get Account Vaults", 'error');
    logger.error(error)
  }
}




onMounted(() => {
  getAccountVaults()
})


</script>

<template>

  <div class="container">
    <section class="row justify-content-center">
      <div class="col-1 m-3 p-3 ">
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#AccountFormModal">
          Account Details
        </button>
      </div>
    </section>

    <div class="about text-center">
      <div v-if="account">
        <img class="cover-img" :src="account.coverImg" alt="">
        <h1>Welcome {{ account.name }}</h1>
        <img class="rounded profile-img" :src="account.picture" alt="" />
        <p>{{ account.email }}</p>
      </div>
      <div v-else>
        <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
      </div>
    </div>
    <section class="row">
      <h1 class="mb-3 p-3 text-center">My Vaults</h1>
      <div class="col-4" v-for="vault in vaults" :key="vault.id">
        <VaultCard :vault="vault" />
      </div>
    </section>

  </div>
  <AccountFormModal />

</template>

<style scoped lang="scss">
// img {
//   max-width: 100px;
// }

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

// .cover-img {
//   width: 100%;
//   height: 20vh;
//   margin-bottom: -10vh;
//   object-fit: cover;
//   object-position: center;
// }

// .profile-img {
//   height: 100px;
//   width: 100px;
//   object-fit: cover;
//   object-position: center;
// }</style>
