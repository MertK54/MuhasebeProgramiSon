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
            <tr v-for="(customer, index) in customers" :key="index">
                <td>{{ customer.name }}</td>
                <td>{{ customer.e_mail }}</td>
                <td>{{ customer.phone_number }}</td>
                <td>{{ customer.adress }}</td>
                <td><button class="btn btn-danger" @click="removeCustomer(customer.customer_id)">Remove</button></td>
            </tr>
        </tbody>
    </table>
</template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
    name: "CustomerDeleteComponent",
    data(){
        return{
            customers:[]
        }
    },
    mounted(){
        this.getCustomer();
    },
    methods:{
        getCustomer(){
            axios.get('http://localhost:5280/api/customer/customer-get')
            .then(response => {
                this.customers = response.data;
            })
            .catch(error => {
                console.log("Customer not get",error)
            })
        },
        removeCustomer(customerId){
            axios.delete('http://localhost:5280/api/customer/customer-delete', {
                data: { customer_id: customerId }, 
                headers: {
                    'Content-Type': 'application/json'
                }
            })
            .then(response => {
                console.log(response.data.token);
                if(response.data.token.length===36){
                this.getCustomer(); 
                swal({ title: "Successfully Deleted", text: "Customer has been deleted successfully.", con: "success" });
            }})
            .catch(error => {
                console.error("Error deleting customer:", error);
                swal({ title: "Error", text: "There was an error deleting the customer.", icon: "error" });
            });
        }
    }
}
</script>
<style>

</style>