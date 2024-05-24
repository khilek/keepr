<script setup>
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

const keeps = computed(() => AppState.keeps)


async function getKeeps() {
  try {
    await keepsService.getKeeps()
  }
  catch (error) {
    Pop.toast("Couldn't get Keeps", 'error');
    logger.error(error)
  }
}

onMounted(() => {
  getKeeps()
})
</script>

<template>
  <div class="container-fluid">
    <section class="row justify-content-between">
      <div class="col-12 masonry mb-3 mt-3 p-3">
        <div v-for="keep in keeps" :key="keep.id">
          <KeepsCard :keep="keep" />

        </div>
      </div>
    </section>
  </div>
  <KeepCardModal />
</template>

<style scoped lang="scss">
// .home {
//   display: grid;
//   height: 80vh;
//   place-content: center;
//   text-align: center;
//   user-select: none;

//   .home-card {
//     width: clamp(500px, 50vw, 100%);

//     >img {
//       height: 200px;
//       max-width: 200px;
//       width: 100%;
//       object-fit: contain;
//       object-position: center;
//     }
//   }
// }

.masonry {
  columns: 300px;
  column-gap: .5em;
}

.masonry>* {
  -webkit-column-break-inside: avoid;
  page-break-inside: avoid;
  break-inside: avoid;
}
</style>
