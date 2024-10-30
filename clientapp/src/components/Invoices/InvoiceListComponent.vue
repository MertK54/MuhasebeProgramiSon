<template>
    <table class="table table-dark">
        <thead>
            <tr>
                <th class="p-3">Product Name</th>
                <th>Invoice Id</th>
                <th>Supplier</th>
                <th>Customer</th>
                <th>Type</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Total Amount</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(invoice, index) in invoices" :key="index">
                <td class="p-3">{{ invoice.product_name }}</td>
                <td>{{ invoice.invoice_id }}</td>
                <td>{{ invoice.supplier_name }}</td>
                <td>{{ invoice.customer_name }}</td>
                <td>{{ invoice.invoice_type }}</td>
                <td>{{ invoice.quantity }}</td>
                <td>{{ invoice.unit_price }}</td>
                <td>{{ invoice.total_amount}}</td>
            </tr>
        </tbody>
    </table>
</template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
name: "InvoiceListComponent",
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
        }
    }
}
</script>