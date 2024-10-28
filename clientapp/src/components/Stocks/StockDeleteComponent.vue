<template>
    <table class="table table-dark">
       <thead>
         <tr>
           <th class="p-3">Product Name</th>
           <th>Supplier</th>
           <th>Supplier Id</th>
           <th>Quantity</th>
           <th>Unit Price</th>
           <th>Delete</th>
         </tr>
       </thead>
       <tbody>
         <tr v-for="(stock, index) in stocks" :key="index">
           <td class="p-3">{{ stock.product_name }}</td>
           <td>{{ stock.supplier_name}}</td>
           <td>{{ stock.supplier_id}}</td>
           <td>{{ stock.quantity }}</td>
           <td>{{ stock.unit_price }}</td>
           <td><button class="btn btn-danger" @click="removeStock(stock.stock_id)">Remove</button></td>
         </tr>
       </tbody>
     </table>
 </template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
    name: "StockDeleteComponent",
    data(){
        return{
            stocks:[]
        }
    },
    mounted() {
     this.getStock();
     },
     methods:{
        getStock(){
             axios.get('http://localhost:5280/api/stock/stock-get')
             .then(response => {
               if(response != null && response.data != null)
                 this.stocks = response.data;
             })
             .catch(error => {
               swal({title:"Stocks not listed",text: "error when listing stock",icon:"warning",dangerMode:true})
             console.error("stocks not get", error);
             });
        },
        removeStock(stockId){
            axios.delete('http://localhost:5280/api/stock/stock-delete', {
                data: { stock_id: stockId }, 
                headers: {'Content-Type': 'application/json'}
            })
            .then(response => {
                console.log(response.data.token);
                if(response.data.token.length===36){
                this.getStock(); 
                swal({ title: "Successfully Deleted", text: "Stock has been deleted successfully.", con: "success" });
            }})
            .catch(error => {
                console.error("Error deleting stock:", error);
                swal({ title: "Error", text: "There was an error deleting the stock.", icon: "error" });
            });
        }
    }
}
</script>
<style>

</style>