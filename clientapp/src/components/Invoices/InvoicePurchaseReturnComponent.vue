<template>
    <table class="table table-dark">
        <thead>
            <tr>
                <th class="p-3">Invoice Id</th>
                <th>Product Name</th>
                <th>Supplier</th>
                <th>Customer</th>
                <th>Payment</th>
                <th>Statu</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Total Amount</th>
                <th>Return</th>
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
                <td>{{ invoice.quantity }}</td>
                <td>{{ invoice.unit_price }}</td>
                <td>{{ invoice.total_amount }}</td>
                <td><button class="btn btn-primary" @click="returnPurchaseInvoice(invoice)">Return</button></td>
            </tr>
        </tbody>
    </table>
</template>  
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
    data(){
        return{
        invoices: [],
        formData:{
            product_name:'',
            supplier_id:0,
            customer_id:0,
            stock_id:0,
            quantity:0,
            unit_price:0.00,
            total_amount:0.00,
            invoice_statu:null,
            payment_method:null
        },   
    }
    },
    mounted(){
        this.getInvoice();
    },
    methods:{
        getInvoice(){
            const params = {
                type: 'purchase'
            };
            axios.post('http://localhost:5280/api/invoice/invoice-get-sale',params,{ headers: { 'Content-Type': 'application/json' } })
            .then(response => {
                if (response != null && response.data != null)
                    this.invoices = response.data; 
                })
            .catch(error => {
                swal({ title: "Sale return invoice not listed", text: "Error when listing stock", icon: "warning", dangerMode: true });
                console.error("stocks not get", error);
                });
        },
        async returnPurchaseInvoice(invoice){
            await this.formDataAddValue(invoice);
            console.log("Form Data:",this.formData);
            const params= {
                supplier_id: this.formData.supplier_id,
                customer_id: this.formData.customer_id,
                stock_id: this.formData.stock_id,
                product_name: this.formData.product_name,
                quantity: parseInt(this.formData.quantity,10),
                unit_price: parseFloat(this.formData.unit_price),
                total_amount: parseFloat(this.formData.total_amount),
                invoice_type: 'purchase_return',
                invoice_statu:this.formData.invoice_statu,
                payment_method:this.formData.payment_method
            };
            console.log("Params being sent:", params);
            axios.post('http://localhost:5280/api/invoice/invoice-create-purchase',params,{headers:{'Content-Type':'application/json'}})
            .then(response => {
                swal({title:"Invoice created",icon:"success"})
                console.log("Invoice Created",response.data);
                this.updateStock(params.stock_id, params.quantity,params.unit_price);
            })
            .catch(error => {
                console.log("Error creating invoice",error.response ? error.response.data : error)
            })
        },
        formDataAddValue(invoice)
        {
            this.formData.product_name = invoice.product_name,
            this.formData.supplier_name = invoice.supplier_name,
            this.formData.supplier_id = invoice.supplier_id,
            this.formData.quantity = invoice.quantity,
            this.formData.unit_price = invoice.unit_price,
            this.formData.total_amount = invoice.quantity * invoice.unit_price;
            this.formData.stock_id = invoice.stock_id;
            this.formData.customer_id = invoice.customer_id;
            this.formData.invoice_statu = invoice.invoice_statu;
            this.formData.payment_method = invoice.payment_method;
        },
        updateStock(id,quantitySold,unitPrice){
            const params = {
                stock_id: id,
                quantity: quantitySold,
                unit_price: unitPrice
            }
            console.log("params value:"+params);
            axios.post('http://localhost:5280/api/stock/stock-update-add',params, {headers: { 'Content-Type': 'application/json' }})
            .then(response => {
                console.log("Stock updated",response.data)
            })
            .catch(error => {
                console.log("Error updating stock",error.response ? error.response : error)
                swal({ title: "Stock update failed", text: "Could not update stock", icon: "error" });
            })
        }
    }
}
</script>    
<style>
</style>