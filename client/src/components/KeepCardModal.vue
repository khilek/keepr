<script setup>
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";

import { useRoute } from "vue-router";
import { keepsService } from "../services/KeepsService.js";
import { Keep } from "../models/Keep.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

const vk = computed(() => AppState.vaultKeeps)
defineProps({ keep: { type: Keep, required: true } })
const activeKeep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const route = useRoute()

async function eraseKeep(keepId) {
  try {

    const wantsToErase = await Pop.confirm('Are you sure you want to Erase?', 'There is no undoing this...', 'ERASE', "question")

    if (!wantsToErase) return

    logger.log('ERASING KEEP', AppState.activeKeep.id)

    await keepsService.eraseKeep(keepId)

  } catch (error) {
    Pop.toast("Couldn't Erase Keep")
    logger.error(error)
  }
}





</script>


<template>
  <!-- Button trigger modal -->


  <!-- Modal -->
  <div class="modal fade" id="keepCardModal" tabindex="-1" aria-labelledby="keepCardModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-fullscreen">
      <div class="modal-content" v-if="activeKeep">
        <div class="modal-header">
          <h1 class="modal-title fs-5 " id="keepCardModalLabel">{{ activeKeep.name }}</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body p-0">
          <div class="container-fluid">
            <div class="row">
              <div class="col-6 col-md-6">
                <img class="img-fluid rounded" :src="activeKeep.img" alt="Recipe-Image">
              </div>
              <div class="col-lg-6 col-md-6 col-sm-12 d-flex justify-content-center flex-column align-items-center">
                <h2 class="fw-bolder fs-1">{{ activeKeep.name }}</h2>
                <p> {{ activeKeep.description }}</p>
              </div>
            </div>
            <p class="text-end "><img class="me-2 creator-img" :src="activeKeep.creator.picture" alt="creator image">{{
              activeKeep.creator.name }}
            </p>
          </div>
        </div>
        <div class="modal-footer">
          <div class="text-start w-100 d-flex justify-content-between">
            <button @click="eraseKeep(AppState.activeKeep.id)" class=" btn btn-outline-danger"
              title="Full Send!">Erase</button>

            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
            <!-- <button type="button" class="btn btn-primary" @click="createVaultKeep()">Save
              changes</button> -->
            <!-- <h2><i role="button" title="Update a Keep!" data-bs-toggle="modal" data-bs-target="#UpdateRecipeForm"
                class="mdi mdi-plus-circle p-3"></i></h2> -->
          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.recipe-img {
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
</style>