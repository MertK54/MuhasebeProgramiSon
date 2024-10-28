<template>
    <table class="table">
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
            <tr v-for="(customer, index) in customers" :key="index">
                <td>{{ customer.name }}</td>
                <td>{{ customer.e_mail }}</td>
                <td>{{ customer.phone_number }}</td>
                <td>{{ customer.adress }}</td>
                <td><button class="btn btn-primary" @click="openUpdateModal(customer)">Update</button></td>
            </tr>
        </tbody>
    </table>
    <div class="modal fade" id="updateModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Update Customer</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <form @submit.prevent="updateCustomer">
                        <div class="mb-3">
                            <label for="name" class="form-label">Name</label>
                            <input type="text" v-model="selectedCustomer.name" class="form-control" id="name">
                        </div>
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <input type="email" v-model="selectedCustomer.e_mail" class="form-control" id="email">
                        </div>
                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone</label>
                            <input type="text" v-model="selectedCustomer.phone_number" class="form-control" id="phone">
                        </div>
                        <div class="mb-3">
                            <label for="address" class="form-label">Address</label>
                            <input type="text" v-model="selectedCustomer.adress" class="form-control" id="address">
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
    name: "CustomerUpdateComponent",
    data() {
        return {
            customers: [],
            selectedCustomer: {}
        }
    },
    mounted() {
        this.getCustomer();
    },
    methods: {
        getCustomer() {
            axios.get('http://localhost:5280/api/customer/customer-get')
                .then(response => {
                    this.customers = response.data;
                })
                .catch(error => {
                    console.log("Customers Not Get", error);
                });
        },
        openUpdateModal(customer) {
            this.selectedCustomer = { ...customer };
            const modal = new Modal(document.getElementById('updateModal'));
            modal.show();
        },
        updateCustomer() {
            axios.post("http://localhost:5280/api/customer/customer-update", this.selectedCustomer)
                .then(response => {
                    if(response.data.token.length===36){
                        swal({title: "Customer succesfully updated",icon: "success"});
                        this.getCustomer();
                        const modal = Modal.getInstance(document.getElementById('updateModal'));
                        modal.hide();
                    }
                })
                .catch(error => {
                    console.log("Update fail", error);
                    swal({title: "Customer not updated",text: "Error when updated customer",icon: "warning",dangerMode: true});

                });
        }
    }
}
</script>

<style>

</style>
