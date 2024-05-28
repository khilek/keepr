<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { Modal } from "bootstrap";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";


const router = useRouter()



const keepData = ref({
  name: '',
  description: '',
  img: ''
})

function resetForm() {
  keepData.value = { name: '', description: '', img: '' }
}

async function createKeep() {
  try {
    const newRecipe = await keepsService.createKeep(keepData.value)
    resetForm()

    keepData.value = {
      name: '',
      description: '',
      img: ''
    }

    Modal.getOrCreateInstance('#KeepFormModal').hide()

    Pop.toast("Keep Created!", 'success')



  } catch (error) {
    Pop.toast("Couldn't Create Keep", 'error')
    logger.error(error)
  }

}


</script>


<template>



  <div class="modal fade" id="KeepFormModal" tabindex="-1" aria-labelledby="KeepFormModalLabel" aria-hidden="true">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="KeepFormModalLabel">Create a new Keep!</h1>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">

          <form @submit.prevent="createKeep()">

            <div class="form-floating mb-3">
              <input type="text" v-model="keepData.name" class="form-control"
                placeholder="What's the name of your recipe?" id="keepName" required minLength="2" maxLength="500">
              <label for="keepName">Keep Name</label>
            </div>
            <div class="form-floating mb-3">
              <textarea type="text" v-model="keepData.description" class="form-control"
                placeholder="Description of Keep" id="keepDescription" required minLength="2"
                maxLength="1000"></textarea>
              <label for="keepDescription">Keep Description</label>
            </div>
            <div class="form-floating mb-3">
              <textarea v-model="keepData.img" type="url" class="form-control" required placeholder="Image" id="keepImg"
                maxLength="1000"></textarea>
              <label for="keepImg">Keep Image</label>
            </div>





            <div class="text-end">
              <button class="btn btn-success" type="submit">Submit</button>
            </div>
          </form>


        </div>
      </div>
    </div>
  </div>



</template>


<style lang="scss" scoped></style>