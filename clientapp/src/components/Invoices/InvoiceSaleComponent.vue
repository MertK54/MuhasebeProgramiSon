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
      
      <div class="col-md-12 mt-2 mb-3">
        <label for="validationDefault03" class="form-label">Unit Price</label>
        <input type="number" class="form-control" v-model="formData.unit_price" required step="0.01" />
      </div>

      <div class="row mb-3">
        <label for="invoice_status" class="form-label">Select Status:</label>
        <v-select :options="invoice_status" v-model="formData.invoice_statu" placeholder="Select Status" style="color: black;"></v-select>
      </div>

      <div class="row mb-3">
        <label for="invoice_status" class="form-label">Select Payment:</label>
        <v-select :options="payment_methods" v-model="formData.payment_method" placeholder="Select Payment" style="color: black;"></v-select>
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
                stock_id: null,
                invoice_type:'sale',
                invoice_statu:null,
                payment_method:null
            },
            supplier:[],
            data:[],
            stocks: [],
            customer:[],
            currentStock:[],
            invoice_status:['pending','approved'],
            payment_methods:['credit card','cash payment','remittance']
        }
    },
    async created(){
        await this.getCustomer();
        await this.getStock();
        await this.getSupplier();
    },
    watch: {
    'formData.product_name'(newValue) {
      this.currentStock = this.stocks.find(stock => stock.product_name === newValue);
      if (this.currentStock) {
        this.formData.unit_price = this.currentStock.unit_price;
        this.formData.stock_id = this.currentStock.stock_id;
      }
    },
    'formData.supplier_id'(newValue) {
      this.stocks = this.stocks.filter(stock => stock.supplier_id === newValue);
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
        getStock(){
            axios.get('http://localhost:5280/api/stock/stock-get')
            .then(response => {
              if (response && response.data) {
                this.stocks = response.data;
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
          if (this.formData.quantity > this.currentStock.quantity) {
              swal({title: "Stock Limit Exceeded",text: `You cannot sell more than ${this.currentStock.quantity} items.`,icon: "warning",dangerMode: true });
              return; 
          }
          const checkNull = Object.values(this.formData).some(value => value === null);
          if(checkNull){
            swal({title: "Fill all field!" ,text:"Please fill all field",icon: "warning",dangerMode: true });
          }
            const params = {
              supplier_id: this.formData.supplier_id,
              customer_id: this.formData.customer_id,
              product_name: this.formData.product_name,
              quantity: parseInt(this.formData.quantity, 10),
              unit_price: parseFloat(this.formData.unit_price),
              total_amount:parseFloat(this.formData.total_amount),
              stock_id: this.formData.stock_id,
              invoice_type:'sale',
              invoice_statu:this.formData.invoice_statu,
              payment_method:this.formData.payment_method
            };
            console.log("Params being sent:", params);
            axios.post('http://localhost:5280/api/invoice/invoice-create-sale', params, {headers: { 'Content-Type': 'application/json' }})
              .then(response => {
                swal({title:"Invoice created",icon:"success"})
                console.log("Invoice created", response.data);
                this.updateStock(params.stock_id,params.quantity,params.unit_price);
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
          axios.post('http://localhost:5280/api/stock/stock-update-reduce',params, {headers: { 'Content-Type': 'application/json' }})
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
  