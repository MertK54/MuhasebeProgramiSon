<template>
    <table class="table table-dark">
       <thead>
         <tr>
           <th class="p-3">Product Name</th>
           <th>Supplier</th>
           <th>Supplier Id</th>
           <th>Quantity</th>
           <th>Unit Price</th>
         </tr>
       </thead>
       <tbody>
         <tr v-for="(stock, index) in stocks" :key="index">
           <td class="p-3">{{ stock.product_name }}</td>
           <td>{{ stock.supplier_name}}</td>
           <td>{{ stock.supplier_id}}</td>
           <td>{{ stock.quantity }}</td>
           <td>{{ stock.unit_price }}</td>
         </tr>
       </tbody>
     </table>
 </template>
 <script>
 import axios from 'axios';
 import swal from 'sweetalert';
 export default {
     name: "InvoiceSaleReturnComponent",
     data(){
         return{
             invoices: []
         }
     },
     mounted() {
     this.getInvoice();
     },
     methods:{
        getInvoice(){
            const params = {
              type:'sale'
            };
             axios.post('http://localhost:5280/api/invoice/invoice-get-sale',params, {headers: { 'Content-Type': 'application/json' }})
             .then(response => {
               if(response != null && response.data != null)
                 this.invoices = response.data;
             })
             .catch(error => {
               swal({title:"Sale return invoice not listed",text: "error when listing stock",icon:"warning",dangerMode:true})
             console.error("stocks not get", error);
             });
         }
     }
 }
 </script>
 <style>
 
 </style>