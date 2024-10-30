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
                <th>Update</th>
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
                <td><button class="btn btn-primary" @click="openUpdateModal(invoice)">Update</button></td>
            </tr>
        </tbody>
    </table>
    <div class="modal fade" id="updateModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Stock</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="" style="color:black">
                        <div class="mb-3">
                            <label for="name" class="form-label">Product Name</label>
                            <input type="text" v-model="selectedInvoice.product_name" class="form-control" id="product_name">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Quantity</label>
                            <input type="text" v-model="selectedInvoice.quantity" class="form-control" id="quantity">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Invoice Type</label>
                            <input type="text" v-model="selectedInvoice.invoice_type" class="form-control" id="invoice_type">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Unit Price</label>
                            <input type="text" v-model="selectedInvoice.unit_price" class="form-control" id="unit_price">
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
            invoices:[]
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
        }
    }
}
</script>

<style>

</style>
