<template>
    <h1>Stock Create</h1>
    <form @submit.prevent="createStock" class="mx-auto my-3 p-5 bg-dark" style="width: 50rem;border: 1px solid grey;">
      <div class="row mb-3">
        <div class="col-md-12 mb-2">
          <label for="validationDefault01" class="form-label">Product Name</label>
          <input type="text" class="form-control" v-model="formData.product_name" required>
        </div>
        <label for="supplier" class="form-label">Select Supplier:</label>
        <div class="row mb-3" style="margin-left:0px;padding: 0px;">
            <v-select :options="supplier" label="name" v-model="formData.supplier_id" :reduce="supplier => supplier.id" placeholder="Select Supplier" style="color:black"></v-select>
        </div>
        <div class="col-md-12 mb-2">
          <label for="validationDefault02" class="form-label">Quantity</label>
          <input type="text" class="form-control" v-model="formData.quantity" required>
        </div>
      </div>
      <div class="row mb-3 mb-2">
        <div class="col-md-12">
          <label for="validationDefault03" class="form-label">Unit Price</label>
          <input type="text" class="form-control" v-model="formData.unit_price" required>
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
        <div class="col-12 mt-2">
          <button class="btn btn-primary" type="submit">Create</button>
        </div>
      </div>
    </form>
  </template>
<script>
import axios from 'axios';
import vSelect from 'vue-select';
import 'vue-select/dist/vue-select.css';
import swal from 'sweetalert';
export default {
  name: "StockCreateComponent",
  components:{
    vSelect,
  },
    data(){
        return {
            formData:{
                supplier_name:'',
                supplier_id: null,
                product_name:'',
                quantity:'',
                unit_price:''
            },
            supplier:[],
            data:[]
        }
    },
    created(){
        this.getSupplier();
    },
    watch:{
      'formData.supplier_id'(newValue){
        console.log(newValue)
        this.formData.supplier_name = this.supplier.find(supplier => supplier.id === newValue)?.name || '';
      }
    },
    methods:{
        getSupplier(){
            axios.get('http://localhost:5280/api/supplier/supplier-get')
            .then(response => {
              if (response && response.data) {
                this.supplier = response.data.map(item => ({ id: item.supplier_id, name: item.name }));
              }
            });
        },
        createStock() {
            const params = {
              supplier_id: this.formData.supplier_id,
              supplier_name: this.formData.supplier_name,
              product_name: this.formData.product_name,
              quantity: parseInt(this.formData.quantity, 10), 
              unit_price: parseFloat(this.formData.unit_price), 
            };
            console.log("params values",params);
            axios.post('http://localhost:5280/api/stock/stock-create', params, {
                headers: { 'Content-Type': 'application/json' }
            })
            .then(response => {
              swal({title:"Stocks created",icon:"success"})
                console.log("Stock created", response.data);
            })
            .catch(error => {
                console.log("Error creating stock", error.response ? error.response.data : error); // Hata mesajını detaylı göster
            });
        }
    }
}
</script>
<style>
.vs__dropdown-toggle {
  background-color: white!important;
  color: black!important;
  width: 100%;
}
</style>