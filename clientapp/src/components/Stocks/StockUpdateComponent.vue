<template>
    <table class="table table-dark">
        <thead>
            <tr>
                <th class="p-3">Product Name</th>
                <th>Supplier</th>
                <th>Supplier Id</th>
                <th>Quantity</th>
                <th>Unit Price</th>
                <th>Update</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(stock, index) in stocks" :key="index">
                <td class="p-3">{{ stock.product_name }}</td>
                <td>{{ stock.supplier_name}}</td>
                <td>{{ stock.supplier_id}}</td>
                <td>{{ stock.quantity }}</td>
                <td>{{ stock.unit_price }}</td>
                <td><button class="btn btn-primary" @click="openUpdateModal(stock)">Update</button></td>
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
                    <form @submit.prevent="updateStock" style="color:black">
                        <!-- <div class="mb-3">
                            <label for="email" class="form-label">Supplier Name</label>
                            <input type="email" v-model="selectedStock.supplier_name" class="form-control" id="supplier_name">
                        </div>
                        <div class="mb-3">
                            <label for="phone" class="form-label">Supplier Id</label>
                            <input type="text" v-model="selectedStock.supplier_id" class="form-control" id="supplier_id">
                        </div> -->
                        <div class="mb-3">
                            <label for="name" class="form-label">Product Name</label>
                            <input type="text" v-model="selectedStock.product_name" class="form-control" id="product_name">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Quantity</label>
                            <input type="text" v-model="selectedStock.quantity" class="form-control" id="quantity">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Unit Price</label>
                            <input type="text" v-model="selectedStock.unit_price" class="form-control" id="unit_price">
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
    name: "StockUpdateComponent",
    data() {
        return {
            stocks: [],
            selectedStock: {}
        }
    },
    mounted() {
        this.getStock();
    },
    methods: {
        getStock() {
            axios.get('http://localhost:5280/api/stock/stock-get')
                .then(response => {
                    console.log(response.data);
                    this.stocks = response.data;
                })
                .catch(error => {
                    console.log("Stock Not Get", error);
                });
        },
        openUpdateModal(stock) {
            this.selectedStock = { ...stock };
            const modal = new Modal(document.getElementById('updateModal'));
            modal.show();
        },
        updateStock() {
            axios.post("http://localhost:5280/api/stock/stock-update", this.selectedStock)
                .then(response => {
                    if(response.data.token.length === 36){
                        swal({title: "Stok succesfully updated",icon: "success"});
                        this.getStock();
                        const modal = Modal.getInstance(document.getElementById('updateModal'));
                        modal.hide();
                    }
                })
                .catch(error => {
                    console.log("Update fail", error);
                    swal({title: "Stock not updated",text: "Error when updated stock",icon: "warning",dangerMode: true});
                });
        }
    }
}
</script>

<style>

</style>
