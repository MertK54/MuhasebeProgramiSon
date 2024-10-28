<template>
  <h1>Create Sale Invoice</h1>
  <form @submit.prevent="createSaleInvoice" class="mx-auto my-3 p-5 bg-dark" style="width: 50rem; border: 1px solid grey;">
    <div class="row mb-3">
      <div class="col-md-6">
        <label for="supplier" class="form-label">Select Supplier:</label>
        <v-select :options="supplier" label="name" v-model="formData.supplier_id" :reduce="supplier => supplier.id" placeholder="Select Supplier" style="color: black;"></v-select>
      </div>

      <div class="col-md-6">
        <label for="customer" class="form-label">Select Customer:</label>
        <v-select :options="customer" label="name" v-model="formData.customer_id" :reduce="customer => customer.id" placeholder="Select Customer" style="color: black;"></v-select>
      </div>
    </div>
    
    <div class="row mb-3">
      <label for="product_name" class="form-label">Select Product:</label>
      <v-select :options="stocks" label="product_name" v-model="formData.product_name" :reduce="stock => stock.product_name" placeholder="Select Product" style="color: black;"></v-select>
    </div>

    <div class="row mb-3">
      <div class="col-md-12">
        <label for="validationDefault02" class="form-label">Quantity</label>
        <input type="number" class="form-control" v-model="formData.quantity" required />
        <small style="color: grey;">Available stock: {{ this.currentStock.quantity }}</small>
      </div>
      
      <div class="col-md-12 mt-2">
        <label for="validationDefault03" class="form-label">Unit Price</label>
        <input type="number" class="form-control" v-model="formData.unit_price" required step="0.01" />
      </div>
    </div>

    <div class="row">
      <div class="col-6">
        <div class="form-check">
          <input class="form-check-input" type="checkbox" required />
          <label class="form-check-label" for="invalidCheck2"> Agree to terms and conditions </label>
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
  name: "InvoiceSaleComponent",
  components:{
    vSelect,
  },
    data(){
        return {
            formData:{
                customer_id:null,
                supplier_id:null,
                product_name:'',
                quantity:0,
                unit_price:0.00,
                total_amount:0.00,
                invoice_type:'sale'
            },
            supplier:[],
            data:[],
            stocks: [],
            customer:[],
            currentStock:[]
        }
    },
    created(){
        this.getCustomer();
        this.getStock();
    },
    watch: {
    'formData.product_name'(newValue) {
      this.currentStock = this.stocks.find(stock => stock.product_name === newValue);
      if (this.currentStock) {
        this.formData.unit_price = this.currentStock.unit_price;
      }
    }
  },
    methods:{
        getStock(){
            axios.get('http://localhost:5280/api/stock/stock-get')
            .then(response => {
              if (response && response.data) {
                this.stocks = response.data;
                this.supplier = response.data.map(item => ({ id: item.supplier_id, name: item.supplier_name }))
              }
            });
        },
        getCustomer(){
            axios.get('http://localhost:5280/api/customer/customer-get')
            .then(response => {
              if(response != null && response.data != null)
                this.customer = response.data.map(item => ({ id:item.customer_id,name: item.name }))
            })
            .catch(error => {
              swal({title:"customer not listed",text: "error when listing customer",icon:"warning",dangerMode:true})
              console.error("customers not get", error);
            });
        },
        createSaleInvoice() {
          this.formData.total_amount = this.formData.quantity * this.formData.unit_price;
          console.log("Form Data:", this.formData);
          const stockToUpdate = this.stocks.find(stock => stock.product_name === this.formData.product_name);
          if (stockToUpdate && this.formData.quantity > stockToUpdate.quantity) {
              swal({title: "Stock Limit Exceeded",text: `You cannot sell more than ${stockToUpdate.quantity} items.`,icon: "warning",dangerMode: true });
              return; 
          }
            const params = {
              supplier_id: this.formData.supplier_id,
              customer_id: this.formData.customer_id,
              product_name: this.formData.product_name,
              quantity: parseInt(this.formData.quantity, 10),
              unit_price: parseFloat(this.formData.unit_price),
              total_amount:parseFloat(this.formData.total_amount),
              type:'sale'
            };
            console.log("Params being sent:", params);
            axios.post('http://localhost:5280/api/invoice/invoice-create-sale', params, {headers: { 'Content-Type': 'application/json' }})
            .then(response => {
              swal({title:"Invoce created",icon:"success"})
              console.log("Invoce created", response.data);
              const stockToUpdate = this.stocks.find(stock => stock.product_name == params.product_name);
              console.log(stockToUpdate);
              const updatedQuantity = stockToUpdate.quantity - params.quantity;
                if (stockToUpdate) {
                    this.updateStock(stockToUpdate.stock_id, updatedQuantity ,params.unit_price);
                } else {
                    console.error("Stock not found for product:", params.product_name);
                }
            })
            .catch(error => {
                console.log("Error creating invoce", error.response ? error.response.data : error); 
            });

        },
        updateStock(id, quantitySold, unitPrice) {
          const params = {
              stock_id: id,
              quantity: quantitySold,
              unit_price: unitPrice
          }
          console.log("params değerleri:"+params);
          axios.post('http://localhost:5280/api/stock/stock-update',params, {headers: { 'Content-Type': 'application/json' }})
          .then(response => {
              console.log("Stock updated", response.data);
          })
          .catch(error => {
              console.log("Error updating stock", error.response ? error.response.data : error);
              swal({ title: "Stock update failed", text: "Could not update stock", icon: "error" });
          });
        }
    }
}
</script>
  
<style>
</style>
  