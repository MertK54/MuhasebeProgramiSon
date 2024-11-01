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
                <th>Type</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Total Amount</th>
                <th>Update</th>
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
                <td>{{ invoice.total_amount }}</td>
                <td><button class="btn btn-primary" @click="openUpdateModal(invoice)">Update</button></td>
            </tr>
        </tbody>
    </table>
    <div class="modal fade" id="updateModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-black">Update Invoice</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="updateInvoice" style="color:black">
                        <div class="mb-3">
                            <label for="product_name" class="form-label">Product Name</label>
                            <input type="text" v-model="selectedInvoice.product_name" class="form-control" id="product_name" disabled>
                        </div>
                        <div class="mb-3">
                            <label for="supplier_name" class="form-label">Supplier Name</label>
                            <input type="text" v-model="selectedInvoice.supplier_name" class="form-control" id="product_name" disabled>
                        </div>
                        <div class="mb-3">
                            <label for="customer_name" class="form-label">Customer Name</label>
                            <input type="text" v-model="selectedInvoice.customer_name" class="form-control" id="product_name" disabled>
                        </div>
                        <div class="mb-3">
                            <label for="payment_method" class="form-label">Payment Method</label>
                            <v-select :options="payment_methods" v-model="selectedInvoice.payment_method"></v-select>
                        </div>
                        <div class="mb-3">
                            <label for="invoice_status" class="form-label">Invoice Status</label>
                            <v-select :options="invoice_status" v-model="selectedInvoice.invoice_statu"></v-select>
                        </div>
                        <div class="mb-3">
                            <label for="quantity" class="form-label">Quantity</label>
                            <input type="number" v-model="selectedInvoice.quantity" class="form-control" id="quantity" disabled>
                        </div>
                        <div class="mb-3">
                            <label for="invoice_tpye" class="form-label">Invoice Type</label>
                            <input type="text" v-model="selectedInvoice.invoice_type" class="form-control" id="invoice_type" disabled>
                        </div>
                        <div class="mb-3">
                            <label for="unit_price" class="form-label">Unit Price</label>
                            <input type="number" v-model="selectedInvoice.unit_price" class="form-control" id="unit_price" disabled>
                        </div>
                        <button type="submit" class="btn btn-primary">Save changes</button>
                    </form>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import { Modal } from 'bootstrap';
import swal from 'sweetalert';
export default {
    name: "InvoiceUpdateComponent",
    data() {
        return {
            selectedInvoice: {},
            invoices:[],
            payment_methods:['credit-card','cash','payment','remittance'],
            invoice_status:['pending','approved']
        }
    },
    mounted() {
        this.getListInvoice();
    },
    methods: {
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
        openUpdateModal(invoice) {
            this.selectedInvoice = { ...invoice };
            const modal = new Modal(document.getElementById('updateModal'));
            modal.show();
        },
        updateInvoice(){
            const params = {
                invoice_id: this.selectedInvoice.invoice_id,
                payment_method:this.selectedInvoice.payment_method,
                invoice_status:this.selectedInvoice.invoice_statu,
            }
            axios.put('http://localhost:5280/api/invoice/invoice-update',params)
            .then(response => {
                swal({title:"Successfuly updated",icon:"success"});
                console.log("Response data: ",response.data);
            })
            .catch(error => {
                swal({title:"Error in updated",icon:"warning"});
                console.log("Error: ",error)
            })
        }
    }
}
</script>

<style>

</style>
