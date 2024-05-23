<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";


defineProps({ keep: { type: Keep, required: true } })


</script>


<template>
  <div class="card">
    <img class="keep-img" :src="keep.img" :alt="keep.description" :title="keep.description">
    <div class="bottom-left text-light fw-bold shadow rounded bg-dark ">{{ keep.name }}</div>
    <img class="creator-img bottom-right" :src="keep.creator.picture" :title="keep.creator.name" alt="account image">
  </div>
</template>


<style lang="scss" scoped>
.keep-img {
  height: 100%;
  width: 100%;
  object-fit: contain;
}

.creator-img {
  height: 6vh;
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