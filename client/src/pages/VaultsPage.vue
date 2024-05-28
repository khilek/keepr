<script setup>
import { computed, onMounted, ref } from 'vue';
import { AppState } from '../AppState.js';
import { vaultsService } from "../services/VaultsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import VaultCard from "../components/VaultCard.vue";
import { useRoute } from "vue-router";
import { Modal } from "bootstrap";

const account = computed(() => AppState.account)
const vaults = computed(() => AppState.activeVault)
// const vaults = computed(() => AppState.vaults)
const route = useRoute()








// onMounted(() => {
//   getVaultById()
// })
</script>


<template>
  <div v-for="vault in vaults" :key="vault.id"> </div>
  <VaultCard :vault="vault" />
</template>


<style lang="scss" scoped></style>