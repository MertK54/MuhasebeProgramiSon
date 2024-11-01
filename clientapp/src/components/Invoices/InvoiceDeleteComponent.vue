<template>
    <table class="table table-dark">
        <thead>
            <tr>
                <th class="p-3">Invoice Id</th>
                <th>Product Name</th>
                <th>Supplier</th>
                <th>Customer</th>
                <th>Payment</th>
                <th>Status</th>
                <th>Type</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Total Amount</th>
                <th>Delete</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(invoice, index) in invoices" :key="index">
                <td class="p-3">{{ invoice.invoice_id }}</td>
                <td>{{ invoice.product_name }}</td>
                <td>{{ invoice.supplier_name }}</td>
                <td>{{ invoice.customer_name }}</td>
                <td>{{ invoice.payment_method }}</td>
                <td>{{ invoice.invoice_statu }}</td>
                <td>{{ invoice.invoice_type }}</td>
                <td>{{ invoice.quantity }}</td>
                <td>{{ invoice.unit_price }}</td>
                <td>{{ invoice.total_amount}}</td>
                <td><button type="button" class="btn btn-danger" @click="deleteInvoice(invoice)">Delete</button></td>
            </tr>
        </tbody>
    </table>
</template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
name: "InvoiceDeleteComponent",
    data(){
        return{
            invoices:[]
        }
    },
    mounted(){
        this.getListInvoice();
    },
    methods:{
        getListInvoice()
        {
            axios.get('http://localhost:5280/api/invoice/invoice-list-all')
            .then(response => {
                this.invoices = response.data;
            })
            .catch(error => {
                console.log("invoice listed error",error)
                swal({title:"invoice listed error"});
            })
        },
        deleteInvoice(invoice){
            this.updateStock(invoice.stock_id,invoice.quantity,invoice.unit_price);
            console.log(invoice.stock_id,invoice.quantity,invoice.unit_price);
            const params = {
                invoice_id:invoice.invoice_id
            };
            console.log("Params being sent:",params);
            axios.delete('http://localhost:5280/api/invoice/invoice-delete',{headers: { 'Content-Type': 'application/json' },data:params})
            .then(response => {
                if(response && response.data && response.data.token.length === 36){
                    console.log("Succesfuly Deleted", response)
                    swal({title:'Successfuly Deleted'});
                    this.getListInvoice();
                }
                else{
                    console.log("error in response", response)
                    swal({title:'Error Deleted'})
                }
            })
            .catch(error => {
                console.log("Error trying to delete:",error)
                swal({title:"Error when trying to delete ",icon:"error"})
            })
        },
        updateStock(id, quantitySold, unitPrice) {
            const params = {
                stock_id: id,
                quantity: quantitySold,
                unit_price: unitPrice
            }
            console.log("params değerleri:"+params);
            axios.post('http://localhost:5280/api/stock/stock-update-add',params, {headers: { 'Content-Type': 'application/json' }})
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