<template>
   <table class="table table-dark">
      <thead>
        <tr>
          <th class="p-3">Name</th>
          <th>Email</th>
          <th>Phone</th>
          <th>Adress</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(customer, index) in customers" :key="index">
          <td class="p-3">{{ customer.name }}</td>
          <td>{{ customer.e_mail }}</td>
          <td>{{ customer.phone_number }}</td>
          <td>{{ customer.adress }}</td>
        </tr>
      </tbody>
    </table>
</template>
<script>
import axios from 'axios';
import swal from 'sweetalert';
export default {
    name: "CustomerListComponent",
    data(){
        return{
            customers: []
        }
    },
    mounted() {
    this.getCustomer();
    },
    methods:{
        getCustomer(){
            axios.get('http://localhost:5280/api/customer/customer-get')
            .then(response => {
              if(response != null && response.data != null)
                this.customers = response.data;
            })
            .catch(error => {
              swal({title:"Supplier not listed",text: "error when listing supplier",icon:"warning",dangerMode:true})
            console.error("customers not get", error);
            });
        }
    }
}
</script>
<style>

</style>