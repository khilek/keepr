<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

const vk = computed(() => AppState.vaultKeeps)
defineProps({ keep: { type: Keep, required: true } })

async function getKeepById(keepId) {
  try {
    await keepsService.getKeepById(keepId)
  }
  catch (error) {
    Pop.toast("Couldn't Get Keeps", 'error');
    logger.error(error)
  }
}




</script>


<template>
  <div class="card mb-4 ">
    <img class="keep-img rounded" :src="keep.img" :alt="keep.description" :title="keep.description">
    <div class="bottom-left text-light fw-bold shadow rounded bg-dark " role="button" data-bs-toggle="modal"
      data-bs-target="#keepCardModal" @click="getKeepById(keep.id)">{{ keep.name }}</div>
    <RouterLink :to="{ name: 'Profile', params: { profileId: keep.creatorId } }">
      <img class="creator-img bottom-right" :src="keep.creator.picture" :title="keep.creator.name" alt="account image">
    </RouterLink>
  </div>

</template>


<style lang="scss" scoped>
.keep-img {
  height: 100%;
  width: 100%;
  object-fit: contain;
}

.creator-img {
  height: 4vh;
  aspect-ratio: 1/1;
  border-radius: 50%;
  object-fit: cover;
}

.bottom-left {
  position: absolute;
  bottom: 8px;
  left: 16px;
}

.bottom-right {
  position: absolute;
  bottom: 8px;
  right: 16px;
}

.masonry {
  columns: 200px;
  column-gap: .5em;
}
</style>