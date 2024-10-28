<template>
    <h1>Create Customer</h1>
    <form @submit.prevent="createCustomer" class="mx-auto my-3 p-5" style="width: 50rem;border: 1px solid grey;">
      <div class="row mb-3">
        <div class="col-md-6 mb-2">
          <label for="validationDefault01" class="form-label">Name</label>
          <input type="text" class="form-control" v-model="formData.name" required>
        </div>
        <div class="col-md-6 mb-2">
          <label for="validationDefault02" class="form-label">Email</label>
          <input type="text" class="form-control" v-model="formData.e_mail" required pattern="^[^\s@]+@[^\s@]+\.[^\s@]+$">
        </div>
      </div>
      <div class="row mb-3 mb-2">
        <div class="col-md-6">
          <label for="phone" class="form-label">Phone</label>
          <input type="tel" class="form-control" v-model="formData.phone_number" required  pattern="^\+?[0-9]{7,15}$">
        </div>
        <div class="col-md-6 mb-2">
          <label for="validationDefault04" class="form-label">Address</label>
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
    name: "CustomerCreateComponent",
  data(){
    return{
        formData:{
            name:'',
            e_mail:'',
            phone_number:'',
            adress:''
        }
    }
  },
  methods:{
    createCustomer(){
      var params={
        name:this.formData.name,
        e_mail:this.formData.e_mail,
        phone_number:this.formData.phone_number,
        adress:this.formData.adress
      }
        axios.post('http://localhost:5280/api/customer/customer-create',params,{
            headers:{
                'Content-Type':'application/json'
            }}
        )
        .then(response => {
        if (response && response.data && response.data.token.length===36) {
          swal({title: "Succesfully added customer",icon: "success"});
        }
      })
        .catch(error => {
            console.log(error.data.token)
        })
    }
  }
};
  </script>
  
  <style>
  </style>
  