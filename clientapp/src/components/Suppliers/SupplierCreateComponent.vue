<template>
    <h1>Create Supplier</h1>
    <form @submit.prevent="createSupplier" class="mx-auto my-3 p-5 form-dark" style="width: 50rem;border: 1px solid grey;">
      <div class="row mb-3">
        <div class="col-md-6 mb-2">
          <label for="name" class="form-label">Name</label>
          <input type="text" class="form-control" v-model="formData.name" required>
        </div>
        <div class="col-md-6 mb-2">
          <label for="email" class="form-label">Email</label>
          <input type="text" class="form-control" v-model="formData.e_mail" required pattern="^[^\s@]+@[^\s@]+\.[^\s@]+$">
        </div>
      </div>
      <div class="row mb-3 mb-2">
        <div class="col-md-6">
          <label for="phone" class="form-label">Phone</label>
          <input type="number" class="form-control" v-model="formData.phone_number" required pattern="^\+?[0-9]{7,15}$">
        </div>
        <div class="col-md-6 mb-2">
          <label for="adress" class="form-label">Address</label>
          <input type="text" class="form-control" v-model="formData.adress" required>
        </div>
      </div>
      <div class="row">
        <div class="col-6">
          <div class="form-check">
            <input class="form-check-input" type="checkbox" required>
            <label class="form-check-label" for="invalidCheck2">
              Agree to terms and conditions
            </label>
          </div>
        </div>
        <div class="col-6">
          <button class="btn btn-primary" type="submit">Create</button>
        </div>
      </div>
    </form>
  </template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
  name: "SupplierCreateComponent",
    data(){
        return {
            formData:{
                name:'',
                e_mail:'',
                phone_number:'',
                adress:''
            }
        }
    },
    methods:{
        createSupplier(){
          const params = {
            name: this.formData.name,
            e_mail: this.formData.e_mail,
            phone_number:this.formData.phone_number.toString(),
            adress: this.formData.adress
          }
            axios.post('http://localhost:5280/api/supplier/supplier-create',params,{headers:{'Content-Type':'application/json'}})
            .then(response => {
              if (response.data.token.length===36) {
                swal({title: "Succesfully added supplier",icon: "success"});
              }
              else{
                swal({title: "There is a customer with this e-mail and name",icon: "error"});
              }
            })
            .catch(error => {
                swal({title: "Error added supplier",icon: "error"});
                console.log("Error creating supplier",error)
            })
        }
    }
}
</script>
<style>
</style>