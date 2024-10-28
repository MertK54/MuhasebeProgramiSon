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
            <tr v-for="(supplier, index) in suppliers" :key="index">
                <td>{{ supplier.name }}</td>
                <td>{{ supplier.e_mail }}</td>
                <td>{{ supplier.phone_number }}</td>
                <td>{{ supplier.adress }}</td>
                <td><button class="btn btn-danger" @click="removeSupplier(supplier.supplier_id)">Remove</button></td>
            </tr>
        </tbody>
    </table>
</template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
    name: "SupplierDeleteComponent",
    data(){
        return{
            suppliers:[]
        }
    },
    mounted(){
        this.getSupplier();
    },
    methods:{
        getSupplier(){
            axios.get('http://localhost:5280/api/supplier/supplier-get')
            .then(response => {
                this.suppliers = response.data;

            })
            .catch(error => {
                console.log("Supplier not get",error)
            })
        },
        removeSupplier(supplier_id){
            axios.delete('http://localhost:5280/api/supplier/supplier-delete', {
                data: { supplier_id }, 
                headers: {
                    'Content-Type': 'application/json'
                }
            }).then(response => {
                console.log(response.data);
                this.getSupplier();
                swal({ title: "Successfully Deleted", text: "Supplier has been deleted successfully.", con: "success" });
            })
        }
    }
}
</script>
<style>
</style>