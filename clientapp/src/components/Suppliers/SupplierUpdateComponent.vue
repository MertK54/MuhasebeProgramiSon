<template>
    <table class="table table-dark">
        <thead>
            <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Phone</th>
                <th>Address</th>
                <th>Update</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="supplier in suppliers" :key="supplier.supplier_id"> 
                <td>{{ supplier.name }}</td>
                <td>{{ supplier.e_mail }}</td>
                <td>{{ supplier.phone_number }}</td>
                <td>{{ supplier.adress }}</td>
                <td><button class="btn btn-primary" @click="openUpdateModal(supplier)">Update</button></td>
            </tr>
        </tbody>
    </table>
    <div class="modal fade text-dark" id="updateModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Supplier</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" style="color:black">
                    <form @submit.prevent="updateSupplier">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input type="text" v-model="selectedSupplier.name" class="form-control" id="name">
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" v-model="selectedSupplier.e_mail" class="form-control" id="email">
                        </div>
                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone</label>
                            <input type="text" v-model="selectedSupplier.phone_number" class="form-control" id="phone">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Address</label>
                            <input type="text" v-model="selectedSupplier.adress" class="form-control" id="address">
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
    name: "SupplierUpdateComponent",
    data() {
        return {
            suppliers: [],
            selectedSupplier: {}
        }
    },
    mounted() {
        this.getSupplier();
    },
    methods: {
        getSupplier() {
                axios.get('http://localhost:5280/api/supplier/supplier-get')
                .then(response => {
                    console.log(response.data);
                    if (response != null && response.data != null) {
                        this.suppliers = response.data;
                    }
                })
                .catch(error => {
                    console.error("Error while fetching suppliers:", error);
                    swal({
                        title: "Supplier not listed",
                        text: "Error when listing suppliers",
                        icon: "warning",
                        dangerMode: true
                    });
                });
            },
        openUpdateModal(supplier) {
            this.selectedSupplier = { ...supplier };
            const modal = new Modal(document.getElementById('updateModal'));
            modal.show();
        },
        updateSupplier() {
            const matched = this.suppliers.some(sup => sup.name === this.selectedSupplier.name || sup.e_mail === this.selectedSupplier.e_mail || sup.supplier_id !== this.selectedSupplier.supplier_id)
            console.log("matched suppliers:",matched)
            if(!matched){
                const params = {
                    supplier_id: this.selectedSupplier.supplier_id,
                    name: this.selectedSupplier.name,
                    e_mail: this.selectedSupplier.e_mail,
                    phone_number: this.selectedSupplier.phone_number,
                    adress: this.selectedSupplier.adress
                }
                console.log("params value", params);
                axios.post("http://localhost:5280/api/supplier/supplier-update",params)
                    .then(response => {
                        if(response.data.token.length===36){
                             console.log("Supplier updated", response.data);
                            swal({title: "Supplier updated",text: "Successfly updated supplier",icon: "success"});
                            this.getSupplier();
                            const modal = Modal.getInstance(document.getElementById('updateModal'));
                            modal.hide();
                        }
                    })
                    .catch(error => {
                        console.log("Update fail", error);
                        swal({title: "Supplier not updated",text: "Error when updated supplier",icon: "warning",dangerMode: true});
                    });
            }
            else{
                swal({title: "There is customer!",text: "There is a supplier with this email and name",icon: "warning",dangerMode: true});
            }
        }
    }
}
</script>

<style>

</style>
